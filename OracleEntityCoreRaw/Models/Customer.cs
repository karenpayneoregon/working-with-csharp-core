using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OracleNorthWindLibrary.Interfaces;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Customer : IBaseEntity
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id => CustomerId;

        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => CompanyName;
    }
}
