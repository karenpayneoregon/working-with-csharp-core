
#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class OrderDetail 
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        /// <summary>
        /// Karen added
        /// </summary>
        public override string ToString() => $"{OrderId} Product id {ProductId}";

    }
}
