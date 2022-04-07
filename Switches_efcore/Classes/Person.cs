using System;
using System.Linq.Expressions;

namespace SwitchExpressions_efcore.Classes
{
    public partial class Person
    {

        public static Expression<Func<Models.Person, PersonEntity>> Projection
        {
            get
            {
                return (student) => new PersonEntity()
                {
                    PersonID = student.PersonID,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Grades = student.StudentGrade
                };
            }
        }
    }
}
