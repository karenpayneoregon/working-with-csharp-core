using System;
using System.Diagnostics;
using System.Globalization;
using static System.DateTime;

namespace Oed.ExtensionsLibrary.LanguageExtensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Format a TimeSpan with AM PM
        /// </summary>
        /// <param name="sender">TimeSpan to format</param>
        /// <param name="format">Optional format</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Formatted(this TimeSpan sender, string format = "hh:mm tt") 
            => DateTime.Today.Add(sender).ToString(format);

        /// <summary>
        /// Conditionally format TimeSpan dependent on if there are days, hours, minutes.
        /// Does not handle years and milliseconds
        /// </summary>
        /// <param name="span"><see cref="TimeSpan"/> from two dates</param>
        /// <returns>Formatted string</returns>
        [DebuggerStepThrough]
        public static string FormatElapsed(this TimeSpan span) => span.Days switch
        {
            > 0 => $"{span.Days} days, {span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds",
            _ => span.Hours switch
            {
                > 0 => $"{span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds",
                _ => span.Minutes switch
                {
                    > 0 => $"{span.Minutes} minutes, {span.Seconds} seconds",
                    _ => span.Seconds switch
                    {
                        > 0 => $"{span.Seconds} seconds",
                        _ => ""
                    }
                }
            }
        };

        /// <summary>
        /// Is end time prior to start time
        /// </summary>
        /// <param name="endTimeSpan"></param>
        /// <param name="startTimeSpan"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsValidEndTime(this TimeSpan endTimeSpan, TimeSpan startTimeSpan)
            => endTimeSpan.Hours < startTimeSpan.Hours;

        /// <summary>
        /// Is start time after end time
        /// </summary>
        /// <param name="startTimeSpan"></param>
        /// <param name="endTimeSpan"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsValidStartTime(this TimeSpan startTimeSpan, TimeSpan endTimeSpan)
            => endTimeSpan.Hours > startTimeSpan.Hours;

        [DebuggerStepThrough]
        public static TimeSpan CreateTime(int hour, int minute, int second) =>
            new(hour, minute, second);

        /// <summary>
        /// Set seconds for <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="sender">valid TimeSpan</param>
        /// <param name="seconds">0-59</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static TimeSpan SetSeconds(this TimeSpan sender, int seconds)
            => new(sender.Days, sender.Hours, sender.Minutes, seconds);

        /// <summary>
        /// Set minutes for <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="sender">valid TimeSpan</param>
        /// <param name="minutes">0-60</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static TimeSpan SetMinutes(this TimeSpan sender, int minutes)
            => new(sender.Days, sender.Hours, minutes, sender.Seconds);

        /// <summary>
        /// Set hours for <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="sender">valid TimeSpan</param>
        /// <param name="hours">0-24</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static TimeSpan SetHours(this TimeSpan sender, int hours)
            => new(sender.Days, hours, sender.Minutes, sender.Seconds);

        /// <summary>
        /// Remove milliseconds from date time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime RemoveMilliseconds(this DateTime dateTime) =>
            ParseExact(dateTime.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null);

        /// <summary>
        /// Remove milliseconds and seconds from date time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime RemoveSeconds(this DateTime dateTime) 
            => ParseExact(dateTime.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", null);

        /// <summary>
        /// Remove milliseconds and seconds from date time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime RemoveMillisecondsAndSeconds(this DateTime dateTime) 
            => ParseExact(dateTime.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", null);

        /// <summary>
        /// Convert TimeSpan into DateTime
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        /// <remarks>
        /// Intended to be used when the date part does not matter
        /// </remarks>
        [DebuggerStepThrough]
        public static DateTime ToDateTime(this TimeSpan sender) 
            => ParseExact(
                sender.Formatted("hh:mm"), "H:mm", null, DateTimeStyles.None);

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minute set to given values.</returns>
        [DebuggerStepThrough]
        public static DateTime At(this DateTime current, int hour, int minute) 
            => current.SetTime(hour, minute);

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes and seconds set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <param name="second">The second to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minutes and seconds set to given values.</returns>
        [DebuggerStepThrough]
        public static DateTime At(this DateTime current, int hour, int minute, int second) 
            => current.SetTime(hour, minute, second);

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes and seconds and milliseconds set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <param name="second">The second to set time to.</param>
        /// <param name="milliseconds">The milliseconds to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minutes and seconds set to given values.</returns>
        [DebuggerStepThrough]
        public static DateTime At(this DateTime current, int hour, int minute, int second, int milliseconds) 
            => current.SetTime(hour, minute, second, milliseconds);

        /// <summary>
        /// Returns the original <see cref="DateTime"/> with Hour part changed to supplied hour parameter.
        /// </summary>
        [DebuggerStepThrough]
        public static DateTime SetTime(this DateTime originalDate, int hour) 
            => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, originalDate.Minute, originalDate.Second, originalDate.Millisecond, originalDate.Kind);

        /// <summary>
        /// Returns the original <see cref="DateTime"/> with Hour and Minute parts changed to supplied hour and minute parameters.
        /// </summary>
        [DebuggerStepThrough]
        public static DateTime SetTime(this DateTime originalDate, int hour, int minute) 
            => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, originalDate.Second, originalDate.Millisecond, originalDate.Kind);

        /// <summary>
        /// Returns the original <see cref="DateTime"/> with Hour, Minute and Second parts changed to supplied hour, minute and second parameters.
        /// </summary>
        [DebuggerStepThrough]
        public static DateTime SetTime(this DateTime originalDate, int hour, int minute, int second) 
            => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, second, originalDate.Millisecond, originalDate.Kind);

        /// <summary>
        /// Returns the original <see cref="DateTime"/> with Hour, Minute, Second and Millisecond parts changed to supplied hour, minute, second and millisecond parameters.
        /// </summary>
        [DebuggerStepThrough]
        public static DateTime SetTime(this DateTime originalDate, int hour, int minute, int second, int millisecond) 
            => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, second, millisecond, originalDate.Kind);

    }
}