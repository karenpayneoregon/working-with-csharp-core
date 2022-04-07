﻿using System.Collections.Generic;
using System.Linq;

namespace OracleNorthWindLibrary.Classes
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
            => source
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(item => item.Index / chunkSize)
                .Select(grp => grp.Select(item => item.Value).ToList())
                .ToList();
    }
}