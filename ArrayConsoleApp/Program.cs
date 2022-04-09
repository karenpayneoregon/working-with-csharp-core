using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ArrayConsoleApp.Classes;
using ArrayConsoleApp.LanguageExtensions;
using ArrayConsoleApp.Models;
using static System.Globalization.DateTimeFormatInfo;


namespace ArrayConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericLastElement();
        }

        /// <summary>
        /// contacts array is is strongly typed meaning it's scope exceeds this method
        /// </summary>
        private static void ExplicitTypedArray()
        {
            SingleContact[] contacts =
            {
                new SingleContact()
                {
                    Name = "Eugene Jones",
                    PhoneNumbers = new[] { "206-555-0108", "425-555-0001" }
                },
                new()
                {
                    Name = "Mary Smith",
                    PhoneNumbers = new[] { "650-555-0199" }
                }
            };

            foreach (var contact in contacts)
            {
                Debug.WriteLine($"{contact.Name}");
                foreach (var phoneNumber in contact.PhoneNumbers)
                {
                    Debug.WriteLine($"\t{phoneNumber}");
                }
            }

        }

        /// <summary>
        /// contacts array is anonymous which means scoped to this method
        /// </summary>
        private static void ImplicitlyTypedArraysInObjectInitializers()
        {
            var contacts = new[]
            {
                new
                {
                    Name = "Eugene Jones",
                    PhoneNumbers = new[] { "206-555-0108", "425-555-0001" }
                },
                new
                {
                    Name = "Mary Smith",
                    PhoneNumbers = new[] { "650-555-0199" }
                }
            };

            foreach (var contact in contacts)
            {
                Debug.WriteLine($"{contact.Name}");
                foreach (var phoneNumber in contact.PhoneNumbers)
                {
                    Debug.WriteLine($"\t{phoneNumber}");
                }
            }
        }

        /// <summary>
        /// Various ways to init arrays
        /// </summary>
        private static void InitializeArrays()
        {

            /*
             * Local function/method
             */
            void WriteJoined(int[] sender)
            {
                Debug.WriteLine(string.Join("," , sender));
            }

            var data0 = new int[3];
            var data1 = new int[3] { 1, 2, 3 };
            var data2 = new int[] { 1, 2, 3 }; // same as line above, easier to understand for some
            var data3 = new[] { 1, 2, 3 };

            var data6 = new int[0];
            var data6a = Array.Empty<int>();

            var data7 = new int[] { };

            int[] data8 = Enumerable.Range(0, 9).ToArray();

            int[] data9 = Enumerable.Repeat(1, 20).ToArray();

            WriteJoined(new int[2]);
            WriteJoined(new int[2] { 1, 2 });
            WriteJoined(new int[] { 1, 2 });
            WriteJoined(new[] { 1, 2 });

            Debug.WriteLine("");

            var anonymous = Enumerable.Range(0, 9)
                .Select((value, index) => new { Value = value, Index = index })
                .ToArray();

            foreach (var item in anonymous)
            {
                Debug.WriteLine($"{item.Index,-3}{item.Value}");
            }

            Debug.WriteLine("");

            var monthNames = Enumerable.Range(1, 12)
                .Select((index) => DateTimeFormatInfo.CurrentInfo.GetMonthName(index))
                .ToList();

            var monthsIndexed = monthNames
                .Select((name, index) => new {Name = name, Index = index +1 })
                .ToList();

            foreach (var monthItem in monthsIndexed)
            {
                Debug.WriteLine($"{monthItem.Index, -3}{monthItem.Name}");
            }
            
        }

        /// <summary>
        /// Example for iterating a multi-dimensional
        /// </summary>
        private static void MultiDimensional()
        {
            int[,] array =
            {
                { 1, 2, 3 }, 
                { 4, 5, 6 }
            };

            for (var index = array.GetLowerBound(0); index <= array.GetUpperBound(0); index++)
            {
                for (var innerIndex= array.GetLowerBound(1); innerIndex <= array.GetUpperBound(1); innerIndex++)
                {
                    Debug.Write($"[{index}][{innerIndex}] = {array[index, innerIndex]}   ");
                }

                Debug.WriteLine("");
            }

            Debug.WriteLine("");

        }

        /// <summary>
        /// Well we need to be prepared for multi type arrays
        /// Not advisable if avoidable
        /// </summary>
        private static void MultiTypeArray()
        {
            object[] array = 
            {
                1, 
                "Karen", 
                new Person() {FirstName = "Mary", LastName = "Kennedy"}, 
                null,
                true,
                "Payne"
            };

            foreach (var item in array)
            {
                switch (item)
                {
                    case int intItem:
                        Debug.WriteLine($"int: {intItem}");
                        break;
                    case Person person:
                        Debug.WriteLine($"Person: {person.FullName}");
                        break;
                    case null:
                        Debug.WriteLine("null");
                        break;
                    case string stringItem:
                        Debug.WriteLine($"String: {stringItem}");
                        break;
                    default:
                        Debug.WriteLine("???");
                        break;
                }
            }

            Debug.WriteLine("");

            // get only strings - @ is to escape reserve keywords
            string[] results = array
                .Where(@object => @object is string)
                .Select(@object => @object.ToString())
                .ToArray();

            Debug.WriteLine(string.Join(",", results));
            
        }

        /// <summary>
        /// Demonstrates getting elements using indexers
        /// </summary>
        private static void DayNamesIndexing()
        {
            string[] dayNames = CurrentInfo?.DayNames;

            /*
             * Using indexers skip first element 'Monday' take up to Friday thus work days
             */
            string[] workDays = dayNames?[1..6];

            foreach (var day in workDays)
            {
                Debug.WriteLine(day);
            }

            Debug.WriteLine("");

            /*
             * Display element index and element values
             */
            var indexed = dayNames!
                .Select((name, index) => new DayIndexer { Name = name, Index = index });

            foreach (var dayItem in indexed)
            {
                Debug.WriteLine($"{dayItem.Index,-3}{dayItem.Name}");
            }

            var indexedArray = indexed.ToArray();

            Debug.WriteLine($"Last in array 1: {indexed.ToArray()[^1].Name}");  // IEnumerable to array
            Debug.WriteLine($"Last in array 2: {indexedArray.GetLast().Name}"); // array
            Debug.WriteLine($"Last in array 3: {indexed.GetLast().Name}");  // IEnumerable


        }

        private static void GenericLastElement()
        {
            string[] names = { "Anne", "Ben", "Carrie", "Dean" };

            int[] numbers = { 1, 2, 3, 4, 5 };

            DateTime[] dates = { new (2021, 1, 1), new (2021, 1, 2), new (2021, 1, 3) };

            SingleContact[] contacts =
            {
                new() { Name = "Eugene Jones", PhoneNumbers = new[] { "206-555-0108", "425-555-0001" } }, 
                new() { Name = "Mary Smith", PhoneNumbers = new[] { "650-555-0199" } }
            };

            Debug.WriteLine($"   Last name: {names.GetLast()}");
            Debug.WriteLine($" Last number: {numbers.GetLast()}");
            Debug.WriteLine($"   Last Date: {dates.GetLast():d}");
            Debug.WriteLine($"Last Contact: {contacts.GetLast().Name}");
        }

        /// <summary>
        /// Idea here is to create a string array of hours to present in a front-end control e.g. dropdown or ComboBox
        /// where nothing else is needed method or property wise.
        ///
        /// Then there is the code in <seealso cref="HoursExtended"/> which in this case a requirement is to store time
        /// in a database such as SQL-Server to store without timezone while Oracle there are several types and with that
        /// SQL-Server can easily match Oracle types.
        /// </summary>
        private static void GoodUseOfArray()
        {
            var hours = Hours.Range(TimeIncrement.Quarterly);

            StringBuilder builder = new();
            foreach (var hour in hours)
            {
                builder.AppendLine(hour);
            }

            /*
             * Note on file name, by default, if no path is given the current folder is used for writing data
             * while Process.Start  does not use the current path as the current folder and an issue may be
             * other code using Directory.SetCurrentDirectory() which alters the current folder.
             *
             * So best not to use just a file name in an application where other code may change the current folder.
             *
             */

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hours.txt");
            File.WriteAllText("Hours.txt", builder.ToString());
            
            EnvironmentHelpers.OpenTextFileWithExplorer(fileName);

        }

        /// <summary>
        /// The method <seealso cref="GoodUseOfArray"/> is great for display although for remembering information consider
        /// a container like <seealso cref="HourItem"/> which has a string property for display and a backing <seealso cref="TimeSpan"/>
        /// </summary>
        private static void HoursExtended()
        {
            string[] hours = Hours.Range(TimeIncrement.Quarterly);
            List<HourItem> hourItemList = Hours.HourItems(hours);
            Console.WriteLine("Place breakpoint here to examine hourItemList");
        }

        /// <summary>
        /// Simple example of a string array
        /// </summary>
        private static void StringArrayBasics()
        {
            string[] names = { "Anne", "Ben", "Carrie", "Dean" };

            // iterate array elements in an indexer
            for (int index = 0; index < names.Length; index++)
            {
                Debug.WriteLine(names[index]);
            }

            Debug.WriteLine("");

            // alternate to for, foreach
            foreach (var name in names)
            {
                Debug.WriteLine(name);
            }

            Debug.WriteLine("");

            // using a language extension add a new element
            names = names.AddToArray("Karen");


            for (int index = 0; index < names.Length; index++)
            {
                Debug.WriteLine(names[index]);
            }


            Debug.WriteLine("");


            Debug.WriteLine($"Skip {names[0]}");
            foreach (var name in names[1..])
            {
                Debug.WriteLine(name);
            }


            Debug.WriteLine("");

            Debug.WriteLine("Get last three names");
            var lastThree = names[2..^0];

            for (int index = 0; index < lastThree.Length; index++)
            {
                Debug.WriteLine(lastThree[index]);
            }


            Debug.WriteLine("");

            // reverse elements in the array
            Array.Reverse(names);

            Debug.WriteLine("Reversed");

            for (int index = 0; index < names.Length; index++)
            {
                Debug.WriteLine(names[index]);
            }

        }

        /// <summary>
        /// Find an element in an array
        /// </summary>
        private static void FindIndexOfElement()
        {
            var days = CurrentInfo.DayNames;
            var dayName = DateTime.Now.DayOfWeek.ToString();

            int index = Array.FindIndex(days, item => item == dayName);
            Debug.WriteLine($"Find {dayName} -> index {index} days[{days[index]}]");
            
            var test = 1.In(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Debug.WriteLine(test);

        }

        /// <summary>
        /// String array find case insensitive
        /// </summary>
        private static void FindIndexCaseInsensitive()
        {
            string[] array = { "hello", "Hi", "bye", "welcome", "hell" } ;
            int index = Array.FindIndex(array, t => t.IndexOf("hi", StringComparison.InvariantCultureIgnoreCase) >= 0);

            Debug.WriteLine(array[index]);
        }

        /// <summary>
        /// Find differences between two arrays using Except extension 
        /// </summary>
        private static void DifferenceBetweenArrays()
        {
            var array1 = new[] { "A", "B", "C" };
            var array2 = new[] { "A", "C", "D" };

            var result1 = array2.Except(array1).ToArray();

            foreach (var item in result1)
            {
                Debug.WriteLine(item);
            }

            Debug.WriteLine("");

            var result2 = array1.Except(array2).ToArray();

            foreach (var item in result2)
            {
                Debug.WriteLine(item);
            }

            var result3 = array1.Intersect(array2).ToArray();

            Debug.WriteLine("");

            foreach (var item in result3)
            {
                Debug.WriteLine(item);
            }
        }

        /// <summary>
        /// Converting string array to int array
        /// </summary>
        private static void StringArrayToIntegerArray()
        {
            string[] values = { "", "1", "2", null, "4", "Text" };

            if (values.AllInt())
            {
                Debug.WriteLine("All can be converted");
            }
            else
            {
                var results1 = values.GetNonIntegerIndexes();

                Debug.WriteLine(string.Join(",", values.JoinWithNulls()));
                Debug.WriteLine($"GetNonIntegerIndexes(): {string.Join(",", results1)}");
            }

            var result2 = values.ToIntegerArray();

            Debug.WriteLine($"ToIntegerArray(): {string.Join(",", result2)}");


            Debug.WriteLine("");

            values[0] = "0";
            values[3] = "45";

            if (!values.AllInt())
            {
                var results = values.GetNonIntegerIndexes();

                Debug.WriteLine(string.Join(",", values));
                Debug.WriteLine(string.Join(",", results));
            }
            else
            {
                Debug.WriteLine("All can be converted");
            }
        }

        // place breakpoint here and examine each array

        /// <summary>
        /// Remove element at index, see also <seealso cref="FindIndexOfElement"/>
        /// </summary>
        private static void RemoveAtIndex()
        {
            string[] array = { "one", "two", "three", "four", "five" };


            Debug.WriteLine(string.Join(",", array));

            int indexToRemove = 1;
            array = array.Where((source, index) => index != indexToRemove).ToArray();

            Debug.WriteLine(string.Join(",", array));
        }


        /// <summary>
        /// Get a range of elements
        ///
        /// 1. Using a language extension
        /// 2. Using Skip/Take
        /// 
        /// </summary>
        private static void SubArray()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int length = 3;

            int index = Array.FindIndex(array, item => item == 4);

            array = array.SubArray(index, length);
            Debug.WriteLine($"Extension: {string.Join(",", array)}");

            /*
             * Create a new array on the fly which matches the original array as
             * the original is smaller now from SubArray extension
             */
            var array1 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Skip(index).Take(3).ToArray();

            Debug.WriteLine($"Skip/Take: {string.Join(",", array1)}");


        }


        /// <summary>
        /// Given a List of string, concatenate and present as an array
        /// </summary>
        /// <remarks>Yep not arrays to start</remarks>
        private static void Ranger()
        {
            var list = new List<string> { "abc", "def", "ghi" };
            
            Range range = new(0, ^1);

            List<string> subList1 = list.GetRange(range);
            List<string> subList2 = list.GetRange(0..1);


            Debug.WriteLine($"{string.Join(",", subList1.ToArray())}");
            Debug.WriteLine($"{string.Join(",", subList2.ToArray())}");
            Debug.WriteLine("");
        }

        /// <summary>
        /// From a Microsoft Q-A forum question which is an oddball question as usually the
        /// question is to get a single duplicate e.g. below would be
        ///
        /// This is a test
        /// This is a orange
        /// 
        /// https://docs.microsoft.com/en-us/answers/questions/650430/how-to-see-only-duplicate-values-in-listbox.html
        ///
        /// Notes
        /// * we are case sensitive as that is truly getting exact dups
        /// * the rare case for one char variables e.g. 'g'
        /// </summary>
        private static void GetAllDuplicates()
        {
            string[] lines = {
                "This is a test",
                "This is a apple",
                "This is a orange",
                "This is a test",
                "This is a orange",
                "This is a test"
            };


            if (lines.ContainsDuplicates())
            {
                /*
                 * Odd
                 */
                List<string> duplicates = lines
                    .GroupBy(line => line)
                    .Where(g => g.Skip(1).Any())
                    .SelectMany(g => g)
                    .ToList();


                duplicates.ForEach(item => Debug.WriteLine(item));

                Debug.WriteLine("");

                /*
                 * Typical
                 */
                duplicates = lines.GroupBy(line => line)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                duplicates.ForEach(item => Debug.WriteLine(item));

                Debug.WriteLine("");

                var indices = lines.Select((value, index) => new { value, index })
                    .Where(a => string.Equals(a.value, "This is a test"))
                    .Select(a => a.index).ToList();

                if (indices.Any())
                {
                    Debug.WriteLine("Indexes");
                    indices.ForEach(x => Debug.WriteLine(x));
                }
                else
                {
                    Debug.WriteLine("None");
                }

                var duplicates1 = lines
                    .Select((text, index) => new IndexItem { Index = index, Text = text })
                    .GroupBy(g => g.Text)
                    .Where(g => g.Count() > 1).ToList();

            }
            else
            {
                Debug.WriteLine("No duplicates");
            }

        }

        /// <summary>
        /// Some developer may like a Push method
        /// Get first three day names, Sunday, Monday, Tuesday and append/push Wednesday
        /// </summary>
        private static void Push()
        {
            var days = CurrentInfo.DayNames;

            var days1 = days[..3];
            ArrayHelpers.Push(ref days1, "Wednesday");

            foreach (var day in days1)
            {
                Debug.WriteLine(day);
            }

        }


        private static void JoinWithNulls()
        {
            string[] namesArray = new[] { "Karen", null, "Lisa", "Vince", null};
            Debug.WriteLine(namesArray.JoinWithNulls());

            int?[] nullableIntArray = { 1, 2, null, 4, 5 };
            var result = nullableIntArray.ToStringArray().JoinWithNulls();
            Debug.WriteLine(result);
        }

        [ModuleInitializer]
        public static void Init1()
        {
            Console.Title = "Working with arrays";
        }
    }


}
