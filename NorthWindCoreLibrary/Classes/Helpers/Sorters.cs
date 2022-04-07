using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NorthWindCoreLibrary.Classes.Helpers
{
    /// <summary>
    /// Provides dynamic sorting methods.
    /// </summary>
    /// <remarks>
    /// Code is advance level and will be discussed in a future lesson
    ///
    /// If curious see Karen's GitHub repository
    /// https://github.com/karenpayneoregon/DynamicExpressions
    /// </remarks>
    public static class Sorters
    {
        /// <summary>
        /// Performs a sort by property name as a string on T
        /// </summary>
        /// <typeparam name="T">Type to perform sorting on</typeparam>
        /// <param name="list">List of T</param>
        /// <param name="propertyName">Valid property name in T</param>
        /// <param name="sortDirection">ascending or descending order, ascending is the default direction</param>
        /// <returns>list sorted by property name in specified order</returns>
        /// <remarks>
        /// Some might like OrderByPropertyName rather than SortByPropertyName
        /// </remarks>
        public static List<T> SortByPropertyName<T>(this List<T> list, string propertyName, SortDirection sortDirection)
        {

            var parameterExpression = Expression.Parameter(typeof(T), "item");

            Expression<Func<T, object>> sortExpression = 
                Expression.Lambda<Func<T, object>>(
                    Expression.Convert(Expression.Property(parameterExpression, propertyName), 
                        typeof(object)), parameterExpression);

            list = sortDirection == SortDirection.Ascending ? 
                list.AsQueryable().OrderBy(sortExpression).ToList() : 
                list.AsQueryable().OrderByDescending(sortExpression).ToList();

            return list;

        }

    }
}
