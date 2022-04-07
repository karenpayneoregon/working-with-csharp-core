using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;


namespace NorthWindCoreLibrary.Classes
{
    public class OrderOperations
    {
        /// <summary>
        /// Read all <see cref="Employees"/>
        /// </summary>
        /// <returns>List of <see cref="Employees"/></returns>
        public static async Task<List<Employees>> GetEmployeesTask()
        {
            List<Employees> employeesList = new();
            await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                
                employeesList = await context.Orders
                    /*
                     * Note !=null, in the Orders table there is one record were employee id is null
                     */
                    .Where(order => order.EmployeeId != null)
                    .Select(order => order.Employee)
                    .ToListAsync();
            });

            return employeesList;
        }

        #region Staple methods

        /// <summary>
        /// An example to get orders with all navigation models
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Orders>> GetOrdersTask()
        {
            List<Orders> ordersList = new();
            await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();

                ordersList = await context.Orders
                    .Include(order => order.OrderDetails)
                    .Include(order => order.CustomerIdentifierNavigation)
                    .Include(order => order.Employee)
                    .Include(order => order.ShipViaNavigation)
                    .Select(order => order).ToListAsync();

            });

            return ordersList;
        }

        /// <summary>
        /// An example for obtaining a single order by order id reusing <see cref="GetOrdersTask"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Orders> GetOrderByOrderIdentifier(int id)
        {
            Orders orders = new();
            await Task.Run(async () =>
            {
                List<Orders> ordersList = await GetOrdersTask();
                orders = ordersList.FirstOrDefault(order => order.OrderId == id);
            });

            return orders;
        }

        #endregion

        #region EmployeeMostOrders with using an expression body member to returning data

        /// <summary>
        /// From list of <see cref="Employees"/> get employee with the most orders
        /// </summary>
        /// <param name="employeesList">List of <see cref="Employees"/></param>
        /// <returns><see cref="Employees"/> that has most orders</returns>
        public static IGrouping<int, Employees> EmployeeMostOrdersExpression(List<Employees> employeesList) => employeesList
                // group on EmployeeId
                .GroupBy(employee => employee.EmployeeId)
                // reverse order it on count
                .OrderByDescending(groupEmployee => groupEmployee.Count())
                // select the first
                .FirstOrDefault();

        #endregion

        /// <summary>
        /// From list of <see cref="Employees"/> get employee with the most orders
        /// </summary>
        /// <param name="employeesList">List of <see cref="Employees"/></param>
        /// <returns><see cref="Employees"/> that has most orders</returns>
        public static IGrouping<int, Employees> EmployeeMostOrders(List<Employees> employeesList)
        {

            return employeesList
                // group on EmployeeId
                .GroupBy(employee => employee.EmployeeId)
                // reverse order it on count
                .OrderByDescending(groupEmployee => groupEmployee.Count())
                // select the first
                .FirstOrDefault();
        }

        /// <summary>
        /// Here is a mirror image of the above which is done in lambda, this one done in LINQ
        /// </summary>
        /// <returns></returns>
        public static async Task EmployeeMostOrders_Linq()
        {
            var employeeList = await GetEmployeesTask();

            IOrderedEnumerable<IGrouping<int, Employees>> groupedResults =
                from employees in employeeList
                group employees by employees.EmployeeId
                into grouped
                orderby grouped.Count() descending 
                select grouped;

            IGrouping<int, Employees> singleGroupedEmployees = groupedResults.FirstOrDefault();
            
            // ReSharper disable once AssignNullToNotNullAttribute
            Debug.WriteLine($"Order count:  {singleGroupedEmployees.Count()} employee id: {singleGroupedEmployees.Key}");

        }
    }
}
