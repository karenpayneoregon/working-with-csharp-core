
#nullable disable


namespace SwitchExpressions_efcore.Models
{
    public class StudentGrade
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal? Grade { get; set; }

        public virtual Person Student { get; set; }
    }
}