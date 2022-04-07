using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dotnet5Standard.LanguageExtensions;

namespace StealingAndConflicts.Classes
{

    public class NotStealing
    {
        public static List<string> Chunking(string input)
        {
            List<string> list = new List<string>();

            var split = input.Split(",")
                .Chunk(2);

            foreach (var chunk in split)
            {
                list.Add(string.Join(" ", chunk));
            }

            return list;

        }
    }
}
