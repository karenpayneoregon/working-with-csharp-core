using BaseOracleCoreConsole1.Classes;
using System;
using System.Diagnostics;
using static BaseOracleCoreConsole1.Classes.Operations;

namespace BaseOracleCoreConsole1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Default connection string: {ConnectionString()}");

            Console.ReadLine();
        }
    }
}
