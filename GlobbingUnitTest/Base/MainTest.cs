using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;


// ReSharper disable once CheckNamespace - do not change
namespace GlobbingUnitTest
{
    public partial class MainTest
    {
        /// <summary>
        /// Perform initialization before test runs using assertion on current test name.
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(ExcludeAssemblyDesignerTest))
            {
                AssemblyDesignerResultList = new List<string>();
                GlobbingOperations.TraverseFileMatch += GlobbingOperationsOnTraverseFileMatch;
                GlobbingOperations.Done += GlobbingOperationsOnDone;
            }
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == nameof(ExcludeAssemblyDesignerTest))
            {
                GlobbingOperations.TraverseFileMatch -= GlobbingOperationsOnTraverseFileMatch;
                GlobbingOperations.Done -= GlobbingOperationsOnDone;
            }
        }

        private void GlobbingOperationsOnDone(string message)
        {
            Console.WriteLine($"{message} from {nameof(GlobbingOperationsOnDone)}");
        }

        private void GlobbingOperationsOnTraverseFileMatch(FileMatchItem sender)
        {
            AssemblyDesignerResultList.Add(Path.Combine(sender.Folder, sender.FileName).Replace(SolutionFolder  + "\\", ""));
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
        /// Obtains root folder for the current Visual Studio solution
        /// </summary>
        private static string SolutionFolder 
            => DirectoryHelper.SolutionFolder();

        /// <summary>
        /// List of files found from <see cref="ExcludeAssemblyDesignerTest"/>
        /// </summary>
        private static List<string> AssemblyDesignerResultList;

        /// <summary>
        /// Static files from assertion in <see cref="ExcludeAssemblyDesignerTest"/>
        /// </summary>
        /// <returns></returns>
        private static List<string> AssemblyDesignerStaticFileList() => 
            File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileMatches.txt"))
                .ToList();
    }
}
