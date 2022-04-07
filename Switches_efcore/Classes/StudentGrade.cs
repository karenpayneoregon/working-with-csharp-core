using System;
using System.Linq.Expressions;

namespace SwitchExpressions_efcore.Classes
{
    public partial class StudentGrade
    {

        public static Expression<Func<Models.StudentGrade, StudentEntity>> Projection
        {
            get
            {
                return (student) => new StudentEntity()
                {
                    PersonID = student.StudentID,
                    CourseID = student.CourseID,
                    FirstName = student.Student.FirstName,
                    LastName = student.Student.LastName,
                    Grade = student.Grade
                };
            }
        }

    }
}