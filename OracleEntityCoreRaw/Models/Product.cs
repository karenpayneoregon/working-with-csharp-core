using System.Collections.Generic;
using OracleNorthWindLibrary.Interfaces;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Product : IBaseEntity
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id => ProductId;
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public string Discontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => ProductName;


    }
}
