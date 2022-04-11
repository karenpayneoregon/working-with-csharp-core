using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSmart.Models;

namespace CodeSmart.Classes
{
    public class DictionaryMockedData
    {
        /// <summary>
        /// For working with duplicate keys
        /// </summary>
        /// <returns></returns>
        public static List<ColorItem> ColorItems() =>
            new()
            {
                new ColorItem() { Id = 1, Name = "Red" },
                new ColorItem() { Id = 2, Name = "Yellow" },
                new ColorItem() { Id = 1, Name = "Red"},
                new ColorItem() { Id = 3, Name = "Green"},
                new ColorItem() { Id = 1, Name = "Red"}
            };

        /// <summary>
        /// For working with null keys, yes there are duplicates but for usage we ignore them.
        /// </summary>
        /// <returns></returns>
        public static List<ColorItem> ColorItemsWithNullIdentifier() =>
            new()
            {
                new ColorItem() { Id = 1, Name = "Red" },
                new ColorItem() {  Name = "Yellow" },
                new ColorItem() { Id = 1, Name = "Red" },
                new ColorItem() { Name = "Green" },
                new ColorItem() { Id = 1, Name = "Red" }
            };
    }
}
