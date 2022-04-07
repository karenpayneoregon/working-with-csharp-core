using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet5Standard.LanguageExtensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Original for .NET Core 5
        /// </summary>
        public static List<List<T>> Chunk<T>(this List<T> source, int chunkSize)
            => source
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(item => item.Index / chunkSize)
                .Select(grp => grp.Select(item => item.Value).ToList())
                .ToList();

        /// <summary>
        /// Same signature as .NET Core 6
        /// </summary>
        public static List<List<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int size) => source
            .Select((value, index) => new { Index = index, Value = value })
            .GroupBy(item => item.Index / size)
            .Select(grp => grp.Select(item => item.Value).ToList())
            .ToList();
    }
}