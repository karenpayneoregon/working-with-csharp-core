using System.Diagnostics;
using DirectoryHelpersLibrary.Classes;
using GlobbingCoreUnitTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobbingCoreUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.Globbing)]
        public void InMemoryDirectoryInfoTest()
        {

            // arrange

            /*
             * any .cs file in parent and sub folder beginning with sq
             * any .cs file in parent and sub folder beginning with st
             * any mark down file
             */
            string[] include = { "**/boot*.css", "**/kendo*.css", "**/agency.css" };
            string[] fileExtensions = { "css" };

            // act
            GlobbingOperations.GenericGetFiles(folders[(int)FolderType.Css], include, fileExtensions);

            // assert
            CollectionAssert.AreEqual(WebFilesList, WebFiles);

        }


    }
}
