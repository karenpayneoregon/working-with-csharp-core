using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindCoreLibrary.Classes.Helpers
{
    public class TypeHelper
    {
        /// <summary>
        /// Get property names for a instantiated class
        /// </summary>
        /// <param name="senderType"></param>
        /// <returns>Property names excluding two specified in where predicate</returns>
        public static List<string> PropertyNamesOrderedByName(object senderType)
        {
            if (senderType is null) return new List<string>();
            
            Type type = senderType.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();

            List<string> nameList = new List<string>();

            string[] excludeNames = {"Projection", "CountryIdentifier"};
            
            foreach (PropertyInfo prp in propertyInfo)
            {
                if (!excludeNames.Contains(prp.Name))
                {
                    nameList.Add(prp.Name);
                }
               
            }
            
            return nameList.OrderBy(name => name).ToList();

        }
    }
}
