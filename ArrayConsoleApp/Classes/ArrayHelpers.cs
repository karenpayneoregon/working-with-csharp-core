using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayConsoleApp.Classes
{
    public static class ArrayHelpers
    {
        /// <summary>
        /// Concatenate arrays
        /// </summary>
        /// <typeparam name="T">type e.g. int, string etc</typeparam>
        /// <param name="sender">data</param>
        /// <returns>an array of T</returns>
        public static T[] ConcatArrays<T>(params T[][] sender)
        {
            var destinationIndex = 0;
            var destinationArray = new T[sender.Sum(array => array.Length)];

            foreach (var sourceArray in sender)
            {
                Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
                destinationIndex += sourceArray.Length;
            }

            return destinationArray;
        }

        /// <summary>
        /// Generic extension to combine two or more list of the same type
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="lists">List to combine</param>
        /// <returns></returns>
        public static List<T> ConcatList<T>(params List<T>[] lists) =>
            lists.Aggregate(new List<T>(), (original, listToConcatenate) =>
                original.Concat(listToConcatenate).ToList());

        public static void Push<T>(ref T[] sequence, object item)
        {
            Array.Resize(ref sequence, sequence.Length + 1); // Resizing the array for the cloned length (+-) (+1)
            sequence.SetValue(item, sequence.Length - 1); // Setting the value for the new element
        }
        public static IEnumerable<T> Add<T>(this IEnumerable<T> sequence, T item)
        {
            return (sequence ?? Enumerable.Empty<T>()).Concat(new[] { item });
        }

        public static T[] AddRangeToArray<T>(this T[] sequence, T[] items)
        {
            return (sequence ?? Enumerable.Empty<T>()).Concat(items).ToArray();
        }

        public static T[] AddToArray<T>(this T[] sequence, T item)
        {
            return Add(sequence, item).ToArray();
        }

    }
}

