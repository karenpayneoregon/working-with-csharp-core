using System.Collections.Generic;

namespace NorthWindCoreLibrary.Classes
{
    public class FromDatabaseMock
    {
        public static IReadOnlyList<Person> People()
        {
            return  new List<Person>()
            {
                new ("Karen", "Payne", new List<string>()), 
                new("Mary", "Jones", new List<string>())
            };
        }
    }
}