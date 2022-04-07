using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NorthWindCoreLibrary.Data.Interceptors
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        public override DbDataReader ReaderExecuted(DbCommand command, 
            CommandExecutedEventData eventData, DbDataReader result)
        {
            Debug.WriteLine($"KAREN ReaderExecuted: {command.CommandText}");
            return base.ReaderExecuted(command, eventData, result);
        }
    }
}
