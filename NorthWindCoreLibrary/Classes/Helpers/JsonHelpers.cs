using System.Collections.Generic;
using Newtonsoft.Json;

namespace NorthWindCoreLibrary.Classes.Helpers
{
    public class JsonHelpers
    {
        /// <summary>
        /// Serialize a list of T (model) with formatting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static string Serialize<T>(List<T> sender) => 
            JsonConvert.SerializeObject(sender, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
}
