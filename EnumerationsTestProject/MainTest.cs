using System;
using System.Collections.Generic;
using System.Linq;
using EnumerationLibrary.Classes;
using EnumerationLibrary.LanguageExtensions;
using EnumerationLibrary.Models;
using EnumerationsTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnumerationsTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Get <see cref="Categories"/> descriptions hardwired unlike the generic version below
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Enum)]
        public void CategoryDetailsHardWired()
        {
            List<string> expected = new()
            {
                "Soft drinks, coffees, teas",
                "Cheeses",
                "Breads, crackers, pasta, and cereal",
                "Dried fruit and bean curd"
            };

            List<ItemContainer> list = Operations.CategoryItems();

            var description = list.Select(cat => cat.Description).ToList();
    
            CollectionAssert.AreEqual(expected,description);


        }

        [TestMethod]
        [TestTraits(Trait.Enum)]
        public void CategoryDetailsGeneric()
        {
            List<string> expected = new()
            {
                "Soft drinks, coffees, teas",
                "Cheeses",
                "Breads, crackers, pasta, and cereal",
                "Dried fruit and bean curd"
            };

            List<ItemContainer> list = Operations.GetItems<Categories>();

            var description = list.Select(cat => cat.Description).ToList();

            CollectionAssert.AreEqual(expected, description);


        }


        [TestMethod]
        [TestTraits(Trait.Enum)]
        public void EnumFromStringGood()
        {
            // arrange
            var value = "Friday";

            
            // act
            DayOfWeek result = value.GetValueFromEnumMember<DayOfWeek>();

            // assert
            Assert.AreEqual(result, DayOfWeek.Friday);

            Console.WriteLine(System.Globalization.CultureInfo.CurrentCulture.Name);

        }

        /// <summary>
        /// ReSharper disable once InvalidXmlDocComment
        /// This test is validating <see cref="EnumExtensions.GetValueFromEnumMember(string)"/>
        /// </summary>
        /// <remarks>
        /// With proper coding in an application we will never have an incorrect value other than
        /// if there is a mismatch with cultures which again when coded correctly will never occur.
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.Enum)]
        // This is the assert
        [ExpectedException(typeof(ArgumentException))]
        public void EnumFromStringThrowException()
        {
            // arrange
            var value = "Friay";

            // act
            value.GetValueFromEnumMember<DayOfWeek>();

            // assert is ExpectedException

        }

        [TestMethod]
        [TestTraits(Trait.Culture)]
        public void EnglishCultureTest()
        {
            Assert.AreEqual(System.Globalization.CultureInfo.CurrentCulture.Name, "en-US");
        }

    }
}
