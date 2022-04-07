using System;
using System.Collections.Generic;
using Relational_Pattern_Matching.Models;

namespace Relational_Pattern_Matching.Classes
{
    public class Helpers
    {
        public static IEnumerable<List<Employee>> GetEmployeesWhereManagerHasThreeYearsAsManager(List<Person> people)
        {
            const int years = 3;

            foreach (var person in people)
            {
                /*
                 * If Manager and has been for three years
                 *
                 * YearsAsManager equates to if YearsAsManager = 3
                 * Employees: { } employees means to return list of employees under this manager
                 */
                if (person is Manager { YearsAsManager: years, Employees: { } employees })
                {
                    yield return employees;
                }
            }
        }

        public static string GetCalendarSeason(DateTime date) => date.Month switch
        {
            >= 3 and < 6 => "spring",
            >= 6 and < 9 => "summer",
            >= 9 and < 12 => "autumn",
            12 or (>= 1 and < 3) => "winter",
            _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
        };
    }
}
