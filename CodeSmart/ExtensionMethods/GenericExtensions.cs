using System;
using System.Collections.Generic;

namespace CodeSmart.ExtensionMethods
{

    /// <summary>
    /// For teaching only
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Example of a generic extension method which takes a predicate to evaluate, in this case is a null Identifier 
        /// </summary>
        /// <typeparam name="T">Type of list to return</typeparam>
        /// <param name="source">List to evaluate</param>
        /// <param name="predicate">Code to evaluate against source</param>
        /// <returns>bool</returns>
        public static bool HasNull<T>(this List<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The sequence is null and contains no elements.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "The predicate function is null and cannot be executed.");
            }


            var result = new List<T>();
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    result.Add(item);
                }
            }

            return result.Count < source.Count;
        }
        /// <summary>
        /// Example to pair with <see cref="HasNull{T}"/> to get indices of null identifier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static (bool hasNulls, List<int> list) NullIndices<T>(this List<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The sequence is null and contains no elements.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "The predicate function is null and cannot be executed.");
            }

            List<int> indices = new();
            for (int index = 0; index < source.Count; index++)
            {
                if (predicate(source[index]))
                {
                    indices.Add(index);
                }
            }

            return (indices.Count >0, indices);

        }


    }
}