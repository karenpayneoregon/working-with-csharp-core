using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServerVeryBasic.Classes;
using SqlServerVeryBasic.Models;
using SqlServerVeryBasicUnitTestProject.Base;

namespace SqlServerVeryBasicUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void UserNames()
        {
            var array = SqlServerOperations.UserNameArray2();
            foreach (var name in array)
            {
                Console.WriteLine(name);
            }
        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        public void ReadCountriesWithSelectOptionTest()
        {
            // arrange
            var list = SqlServerOperations.Countries();

            // act
            var country= list.FirstOrDefault();
            
            // assert
            Assert.IsTrue(country.Name == "Select");
            Assert.IsTrue(country.Id == -1);

        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        public void UsingParameters()
        {
            var expected = "Alfreds Futterkiste";

            Assert.AreEqual(SqlServerOperations.CompanyNameWrong(1), expected);
            Assert.AreEqual(SqlServerOperations.CompanyNameRight(1), expected);
        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        public void SafelyReturningDataTest()
        {
            var (name, exception) = SqlServerOperations.CompanyNameSafe(1);
            Assert.IsNull(exception);
        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SafelyReturningData_NullParameterTest()
        {
            var neverUsed = SqlServerOperations.CompanyNameSafe(null);
        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        public async Task ProductsByCategoryIdentifierTest()
        {
            // arrange
            var identifier = 1;

            // act
            List<Product> list = await SqlServerOperations.ProductsByCategoryIdentifier(identifier);

            // assert

            Assert.AreEqual(list.Count,12);

            // let' look at the results

            StringBuilder builder = new ();
            list
                .OrderBy(product => product.ProductName)
                .ToList()
                .ForEach(product => builder.AppendLine(product.ProductName));

            Console.WriteLine(builder);

            Console.WriteLine();

            foreach (Product prod in list.OrderBy(product => product.ProductName))
            {
                Console.WriteLine(prod);
            }

        }

        [TestMethod]
        [TestTraits(Trait.SqlDataProvider)]
        public async Task GetTopFiveOrders()
        {
            Console.WriteLine(nameof(GetTopFiveOrders));

            var table = await SqlServerOperations.GetTopFiveOrders();
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row.Field<int>("OrderID"),-7}{row.Field<string>("CompanyName")}");
            }
        }
        

        [TestMethod]
        [TestTraits(Trait.Configurations)]
        public void GetSqlServerCurrentEnvironmentTest()
        {
            
            // arrange
            var expectedCurrentEnvironment = ConnectionsConfiguration.Development;


            // act
            var currentEnvironment = ConfigurationHelper.CurrentEnvironment;


            // assert
            Assert.AreEqual(expectedCurrentEnvironment, currentEnvironment);

        }

        [TestMethod]
        [TestTraits(Trait.Configurations)]
        public void GetCurrentConnectionString()
        {

            // arrange
            var expectedConnectionString = "Server=.\\SQLEXPRESS;Database=NorthWind2020;Integrated Security=true";

            // act
            var currentConnectionString = ConfigurationHelper.ConnectionString();

            // assert
            Assert.AreEqual(expectedConnectionString, currentConnectionString);
        }

    }
}
