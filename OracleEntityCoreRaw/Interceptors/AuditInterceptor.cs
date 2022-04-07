using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OracleNorthWindLibrary.Extensions;

namespace OracleNorthWindLibrary.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
        {
            Inspect(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            Inspect(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override int SavedChanges(
            SaveChangesCompletedEventData 
                eventData, int result)
        {
            Debug.WriteLine($"changes:{eventData.EntitiesSavedCount}");
            return base.SavedChanges(eventData, result);
        }

        private static void Inspect(DbContextEventData eventData)
        {
            var changesList = new List<CompareModel>();

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        changesList.Add(new CompareModel()
                        {
                            OriginalValue = null,
                            NewValue = entry.CurrentValues.ToObject(),
                        });
                        break;
                    case EntityState.Deleted:
                        changesList.Add(new CompareModel()
                        {
                            OriginalValue = entry.OriginalValues.ToObject(),
                            NewValue = null,
                        });
                        break;
                    case EntityState.Modified:
                        changesList.Add(new CompareModel()
                        {
                            OriginalValue = entry.OriginalValues.ToObject(),
                            NewValue = entry.CurrentValues.ToObject(),
                        });
                        break;
                }

                Debug.WriteLine($"change list:{changesList.ToJson()}");
            }
        }

        private class CompareModel
        {
            public object OriginalValue { get; set; }

            public object NewValue { get; set; }
        }
    }
}
