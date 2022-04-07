﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Saving.Classes;
using static Saving.Utilities.ConsoleKeysHelper;

namespace Saving
{
    public partial class Program
    {
        public static async Task Main()
        {


            await Task.Delay(0);
            //await BlogPostSample.CreateNewPopulateRead();
            await BlogPostSample.DeleteAndModifyRecordIndividualContexts();
            //PauseTenSeconds("Press a key or timeout in 10 seconds");
            
            //await  BlogPostSample.UpdateExisting();

            Console.ReadLine();
        }

    }
}