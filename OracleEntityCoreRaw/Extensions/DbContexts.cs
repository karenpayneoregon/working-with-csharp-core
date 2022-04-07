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
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context) =>
            await Task.Run(async () => await context.Database.CanConnectAsync());

        /// <summary>
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <param name="token">token which can be used to set the timeout</param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context, CancellationToken token) =>
            await Task.Run(async () => await context.Database.CanConnectAsync(token), token);

        /// <summary>
        /// Determine if a connection can be made synchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns></returns>
        public static bool TestConnection(this DbContext context) =>
            context.Database.CanConnect();

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"></param>
        /// <returns>if a connection can be made</returns>
        public static (bool success, Exception exception) CanConnect(this DbContext context)
        {
            try
            {
                return (context.Database.CanConnect(), null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns></returns>
        public static async Task<(bool success, Exception exception)> CanConnectAsync(this DbContext context)
        {
            try
            {
                var result = await Task.Run(async () => await context.Database.CanConnectAsync());
                return (result, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }

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


        private static readonly MethodInfo ContainsMethod =
    typeof(Enumerable).GetMethods()
        .FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)?
        .MakeGenericMethod(typeof(object));

        /// <summary>
        /// Find by one or more keys
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="dbContext">DbContext</param>
        /// <param name="keyValues">One or more keys</param>
        /// <code>
        /// await using var context = new NorthWindContext();
        /// var keys = new object[] {1, 2, 3, 4};
        /// var results = await context.FindAllAsync&lt;Category&gt;(keys);
        /// </code>
        public static Task<T[]> FindAllAsync<T>(this DbContext dbContext, params object[] keyValues) where T : class
        {
            var entityType = dbContext.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey.Properties.Count != 1)
            {
                throw new NotSupportedException("Only a single primary key is supported");
            }

            var pkProperty = primaryKey.Properties[0];
            var pkPropertyType = pkProperty.ClrType;

            foreach (var keyValue in keyValues)
            {
                if (!pkPropertyType.IsAssignableFrom(keyValue.GetType()))
                {
                    throw new ArgumentException($"Key value '{keyValue}' is not of the right type");
                }
            }

            var pkMemberInfo = typeof(T).GetProperty(pkProperty.Name);
            if (pkMemberInfo is null)
            {
                throw new ArgumentException("Type does not contain the primary key as an accessible property");
            }


            var parameter = Expression.Parameter(typeof(T), "e");
            var body = Expression.Call(null, ContainsMethod,
                Expression.Constant(keyValues),
                Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo), typeof(object)));
            var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            return dbContext.Set<T>().Where(predicateExpression).ToArrayAsync();
        }


    }

}