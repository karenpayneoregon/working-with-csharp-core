using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NorthWindCoreLibrary.Data.Interceptors
{
    public class TaggedQueryCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            ManipulateCommand(command);

            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            ManipulateCommand(command);

            return new ValueTask<InterceptionResult<DbDataReader>>(result);
        }
        /// <summary>
        /// When Use hint: robust plan is detected, append OPTION (ROBUST PLAN) to the command text
        /// List&lt;Contacts&gt; results = context.Contacts.TagWith("Use hint: robust plan").ToList();
        /// </summary>
        /// <param name="command"></param>
        private static void ManipulateCommand(DbCommand command)
        {
            if (command.CommandText.StartsWith("-- Use hint: robust plan", StringComparison.Ordinal))
            {
                command.CommandText += " OPTION (ROBUST PLAN)";
            }
        }
    }
}
