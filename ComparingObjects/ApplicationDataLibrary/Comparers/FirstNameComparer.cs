using System.Collections.Generic;

namespace ApplicationDataLibrary.Comparers
{
    public class FirstNameComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person person1, Person person)
        {
            if (ReferenceEquals(person1, person)) return true;
            if (ReferenceEquals(person1, null)) return false;
            if (ReferenceEquals(person, null)) return false;
            if (person1.GetType() != person.GetType()) return false;
            return person1.FirstName == person.FirstName;
        }

        public int GetHashCode(Person person) => (person.FirstName != null ? person.FirstName.GetHashCode() : 0);
    }
}