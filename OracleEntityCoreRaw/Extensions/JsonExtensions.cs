using System.Collections.Generic;
using Newtonsoft.Json;

namespace OracleNorthWindLibrary.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this List<T> source)
        {
            return JsonConvert.SerializeObject(source, Formatting.Indented);
        }
    }
}
