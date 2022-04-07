using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OracleNorthWindLibrary.Interceptors
{
    /// <summary>
    /// Example for setting a command timeout 
    /// </summary>
    public class NorthCommandInterceptor : DbCommandInterceptor
    {
        /// <summary>
        /// When implemented, timeout a command in two seconds
        /// </summary>
        /// <remarks>
        /// .AddInterceptors(new NorthCommandInterceptor()); 
        /// </remarks>
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.CommandTimeout = 2;
            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
