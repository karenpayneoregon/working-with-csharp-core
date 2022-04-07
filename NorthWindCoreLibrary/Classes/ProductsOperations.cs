using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;

namespace NorthWindCoreLibrary.Classes
{
    public class ProductsOperations
    {
        /// <summary>
        /// Example for retrieving products via nested projection
        ///
        /// Query <see cref="Products"/> with a join on <see cref="Suppliers"/>
        /// 
        /// </summary>
        /// <returns>Task&lt;List of <see cref="Product"/> which is a projection</returns>
        public static async Task<List<Product>> GetProductsWithProjection()
        {
            var productList = new List<Product>();

            await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();

                productList = await context.Products
                    .Include(product => product.Supplier)
                    .Select(Product.Projection)
                    .ToListAsync();

            });

            return productList;

        }

        /// <summary>
        /// Using a projection <see cref="Product"/> get <see cref="Products"/> by <see cref="Categories"/>
        ///
        /// For grouping on multiple properties generally developers will use an anonymous object array,
        /// here the grouping is done on a concrete class <see cref="CategoryProduct"/>  followed by selecting
        /// <see cref="Product"/> projection to a list
        /// </summary>
        /// <returns>List of <see cref="Product"/></returns>
        public static async Task<List<Product>> GetProductsWithProjectionGroupByCategory()
        {
            List<Product> productList = new();

            await Task.Run(async () =>
            {

                var products = await GetProductsWithProjection();

                productList = products
                    .GroupBy(product => new CategoryProduct
                    {
                        CategoryName = product.CategoryName, 
                        ProductName = product.ProductName
                    })
                    .Select(product => new Product()
                    {
                        ProductId = products.FirstOrDefault().ProductId,
                        CategoryName = product.FirstOrDefault().CategoryName,
                        CategoryId = product.FirstOrDefault().CategoryId,
                        ProductName = product.FirstOrDefault().ProductName,
                        SupplierName = products.FirstOrDefault().SupplierName
                    })
                    .ToList();

            });

            return productList;

        }

        /// <summary>
        /// Perform a group-by on two properties, CategoryName and ProductName
        /// reusing <see cref="GetProductsWithProjection"/> for a base read 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Product>> GetProductsWithProjectionGroupByCategoryOrderedTask()
        {
            List<Product> productList = new();

            await Task.Run(async () =>
            {

                var products = await GetProductsWithProjection();

                productList = products
                    .GroupBy(product => new CategoryProduct
                    {
                        CategoryName = product.CategoryName,
                        ProductName = product.ProductName
                    })
                    .Select(product => new Product()
                    {
                        ProductId = products.FirstOrDefault().ProductId,
                        CategoryName = product.FirstOrDefault().CategoryName,
                        CategoryId = product.FirstOrDefault().CategoryId,
                        ProductName = product.FirstOrDefault().ProductName,
                        SupplierName = products.FirstOrDefault().SupplierName
                    })
                    .OrderBy(product => product.CategoryId)
                    .ToList();

            });

            return productList;

        }

        #region For testing - see comments

        /// <summary>
        /// Used to validate a test method and really belongs in the test class.
        /// </summary>
        /// <param name="fileName">Existing json file containing <see cref="Products"/></param>
        /// <returns>A list of <see cref="Products"/> or an empty list of <see cref="Products"/></returns>
        public static List<Product> ReadProductsFromJsonFile(string fileName)
        {
            List<Product> products = new();

            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            return products;

        }

        #endregion


    }
}
