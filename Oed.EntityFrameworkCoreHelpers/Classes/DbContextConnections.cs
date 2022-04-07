using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Oed.EntityFrameworkCoreHelpers.Classes
{
    public class DbContextConnections
    {
        /// <summary>
        /// Simple configuration for setting the connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out _);
            var test = config.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection"));
        }

        /// <summary>
        /// Default logging to output window
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out _);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }
        /// <summary>
        /// Writes/appends to a file
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void CustomLogging(DbContextOptionsBuilder optionsBuilder)
        {

            var config = ReadAppsettings(out _);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection")).EnableSensitiveDataLogging()
                .LogTo(new DbContextLogger().Log)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        private static IConfigurationRoot ReadAppsettings(out IConfigurationBuilder builder)
        {
            builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();

            return config; // connection string
        }
    }
}
