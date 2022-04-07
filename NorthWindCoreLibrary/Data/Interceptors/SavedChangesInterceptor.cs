using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NorthWindCoreLibrary.Data.Interceptors
{
    public class SavedChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            Debug.WriteLine($"Started saving changes.");

            return new ValueTask<InterceptionResult<int>>(result);
        }

        public override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {

            Debug.WriteLine($"Saved {result} No of changes.");

            return new ValueTask<int>(result);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            eventData.Context.ChangeTracker.DetectChanges();
            Debug.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);
            

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                
                var auditMessage = entry.State switch
                {
                    EntityState.Deleted => CreateDeletedMessage(entry),
                    EntityState.Modified => CreateModifiedMessage(entry),
                    EntityState.Added => CreateAddedMessage(entry),
                    _ => null
                };

                if (auditMessage is not null)
                {
                    Console.WriteLine();
                }
            }
            
            return base.SavedChanges(eventData, result);
        }

        string CreateAddedMessage(EntityEntry entry)
            => entry.Properties.Aggregate(
                $"Inserting {entry.Metadata.DisplayName()} with ",
                (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

        string CreateModifiedMessage(EntityEntry entry)
            => entry.Properties.Where(property => property.IsModified || property.Metadata.IsPrimaryKey()).Aggregate(
                $"Updating {entry.Metadata.DisplayName()} with ",
                (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

        string CreateDeletedMessage(EntityEntry entry)
            => entry.Properties.Where(property => property.Metadata.IsPrimaryKey()).Aggregate(
                $"Deleting {entry.Metadata.DisplayName()} with ",
                (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");
    }
}
