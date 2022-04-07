using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace OracleNorthWindLibrary.Data
{
    /// <summary>
    /// Karen isolated this code on purpose as not to clutter the main class and the
    /// configuration aspect of the DbContext.
    /// </summary>
    public partial class NorthwindContext
    {
        /// <summary>
        /// Returns added, modified or deleted entity objects, listing their class names (not table names)
        /// and their properties, with original and current values. The key values are also included to
        /// be able to make a distinction between objects of the same type.
        ///
        /// IMPORTANT
        /// Future versions of EF may have different paths to this functionality.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<(string Key, string Entity, EntityState state, IEnumerable<(string Property, object OriginalValue, object CurrentValue)> Properties)> GetChanges()
        {
            var states = new[]
            {
                EntityState.Added, 
                EntityState.Modified, 
                EntityState.Deleted
            };

            return ChangeTracker.Entries().Where(c => states.Contains(c.State))
                .Select(entry =>
                (
                    string.Join(",", entry.Metadata.FindPrimaryKey().Properties.Select(p => p.PropertyInfo.GetValue(entry.Entity))),
                    entry.Metadata.ClrType.Name,
                    entry.State,
                    entry.Properties
                        .Where(pe => pe.IsModified == (pe.EntityEntry.State == EntityState.Modified))
                        .Select(prop =>
                            (
                                prop.Metadata.PropertyInfo.Name,
                                prop.OriginalValue,
                                prop.CurrentValue
                            )
                        )));
        }
    }
}
