using System;
using System.Linq;
using System.Threading.Tasks;
using DirectoryHelpersLibrary.Classes;
using GlobbingUnitTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobbingUnitTest
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Get all .cs files under this solution excluding
        /// Assembly.cs and Designer.cs
        /// </summary>
        /// <remarks>
        /// This is a fragile test in that if new files or current files renamed
        /// this test will fail.
        ///
        /// See patterns in readme.md in GlobbingProject
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.Globbing)]
        public async Task ExcludeAssemblyDesignerTest()
        {
            // arrange
            string path = DirectoryHelper.SolutionFolder();

            string[] include = { "**/*.cs" };
            string[] exclude =
            {
                "**/*Assembly*.cs", 
                "**/*Designer*.cs"
            };

            // act
            await GlobbingOperations.GetFiles(path, include, exclude);

            // assert
    
            /*
             * Determine if real-time list contains files in static list
             */
            bool hasAll = AssemblyDesignerStaticFileList()
                .All(fileName => AssemblyDesignerResultList.Contains(fileName));

            Assert.IsTrue(hasAll, 
                "Globbing Assembly Designer with Contains.All failed.");

        }

    }
}
