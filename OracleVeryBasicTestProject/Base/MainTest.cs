using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ReSharper disable once CheckNamespace - do not change
namespace OracleVeryBasicTestProject
{
    public partial class MainTest
    {

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
        public async Task Init()
        {
            if (TestContext.TestName == nameof(ReadCategoriesWithSelectOptionTest))
            {
                await Task.Delay(0);
            }
        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
        /// <summary>
        /// Here is where any clean operations are performed for this class
        /// </summary>
        /// <returns></returns>
        [ClassCleanup()]
        public static async Task ClassCleanup()
        {
            await Task.Delay(0);
        }


        /// <summary>
        /// Create a well formed connection string
        /// </summary>
        /// <param name="index">database index</param>
        /// <returns>connection string</returns>
        public static string DevelopmentConnectionString(int index) =>
            "Data Source=aix-aixdev.emp.state.or.us:1521/northwind_demo;" +
            "Persist Security Info=True;Enlist=false;Pooling=true;" +
            $"Statement Cache Size=10;User ID=northwind{index:D2};Password=\"!northwind!DEMO!\"";
    }

}
