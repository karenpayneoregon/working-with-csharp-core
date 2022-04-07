using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OracleNorthWindLibrary.Models;


// ReSharper disable once CheckNamespace
namespace OracleNorthWindLibrary.Data
{
    /// <summary>
    /// Used to configure models and set the default database
    /// </summary>
    public partial class NorthwindContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("NORTHWIND01")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");


            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORIES");

                entity.HasIndex(e => e.CategoryName, "UIDX_CATEGORY_NAME")
                    .IsUnique();

                entity.Property(e => e.CategoryId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Picture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PICTURE");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.HasIndex(e => e.City, "IDX_CUSTOMERS_CITY");

                entity.HasIndex(e => e.CompanyName, "IDX_CUSTOMERS_COMPANY_NAME");

                entity.HasIndex(e => e.PostalCode, "IDX_CUSTOMERS_POSTAL_CODE");

                entity.HasIndex(e => e.Region, "IDX_CUSTOMERS_REGION");

                entity.HasIndex(e => e.CustomerCode, "UIDX_CUSTOMERS_CODE")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_NAME");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_TITLE");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                /*
                 * Karen Payne note
                 * This column should never be used
                 */
                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_CODE");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEES");

                entity.HasIndex(e => e.Lastname, "IDX_EMPLOYEES_LASTNAME");

                entity.HasIndex(e => e.PostalCode, "IDX_EMPLOYEES_POSTAL_CODE");

                entity.Property(e => e.EmployeeId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    //.ValueGeneratedOnAdd()
                    //.UseIdentityColumn()
                    .HasColumnName("EMPLOYEE_ID");

                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Extension)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("EXTENSION");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Hiredate)
                    .HasColumnType("DATE")
                    .HasColumnName("HIREDATE");

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("HOME_PHONE");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("NOTES");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHOTO");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");

                entity.Property(e => e.ReportsTo)
                    .HasPrecision(9)
                    .HasColumnName("REPORTS_TO");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.Property(e => e.TitleOfCourtesy)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_OF_COURTESY");

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_REPORTS_TO");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.HasIndex(e => e.CustomerId, "IDX_ORDERS_CUSTOMER_ID");

                entity.HasIndex(e => e.EmployeeId, "IDX_ORDERS_EMPLOYEE_ID");

                entity.HasIndex(e => e.OrderDate, "IDX_ORDERS_ORDER_DATE");

                entity.HasIndex(e => e.ShippedDate, "IDX_ORDERS_SHIPPED_DATE");

                entity.HasIndex(e => e.ShipVia, "IDX_ORDERS_SHIPPER_ID");

                entity.HasIndex(e => e.ShipPostalCode, "IDX_ORDERS_SHIP_POSTAL_CODE");

                entity.Property(e => e.OrderId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(9)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.EmployeeId)
                    .HasPrecision(9)
                    .HasColumnName("EMPLOYEE_ID");

                entity.Property(e => e.Freight)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("FREIGHT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUIRED_DATE");

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_ADDRESS");

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_CITY");

                entity.Property(e => e.ShipCountry)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_COUNTRY");

                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_NAME");

                entity.Property(e => e.ShipPostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_POSTAL_CODE");

                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SHIP_REGION");

                entity.Property(e => e.ShipVia)
                    .HasPrecision(9)
                    .HasColumnName("SHIP_VIA");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("SHIPPED_DATE");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_ID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_ID");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_SHIPPER_ID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("ORDER_DETAILS");

                entity.HasIndex(e => e.OrderId, "IDX_ORDER_DETAILS_ORDER_ID");

                entity.HasIndex(e => e.ProductId, "IDX_ORDER_DETAILS_PRODUCT_ID");

                entity.Property(e => e.OrderId)
                    .HasPrecision(9)
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasPrecision(9)
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Discount)
                    .HasColumnType("NUMBER(4,2)")
                    .HasColumnName("DISCOUNT")
                    .HasDefaultValueSql("0\n   ");

                entity.Property(e => e.Quantity)
                    .HasPrecision(9)
                    .HasColumnName("QUANTITY")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("UNIT_PRICE")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCT_ID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.HasIndex(e => e.CategoryId, "IDX_PRODUCTS_CATEGORY_ID");

                entity.HasIndex(e => e.SupplierId, "IDX_PRODUCTS_SUPPLIER_ID");

                entity.Property(e => e.ProductId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.CategoryId)
                    .HasPrecision(9)
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Discontinued)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DISCONTINUED")
                    .HasDefaultValueSql("'N'\n   ")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("QUANTITY_PER_UNIT");

                entity.Property(e => e.ReorderLevel)
                    .HasPrecision(9)
                    .HasColumnName("REORDER_LEVEL")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SupplierId)
                    .HasPrecision(9)
                    .HasColumnName("SUPPLIER_ID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("UNIT_PRICE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitsInStock)
                    .HasPrecision(9)
                    .HasColumnName("UNITS_IN_STOCK")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitsOnOrder)
                    .HasPrecision(9)
                    .HasColumnName("UNITS_ON_ORDER")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CATEGORY_ID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUPPLIER_ID");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("SHIPPERS");

                entity.Property(e => e.ShipperId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("SHIPPER_ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("SUPPLIERS");

                entity.HasIndex(e => e.CompanyName, "IDX_SUPPLIERS_COMPANY_NAME");

                entity.HasIndex(e => e.PostalCode, "IDX_SUPPLIERS_POSTAL_CODE");

                entity.Property(e => e.SupplierId)
                    .HasPrecision(9)
                    .ValueGeneratedNever()
                    .HasColumnName("SUPPLIER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_NAME");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_TITLE");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.HomePage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("HOME_PAGE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.HasSequence("SEQ_NW_CATEGORIES");

            modelBuilder.HasSequence("SEQ_NW_CUSTOMERS");

            modelBuilder.HasSequence("SEQ_NW_EMPLOYEES");

            modelBuilder.HasSequence("SEQ_NW_ORDERS");

            modelBuilder.HasSequence("SEQ_NW_PRODUCTS");

            modelBuilder.HasSequence("SEQ_NW_SHIPPERS");

            modelBuilder.HasSequence("SEQ_NW_SUPPLIERS");


            //modelBuilder.Entity<Customer>().HasQueryFilter(customer => customer.Country == "Mexico");

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
