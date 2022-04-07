using System;
using System.Diagnostics;
using BaseOracleCoreConsole.Classes;
using static BaseOracleCoreConsole.Classes.Operations;

namespace BaseOracleCoreConsole
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine($"Default connection string: {ConnectionString()}");
            try
            {
                var table = DataOperations.GetCategoryData();
                Debug.WriteLine(table.Rows.Count);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex is ConnectionException ? 
                    "Need to fix connection string in appsettings.json" : 
                    ex.Message);
            }
            
        }
    }
}
