using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;

namespace NorthWindCoreLibrary.Classes
{
    public class ContactOperations
    {
        public static async Task<List<ContactItem>> GetContactsWithProjection()
        {
            List<ContactItem> contactList = new();
            
            await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                contactList =  await context.Contacts.Select(ContactItem.Projection).ToListAsync();
            });

            return contactList;

        }

        public static async Task<List<IGrouping<int?, ContactItem>>> ContactsGroupedByTitleAsync()
        {
            List<ContactItem> contactList = new();
            await Task.Run(async () =>
            {
                contactList = await GetContactsWithProjection();
            });

            /*
             * Alternate via LINQ, just add a order by
             *
             * (from c in contacts group c by c.ContactTypeIdentifier into items select items).ToList();
             *
             */
            return contactList
                .GroupBy(contactItem => contactItem.ContactTypeIdentifier)
                .Select(grouped => grouped)
                .OrderBy(contactItem => contactItem.FirstOrDefault().ContactTitle)
                .ToList();

        }

        /// <summary>
        /// Code sample which obtains all phone numbers for each contact
        /// </summary>
        /// <returns></returns>
        public static List<Contacts> Phones()
        {
            using var context = new NorthwindContext();
            
            List<Contacts> results = context.Contacts
                .Include(contacts => contacts.ContactDevices)
                .ThenInclude(contactDevices => contactDevices.PhoneTypeIdentifierNavigation)
                .ToList();
            
            return results;

        }
        public static Contacts Phones(int identifier)
        {
            using var context = new NorthwindContext();

            Contacts results = context.Contacts
                .Include(x => x.ContactDevices)
                .ThenInclude(x => x.PhoneTypeIdentifierNavigation)
                .FirstOrDefault();

            return results;

        }

        /// <summary>
        /// For unit testing only
        /// </summary>
        public static void Warmup()
        {
            using var context = new NorthwindContext();
            var results = context.Contacts.Count();
        }
    }
}
