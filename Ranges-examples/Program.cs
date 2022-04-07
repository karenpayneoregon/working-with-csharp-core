using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ranges_examples.Classes;
using Ranges_examples.LanguageExtensions;
using Ranges_examples.Models;

namespace Ranges_examples
{
    partial class Program
    {
        static void Main(string[] args)
        {

            //var salem = SomeOregonCities[^3];
            //Console.WriteLine(salem);
            List<int> list = new() { 1, 2, 3, 4, 5 };

            Console.WriteLine($"list.LastOrDefault() => {list.LastOrDefault()}");
            Console.WriteLine($"            list[^1] => {list[^2]}");
            //GetItemsBetweenTwoValues();

            GetRangeForList();
            Console.ReadLine();
        }

        private static void GetRangeForList()
        {
            
            Console.WriteLine(nameof(GetRangeForList));

            List<string> results = PeopleList.GetRange(1, 3);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
        private static void IndexesUnplugged()
        {
            
            Console.WriteLine(nameof(IndexesUnplugged));

            /*
             * Local method
             */
            static string[] GetFirstFourPersons(string[] people)
            {
                var result = new string[4];
                for (int index = 0; index < 4; index++)
                {
                    result[index] = people[index];
                }
                return result;
            }


            var firstFour = GetFirstFourPersons(People);

            // conventional iterating for-each
            foreach (var person in firstFour)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine();

            // conventional iterating for-next
            for (int index = 0; index < firstFour.Length; index++)
            {
                Console.WriteLine(firstFour[index]);
            }

            Console.WriteLine();

            /*
             * We happen to know where Marcus is (try that in a real app)
             * 0 is redundant 
             */
            firstFour = People[0..4];

            foreach (var person in firstFour)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine();

            /*
             * Be proactive, check first
             *
             * +1 for inclusion 
             */
            Index indexer = new Index(Array.IndexOf(People, "Marcus") +1, false);

            firstFour = People[0..indexer];

            foreach (var person in firstFour)
            {
                Console.WriteLine(person);
            }


            Console.WriteLine();

            var name = "Karen";
            var karenIndex = Array.IndexOf(People, name);
            if (karenIndex > -1)
            {
                indexer = new Index(karenIndex + 1, false);

                foreach (var person in firstFour)
                {
                    Console.WriteLine(person);
                }
            }
            else
            {
                Console.WriteLine($"Array does not contain '{name}'");
            }


            Console.WriteLine();
            Console.WriteLine("Ranges");
            Range firstFourRange = ..4;
            firstFour = People[firstFourRange];


            Range  lastTwoElement = ^2..;

            var lastTwo = People[lastTwoElement];
        }


        /// <summary>
        /// Demonstrates
        ///
        /// 1. How to get the index into a value case insensitive
        /// 2. Get a range from a List of strings
        /// </summary>
        private static void Generics()
        {

            Console.WriteLine(nameof(Generics));

            var monthNames = Helpers.MonthNames();

            Index indexer = new Index(monthNames.FindIndex(item 
                => item.Equals("april", StringComparison.OrdinalIgnoreCase)));

            var fourMonths = monthNames.ByIndex(indexer.Value, 4);

            Console.WriteLine(string.Join(",",fourMonths));
        }

        /// <summary>
        /// Demonstrates conventional and current methods to get the last item
        /// </summary>
        private static void GetLastCharInString()
        {

            Console.WriteLine(nameof(GetLastCharInString));

            var sentence = "Hello World";

            // ReSharper disable once UseIndexFromEndExpression
            var lastChar = sentence[sentence.Length - 1];
            var lastCharBetter = sentence[^1];
            Console.WriteLine($"Conventional: '{lastChar}' Indexing: `{lastCharBetter}`");
            
        }

        /// <summary>
        /// Demonstrates obtaining last element in an array. First method is conventional while
        /// the second is preferred as it's easier to code but must be .NET Core, not pre-code framework
        /// </summary>
        private static void GetLastElementInList()
        {

            Console.WriteLine(nameof(GetLastElementInList));

            List<int> list = new() { 1, 2, 3, 4, 5 };

            Console.WriteLine($"list.LastOrDefault() => {list.LastOrDefault()}");
            Console.WriteLine($"            list[^1] => {list[^1]}");


            list = new();
            Console.WriteLine($"With no elements: {list.DefaultIfEmpty(-1).LastOrDefault()}");

            list = NullList();
            Console.WriteLine("Results with null list");
            if (list is not null)
            {
                Console.WriteLine($"With no elements: {list.DefaultIfEmpty(-1).LastOrDefault()}");
            }
            else
            {
                Console.WriteLine("null");
            }

            // pointless
            Console.WriteLine("After");
            Console.WriteLine($"With no elements: '{list?.LastOrDefault()}'");

        }

        /// <summary>
        /// Demonstrates a simple for-next
        /// </summary>
        private static void BasicForIterateCharsInString()
        {

            Console.WriteLine(nameof(BasicForIterateCharsInString));

            var sentence = "Just want to say, Hello World there!";

            for (int index = 0; index < sentence.Length; index++)
            {
                Console.WriteLine($"{index,-4:D3}{sentence[index]}");
            }

            var substring = sentence.SubstringByIndexes(5, 8);
            Console.WriteLine($"subString '{substring}'");

        }

        /// <summary>
        /// Demonstrates obtaining elements in an array between two values.
        /// </summary>
        /// <remarks>
        /// <see cref="Helpers.GetBetweenInclusiveFixed"/> may be hard to wrap one's head around.
        /// No different than peeking at much of C# source code so best to think of this
        /// method as I will use it and not worry about the mechanics.
        /// </remarks>
        private static void GetItemsBetweenTwoValues()
        {

            Console.WriteLine(nameof(GetItemsBetweenTwoValues));

            string startCity = "Aloha";
            string endCity = "Ashland";

            string[] cityFile = FileOperations.OregonCities();
            string[] cities = Helpers.GetBetweenInclusiveFixed(cityFile, startCity, endCity);

            string[] genericCities  = Helpers.GetBetweenInclusive(cityFile, startCity, endCity);

            var listCity = Helpers.GetBetweenInclusive(cityFile.ToList(), startCity, endCity);

            if (cities is not null)
            {

                IEnumerable<CityIndexer> results = cities.ToList().Select((name, index) => new CityIndexer(index, name));
                
                Console.WriteLine($"{results.FirstOrDefault().Index}..{results.LastOrDefault().Index} =>");
                Console.WriteLine($"\t{cities[results.FirstOrDefault().Index]} : {cities[results.LastOrDefault().Index]} ");

                Console.WriteLine();

                foreach (var result in results)
                {
                    Console.WriteLine($"{result.Index,-5:D3}{result.CityName}");
                }

            }
            else
            {
                Console.WriteLine("Operation failed");
            }

            Console.WriteLine();
            
        }
    }

}
