using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;


// ReSharper disable once CheckNamespace
namespace SwitchExpressions_efcore.Data
{
    public partial class SchoolContext
    {
        private static void WithLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString())
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }
        private static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString());
        }
    }
}
