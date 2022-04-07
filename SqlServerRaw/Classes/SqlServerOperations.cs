using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using SqlServerVeryBasic.Models;

namespace SqlServerVeryBasic.Classes
{
    public class SqlServerOperations
    {

        /// <summary>
        /// Read connection string from appsettings.json (which has environments for dev/stage/prod)
        /// </summary>
        protected static string ConnectionString = ConfigurationHelper.ConnectionString();

        // what most novice developers write rather than the above
        //protected static string ConnectionString = "Server=.\\SQLEXPRESS;Database=NorthWind2020;Integrated Security=true";

        /// <summary>
        /// Return a list of <see cref="Country"/> suitable for a selection in a user interface
        /// </summary>
        /// <returns><list type="Country"></list></returns>
        public static List<Country> Countries()
        {
            List<Country> list = new() { new() {Id = -1, Name = "Select"} };

            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT CountryIdentifier, [Name] FROM dbo.Countries;" };

            cn.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Country() {Id = reader.GetInt32(0), Name = reader.GetString(1)});
            }

            return list;

        }
        /// <summary>
        /// Readonly version of <see cref="Countries"/> which is for when the user of this method
        /// is not permitted to add or remove items.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<CountryItem> CountriesReadOnly()
        {
            List<CountryItem> list = new() { new CountryItem(-1, "Select") };

            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT CountryIdentifier, [Name] FROM dbo.Countries;" };

            cn.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new CountryItem(reader.GetInt32(0), reader.GetString(1)) );
            }

            return list.ToImmutableList();

        }

        public static List<Category> Categories()
        {
            List<Category> list = new() { new() { CategoryID = -1, CategoryName = "Select" } };

            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT CategoryID ,CategoryName FROM dbo.Categories" };

            cn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Category() { CategoryID = reader.GetInt32(0), CategoryName = reader.GetString(1) });
            }

            return list;

        }


        public static string CompanyNameWrong(int identifier)
        {
            using var cn = new SqlConnection { ConnectionString = ConnectionString };
            using var cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "SELECT CompanyName FROM Customers WHERE CustomerIdentifier = @Id"
            };

            cmd.Parameters.AddWithValue("@Id", identifier);
            cn.Open();

            return (string)cmd.ExecuteScalar();
        }
        public static string CompanyNameRight(int identifier)
        {
            using var cn = new SqlConnection { ConnectionString = ConnectionString };
            using var cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "SELECT CompanyName FROM Customers WHERE CustomerIdentifier = @Id"
            };


            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = identifier;
            cn.Open();

            return (string)cmd.ExecuteScalar();
        }
        public static (string name, Exception exception) CompanyNameSafe(int? identifier)
        {
            using var cn = new SqlConnection { ConnectionString = ConnectionString };
            using var cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "SELECT CompanyName FROM Customers WHERE CustomerIdentifier = @Id"
            };


            if (identifier.HasValue)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = identifier.Value;
                try
                {
                    cn.Open();

                    var name = (string)cmd.ExecuteScalar();
                    return (name, null);
                }
                catch (Exception ex)
                {
                    return (null, ex);
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }
        public static async Task<List<Product>> ProductsByCategoryIdentifier(int pCategoryIdentifier)
        {
            List<Product> productList = new List<Product>();

            var selectStatement =
                @"SELECT 
                    ProductID, 
                    ProductName, 
                    SupplierID, 
                    QuantityPerUnit, 
                    UnitPrice, 
                    UnitsInStock, 
                    UnitsOnOrder, 
                    ReorderLevel, 
                    Discontinued 
                FROM dbo.Products 
                WHERE CategoryID = @Identifier";

            await using var cn = new SqlConnection { ConnectionString = ConnectionString };
            await using var cmd = new SqlCommand { Connection = cn, CommandText = selectStatement };
            cmd.Parameters.AddWithValue("@Identifier", pCategoryIdentifier);

            await cn.OpenAsync();

            var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                productList.Add(new Product()
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Discontinued = reader.GetBoolean(8)
                });
            }

            return productList;
        }

        public static async Task<DataTable> GetTopFiveOrders()
        {
            DataTable table = new ();

            var selectStatement =
                "SELECT TOP 5 O.OrderID, O.CustomerIdentifier, C.CompanyName, O.OrderDate, O.RequiredDate, O.ShippedDate " +
                "FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerIdentifier = C.CustomerIdentifier";

            await using var cn = new SqlConnection { ConnectionString = ConnectionString };
            await using var cmd = new SqlCommand { Connection = cn, CommandText = selectStatement };
            await cn.OpenAsync();
            table.Load(await cmd.ExecuteReaderAsync());

            return table;
        }
    }
}
