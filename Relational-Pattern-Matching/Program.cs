using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using Relational_Pattern_Matching.Classes;
using Relational_Pattern_Matching.LanguageExtensions;
using Relational_Pattern_Matching.MockedData;
using Relational_Pattern_Matching.Models;


namespace Relational_Pattern_Matching
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //Example1();
            //Example2();
            //Example3();
            RecursivePatternStaticCondition();
            //switch_expression();
            //CaseWhen();

            //Console.ReadLine();
        }

        private static void CaseWhen()
        {
            Operations.CaseWhen(1);
            Operations.CaseWhen(5);
            Operations.CaseWhen(7);
        }

        static void switch_expression()
        {
            Conventional1(2);
            ExpressionBodiedMember1_int(2);
            Console.WriteLine();
            EnvironmentData.CostCenter = "700";
            Console.WriteLine(ExpressionBodiedMember2());
            Console.ReadLine();
        }

    }
}
