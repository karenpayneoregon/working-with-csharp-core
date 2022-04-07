using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSmart.ExtensionMethods;
using CodeSmart.Models;

namespace CodeSmart.Classes
{
    public class DictionaryExamples
    {
        /// <summary>
        /// Brute force adding items to a dictionary where there may be dups
        /// </summary>
        public static void AddToDictionaryReallyBad()
        {

            Debug.WriteLine(nameof(AddToDictionaryReallyBad));

            Dictionary<int, string> dictionary = new();

            List<ColorItem> list = DictionaryMockedData.ColorItems();

            try
            {
                foreach (ColorItem item in list)
                {
                    Debug.Assert(item.Id != null, "item.Id != null");
                    dictionary.Add(item.Id.Value, item.Name);
                }
            }
            catch (Exception ouchException)
            {
                Debug.WriteLine(ouchException.Message);
            }

            foreach (var kvp in dictionary)
            {
                Debug.WriteLine($"{kvp.Key,-3}{kvp.Value}");
            }

            Debug.WriteLine("");
        }

        /// <summary>
        /// Use conventional assertion to avoid duplicates
        /// </summary>
        public static void AddToDictionaryConventional()
        {
            Debug.WriteLine(nameof(AddToDictionaryConventional));

            Dictionary<int, string> dictionary = new();
            List<ColorItem> list = DictionaryMockedData.ColorItems();

            foreach (ColorItem item in list.Where(item => !dictionary.ContainsKey(item.Id.Value)))
            {
                Debug.Assert(item.Id != null, "item.Id != null");
                dictionary.Add(item.Id.Value, item.Name);
            }

            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                Debug.WriteLine($"{kvp.Key,-3}{kvp.Value}");
            }

            Debug.WriteLine("");
        }
        /// <summary>
        /// A one liner to avoid duplicates
        /// </summary>
        public static void AddToDictionaryBetter()
        {

            Debug.WriteLine(nameof(AddToDictionaryBetter));

            Dictionary<int, string> dictionary = DictionaryMockedData.ColorItems()
                // ReSharper disable once PossibleInvalidOperationException
                .GroupBy(colorItem => colorItem.Id.Value, colorItem => colorItem.Name)
                .ToDictionary(group => group.Key, group => group.First());

            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                Debug.WriteLine($"{kvp.Key,-3}{kvp.Value}");
            }

            Debug.WriteLine("");
        }
        /// <summary>
        /// Next step up from <see cref="AddToDictionaryBetter"/>
        /// </summary>
        public static void AddToDictionaryBetterWithEmptyCheck()
        {
            Debug.WriteLine(nameof(AddToDictionaryBetterWithEmptyCheck));
            List<ColorItem> list = DictionaryMockedData.ColorItemsWithNullIdentifier();

            // Best for simple check
            //if (list.Any(x => x.Id is null))
            //{
            //    Debug.WriteLine("One or more empty keys");
            //    return;
            //}

            // Need to also know indices?
            var (hasNulls, indicesList) = list.NullIndices(x => x.Id is null);
            if (hasNulls)
            {
                Debug.WriteLine($"Has null identifiers {string.Join(",", indicesList)}");
                return;
            }

            Dictionary<int, string> dictionary = list
                .GroupBy(colorItem => colorItem.Id.Value, colorItem => colorItem.Name)
                .ToDictionary(group => group.Key, group => group.First());

            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                Debug.WriteLine($"{kvp.Key,-3}{kvp.Value}");
            }

            Debug.WriteLine("");
        }
    }
}
