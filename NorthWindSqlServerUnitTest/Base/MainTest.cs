using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWindSqlServerUnitTest.Base;


// ReSharper disable once CheckNamespace - do not change
namespace NorthWindSqlServerUnitTest
{
    public partial class MainTest
    {

        private static string _InspectFileName1 = "EditInspectOriginalAndCurrentValue1.txt";
        private static string _InspectFileName2 = "EditInspectOriginalAndCurrentValue2.txt";

        private List<CustomerCountry> _customerCountries() =>
            new()
            {
                new() { Id = 15, Name = "Drachenblut Delikatessen", Country = "Germany" },
                new () { Id = 22, Name = "Frankenversand", Country = "Germany" }
            };

        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(EditInspectOriginalAndCurrentValue))
            {
                
                if (File.Exists(_InspectFileName1))
                {
                    File.Delete(_InspectFileName1);
                }

                if (File.Exists(_InspectFileName2))
                {
                    File.Delete(_InspectFileName2);
                }
            }
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == nameof(CanConnectTest))
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
