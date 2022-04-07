using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterrogatingConsoleApp.LanguageExtensions
{
    static class Extensions
    {
        public static bool Implements<I>(this Type source) where I : class
            => typeof(I).IsAssignableFrom(source);
    }
}

