using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeSmart.Classes;
using CodeSmart.Models;

namespace CodeSmart
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Operations.Dummy();
        }

    }

    public class Customer
    {

    }

    public class Operations
    {
        public static (string param1, Exception exception) Dummy()
        {
            try
            {
                return ("", null);
            }
            catch (Exception e)
            {
                return (null, e);
            }
        }
    }
}
