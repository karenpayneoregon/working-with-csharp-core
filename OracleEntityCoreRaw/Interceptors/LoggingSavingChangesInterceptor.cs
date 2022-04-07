using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OracleNorthWindLibrary.Models;

namespace OracleNorthWindLibrary.Interceptors
{
    /// <summary>
    /// SaveChangesInterceptor interceptor which in it's current state
    /// has commented out code which works, provides alternate possibilities
    ///
    /// Objective here is to assert for Region property equal to specific value
    /// and if so reject the save.
    /// </summary>
    public class LoggingSavingChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new())
        {
            Debug.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);

            // here we expect one customer
            Customer customer = (Customer)eventData.Context.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType() == typeof(Customer)).Entity;
            if (customer.Region == "KP")
            {
                result = InterceptionResult<int>.SuppressWithResult(0);
            }

            // here we expect multiples
            //if (InspectCustomerRegion(eventData.Context.ChangeTracker.Entries()))
            //{
            //    result = InterceptionResult<int>.SuppressWithResult(0);
            //}

            return new ValueTask<InterceptionResult<int>>(result);
        }

        /// <summary>
        /// Provides a way to inspect model/entry values
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private static bool InspectCustomerRegion(IEnumerable<EntityEntry> entries)
        {
            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                    if (entry.Entity is Customer customer)
                    {
                        Debug.WriteLine($"Region is {customer.Region}");
                        if (customer.Region == "KP")
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }
    }
}