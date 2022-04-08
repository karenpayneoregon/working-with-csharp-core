using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.LanguageExtensions
{
    /// <summary>
    /// The purpose of this class came from
    /// - using C-Sharp-Cheatsheet.md from a git gist that has wrong information.
    /// - then from how extensions can be overused 
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Convert string to int with abandonment 
        /// </summary>
        public static int AsInt(this string sender) => Convert.ToInt32(sender);

        /// <summary>
        /// Convert string to decimal with abandonment 
        /// </summary>
        public static decimal AsDecimal(this string sender) => Convert.ToDecimal(sender);

        /// <summary>
        /// Convert string to DateTime with abandonment 
        /// </summary>
        public static DateTime AsDateTime(this string sender) => Convert.ToDateTime(sender);

        /// <summary>
        /// Someone is over thinking
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static (DateTime dateTime, bool valid) ToDateTimeSafe(this string sender)
        {
            return string.IsNullOrWhiteSpace(sender) ?
                (DateTime.Now, false) :
                DateTime.TryParse(sender, out var value) ?
                    (value, true) :
                    (DateTime.Now, false);
        }

        /// <summary>
        /// Is value of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">type to check</typeparam>
        /// <param name="sender">actual value</param>
        /// <returns>true if able to convert</returns>
        public static bool Is<T>(this string sender)
        {
            if (string.IsNullOrEmpty(sender)) return false;
            TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));

            if (!conv.CanConvertFrom(typeof(string))) return false;

            try
            {
                conv.ConvertFrom(sender);
                return true;
            }
            catch
            {
                // ignored - this is valid but don't use an empty catch in general
            }

            return false;
        }

        /// <summary>
        /// Same as above but this -> type.GetMethod("TryParse" makes it fragile 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsNotGood<T>(this string sender)
        {
            var type = typeof(T);
            var temp = default(T);

            var method = type.GetMethod("TryParse", new[] { typeof (string),Type.GetType($"{type.FullName}&") });

            return (bool)method.Invoke(null, new object[] { sender, temp });
        }


        /// <summary>
        /// Using double covers for instance int
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string sender) => 
            double.TryParse(sender, out _);


        /// <summary>
        /// Generic 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                // can be pattern matching which is harder to read for most developers
                if (converter is not null)
                {
                    /*
                     * using our Is extension from above.
                     * without the type check an exception is raised if not able to convert
                     */

                    if (input.Is<T>())
                    {
                        return (T)converter.ConvertFromString(input);
                    }
                    else
                    {
                        return default;
                    }
                    
                }

                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

    }
}
