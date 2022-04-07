using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataLibrary
{
    public class OrderOperations
    {
        public static List<Orders> Read(string fileName)
        {
            List<Orders> orders = new ();
            
            var ordersLines = File.ReadAllLines(fileName);

            foreach (var line in ordersLines)
            {
                var parts = line.Split(',');
                var order = new Orders()
                {
                    OrderID = Convert.ToInt32(parts[0]), 
                    Company = parts[1], 
                    CustomerIdentifier = Convert.ToInt32(parts[2]),
                    OrderDate = Convert.ToDateTime(parts[3]),
                    RequiredDate = Convert.ToDateTime(parts[4]),
                    ShippedDate = Convert.ToDateTime(parts[5]),
                };
                
                orders.Add(order);
                
            }

            return orders;
        }
    }
}
