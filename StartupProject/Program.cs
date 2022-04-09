using System;
using System.Data;
using System.Threading.Tasks;
using StartupProject.Classes;
using StartupProject.LanguageExtensions;

namespace StartupProject
{
    class Program
    {
        static async Task Main(string[] args)
        {

            DataTable customers = await SqlServerOperations
                .CustomersByPhoneTypeAndContactType(0, 0);


            Console.WriteLine(customers.Rows.Count);

            Console.ReadLine();

        }

    }
}
