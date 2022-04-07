using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleNorthWindLibrary.Models;

namespace OracleEntityFrameworkUnitTest.Base
{
    public class OrderTestBase
    {
        protected Order createOrder()
        {
            Order order = new Order();

            return order;
        }
    }
}
