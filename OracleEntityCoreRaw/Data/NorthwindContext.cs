
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConfigurationLibrary.Classes;
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
        /// <summary>
        /// Vanilla setup indicating our database connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        private static void StandardConnection(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(ConfigurationHelper.ConnectionString());
        }
        #endregion


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
