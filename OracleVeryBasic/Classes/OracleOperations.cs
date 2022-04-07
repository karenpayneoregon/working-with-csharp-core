
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleVeryBasic.Models;

namespace OracleVeryBasic.Classes
{
    public class OracleOperations
    {
        public static List<Category> Categories()
        {
            List<Category> list = new List<Category> { new() { Id = -1, Name = "Select" } };

            try
            {
                using var cn = new OracleConnection() { ConnectionString = ConnectionString(1) };
                using var cmd = new OracleCommand { Connection = cn, CommandText = "SELECT category_id, description FROM categories" };
                cn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Category() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Create a well formed connection string
        /// </summary>
        /// <param name="index">database index</param>
        /// <returns>connection string</returns>
        public static string ConnectionString(int index) =>
            "Data Source=aix-aixdev.emp.state.or.us:1521/northwind_demo;" +
            "Persist Security Info=True;Enlist=false;Pooling=true;" +
            $"Statement Cache Size=10;User ID=northwind{index:D2};Password=\"!northwind!DEMO!\"";
    }
}
