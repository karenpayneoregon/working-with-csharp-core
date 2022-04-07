using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace SomeLibrary
{
    static class Extensions
    {
        /// <summary>
        /// Replace multiple comma with single comma
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string TruncateCommas(this string input) => Regex.Replace(input, @",+", ",");

        /// <summary>
        /// Remove all whitespace anyplace in the string
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string RemoveAllWhiteSpace(this string sender)
            => sender
                .ToCharArray().Where(character => !char.IsWhiteSpace(character))
                .Select(c => c.ToString()).Aggregate((value1, value2) => value1 + value2);
    }
}