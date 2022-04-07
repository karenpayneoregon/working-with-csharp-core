using System;

namespace ApplicationDataLibrary.ExtensionMethods
{
    public static class Extension
    {
        /// <summary>
        /// Perform case insensitive equal on two strings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        /// <returns>true if both strings are a match, false if not a match</returns>
        public static bool AreEqual(this string sender, string item) 
            => string.Equals(sender, item, StringComparison.OrdinalIgnoreCase);
    }
}
