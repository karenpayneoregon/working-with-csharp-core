using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleNorthWindLibrary.Models;

namespace OracleEntityFrameworkUnitTest.Base
{
    public class CustomerTestBase : TestBase
    {
        /// <summary>
        /// Create one <seealso cref="Customer"/>
        /// </summary>
        protected Customer CreateSingleCustomer()
        {
            Customer customer = new ();

            Order order = AddSandboxEntity(CreateSingleOrder());

            return customer;
        }

        /// <summary>
        /// Create one <see cref="Order"/>
        /// </summary>
        private Order CreateSingleOrder()
        {
            return new Order();
        }

        /// <summary>
        /// Create a single <see cref="Employee"/>
        /// </summary>
        protected static Employee CreateSingleEmployee() 
            => CreateEmployees().FirstOrDefault();

        /// <summary>
        /// Create a single <see cref="Employee"/>
        /// </summary>
        /// <param name="identifier">employee primary key</param>
        /// <returns></returns>
        protected Employee CreateSingleEmployee(int identifier) 
            => CreateEmployees()
                .FirstOrDefault(employee => employee.Id == identifier);

        /// <summary>
        /// Create a list of <see cref="Employee"/>
        /// </summary>
        protected static List<Employee> CreateEmployees()
        {
            return new List<Employee>()
            {
                new () { Lastname = "Davolio", Firstname = "Nancy", TitleOfCourtesy = "Ms.", Title = "Sales Representative", Birthdate = new DateTime(1968,12,8), Hiredate = new DateTime(1992,12,8), Address = "507 - 20th Ave. E.Apt. 2A", City = "Seattle", Region = "WA", PostalCode = "98122", Country = "USA", HomePhone = "(206) 555-9857", Extension = "5467", ReportsTo = 2},
                new () { Lastname = "Fuller", Firstname = "Andrew", TitleOfCourtesy = "Dr.", Title = "Vice President", Birthdate = new DateTime(1952,2,19), Hiredate = new DateTime(1992,8,14), Address = "908 W. Capital Way", City = "Tacoma", Region = "WA", PostalCode = "98401", Country = "USA", HomePhone = "(206) 555-9482", Extension = "3457"},
                new () { Lastname = "Leverling", Firstname = "Janet", TitleOfCourtesy = "Ms.", Title = "Sales Representative", Birthdate = new DateTime(1963,8,30), Hiredate = new DateTime(1992,4,1), Address = "722 Moss Bay Blvd.", City = "Kirkland", Region = "WA", PostalCode = "98033", Country = "USA", HomePhone = "(206) 555-3412", Extension = "3355", ReportsTo = 2},
                new () { Lastname = "Peacock", Firstname = "Margaret", TitleOfCourtesy = "Mrs.", Title = "Sales Representative", Birthdate = new DateTime(1958,9,19), Hiredate = new DateTime(1993,5,3), Address = "4110 Old Redmond Rd.", City = "Redmond", Region = "WA", PostalCode = "98052", Country = "USA", HomePhone = "(206) 555-8122", Extension = "5176", ReportsTo = 2},
                new () { Lastname = "Buchanan", Firstname = "Steven", TitleOfCourtesy = "Mr.", Title = "Sales Manager", Birthdate = new DateTime(1955,3,4), Hiredate = new DateTime(1993,10,17), Address = "14 Garrett Hill", City = "London", Region = "", PostalCode = "SW1 8JR", Country = "UK", HomePhone = "(71) 555-4848", Extension = "3453", ReportsTo = 2},
                new () { Lastname = "Suyama", Firstname = "Michael", TitleOfCourtesy = "Mr.", Title = "Sales Representative", Birthdate = new DateTime(1963,7,2), Hiredate = new DateTime(1993,10,17), Address = "Coventry House Miner Rd.", City = "London", Region = "", PostalCode = "EC2 7JR", Country = "UK", HomePhone = "(71) 555-7773", Extension = "428", ReportsTo = 5},
                new () { Lastname = "King", Firstname = "Robert", TitleOfCourtesy = "Mr.", Title = "Sales Representative", Birthdate = new DateTime(1960,5,9), Hiredate = new DateTime(1994,1,2), Address = "Edgeham Hollow  Winchester Way", City = "London", Region = "", PostalCode = "EC2 7JR", Country = "UK", HomePhone = "(71) 555-7773", Extension = "465", ReportsTo = 5},
                new () { Lastname = "Callahan", Firstname = "Laura", TitleOfCourtesy = "Ms.", Title = "Sales Agent", Birthdate = new DateTime(1958,1,9), Hiredate = new DateTime(1994,3,5), Address = "4726 - 11th Ave. N.E.", City = "Seattle", Region = "WA", PostalCode = "98105", Country = "USA", HomePhone = "(71) (206) 555-1189", Extension = "2344", ReportsTo = 2},
                new () { Lastname = "Dodsworth", Firstname = "Anne", TitleOfCourtesy = "Ms.", Title = "Sales Representative", Birthdate = new DateTime(1969,7,2), Hiredate = new DateTime(1994,11,15), Address = "7 Houndstooth Rd.", City = "London", Region = "", PostalCode = "WG2 7LT", Country = "UK", HomePhone = "(71) 555-4444", Extension = "452", ReportsTo = 5},
            };
        }

        protected List<Employee> ListEmployees()
            => AddSandboxEntities(CreateEmployees()).ToList();

        /// <summary>
        /// Create a <see cref="Shipper"/>
        /// </summary>
        /// <returns></returns>
        private Shipper CreateSingleShipper()
        {
            return new Shipper()
            {
                ShipperId = 1, 
                CompanyName = "Speedy Express", 
                Phone = "(503) 555-9831", 
                Orders = new List<Order>()
            };
        }

        private OrderDetail CreateSingleOrderDetail()
        {
            return new OrderDetail();
        }

        private Product CreateSingleProduct()
        {
            return new Product();
        }
    }

}
