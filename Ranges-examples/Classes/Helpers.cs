using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Ranges_examples.Models;

namespace Ranges_examples.Classes
{
    public class Helpers
    {
        public static List<string> MonthNames() 
            => Enumerable.Range(1, 12).Select((index) => DateTimeFormatInfo.CurrentInfo.GetMonthName(index)).ToList();

        /// <summary>
        /// Get elements between two elements 
        /// </summary>
        /// <param name="sender">string array</param>
        /// <param name="startItem">start item</param>
        /// <param name="endItem">end item</param>
        /// <returns>Range between start and end items or null</returns>
        public static string[] GetBetweenInclusiveFixed(string[] sender, [DisallowNull] string startItem, [DisallowNull] string endItem)
        {

            /*
             * Create a strong typed list of values and indexes 
             */
            List<ElementItem> elements = sender.Select((element, index) => 
                new ElementItem(element, new Index(index),
                    // Reverse is the key here
                    new Index(Enumerable.Range(0, sender.Length).Reverse().ToList()[index], true)))
                .ToList();

            /*
             * Get first value and last value positions/indexs
             */
            ElementItem start = elements.FirstOrDefault(item => item.Name == startItem);
            ElementItem end = elements.FirstOrDefault(item => item.Name == endItem);


            /*
             * Result
             *
             * Start index   4, front start
             * End index  ^360, from end
             *
             */

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return start is null || end is null ? 
                null : 
                sender[start.StartIndex..end.EndIndex];

        }


        public static TCollection[] GetBetweenInclusiveGeneric<TCollection>(TCollection[] sender, [DisallowNull] TCollection startItem, [DisallowNull] TCollection endItem)
        {

            List<GenericElement<TCollection>> elementsList = sender.Select((element, index) => 
                new GenericElement<TCollection>(element, new Index(index), 
                    new Index(Enumerable.Range(0, sender.Length).Reverse().ToList()[index], true)))
                .ToList();

            GenericElement<TCollection> start = elementsList.FirstOrDefault(item => Equals(item.Name, startItem));
            GenericElement<TCollection> end = elementsList.FirstOrDefault(item => Equals(item.Name, endItem));

            return start is null || end is null ? 
                null : 
                sender[start.StartIndex..end.EndIndex];

        }

        public static T[] GetBetweenInclusive<T>(T[] sender, T startItem, T endItem)
        {
            List<GenericElement<T>> elementsList = sender.Select((element, index) =>
                    new GenericElement<T>(element, new Index(index),
                        new Index(Enumerable.Range(0, sender.Length).Reverse().ToList()[index], true)))
                .ToList();

            var start = elementsList.FirstOrDefault(item => Equals(item.Name, startItem));
            var end = elementsList.FirstOrDefault(item => Equals(item.Name, endItem));

            return start is null || end is null ? null : sender[start.StartIndex..end.EndIndex];
        }

        public static List<TCollection> GetBetweenInclusive<TCollection>(List<TCollection> sender, TCollection startItem, TCollection endItem)
        {
            List<GenericElement<TCollection>> elementsList = sender.Select((element, index) =>
                    new GenericElement<TCollection>(element, new Index(index),
                        new Index(Enumerable.Range(0, sender.Count).Reverse().ToList()[index], true)))
                .ToList();

            var start = elementsList.FirstOrDefault(item => Equals(item.Name, startItem));
            var end = elementsList.FirstOrDefault(item => Equals(item.Name, endItem));

            return start is null || end is null ? null : sender.ToArray()[start.StartIndex..end.EndIndex].ToList();

        }

    }

}
