using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranges_examples.LanguageExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Go outside of a range and a runtime exception will be thrown,
    /// no different from indexing an array and get out of bounds exception
    /// </remarks>
    public static class GenericExtensions
    {
        /// <summary>
        /// Get range from <see cref="Array"/>
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="value">The array</param>
        /// <param name="startIndex">Start of range</param>
        /// <param name="endIndex">End of range</param>
        /// <returns>sub-set of array</returns>
        public static T[] ByIndex<T>(this T[] value, int startIndex, int endIndex) 
            => value[startIndex..(endIndex + 1)];

        /// <summary>
        /// Get range from <see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="value">The list</param>
        /// <param name="startIndex">Start of range</param>
        /// <param name="endIndex">End of range</param>
        /// <returns>sub-set of <see cref="List{T}"/></returns>
        public static List<T> ByIndex<T>(this List<T> value, int startIndex, int endIndex) 
            => value.GetRange(startIndex, endIndex);

        /// <summary>
        /// Provides the ability to obtain a range for a list (back several versions back from the current Framework)
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="list">Actual list</param>
        /// <param name="range"><see cref="Range"/></param>
        /// <returns></returns>
        /// <remarks>
        /// ranges are not supported for list which this method fills this gap (now they are)
        /// </remarks>
        public static List<T> GetRange<T>(this List<T> list, Range range)
        {
            var (start, length) = range.GetOffsetAndLength(list.Count);
            return list.GetRange(start, length);
        }
    }
}
