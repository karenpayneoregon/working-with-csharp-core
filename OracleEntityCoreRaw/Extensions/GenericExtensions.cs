using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleNorthWindLibrary.Extensions
{
    public static class GenericExtensions
    {
        static async Task<IEnumerable<TSource>> WhereAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
        {
            var results = new ConcurrentQueue<TSource>();
            var tasks = source.Select(
                async item =>
                {
                    if (await predicate(item))
                    {
                        results.Enqueue(item);
                    }
                });

            await Task.WhenAll(tasks);

            return results;
        }
    }
}
