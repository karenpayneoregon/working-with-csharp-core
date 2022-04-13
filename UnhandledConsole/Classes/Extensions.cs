using System;
using DirectoryHelpersLibrary.Classes;

namespace UnhandledConsole.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// Strip path to source file
        /// </summary>
        /// <param name="sender">path and file name some place in this string</param>
        /// <returns></returns>
        public static string StripSolutionFolder(this string sender) 
            => sender.Replace(
                DirectoryHelper.SolutionFolder(), 
                "", 
                StringComparison.Ordinal).TrimStart('\\');

        /// <summary>
        /// Remove first occurrence of 'remove' (not a replace)
        /// </summary>
        /// <param name="sender">string to use</param>
        /// <param name="remove">what to remove</param>
        /// <returns></returns>
        public static string RemoveFirst(this string sender, string remove)
        {
            int index = sender.IndexOf(remove, StringComparison.Ordinal);
            return (index < 0)
                ? sender
                : sender.Remove(index, remove.Length);
        }
    }
}
