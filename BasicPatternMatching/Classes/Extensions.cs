using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace BasicPatternMatching.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// Convert a string value to an enum member with default value
        /// </summary>
        /// <typeparam name="TEnum">Enum to base conversion too</typeparam>
        /// <param name="enumValue">Valid enum member for TEnum</param>
        /// <param name="defaultValue">Default member value if conversion can not be performed</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string enumValue, TEnum defaultValue) =>
            !Enum.IsDefined(typeof(TEnum), enumValue) ? defaultValue : (TEnum)Enum.Parse(typeof(TEnum), enumValue);

        public static bool Is<T>(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var conv = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));

            if (!conv.CanConvertFrom(typeof(string))) return false;

            try
            {
                conv.ConvertFrom(value);
                return true;
            }
            catch
            {
                // ignored
            }
            return false;
        }

        public static DateTime ToDateTime(this string sender)
        {
            return string.IsNullOrWhiteSpace(sender) ? throw new Exception("Cannot convert an empty string to DateTime") :
                DateTime.TryParse(sender, out var value) ? value :
                throw new Exception($"Cannot convert [{sender}] to DateTime");
        }

        public static (DateTime dateTime, bool valid) ToDateTimeSafe(this string sender)
        {
            return string.IsNullOrWhiteSpace(sender) ?
                (DateTime.Now, false) :
                DateTime.TryParse(sender, out var value) ?
                    (value, true) :
                    (DateTime.Now, false);
        }

        public static ImmutableList<string> PossibleTimeZones(this DateTimeOffset offsetTime)
        {
            List<string> list = new();
            TimeSpan offset = offsetTime.Offset;

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            list.AddRange(from TimeZoneInfo timeZone in timeZones
                where timeZone.GetUtcOffset(offsetTime.DateTime).Equals(offset)
                select timeZone.DaylightName);

            return list.ToImmutableList();
        }
    }
}