using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Oed.ExtensionsLibrary.Classes;

namespace Oed.ExtensionsLibrary.LanguageExtensions
{
    /// <summary>
    /// Common string extensions 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Provides strong type return for use in a Lambda statement to split a string
        /// into chars along with their respected indices.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>List&lt;CharIndexed&gt;</returns>
        [DebuggerStepThrough]
        public static List<CharIndexed> Indexed(this string sender) 
            => sender.Select((@char, index) =>
                new CharIndexed { Char = @char, Index = index }).ToList();

        /// <summary>
        /// Used to remove last character from a string
        /// </summary>
        /// <param name="sender">string with at least one char</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string TrimLastCharacter(this string sender)
            => string.IsNullOrWhiteSpace(sender) ? sender : sender.TrimEnd(sender[^1]);

        /// <summary>
        /// Remove all white space in a string, at start, end and in-between
        /// </summary>
        /// <param name="sender">string to work on</param>
        [DebuggerStepThrough]
        public static string RemoveAllWhiteSpace(this string sender)
            => sender
                .ToCharArray().Where(character => !char.IsWhiteSpace(character))
                .Select(c => c.ToString()).Aggregate((value1, value2) => value1 + value2);

        /// <summary>
        /// Replace multiple comma with one comma
        /// </summary>
        /// <param name="sender">string to work on</param>
        [DebuggerStepThrough]
        public static string TruncateCommas(this string sender) => Regex.Replace(sender, @",+", ",");
        /// <summary>
        /// Split string by upper cased chars e.g. KarenAnnePayne becomes Karen Anne Payne
        /// </summary>
        /// <param name="sender">string to work on</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string SplitCamelCase(this string sender)
            => Regex.Replace(Regex.Replace(sender,
                    "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"),
                "(\\p{Ll})(\\P{Ll})", "$1 $2");

        /// <summary>
        /// Determine if string is empty
        /// </summary>
        /// <param name="sender">String to test if null or whitespace</param>
        /// <returns>true if empty or false if not empty</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrWhiteSpace(this string sender) => string.IsNullOrWhiteSpace(sender);
        /// <summary>
        /// Determine if a string can be represented as a numeric.
        /// </summary>
        /// <param name="text">value to determine if can be converted to a string</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsNumeric(this string text) => double.TryParse(text, out _);

        /// <summary>
        /// Determine if any token exists in a string
        /// </summary>
        /// <param name="sender">string to check</param>
        /// <param name="items">tokens to check if in sender</param>
        /// <returns>true/false</returns>
        public static bool Has(this string sender, string[] items)
        {
            foreach (var item in items)
            {
                if (sender.Contains(item, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;

        }
    }
}
