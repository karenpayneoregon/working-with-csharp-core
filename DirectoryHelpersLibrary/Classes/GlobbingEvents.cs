using DirectoryHelpersLibrary.Models;

namespace DirectoryHelpersLibrary.Classes
{

    public partial class GlobbingOperations
    {
        public delegate void OnTraverseFileMatch(FileMatchItem sender);
        /// <summary>
        /// Informs listener of a <see cref="FileMatchItem"/>
        /// </summary>
        public static event OnTraverseFileMatch TraverseFileMatch;

        public delegate void OnDone(string message);
        /// <summary>
        /// Indicates processing has completed
        /// </summary>
        public static event OnDone Done;

    }
}
