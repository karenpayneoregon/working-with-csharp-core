using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlWhereInUnitTestProject.Base;

namespace SqlWhereInUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod1()
        {
            // arrange


            // act


            // assert
        }

        /// <summary>
        /// Determine if company names are returned for WHERE IN condition
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void StringWhereConditions()
        {
            // arrange
            var expectedResults = new List<string>()
            {
                "Apple",
                "FaceBook",
                "Karen's Coffee"
            };

            // act
            var (list, _) = DataOperations.GetCustomersNamesBack(new List<string>()
            {
                "Apple",
                "FaceBook",
                "Karen's Coffee"
            });

            // assert
            Assert.IsTrue(expectedResults.SequenceEqual(list));

        }

        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void StringWhereInConditionsReturnKeys()
        {
            // arrange
            var expectedResults = new List<int>() { 2, 4, 5 };

            // act
            var (list, _) = DataOperations.GetCustomersKeysBack(new List<string>()
            {
                "Apple",
                "FaceBook",
                "Karen's Coffee"
            });

            // assert
            Assert.IsTrue(expectedResults.SequenceEqual(list));
        }

        /// <summary>
        /// Determine if company identifiers are returned for WHERE IN condition,
        /// validate by names of companies in expectedResults
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void IntWhereConditions()
        {
            // arrange
            var expectedResults = new List<string>()
            {
                "Apple",
                "FaceBook",
                "Karen's Coffee"
            };

            // act
            var (list, _) = DataOperations.GetByPrimaryKeys(new List<int>() { 2, 4, 5 });

            // assert
            Assert.IsTrue(expectedResults.SequenceEqual(list));
        }

        /// <summary>
        /// Example for NOT IN condition, reverse of above method
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void IntWhereNotInConditions()
        {
            // arrange
            var expectedResults = new List<string>()
            {
                "Microsoft",
                "IBM",
                "Macy's",
                "JetBrains",
                "Telerik",
                "GemBox Software",
                "Red Gate"
            };

            // act
            var (list, _) = DataOperations.GetByPrimaryKeys_NotIn(new List<int>() { 2, 4, 5 });

            // assert
            Assert.IsTrue(expectedResults.SequenceEqual(list));

        }

        /// <summary>
        /// Determine if specific dates are found in a table where in this case 
        /// these dates do exists.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void DateWhereInConditions()
        {
            // arrange
            var expectedResults = new List<int>() { 1, 3 };

            // act
            var (list, _) = DataOperations.GetStartDatesList(new List<string>()
            {
                "2018-01-01",
                "2017-01-03"
            });

            // assert
            Assert.IsTrue(expectedResults.SequenceEqual(list));
        }

        [TestMethod]
        [TestTraits(Trait.SqlWhereCondition)]
        public void UpdateExample()
        {

            // arrange
            var expected = "UPDATE table SET column = 0 WHERE id IN (1,3,20,2,45)";
            var identifiers = new List<int>() { 1, 3, 20, 2, 45 };


            /*
             * act
             *
             * The SET value is exposed but we could parametrize it too
             */
            var (actual, exposed) = DataOperations.UpdateExample(
                "UPDATE table SET column = 0 WHERE id IN", identifiers);

            // what is sent to the database, we can not see values
            Console.WriteLine($" Actual command: {actual}");
            // here we expose the values
            Console.WriteLine($"Exposed command: {exposed}");


            // assert
            Assert.AreEqual(expected, exposed);
        }


    }
}
