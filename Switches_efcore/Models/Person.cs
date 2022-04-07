using System;
using System.Collections.Generic;

#nullable disable

namespace SwitchExpressions_efcore.Models
{
    public partial class Person
    {
        public Person()
        {
            StudentGrade = new HashSet<SwitchExpressions_efcore.Models.StudentGrade>();
        }

        public int PersonID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; }

        public virtual ICollection<SwitchExpressions_efcore.Models.StudentGrade> StudentGrade { get; set; }
    }
}