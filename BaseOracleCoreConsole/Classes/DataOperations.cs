using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseOracleCoreConsole.Classes
{
    public class DataOperations
    {
        public static void Starter()
        {
            string sqlStatement = "TODO";

            using var cn = new OracleConnection() {ConnectionString = Operations.ConnectionString()};
            using var cmd = new OracleCommand(sqlStatement, cn);

            try
            {
                cn.Open();

                /*
                 * Add parameters if needed
                 * Execute command
                 */

            }
            catch (Exception)
            {
                // TODO
            }

        }
        public static DataTable GetCategoryData(int categoryId = 1)
        {
            DataTable table = new();


            if (Operations.ConnectionString().Contains("MasterClass_Notes"))
            {
                throw new ConnectionException();
            }

            using var cn = new OracleConnection()
            {
                ConnectionString = Operations.ConnectionString()
            };
            using var cmd = new OracleCommand(
                "SELECT p.Product_ID, p.Product_Name " +
                "FROM Products p " +
                "INNER JOIN Categories c ON p.Category_ID = c.Category_ID " +
                "WHERE c.Category_ID = :id " +
                "ORDER BY p.Product_Name", cn);

            cmd.BindByName = true;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new OracleParameter(":id", categoryId) { OracleDbType = OracleDbType.Decimal });


            cn.Open();
            table.Load(cmd.ExecuteReader());

            return table;

        }

    }
}
