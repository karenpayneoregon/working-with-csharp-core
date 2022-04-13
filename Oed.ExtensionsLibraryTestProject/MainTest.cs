using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using EnumerationLibrary.Classes;
using EnumerationLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oed.ExtensionsLibrary.Classes;
using Oed.ExtensionsLibrary.LanguageExtensions;
using Oed.ExtensionsLibraryTestProject.Base;
using static Oed.ExtensionsLibrary.LanguageExtensions.DateTimeExtensions;

namespace Oed.ExtensionsLibraryTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.DateTimeExtensions)]
        public void ZeroPadDateTest()
        {
            // arrange
            DateTime newDateTime = new DateTime(2022, 3, 1, 1, 14, 0);

            // act
            var result = newDateTime.ZeroPad();

            // assert
            Assert.AreEqual("2022/03/01 01:14:00", result);

        }
        [TestMethod]
        [TestTraits(Trait.DateTimeExtensions)]
        public void CreateDateTime_Year_Month_Day_Hour_MinutesTest()
        {
            // arrange
            DateTime expected = new DateTime(2022, 3, 1, 1, 1,0);

            // act
            DateTime dateTime = CreateDateTime(2022, 3, 1, 1, 1);

            // assert
            Assert.AreEqual(expected, dateTime);

        }

        [TestMethod]
        [TestTraits(Trait.BoolExtensions)]
        public void YesNoToStringTest()
        {

            //List<ItemContainer> list = Operations.GetItems<LanguageCode>();

            // arrange
            var languageCode = LanguageCode.Russian;

            // act 
            var result = true.ToYesNoStringIs(languageCode);
            
            Assert.AreEqual(result, "da");

            result = false.ToYesNoStringIs(languageCode);

            Assert.AreEqual("Net", result);

        }

        [TestMethod]
        [TestTraits(Trait.BoolExtensions)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void YesNoToStringBadTest()
        {
            // arrange
            var languageCode = LanguageCode.None;
            // act
            var result = true.ToYesNoStringIs(languageCode);
            // assert is ExpectedException
        }

        [TestMethod]
        [TestTraits(Trait.IntExtensions)]
        public void SequenceFindMissingTest()
        {

            int[] values = { 1, 2, 3, 4, 6, 7, 8, 10 };
            int[] expect = { 5, 9 };

            var result = (values.SequenceFindMissing() as IEnumerable<int>)!.ToArray();
            CollectionAssert.AreEqual(result, expect);

        }
        [TestMethod]
        [TestTraits(Trait.IntExtensions)]
        public void SequenceHasMissingTest()
        {

            int[] values = { 1, 2, 3, 4, 6, 7, 8, 10 };

            IEnumerable result = values.SequenceFindMissing() ;
            Assert.IsTrue(result.Cast<int>().Any());

        }

        [TestMethod]
        [TestTraits(Trait.StringArrayExtensions)]
        public void AllIntTest()
        {
            var values = Enumerable.Range(1, 10)
                .Select(number => number.ToString())
                .ToArray();

            Assert.IsTrue(values.AllInt());

            values[3] = "A";
            Assert.IsFalse(values.AllInt());

        }

        [TestMethod]
        [TestTraits(Trait.StringArrayExtensions)]
        public void IntArrayAverage()
        {
            // arrange
            string[] values = { "10", "20", "10", "30" };


            // act
            var result = values.ToIntegerArray();

            Assert.AreEqual(result.Average(), 17.5);
        }

        /// <summary>
        /// Test obtaining values in a string array which cannot represent an int
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.StringArrayExtensions)]
        public void GetNonIntegerIndexesTest()
        {
            // arrange
            string[] values = { "100", "100B", "200", "200B", "1", "", "A", ".4", "2.3" };
            int[] expected = { 1, 3, 5, 6, 7, 8 };

            // act
            var results = values.GetNonIntegerIndexes();

            // assert
            CollectionAssert.AreEqual(expected, results);

        }

        /// <summary>
        /// Test Converting all values in array to int array where non int values will be set to the default value.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.StringArrayExtensions)]
        public void ToIntegerPreserveArrayTest()
        {
            // arrange
            string[] values = { "100", "100B", "200", "200B", "1", "", "A", ".4", "2.3" };
            int[] expected = { 100, 0, 200, 0, 1, 0, 0, 0, 0 };

            // act
            var results = values.ToIntegerPreserveArray();

            // assert
            CollectionAssert.AreEqual(expected, results);

        }

        [TestMethod]
        [TestTraits(Trait.StringExtensions)]
        public void SplitCamelCaseTest()
        {
            // arrange
            string name = "KarenPayne";
            string expected = "Karen Payne";

            // act
            var result = name.SplitCamelCase();

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void HasValueTest()
        {
            var value = Assembly.GetEntryAssembly()?
                .GetCustomAttribute<TargetFrameworkAttribute>()?
                .FrameworkName.Contains("Core");

            Assert.IsTrue(value.HasValue);
        }


    }
}
