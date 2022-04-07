using System;
using System.Collections.Generic;

namespace ApplicationDataLibrary.Comparers
{
    /// <summary>
    /// TODO documentation needed for Equality<T>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Equality<T>
    {
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return CreateComparer(keySelector, null);
        }

        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new KeyEqualityComparer<V>(keySelector, comparer);
        }

        class KeyEqualityComparer<V> : IEqualityComparer<T>
        {
            readonly Func<T, V> keySelector;
            readonly IEqualityComparer<V> comparer;

            public KeyEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                // REVIEW null assertion
                if (keySelector is null)
                {
                    throw new ArgumentNullException("keySelector");
                }

                this.keySelector = keySelector;
                this.comparer = comparer ?? EqualityComparer<V>.Default;
            }

            public bool Equals(T x, T y) => comparer.Equals(keySelector(x), keySelector(y));

            public int GetHashCode(T obj) => comparer.GetHashCode(keySelector(obj));
        }
    }
}
