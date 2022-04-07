using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDataLibrary.ExtensionMethods;

// ReSharper disable once CheckNamespace
namespace ApplicationDataLibrary
{
    public partial class Person : IEquatable<Person>
    {
        public override bool Equals(object sender)
        {
            if (sender == null)
            {
                return false;
            }

            if (!(sender is Person person))
            {
                return false;
            }

            return Id == person.Id &&
                   string.Equals(FirstName, person.FirstName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(LastName, person.LastName, StringComparison.OrdinalIgnoreCase);

        }

        public bool Equals(Person person)
        {
            if (person == null)
            {
                return false;
            }

            return Id == person.Id && FirstName.AreEqual(person.FirstName) && LastName.AreEqual(person.LastName);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode();
        }

        public static bool operator ==(Person person1, Person person2)
        {
            if (ReferenceEquals(person1, person2))
            {
                return true;
            }

            if ((object)person1 == null || (object)person2 == null)
            {
                return false;
            }


            return person1.Id == person2.Id &&
                   person1.FirstName.AreEqual(person2.FirstName) &&
                   person1.LastName.AreEqual(person2.LastName);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return !(person1 == person2);
        }
    }
}
