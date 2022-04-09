using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseOracleCoreConsole1.Classes
{
    public class DataOperations
    {
        public static void Starter()
        {
            string sqlStatement = "TODO";

            using var cn = new OracleConnection() { ConnectionString = Operations.ConnectionString() };
            using var cmd = new OracleCommand(sqlStatement, cn);

            try
            {
                cn.Open();

                /*
                 * Add parameters if needed
                 * Execute command
                 */

            }
            catch (Exception ex)
            {
                // TODO
            }

        }

    }
}
