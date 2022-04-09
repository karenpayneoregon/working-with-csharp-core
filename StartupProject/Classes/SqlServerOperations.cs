using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using DbLibrary.LanguageExtensions;


namespace StartupProject.Classes
{
    public class SqlServerOperations
    {


        protected static string ConnectionString = ConfigurationHelper.ConnectionString();


        public static async Task<DataTable> CustomersByPhoneTypeAndContactType(int phoneType, int contactType)
        {

            DataTable table = new();
            await using var cn = new SqlConnection(ConnectionString);
            await using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.ByPhoneByContactTypeSelect };

            cmd.Parameters.Add("@PhoneType", SqlDbType.Int).Value = phoneType;
            cmd.Parameters.Add("@ContactType", SqlDbType.Int).Value = contactType;

            Console.WriteLine(cmd.CommandText);
            Console.WriteLine();
            Console.WriteLine(cmd.ActualCommandText());

            await cn.OpenAsync();

            table.Load(await cmd.ExecuteReaderAsync());

            return table;

        }



    }
}