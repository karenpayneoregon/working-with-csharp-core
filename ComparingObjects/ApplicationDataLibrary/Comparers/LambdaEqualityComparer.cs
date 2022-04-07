using System;
using System.Collections.Generic;

namespace ApplicationDataLibrary.Comparers
{
    public class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> _equalsFunction;
        readonly Func<T, int> _hashCodeFunction;

        public LambdaEqualityComparer(Func<T, T, bool> equalsFunction, Func<T, int> hashCodeFunction)
        {
            _equalsFunction = equalsFunction ?? throw new ArgumentNullException();
            _hashCodeFunction = hashCodeFunction ?? throw new ArgumentNullException();
        }

        public bool Equals(T x, T y) => _equalsFunction(x, y);

        public int GetHashCode(T obj) => _hashCodeFunction(obj);
    }
}