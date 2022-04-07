using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsLibrary.LanguageExtensions
{
    /// <summary>
    /// Common string extensions 
    /// </summary>
    public static class Extensions
    {
        [DebuggerStepThrough]
        public static string ToYesNoString(this bool value) => (value ? "Yes" : "No");

    }
}
