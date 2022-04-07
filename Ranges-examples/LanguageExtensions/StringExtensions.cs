namespace Ranges_examples.LanguageExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get range
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="startIndex">Start of range</param>
        /// <param name="endIndex">End of range</param>
        /// <returns>sub-set of string</returns>
        public static string SubstringByIndexes(this string value, int startIndex, int endIndex) 
            => value[startIndex..(endIndex + 1)];
    }
}
