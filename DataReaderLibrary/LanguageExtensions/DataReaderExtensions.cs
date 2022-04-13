using System;
using System.Data;

namespace DataReaderLibrary.LanguageExtensions
{
    /// <summary>
    /// Language extensions to deal with NULL values
    ///
    /// - Note the duplicate extensions, if we dealt only with one data provider, target it else use <seealso cref="IDataReader"/>
    ///   which a specific data provider inherits this Interface so we are better off with using <seealso cref="IDataReader"/> always
    ///         We can not have both as Visual Studio will default to the provider rather than the generic version
    ///
    /// - Then there is the case of dealing with null values, what is the default and is null permitted???
    ///
    /// 
    /// </summary>
    public static class DataReaderExtensions
    {

        /// <summary>
        /// Get string with null check. In this case if a null value is returned an empty string is returned
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnIndex">Column ordinal index for column to return data</param>
        /// <returns></returns>
        public static string SafeGetString(this IDataReader reader, int columnIndex) 
            => !reader.IsDBNull(columnIndex) ? reader.GetString(columnIndex) : "";

        /// <summary>
        /// Get int with null check. If there is a null value the default value for an int is returned
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnIndex">Column ordinal index for column to return data</param>
        /// <returns></returns>
        public static int? SafeGetInt(this IDataReader reader, int columnIndex) 
            => !reader.IsDBNull(columnIndex) ? 
                reader.GetInt32(columnIndex) : 
                null;

    }

    /// <summary>
    /// Not used at all, here to show another way to assert <see cref="DBNull"/>
    /// </summary>
    public static class Dummy
    {
        public static bool IsNull(this object sender) 
            => sender == null || sender == DBNull.Value || Convert.IsDBNull(sender) == true;
    }
}