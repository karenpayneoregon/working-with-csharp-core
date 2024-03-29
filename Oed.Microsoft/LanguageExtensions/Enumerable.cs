﻿using System;
using System.Collections.Generic;

/*
 * Karen Payne note
 * This code was taken from Microsoft's source code for .NET Core 6 framework to
 * make available in .NET Core 5 framework.
 *
 * The file name must be 
 *
 * See simple usage in code tag below
 */


// ReSharper disable once CheckNamespace
namespace System.Linq
{
    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    public static  class Enumerable
    {
        /// <summary>
        /// Split the elements of a sequence into chunks of size at most <paramref name="size"/>.
        /// </summary>
        /// <remarks>
        /// Every chunk except the last will be of size <paramref name="size"/>.
        /// The last chunk will contain the remaining elements and may be of a smaller size.
        ///
        ///
        /// ~~ This is native in .NET Core 6 ~~
        /// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Linq/src/System/Linq/Chunk.cs
        ///
        ///  
        /// </remarks>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}"/> whose elements to chunk.
        /// </param>
        /// <param name="size">
        /// Maximum size of each chunk.
        /// </param>
        /// <typeparam name="TSource">
        /// The type of the elements of source.
        /// </typeparam>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the elements the input sequence split into chunks of size <paramref name="size"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="size"/> is below 1.
        /// </exception>
        /// <code>
        /// string input = "This,is,an,example,for,the,C#,community";
        /// var output = input.Split(",").Chunk(2).Select(chunk => string.Join(" ", chunk));
        /// </code>
        public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            return ChunkIterator(source, size);
        }

        private static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int size)
        {
            using IEnumerator<TSource> e = source.GetEnumerator();
            while (e.MoveNext())
            {
                TSource[] chunk = new TSource[size];
                chunk[0] = e.Current;

                int index = 1;
                for (; index < chunk.Length && e.MoveNext(); index++)
                {
                    chunk[index] = e.Current;
                }

                if (index == chunk.Length)
                {
                    yield return chunk;
                }
                else
                {
                    Array.Resize(ref chunk, index);
                    yield return chunk;
                    yield break;
                }
            }
        }
    }
}