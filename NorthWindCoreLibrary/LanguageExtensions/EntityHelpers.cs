using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NorthWindCoreLibrary.Data;

namespace NorthWindCoreLibrary.LanguageExtensions
{
    public static class EntityHelpers
    {
        private static readonly MethodInfo ContainsMethod = typeof(Enumerable).GetMethods()
            .FirstOrDefault(methodInfo => methodInfo.Name == "Contains" && methodInfo.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(object));

        public static Task<T[]> FindAllAsync<T>(this DbContext dbContext, params object[] keyValues) where T : class
        {

            IEntityType entityType = dbContext.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();
            
            if (primaryKey.Properties.Count != 1)
            {
                throw new NotSupportedException("Only a single primary key is supported");
            }

            var pkProperty = primaryKey.Properties[0];
            var pkPropertyType = pkProperty.ClrType;

            // validate passed key values
            foreach (var keyValue in keyValues)
            {
                if (!pkPropertyType.IsAssignableFrom(keyValue.GetType()))
                {
                    throw new ArgumentException($"Key value '{keyValue}' is not of the right type");
                }
            }

            // retrieve member info for primary key
            var pkMemberInfo = typeof(T).GetProperty(pkProperty.Name);
            
            if (pkMemberInfo == null)
            {
                throw new ArgumentException("Type does not contain the primary key as an accessible property");
            }

            // build lambda expression
            var parameter = Expression.Parameter(typeof(T), "e");
            
            var body = Expression.Call(null, ContainsMethod,
                Expression.Constant(keyValues),
                Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo), typeof(object)));
            
            var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            // run query
            return dbContext.Set<T>().Where(predicateExpression).ToArrayAsync();
        }

    }
}
