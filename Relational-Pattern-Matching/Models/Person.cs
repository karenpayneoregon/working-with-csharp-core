using System;
using Relational_Pattern_Matching.Interfaces;

namespace Relational_Pattern_Matching.Models
{
    public class Person : IPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public override string ToString() => $"{Id} {FullName}";
    }
}
