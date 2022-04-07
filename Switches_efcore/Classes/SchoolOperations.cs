using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwitchExpressions_efcore.Data;


namespace SwitchExpressions_efcore.Classes
{
#nullable enable
    public class SchoolOperations
    {
        public delegate void OnIteratePersonGrades(PersonGrades personData);
        public static event OnIteratePersonGrades? OnIteratePersonGradesEvent;

        /// <summary>
        /// Get students in a course by course identifier
        /// </summary>
        /// <param name="courseIdentifier"></param>
        public static async Task<List<StudentEntity>> GetGradesForPeople(int courseIdentifier = 2021)
        {

            await  using var context = new SchoolContext();
            
            List<StudentEntity> studentEntities = context
                .StudentGrade
                .Include(studentEntity => studentEntity.Student)
                .Select(StudentGrade.Projection)
                .Where(studentEntity => studentEntity.CourseID == courseIdentifier)
                .ToList();

            foreach (StudentEntity entity in studentEntities)
            {
                var letterGrade = entity.Grade!.Value switch
                {
                    >= 1.00m and <= 2.00m => "F",
                    2.50m => "C",
                    3.00m => "B",
                    3.50m => "A",
                    4.00m => "A+",
                    _ => "unknown",
                };

                /*
                 * The form subscribes to this event to add to the ListView
                 */
                OnIteratePersonGradesEvent?.Invoke(new PersonGrades()
                {
                    PersonID = entity.PersonID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Grade = entity.Grade,
                    GradeLetter = letterGrade
                });

            }

            return studentEntities;
            
        }
        
    }
}
