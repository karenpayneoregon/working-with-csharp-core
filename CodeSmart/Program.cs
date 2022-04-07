using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeSmart.Classes;


namespace CodeSmart
{
    class Program
    {
        static void Main(string[] args)
        {
            //DictionaryCodeSamples();
        }

        private static void DictionaryCodeSamples()
        {
            DictionaryExamples.AddToDictionaryReallyBad();
            DictionaryExamples.AddToDictionaryConventional();
            DictionaryExamples.AddToDictionaryBetter();
            DictionaryExamples.AddToDictionaryBetterWithEmptyCheck();
        }
    }
}
