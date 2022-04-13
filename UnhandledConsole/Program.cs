using System;
using System.Diagnostics;
using UnhandledConsole.Classes;

namespace UnhandledConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Code sample";

            try
            {
                throw new Exception();
            }
            catch (Exception exception)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(exception, true);

                // Get the top stack frame
                StackFrame frame = st.GetFrame(0);

                var offender = $"{frame.GetFileName().StripSolutionFolder()} line: {frame.GetFileLineNumber()}";
                Console.WriteLine("No project path from StackFrame");
                Console.WriteLine(offender);
                Console.WriteLine();

                Console.WriteLine("No project path from ex.ToString()");
                Console.WriteLine(exception.ToString().StripSolutionFolder().RemoveFirst("\\"));

                Console.WriteLine();
                Console.WriteLine("ex.ToString()");
                Console.WriteLine(exception.ToString());
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
