using System;
using System.Collections.Generic;
using OracleNorthWindLibrary.Interfaces;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Order : IBaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id => OrderId;
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shipper ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => $"{OrderId} {OrderDate:d}";
    }
}
