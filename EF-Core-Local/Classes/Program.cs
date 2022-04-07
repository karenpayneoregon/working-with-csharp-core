using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Saving
{
    /// <summary>
    /// Make this console window full-screen, set title
    /// </summary>
    public partial class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static readonly IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        [ModuleInitializer]
        public static void Init()
        {
            Console.Title = "EF Core local code samples";
            ShowWindow(ThisConsole, 3);
        }
    }
}
