using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWind2022CoreLibrary.Data;


// ReSharper disable once CheckNamespace - do not change
namespace NorthWind2022CoreLibraryUnitTest
{
    public partial class MainTest
    {
        private static string _InspectFileName1 = "EditInspectOriginalAndCurrentValue1.txt";
        private static string _InspectFileName2 = "EditInspectOriginalAndCurrentValue2.txt";

        [TestInitialize]
        public async Task Initialization()
        {
            // warm-up entity framework, shaves a little time off each unit test method
            await using var context = new NorthwindContext();
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == nameof(TestMethod1))
            {
                // TODO
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
    }
}
