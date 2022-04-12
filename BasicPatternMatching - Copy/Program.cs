using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BasicPatternMatching.Classes;


namespace BasicPatternMatching
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            StringPropertyIsNull();
        }

        private static async Task PerformingAssertsOnVariables()
        {
            var someDate = "";

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (someDate.Is<DateTime>())
            {
                Debug.WriteLine("Is a date");
            }
            else
            {
                Debug.WriteLine("Is not a date");
            }

            string date = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (date.Is<DateTime>())
            {
                Debug.WriteLine($"{date.ToDateTime():hh:mm:ss tt}");
            }
            else
            {
                Debug.WriteLine("Is not a date");
            }


            var dateItem = DateTime.Now.AddMinutes(15);
            date = dateItem.ToString(CultureInfo.InvariantCulture);

            await Task.Delay(1000);

            var (dateTime, valid) = date.ToDateTimeSafe();
            Debug.WriteLine(valid ? $"{dateTime:hh:mm:ss tt}" : "Is not a date");

            if (DateTime.TryParse(date, out var resultDateTime))
            {
                Debug.WriteLine($"{resultDateTime:h:mm:ss tt} {TimeZoneInfo.Local.StandardName}");
            }
            
        }

        private static void IsListNull()
        {

            Debug.WriteLine(nameof(IsListNull));

            List<int> list = FullList;
            if (list is not null)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (int index = 0; index < list.Count; index++)
                {
                    Debug.WriteLine(list[index]);
                }
            }
            else
            {
                Debug.WriteLine("List is null");
            }

            list = NullList();
            Debug.WriteLine("Results with null list");

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (list is not null)
            {
                Debug.WriteLine($"With no elements: {list.DefaultIfEmpty(-1).LastOrDefault()}");
            }
            else
            {
                Debug.WriteLine("null list");
            }

            /*
             * Old school
             */
            if (list == null)
            {
                Debug.WriteLine("null list");
            }

        }

        private static void StringPropertyIsNull()
        {
            Person person = new();

            if (person.FirstName is { } name)
            {
                Debug.WriteLine($"First Name: {name}");
            }
            else
            {
                Debug.WriteLine("First name is null");
            }

            person.FirstName = "Mary";

            if (person.FirstName is { } name1)
            {
                Debug.WriteLine($"First Name: {name1}");
            }
            else
            {
                Debug.WriteLine("First name is null");
            }


            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                Debug.WriteLine("First name is null");
            }
            else
            {
                Debug.WriteLine($"First Name: {person.FirstName}");
            }
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
