using System;
using System.Diagnostics;
using Oed.ExtensionsLibrary.Classes;

namespace Oed.ExtensionsLibrary.LanguageExtensions
{
    public static class BoolExtensions
    {

        /// <summary>
        /// convert bool to yes/no
        /// </summary>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToYesNoStringIs(this bool value, LanguageCode code) =>
            code is LanguageCode.English ? value ? "Yes" : "No" :
            code is LanguageCode.Spanish ? value ? "sí" : "No" :
            code is LanguageCode.Russian ? value ? "da" : "Net" :
            code is LanguageCode.Vietnamese ? value ? "Đúng" : "Không" :
            throw new ArgumentOutOfRangeException("Unknown language code");


    }
}