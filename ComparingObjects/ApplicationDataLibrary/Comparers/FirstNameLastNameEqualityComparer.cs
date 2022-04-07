using System;
using System.Collections.Generic;

namespace ApplicationDataLibrary.Comparers
{
    public class FirstNameLastNameEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person person1, Person person)
        {
            if (ReferenceEquals(person1, person)) return true;
            if (ReferenceEquals(person1, null)) return false;
            if (ReferenceEquals(person, null)) return false;
            if (person1.GetType() != person.GetType()) return false;
            
            return person1.FirstName == person.FirstName && person1.LastName == person.LastName;
        }

        public int GetHashCode(Person person) => HashCode.Combine(person.FirstName, person.LastName);
    }
}
