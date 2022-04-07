using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DirectoryHelpersLibrary.Models;
using Microsoft.Extensions.FileSystemGlobbing;


namespace DirectoryHelpersLibrary.Classes
{
    /// <summary>
    /// Provides a method to find files given a root folder and
    /// glob patterns for what type of files to find and what type
    /// of files to exclude.
    ///
    /// Globbing 
    /// - is not widely known to most C# developers.
    /// - is not easy to figure out for many C# developers
    /// - alternates for most developers is Directory.EnumerateFiles
    ///
    /// Comprehensive examples
    /// https://github.com/karenpayneoregon/enumeration-globbing-folders-files
    /// </summary>
    /// <remarks>
    /// This class is done as a partial class to show how a partial
    /// class may be done to, in this case separate events from methods.
    /// </remarks>
    public partial class GlobbingOperations
    {

        /// <summary>
        /// Pass back an object which can represent path and file name
        /// </summary>
        /// <param name="parentFolder">folder to start in</param>
        /// <param name="patterns">search include pattern</param>
        /// <param name="excludePatterns">pattern to exclude</param>
        public static async Task GetFiles(string parentFolder, string[] patterns, string[] excludePatterns)
        {

            List<FileMatchItem> list = new();

            Matcher matcher = new();
            matcher.AddIncludePatterns(patterns);
            matcher.AddExcludePatterns(excludePatterns);

            await Task.Run( () =>
            {
                
                foreach (string file in matcher.GetResultsInFullPath(parentFolder))
                {
                    TraverseFileMatch?.Invoke(new FileMatchItem(file));
                }
            });

            Done?.Invoke("Finished");

        }

    }
}
