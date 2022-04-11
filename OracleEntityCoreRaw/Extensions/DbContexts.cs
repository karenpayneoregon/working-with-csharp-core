using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OracleNorthWindLibrary.Extensions
{
    public static class DbContexts
    {

        /// <summary>
        /// Get model names for a <see cref="DbContext"/>
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns></returns>
        public static List<string> GetModelNames(this DbContext context) =>
            context.ModelTypeInformation().Select(item => item.Name).ToList();

        /// <summary>
        /// Get models details for a <see cref="DbContext"/>
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns>List&lt;<see cref="Type"/>> for each model</returns>
        public static List<Type> ModelTypeInformation(this DbContext context)
        {
            return context.Model.GetEntityTypes().Select(entityType => entityType.ClrType).ToList();
        }
    }

}