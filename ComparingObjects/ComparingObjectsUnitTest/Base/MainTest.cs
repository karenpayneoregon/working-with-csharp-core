using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDataLibrary;



// ReSharper disable once CheckNamespace - do not change
namespace ComparingObjectsUnitTest
{
    public partial class MainTest
    {
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        /// <summary>
        /// Read people from file
        /// </summary>
        public static List<Person> ReadPeopleFromFile => PersonOperations.Read("Data\\people.json");

        public static List<Orders> ReadOrdersFromFile => OrderOperations.Read("Data\\Orders.txt");


        /// <summary>
        /// People list which are manipulated in test methods
        /// </summary>
        public static List<Person> TwoPersons => 
            new()
            {
                new() { FirstName = "Karen", LastName = "Payne" }, 
                new() { FirstName = "Karen", LastName = "Payne" }
            };
        public static List<Person> FourPersons =>
            new()
            {
                new() { FirstName = "Karen", LastName = "Payne" },
                new() { FirstName = "karen", LastName = "payne" },
                new() { FirstName = "Karen", LastName = "Adams" },
                new() { FirstName = "Karen", LastName = "Payne" }
            };

        public static List<Person> FivePeople =>
            new()
            {
                new() { Id = 1, FirstName = "Freddie", LastName = "Mercury" },
                new() { Id = 2, FirstName = "Elvis", LastName = "Presley" },
                new() { Id = 3, FirstName = "Chuck", LastName = "Berry" },
                new() { Id = 4, FirstName = "Ray", LastName = "Charles" },
                new() { Id = 5, FirstName = "David", LastName = "Bowie" }
            };

        readonly IEnumerable<Concert> Concerts = new List<Concert>() {
              new ()  {PersonId = 1, ConcertCount = 53, Year = 1979}
            , new ()  {PersonId = 1, ConcertCount = 74, Year = 1980}
            , new ()  {PersonId = 1, ConcertCount = 38, Year = 1981}
            , new ()  {PersonId = 2, ConcertCount = 43, Year = 1970}
            , new ()  {PersonId = 2, ConcertCount = 64, Year = 1968}
            , new ()  {PersonId = 3, ConcertCount = 32, Year = 1960}
            , new ()  {PersonId = 3, ConcertCount = 51, Year = 1961}
            , new ()  {PersonId = 3, ConcertCount = 95, Year = 1962}
            , new ()  {PersonId = 4, ConcertCount = 42, Year = 1950}
            , new ()  {PersonId = 4, ConcertCount = 12, Year = 1951}
            , new ()  {PersonId = 5, ConcertCount = 53, Year = 1983}
        };

       
    }

}
