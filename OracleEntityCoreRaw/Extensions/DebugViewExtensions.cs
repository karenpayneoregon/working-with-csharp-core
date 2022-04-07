using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OracleNorthWindLibrary.Classes;
using OracleNorthWindLibrary.Models;

namespace OracleNorthWindLibrary.Extensions
{
    /// <summary>
    /// Extension methods for assisting in both learning EF Core and debugging.
    /// </summary>
    /// <remarks>
    /// See also
    /// https://docs.microsoft.com/en-us/ef/core/change-tracking/debug-views
    /// </remarks>
    public static class DebugViewExtensions
    {

        /// <summary>
        /// Provides a slimmed down view for dates and primary key
        /// </summary>
        /// <param name="sender"><seealso cref="DebugView"/> enabled to track changes</param>
        /// <returns>original and current values</returns>
        public static string OrdersDatesOnlyView(this DebugView sender)
        {
            var longViewLines = sender.LongView
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder builder = new();

            foreach (var line in longViewLines)
            {
                if (line.Contains("OrderId", StringComparison.OrdinalIgnoreCase) || 
                    line.Contains("Date", StringComparison.OrdinalIgnoreCase))
                {
                    builder.AppendLine(line);
                }
            }

            return builder.ToString();

        }

        /// <summary>
        /// Write <see cref="OrdersDatesOnlyView"/> to file
        /// </summary>
        /// <param name="sender"><seealso cref="DebugView"/> enabled to track changes</param>
        /// <param name="fileName">path and file name to write to.
        /// Recommend writing to a folder that the developer has create permissions too.</param>
        /// <remarks>
        /// Void of error-handling as this is designed for developer mode,
        /// if used in test or prod then add exception handling for assertions
        /// on fail to write which most likely is lack of permissions to a folder.
        /// </remarks>
        public static void OrdersDatesOnlyViewToFile(this DebugView sender, string fileName)
        {
            File.WriteAllText(fileName, sender.OrdersDatesOnlyView());
        }

        /// <summary>
        /// Display only details we are interested in for a <see cref="Customer"/>
        /// </summary>
        /// <param name="sender"><seealso cref="DebugView"/> enabled to track changes</param>
        /// <returns></returns>
        public static string CustomerNameChangeView(this DebugView sender)
        {
            var longViewLines = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder builder = new();
            string[] tokens = { "CustomerId", "was", "CompanyName" };

            foreach (var line in longViewLines)
            {

                if ( line.Has(tokens))
                {
                    builder.AppendLine(line);
                }
                
            }

            return builder.ToString();

        }

        /// <summary>
        /// Used to focus LongView to specific parts of the resulting string
        /// </summary>
        /// <param name="sender"><seealso cref="DebugView"/> enabled to track changes</param>
        /// <param name="tokens">one or more strings to locate in the view</param>
        /// <returns>tailor LongView</returns>
        public static string CustomView(this DebugView sender, string[] tokens)
        {
            var longViewLines = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder builder = new();

            foreach (var line in longViewLines)
            {

                if (line.Has(tokens))
                {
                    builder.AppendLine(line);
                }

            }

            var test = longViewLines.Where(x => x.Has(tokens));


            return builder.ToString();
        }

        public static string CustomView(this DebugView sender, string[] tokens, int? lineCount)
        {
            var longViewLines = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder builder = new();
            if (lineCount.HasValue)
            {
                var result = longViewLines.Where(x => x.Has(tokens)).Take(lineCount.Value).ToArray();
                foreach (var line in result)
                {
                    builder.AppendLine(line.Contains("Unchanged", StringComparison.OrdinalIgnoreCase) ? 
                        "" : 
                        line.TrimStart());
                }
            }

            return builder.ToString();


        }

        public static string CustomViewByChunks(this DebugView sender, string[] tokens, int chunkSize)
        {
            var longViewLinesList = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .ToList();

            var chunks = longViewLinesList.ChunkBy(chunkSize);

            StringBuilder builder = new();

            foreach (var chunk in chunks)
            {
                foreach (var item in chunk)
                {
                    if (item.Has(tokens))
                    {
                        builder.AppendLine(item);
                    }
                }
            }
   
            return builder.ToString();
        }

        /// <summary>
        /// Direct custom view to a text file
        /// </summary>
        /// <param name="sender"><seealso cref="DebugView"/> enabled to track changes</param>
        /// <param name="tokens">one or more strings to locate in the view</param>
        /// <param name="fileName">path and file name to write to.
        /// Recommend writing to a folder that the developer has create permissions too.</param>
        /// <remarks>
        /// Void of error-handling as this is designed for developer mode,
        /// if used in test or prod then add exception handling for assertions
        /// on fail to write which most likely is lack of permissions to a folder.
        /// </remarks>
        public static void ToFile(this DebugView sender, string[] tokens,string fileName)
        {
            File.WriteAllText(fileName, sender.CustomView(tokens));
        }
    }
}
