using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using OracleEntityFrameworkUnitTest.Base;
using OracleNorthWindLibrary.Data;


// ReSharper disable once CheckNamespace - do not change
namespace OracleEntityFrameworkUnitTest
{
    public partial class MainTest
    {

        public MainTest()
        {
        }

        /// <summary>
        /// Perform initialization before each test runs
        /// </summary>
        /// <returns>Nothing we care about</returns>
        /// <remarks>
        /// For synchronous preparation
        /// * Remove the async modifier
        /// * Remove the line with await Task.Delay(0);
        /// </remarks>
        [TestInitialize]
        public void Init()
        {
            if (TestContext.TestName == nameof(CountTest))
            {
                // TODO
            }
        }
        [TestCleanup]
        public void TestCleanup()
        {

        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        private static async Task<(decimal? RowCount, Exception Exception)> CustomersCount()
        {
            try
            {
                await using var cn = new OracleConnection() { ConnectionString = DevelopmentConnectionString(1) };

                await using var cmd = new OracleCommand("SELECT COUNT(customer_id) FROM Customers", cn);
                await cn.OpenAsync();
                var rowCount = await cmd.ExecuteScalarAsync();

                cn.Close();

                return ((decimal)rowCount, null);
            }
            catch (Exception exception)
            {
                return (null, exception);
            }
        }



    }

}
