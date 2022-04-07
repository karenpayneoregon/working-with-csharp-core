using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GlobbingProject.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Check if object is null or DBNull
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsNull(this object sender) => 
            sender == null || sender == DBNull.Value || Convert.IsDBNull(sender);

        /// <summary>
        /// Is the instance of a class null
        /// </summary>
        /// <typeparam name="T">Concrete class type</typeparam>
        /// <param name="senderInstance">Instance of concrete class</param>
        /// <returns>True if null, false if not null</returns>
        [DebuggerStepThrough]
        public static bool IsNull<T>(this T senderInstance) where T : new() => 
            senderInstance is null;

        /// <summary>
        /// Is the instance of a class not null
        /// </summary>
        /// <typeparam name="T">Concrete class type</typeparam>
        /// <param name="senderInstance">Instance of concrete class</param>
        /// <returns>True if not null, false if null</returns>
        [DebuggerStepThrough]
        public static bool IsNotNull<T>(this T senderInstance) where T : new() => 
            !senderInstance.IsNull();

        #region Reverse Func logic 

        [DebuggerStepThrough]
        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate) => 
            source.Where(element => !predicate(element));

        [DebuggerStepThrough]
        public static bool NotAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}
