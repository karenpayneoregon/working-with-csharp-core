using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;
using NorthWindSqlServerUnitTest.Base;
using Oed.EntityFrameworkCoreHelpers.Classes;
using Oed.EntityFrameworkCoreHelpers.LanguageExtensions;

namespace NorthWindSqlServerUnitTest
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task CanConnectTest()
        {
            // arrange

            await using var context = new NorthwindContext();

            // act - deconstruct 
            var ( _ , exception) = await context.CanConnectAsync();
            
            // assert
            Assert.IsNull(exception);

        }


        /// <summary>
        /// Demonstrates using ToQueryString to get a string representation of the query used
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void ToQueryString()
        {
            // arrange
            var expected = @"SELECT [c].[CategoryID], [c].[CategoryName], [c].[Description], [c].[Picture]
FROM [Categories] AS [c]";

            using var context = new NorthwindContext();

            // act
            var categoryQueryString = context.Categories.ToQueryString();
            
            Debug.WriteLine(categoryQueryString);

            // assert
            Assert.AreEqual(expected,categoryQueryString);

            Debug.WriteLine("");

            int categoryIdentifier = 2;

            var productsQueryString = context
                .Products
                .Include(product => product.OrderDetails)
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Where(c => c.CategoryId == categoryIdentifier).ToQueryString();

            Debug.WriteLine(productsQueryString);

            Assert.IsTrue(productsQueryString.Contains("DECLARE @__categoryIdentifier_0 int = 2;"));
        }


        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task ToQueryStringProjection()
        {
            await using var context = new NorthwindContext();

            var result = context.Customers.Select(CustomerItem.Projection).ToQueryString();
            Console.WriteLine(result);

        }

        [TestMethod]
        [TestTraits(Trait.Extensions)]
        public void ConvertToTest()
        {
            // arrange
            int[] values = { 9, 42, 60 };

            // act
            object[] objectArray = values.ToObjectArray(); 

            // assert
            CollectionAssert.AllItemsAreInstancesOfType(objectArray, typeof(int));

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindCustomerByPrimaryKey()
        {
            // arrange
            int key = 60;
            string expected = "Wolski  Zajazd";
            
            // act
            await using var context = new NorthwindContext();
            var customer = await context.Customers.FindAsync(key);

            // assert
            Assert.AreEqual(expected, customer.CompanyName);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindCustomerByPrimaryKeyNotFound()
        {
            // arrange
            int key = 99;

            // act
            await using var context = new NorthwindContext();
            var customer = await context.Customers.FindAsync(key);

            // assert
            Assert.IsNull(customer);

        }
        /// <summary>
        /// EF Core permits finding by key via DbSet&lt;TEntity&gt;.Find(object[]) but not by
        /// multiple keys which FindAllAsync does
        /// https://social.technet.microsoft.com/wiki/contents/articles/53841.entity-framework-core-find-all-by-primary-key-c.aspx
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindAllCustomersByPrimaryKeyTest()
        {
            // arrange
            string[] expected = { "Bon app'", "QUICK Stop", "Wolski  Zajazd" };
            var primaryKeys = new[] { 9, 42, 60 };

            // act
            await using var context = new NorthwindContext();
            Customers[] results = await context.FindAllAsync<Customers>(primaryKeys.ToObjectArray());
            string[] names = results.Select(customer => customer.CompanyName).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, names);
        }


        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void EditInspectOriginalAndCurrentValue()
        {
            using var context = new NorthwindContext();
            int categoryIdentifier = 2;
            List<Products> products = context
                .Products
                .Include(product => product.OrderDetails)
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Where(product => product.CategoryId == categoryIdentifier)
                .Take(3)
                .ToList();

            Products firstProduct = products.FirstOrDefault();
            Debug.WriteLine($"First product name: {firstProduct.ProductName,-15} Current state: {context.Entry(firstProduct).State}");


            firstProduct.ProductName = "ABC";
            Debug.WriteLine($"First product name: {firstProduct.ProductName,-15} Current state: {context.Entry(firstProduct).State}");
            //context.ChangeTracker.DetectChanges();

            Debug.WriteLine("");

            Debug.WriteLine($"Remove: {context.BusinessEntityPhone.Remove(context.BusinessEntityPhone.Find(1))}");

            string[] tokens = { "ProductName", "ProductId" };
            
            File.WriteAllText(_InspectFileName1, context.ChangeTracker.DebugView.CustomViewByChunks(tokens, 10));
            File.WriteAllText(_InspectFileName2, context.ChangeTracker.DebugView.LongView);

            //Console.WriteLine(context.ChangeTracker.DebugView.CustomView(new[] { "Products" }, 2));



            var originalProductName = context.Entry(firstProduct).Property(x => x.ProductName)
                .OriginalValue;

            var currentProductName = context.Entry(firstProduct).Property(x => x.ProductName)
                .CurrentValue;

            Debug.WriteLine($"original name: '{originalProductName}' current: '{currentProductName}'");
        }
    }
}
