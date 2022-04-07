using System;
using System.Collections.Generic;

#nullable disable

namespace OracleNorthWindLibrary.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString() => CategoryName;

        public void Deconstruct(out int identifier, out string name)
        {
            identifier = CategoryId;
            name = CategoryName;
        }
        public void Deconstruct(out int identifier, out string name, out string description)
        {
            identifier = CategoryId;
            name = CategoryName;
            description = Description;
        }


    }
}
