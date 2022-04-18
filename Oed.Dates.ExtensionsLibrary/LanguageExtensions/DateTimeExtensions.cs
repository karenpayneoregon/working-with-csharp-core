using System;
using System.Diagnostics;
using System.Globalization;

namespace Oed.ExtensionsLibrary.LanguageExtensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns passed datetime with zero padding using current culture separators
        /// </summary>
        /// <param name="sender"><seealso cref="DateTime"/></param>
        /// <returns>month zero padded/day zero padded/year zero padded</returns>
        /// <remarks>
        /// order of date parts year, month, day which can be changed to say month, day, year
        /// </remarks>
        [DebuggerStepThrough]
        public static string ZeroPad(this DateTime sender)
        {
            string dateSeparator = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;
            string timeSeparator = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator;

            return $"{sender.Year:D2}{dateSeparator}{sender.Month:D2}{dateSeparator}{sender.Day:D2} {sender.Hour:D2}{timeSeparator}{sender.Minute:D2}{timeSeparator}{sender.Second:D2}";
        }

        [DebuggerStepThrough]
        public static DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int seconds, int milliSeconds) 
            => new(year, month, day, hour, minute, seconds, milliSeconds);

        [DebuggerStepThrough]
        public static DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int seconds) 
            => new(year, month, day, hour, minute, seconds, 0);

        [DebuggerStepThrough]
        public static DateTime CreateDateTime(int year, int month, int day, int hour, int minute) 
            => new(year, month, day, hour, minute, 0, 0);

        [DebuggerStepThrough]
        public static DateTime CreateDateTime(int year, int month, int day, int hour) 
            => new(year, month, day, hour, 0, 0, 0);

        [DebuggerStepThrough]
        public static DateTime CreateDateTime(int year, int month, int day) =>
            new(year, month, day, 0, 0, 0, 0);
    }
}
