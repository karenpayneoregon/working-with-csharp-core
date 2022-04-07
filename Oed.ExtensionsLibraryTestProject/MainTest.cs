using System;
using System.Collections.Generic;
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


    }
}
