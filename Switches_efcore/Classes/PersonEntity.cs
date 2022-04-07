using System.Collections.Generic;


namespace SwitchExpressions_efcore.Classes
{
    public class PersonEntity
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<Models.StudentGrade> Grades { get; set; }
    }
}