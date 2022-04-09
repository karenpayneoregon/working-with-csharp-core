using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Classes.Helpers;
using NorthWindCoreLibrary.Classes.North.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;


namespace NorthWindCoreLibrary.Classes
{
    public class CustomersOperations
    {
        public static async Task<List<CustomerItem>> GetCustomersWithProjectionAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Select(CustomerItem.Projection)
                    .ToListAsync();
            });
        }

        public static async Task<List<CustomerItem>> GetCustomersWithProjectionAsync(int phoneType)
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Select(CustomerItem.ProjectionByPhoneType(phoneType))
                    .ToListAsync();
            });
        }
        /// <summary>
        /// Custom projection for teaching sorting by property name as a string
        /// </summary>
        /// <returns>List&lt;<see cref="CustomerItemSort"/>&gt;</returns>
        public static async Task<List<CustomerItemSort>> GetCustomersWithProjectionSortAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Select(CustomerItemSort.Projection)
                    .ToListAsync();
            });
        }

        
        public static CustomerEntity CustomerByIdentifier(int identifier)
        {
            using var context = new NorthwindContext();
            return context.Customers.Select(Customers.Projection)
                .FirstOrDefault(custEntity => custEntity.CustomerIdentifier == identifier);
        }

        public static async Task<List<Customers>> GetCustomersAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .ToListAsync();
            });
        }

        public static IReadOnlyList<Categories> GetCategoriesList()
        {
            using var context = new NorthwindContext();
            return context.Categories.ToList().ToImmutableList();
        }

        public static readonly IReadOnlyList<Categories> CategoriesList = GetCategoriesList();
        public static readonly ImmutableList<int> ImmutableList = new [] { 1, 2, 3 }.ToImmutableList();
        public static readonly ImmutableList<int> ImmutableList1 = Enumerable.Range(1, 5).ToImmutableList();
        public static readonly IReadOnlyList<Person> PersonList = FromDatabaseMock.People();


        /// <summary>
        /// TODO This is for a lesson in a bad query and how to fix it
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CustomerItem>> GetCustomersAsync1()
        {

            var currentExecutable = Process.GetCurrentProcess().MainModule.FileName;

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers.AsNoTracking()
                    .Include(customer => customer.Contact)
                    .ThenInclude(contact => contact.ContactDevices)
                    .ThenInclude(contactDevices => contactDevices.PhoneTypeIdentifierNavigation)
                    .Include(customer => customer.ContactTypeIdentifierNavigation)
                    .Include(customer => customer.CountryIdentifierNavigation)
                    .Select(customer => new CustomerItem()
                    {
                        CustomerIdentifier = customer.CustomerIdentifier,
                        CompanyName = customer.CompanyName,
                        ContactId = customer.Contact.ContactId,
                        Street = customer.Street,
                        City = customer.City,
                        PostalCode = customer.PostalCode,
                        CountryIdentifier = customer.CountryIdentifier,
                        Phone = customer.Phone,
                        ContactTypeIdentifier = customer.ContactTypeIdentifier,
                        Country = customer.CountryIdentifierNavigation.Name,
                        FirstName = customer.Contact.FirstName,
                        LastName = customer.Contact.LastName,
                        ContactTitle = customer.ContactTypeIdentifierNavigation.ContactTitle,
                        OfficePhoneNumber = customer.Contact.ContactDevices.FirstOrDefault().PhoneNumber
                    })
                    .TagWith($"App name: {currentExecutable}")
                    .TagWith($"From: {nameof(CustomersOperations)}.{nameof(GetCustomersAsync1)}")
                    .TagWith("Parameters: None")
                    .ToListAsync();
            });
        }
    }
}
