using System.Collections.Generic;

namespace Relational_Pattern_Matching.Models
{
    public class Manager : Person
    {
        public int YearsAsManager { get; set; }
        public List<Employee> Employees { get; set; }
    }
}