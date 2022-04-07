using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relational_Pattern_Matching.Classes;
using Relational_Pattern_Matching.LanguageExtensions;
using Relational_Pattern_Matching.MockedData;

// ReSharper disable once CheckNamespace
namespace Relational_Pattern_Matching
{
    partial class Program
    {
        static void ShowPrompt()
        {
            Console.Clear();
            Console.Write("Enter a int following by ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ENTER ");
            Console.ResetColor();
            Console.Write("or press ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ENTER ");
            Console.ResetColor();
            Console.Write("to exit.\n");
        }

        /// <summary>
        /// Demonstrates
        /// * How to validate a input string is a int
        /// * Pattern matching as in
        ///   if (result is > 0 and <= 10) vs if (result >0 && result <= 10)
        /// </summary>
        static void Example1()
        {
            while (true)
            {

                ShowPrompt();

                var userValue = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userValue))
                {
                    return;
                }

                if (int.TryParse(userValue, out var result))
                {
                    /*
                     *  C# 9 has introduced the "is" and "and" keywords
                     *  Before C# 9 we would use (result >0 && result <= 10)
                     */
                    if (result is > 0 and <= 10)
                    {
                        Console.WriteLine($"{result} is more than 0 but less than or equal to 10");
                    }
                    else if (result is > 10)
                    {
                        Console.WriteLine($"{result} is more than 10");
                    }
                    else if (result < 0)
                    {
                        Console.WriteLine($"{result} is less than 0");
                    }


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write($"{userValue}");
                    Console.ResetColor();
                    Console.Write(" is not an int\n");

                }

                Console.ReadLine();
            }
        }
        /// <summary>
        /// switch expression vs switch statement
        /// Variation of <see cref="Example3"/>
        /// </summary>
        static void Example2()
        {

            Console.WriteLine(nameof(Example2));

            int[] values = { -1, 10, 0, 11, 1 };

            foreach (var value in values)
            {
                Console.WriteLine($"{value,4} is {value.Determination()}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Variation of <see cref="Example2"/>
        /// </summary>
        static void Example3()
        {

            Console.WriteLine(nameof(Example3));

            List<int> values = new() { 1, 2, 3, 0, 4, 5 };

            values.ForEach(value => Console.WriteLine($"{value,-3}{value.Determination4()}"));

            Console.ReadLine();

        }

        static void RecursivePatternStaticCondition()
        {
            Console.WriteLine(nameof(RecursivePatternStaticCondition));

            StringBuilder builder = new();

            builder.AppendLine("");


            foreach (var employeeList in Helpers.GetEmployeesWhereManagerHasThreeYearsAsManager(Mocked.PeopleList()))
            {
                var names = employeeList.Select(employee => employee.FullName).ToList().Select(fullName => fullName);

                foreach (var name in names)
                {
                    builder.AppendLine(name);
                }

            }

            Console.WriteLine(builder);
            Console.ReadLine();
        }
    }
}
