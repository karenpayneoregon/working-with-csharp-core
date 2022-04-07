using Microsoft.VisualStudio.TestTools.UnitTesting;
using SidesMpcTestProject.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using SidesMpcLibrary.Classes;
using SidesMpcLibrary.Models;

namespace SidesMpcTestProject
{
    /// <summary>
    /// Notes
    /// 
    /// An _ is known as a discard
    ///
    /// Discards:
    /// Are placeholder variables that are intentionally unused in application code.
    /// Discards are equivalent to unassigned variables; they don't have a value. A discard communicates intent
    /// to the compiler and others that read your code: You intended to ignore the result of an expression.
    /// You may want to ignore the result of an expression, one or more members of a tuple expression,
    /// an out parameter to a method, or the target of a pattern matching expression.
    /// https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards
    /// </summary>
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.SideMpc)]
        public void LoadDevelopmentEnvironmentTest()
        {
            // act
            Assert.IsTrue(Environment.UserName == "PayneK");

            // arrange
            var (success, _ , _ ) = SettingsOperations.ReadSettings(SidesEnvironment.Development);

            // assert
            Assert.IsTrue(success);

        }

        [TestMethod]
        [TestTraits(Trait.SideMpc)]
        public void ReadackOutput_SettingDevelopmentEnvironmentTest()
        {
            // arrange
            var ( _, _ , items) = SettingsOperations.ReadSettings(SidesEnvironment.Development);

            // act
            var item = items.FirstOrDefault(x => x.Name == "ackOutput");

            // assert
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Value, "DEV_TEXT");
        }

        [TestMethod]
        [TestTraits(Trait.SideMpc)]
        public void ReadackOutput_SettingTestEnvironmentTest()
        {
            // arrange
            var (_, _, items) = SettingsOperations.ReadSettings(SidesEnvironment.Testing);

            // act
            var item = items.FirstOrDefault(x => x.Name == "ackOutput");

            // assert
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Value, "TEST_TEXT");
        }

        [TestMethod]
        [TestTraits(Trait.SideMpc)]
        public void ReadackOutput_SettingProductionEnvironmentTest()
        {
            // arrange
            var (_, _, items) = SettingsOperations.ReadSettings(SidesEnvironment.Production);

            // act
            var item = items.FirstOrDefault(x => x.Name == "ackOutput");

            // assert
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Value, "PROD_TEXT");
        }

        private void Temp()
        {
            List<string> list = new List<string>();
            
        }
    }
}