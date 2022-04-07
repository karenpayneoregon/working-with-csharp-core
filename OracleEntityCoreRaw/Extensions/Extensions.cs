using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OracleNorthWindLibrary.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToYesNo(this bool value) => value ? "Yes" : "No";

        /// <summary>
        /// Determine if any token exists in a string
        /// </summary>
        /// <param name="sender">string to check</param>
        /// <param name="items">tokens to check if in sender</param>
        /// <returns>true/false</returns>
        public static bool Has(this string sender, string[] items)
        {
            foreach (var item in items)
            {
                if (sender.Contains(item, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// Get non-null values in sequence
        /// </summary>
        /// <typeparam name="T">sequence type</typeparam>
        /// <param name="sequence">elements in container</param>
        /// <returns>non null values</returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> sequence) 
            => sequence.Where(item => item is not null);

        /// <summary>
        /// Get non-null values in sequence with a constraint of struct
        /// </summary>
        /// <typeparam name="T">sequence type</typeparam>
        /// <param name="sequence">elements in container</param>
        /// <returns>non null values</returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> sequence) where T : struct 
            => sequence.Where(item => item is not null).Select(e => e.Value);

        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        }
    }
}
