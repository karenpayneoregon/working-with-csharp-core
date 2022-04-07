using System;
using ApplicationDataLibrary.ExtensionMethods;


namespace ApplicationDataLibrary
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public int CarId { get; set; }
        public override string ToString() => $"{FirstName} {LastName}";
    }

}

