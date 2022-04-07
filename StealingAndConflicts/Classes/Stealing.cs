using System.Collections.Generic;
using System.Linq;

namespace StealingAndConflicts.Classes
{
    public class Stealing
    {
        public static List<string> Chunking(string input) =>
            input.Split(",")
                .Chunk(2)
                .Select(chunk => string.Join(" ", chunk)).ToList();
    }
}
