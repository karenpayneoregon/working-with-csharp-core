using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDataLibrary.Comparers;

namespace ApplicationDataLibrary.ExtensionMethods
{
    public static class GroupingExtensions
    {
        /// <summary>
        /// Provides grouping for <see cref="Concert"/> by identifier under or over 3
        /// </summary>
        /// <param name="sender">IEnumerable of <see cref="Concert"/></param>
        /// <param name="comparer"><see cref="PersonIdentifierGroupComparer"/></param>
        /// <returns>IEnumerable&lt;IGrouping&lt;int, Concert&gt;&gt;</returns>
        public static IEnumerable<IGrouping<int, Concert>> 
            ConcertGroup(this IEnumerable<Concert> sender, PersonIdentifierGroupComparer comparer) 
                => sender.GroupBy(convert 
                    => convert.PersonId, comparer);
        
    }
}
