using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryHelpersLibrary.Classes;


// ReSharper disable once CheckNamespace - do not change
namespace GlobbingCoreUnitTestProject
{
    public partial class MainTest
    {
        private readonly string[] folders =
        {
            "C:\\Users\\paynek\\Documents\\Snagit",
            "C:\\OED\\Dotnetland\\VS2019\\KP_Extensions",
            @"\\devweb07\HTTP\\headfoot\css"
        };

        private string[] WebFiles =
        {
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\agency.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap35\\bootstrap-dialog.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap35\\bootstrap-dialog.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap35\\bootstrap.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-grid.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-grid.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-karen.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-oed-master.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-reboot.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap-reboot.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\bootstrap4.2.1\\bootstrap.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\kendo\\styles\\kendo.common.min.css",
            "\\\\devweb07\\HTTP\\\\headfoot\\css\\kendo\\styles\\kendoConsole.css"
        };

        private List<string> WebFilesList = new List<string>();



        private enum FolderType
        {
            Images = 0,
            Source = 1,
            Css = 2
        }


        /// <summary>
        /// Perform initialization before test runs using assertion on current test name.
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName is  nameof(InMemoryDirectoryInfoTest))
            {
                GlobbingOperations.TraverseHandler += GlobbingOperationsOnTraverseHandler;
                WebFilesList = new List<string>();
            }
        }

        private void GlobbingOperationsOnTraverseHandler(string sender)
        {
            WebFilesList.Add(sender);
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName is  nameof(InMemoryDirectoryInfoTest))
            {
                GlobbingOperations.TraverseHandler -= GlobbingOperationsOnTraverseHandler;
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
