using System;

namespace Ranges_examples.Models
{
    public class ElementItem
    {
        /// <summary>
        /// Value
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Start index
        /// </summary>
        public Index StartIndex { get; }
        /// <summary>
        /// End index ^ hat
        /// </summary>
        public Index EndIndex { get; }

        public ElementItem(string name, Index startIndex, Index endIndex)
        {
            Name = name;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public override string ToString() => $"{{ Name = {Name}, StartIndex = {StartIndex}, EndIndex = {EndIndex} }}";
    }
}