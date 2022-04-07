using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataLibrary
{
    public class Orders
    {
        public int OrderID { get; set; }

        public int? CustomerIdentifier { get; set; }
        public string Company { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public override string ToString() => $"{OrderID}, {Company}";

    }
}
