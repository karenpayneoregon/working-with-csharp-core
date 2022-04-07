using System;

namespace Ranges_examples.Classes
{
    public class GenericElement<T>
    {
        public T Name { get; }
        public Index StartIndex { get; }
        public Index EndIndex { get; }

        public GenericElement(T name, Index startIndex, Index endIndex)
        {
            Name = name;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public override string ToString()
        {
            return $"{{ Name = {Name}, StartIndex = {StartIndex}, EndIndex = {EndIndex} }}";
        }
    }
}