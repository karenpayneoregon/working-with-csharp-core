using System.Collections.Generic;
using System.Reflection;

namespace ApplicationDataLibrary.Comparers
{
    /// <summary>
    /// Comparer for generic type, using a property name as string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Generic comparer such as this is brittle when property name changes in a model and not changed passing to this comparer.
    /// </remarks>
    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private readonly PropertyInfo propertyToCompare;

        public PropertyComparer(string propertyName)
        {
            propertyToCompare = typeof(T).GetProperty(propertyName);
        }
        public bool Equals(T x, T y)
        {
            object xValue = propertyToCompare.GetValue(x, null);
            object yValue = propertyToCompare.GetValue(y, null);

            return xValue.Equals(yValue);

        }

        public int GetHashCode(T source)
        {
            object value = propertyToCompare.GetValue(source, null);
            return value.GetHashCode();
        }
    }
}