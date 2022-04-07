using System;
using System.Collections.Generic;
using OracleNorthWindLibrary.Interfaces;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Employee : IBaseEntity
    {
        public Employee()
        {
            InverseReportsToNavigation = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public int Id => EmployeeId;
        public int EmployeeId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }

        public virtual Employee ReportsToNavigation { get; set; }
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => $"{Title} {Firstname} {Lastname}";

        
    }
}
