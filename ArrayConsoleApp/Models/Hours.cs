using System;
using System.Collections.Generic;
using System.Linq;
using ArrayConsoleApp.Classes;

namespace ArrayConsoleApp.Models
{
    /// <summary>
    /// Used to create a string array which represent time in a day.
    /// </summary>
    public class Hours
    {
        /// <summary>
        /// Creates an array quarter hours
        /// </summary>
        public static string[] Quarterly => Range(TimeIncrement.Quarterly);
        /// <summary>
        /// Creates an array of hours
        /// </summary>
        public static string[] Hourly => Range();
        /// <summary>
        /// Creates an array of half-hours
        /// </summary>
        public static string[] HalfHour => Range(TimeIncrement.HalfHour);

        public static string TimeFormat { get; set; } = "hh:mm tt";

        /// <summary>
        /// Create a list of time as string and TimeSpan
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static List<HourItem> HourItems(string[] range)
        {
            List<HourItem> list = new();

            foreach (var item in range)
            {
                list.Add(new HourItem() { Text = item, TimeSpan = DateTime.Parse(item).TimeOfDay });
            }

            return list;
        }

        /// <summary>
        /// Create a string array of hours
        /// </summary>
        /// <param name="pTimeIncrement"><seealso cref="TimeIncrement"/></param>
        /// <returns></returns>
        public static string[] Range(TimeIncrement pTimeIncrement = TimeIncrement.Hourly)
        {

            IEnumerable<DateTime> hours = Enumerable.Range(0, 24)
                .Select((index) => (DateTime.MinValue.AddHours(index)));

            var timeList = new List<string>();

            foreach (var dateTime in hours)
            {

                timeList.Add(dateTime.ToString(TimeFormat));

                if (pTimeIncrement == TimeIncrement.Quarterly)
                {
                    timeList.Add(dateTime.AddMinutes(15).ToString(TimeFormat));
                    timeList.Add(dateTime.AddMinutes(30).ToString(TimeFormat));
                    timeList.Add(dateTime.AddMinutes(45).ToString(TimeFormat));
                }
                else if (pTimeIncrement == TimeIncrement.HalfHour)
                {
                    timeList.Add(dateTime.AddMinutes(30).ToString(TimeFormat));
                }
            }

            return timeList.ToArray();

        }
    }
}