using System.Collections.Generic;
using System.Linq;


namespace ApplicationDataLibrary.Comparers
{
    public static class ComparerExtensions
    {
        /// <summary>
        /// Wrapper for <see cref="FirstNameLastNameEqualityComparer"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static IEnumerable<Person> DistinctFirstLastName(this IEnumerable<Person> sender) 
            => sender.Distinct(new FirstNameLastNameEqualityComparer());

        /// <summary>
        /// Wrapper for <see cref="FirstNameComparer"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static IEnumerable<Person> DistinctFirstName(this IEnumerable<Person> sender) 
            => sender.Distinct(new FirstNameComparer());

        /// <summary>
        /// Wrapper for <see cref="PropertyComparer"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IEnumerable<T> WithDistinctProperty<T>(this IEnumerable<T> sender, string propertyName) 
            => sender.Distinct(new PropertyComparer<T>(propertyName));
    }
}