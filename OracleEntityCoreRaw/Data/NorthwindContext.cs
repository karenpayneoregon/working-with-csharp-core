
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OracleNorthWindLibrary.Interceptors;
using OracleNorthWindLibrary.Models;

#nullable disable

namespace OracleNorthWindLibrary.Data
{
    /// <summary>
    /// Primary DbContext for NorthWind database
    /// </summary>
    public partial class NorthwindContext : DbContext
    {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public NorthwindContext()
        {

        }

        /// <summary>
        /// Provides the ability to customize settings like connection string
        /// </summary>
        /// <param name="options"></param>
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                StandardConnection(optionsBuilder);

                /*
                 * See also Interceptors.NorthCommandInterceptor
                 */
                //Database.SetCommandTimeout(TimeSpan.FromMinutes(2));


            }
        }

        #region Connection setup

        private static IConfigurationRoot ReadAppsettings(out IConfigurationBuilder builder)
        {
            builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();

            return config; 
        }

        /// <summary>
        /// Vanilla setup indicating our database connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void StandardConnection(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out var builder);
            optionsBuilder.UseOracle(config.GetConnectionString("DatabaseConnection"));
        }
        /// <summary>
        /// Demo in this case to prevent a Customer Region from being KP
        /// See test method SaveChangesInterceptor
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void ConnectionWithSaveInterceptor(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out var builder);
            optionsBuilder
                .AddInterceptors(new LoggingSavingChangesInterceptor())
                .UseOracle(config.GetConnectionString("DatabaseConnection"));
        }

        private static void ConnectionWithAuditInterceptor(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out var builder);
            optionsBuilder
                .AddInterceptors(new AuditInterceptor())
                .UseOracle(config.GetConnectionString("DatabaseConnection"));
        }

        /// <summary>
        /// Connection setup for development debugging
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void LogQueryInfoToDebugOutputWindow(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ReadAppsettings(out var builder);
            optionsBuilder.UseOracle(config.GetConnectionString("DatabaseConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }

        #endregion


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
