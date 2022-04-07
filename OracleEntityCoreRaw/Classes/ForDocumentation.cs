using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleNorthWindLibrary.Data;
using OracleNorthWindLibrary.Models;

namespace OracleNorthWindLibrary.Classes
{
    /// <summary>
    /// Used for teaching
    /// </summary>
    public class ForDocumentation
    {
        /// <summary>
        /// Example for adding new records, orders and order details
        /// </summary>
        public static void OrderAndDetailsAddition()
        {
            using var context = new NorthwindContext();

            Employee employee = new ();
            Product product1 = new ();
            Product product2 = new ();

            Order order = new ()
            {
                CustomerId = 1,
                Employee = employee,
                RequiredDate = new DateTime(2022, 2, 2)
            };

            order.OrderDetails.Add(new OrderDetail()
            {
                Product = product1, 
                Quantity = 2, 
                UnitPrice = 12.4m
            });

            order.OrderDetails.Add(new OrderDetail()
            {
                Product = product2,
                Quantity = 3,
                UnitPrice = 2.34m
            });

            context.SaveChanges();


        }
    }
}
