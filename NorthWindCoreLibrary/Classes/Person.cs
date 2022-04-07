using System.Collections.Generic;

namespace NorthWindCoreLibrary.Classes
{
    public class Person
    {
        public Person(string firstName, string lastName, List<string> items)
        {
            FirstName = firstName;
            LastName = lastName;
            Items = items;
        }
        public string LastName { get; }
        public string FirstName { get; }
        public List<string> Items { get; }
    }
}