using System;
using System.Collections.Generic;
using System.Linq;
using InterrogatingConsoleApp.Classes;
using InterrogatingConsoleApp.Interfaces;
using InterrogatingConsoleApp.LanguageExtensions;

namespace InterrogatingConsoleApp
{
    partial class Program
    {
        static void Main(string[] args)
        {



            //Version1();
            //Version2();
            //Version3();
            MethodImplementsAttribute();

            Console.ReadLine();
        }

        /// <summary>
        /// Check if <see cref="Customer.DummyMethod"/> is decorated
        /// with <see cref="MyCustomAttribute"/>
        /// </summary>
        private static void MethodImplementsAttribute()
        {

            Console.WriteLine(nameof(MethodImplementsAttribute));

            var checkForAttribute = typeof(Customer)
                /*
                 * ! is (null-forgiving) operator
                 * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving
                 */
                .GetMethod(nameof(Customer.DummyMethod))! 
                .GetCustomAttributes(inherit: false)
                .OfType<MyCustomAttribute>()
                .SingleOrDefault();

            Console.WriteLine(checkForAttribute is not null ? "Has attribute" : "Does not have attribute");
        }

        private static void Version1()
        {
            Console.WriteLine(nameof(Version1));

            var types = TypesArray();

            foreach (var type in types)
            {
                if (type as IBase is { } item)
                {
                    Console.WriteLine(item.Id);
                }
            }

        }
        private static void Version2()
        {
            Console.WriteLine(nameof(Version2));

            var types = TypesArray();

            foreach (var type in types)
            {
                if (type is IBase item)
                {
                    Console.WriteLine($"{item.Id}");
                }
            }

        }

        private static void Version3()
        {
            IEnumerable<Type> result = TypesList().Where(type => type.Implements<IBase>());
        }

        private static object[] TypesArray() => 
            new object[] { new Person() { Identifier = 3 }, new Human() { Identifier = 2 } };

        private static List<Type> TypesList() => 
            new() { typeof(Person), typeof(Human) };
    }

}

