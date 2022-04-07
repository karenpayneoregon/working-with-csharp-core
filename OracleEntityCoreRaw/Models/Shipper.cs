using System.Collections.Generic;
using OracleNorthWindLibrary.Interfaces;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Shipper : IBaseEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int Id => ShipperId;
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => CompanyName;

    }
}
