using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OracleVeryBasic.Base;
using OracleVeryBasic.Classes;
using OracleVeryBasicTestProject.Base;

namespace OracleVeryBasicTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {

        [TestMethod]
        [TestTraits(Trait.OracleDataProvider)]
        public void ReadCategoriesWithSelectOptionTest()
        {
            // arrange
            var list = OracleOperations.Categories();

            // act
            var category = list.FirstOrDefault();

            // assert
            Assert.IsTrue(category.Name == "Select");
            Assert.IsTrue(category.Id == -1);

        }
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod1()
        {
            // arrange


            // act


            // assert
        }

    }
}
