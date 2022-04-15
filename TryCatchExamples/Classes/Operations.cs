using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TryCatchExamples.Classes.Helpers;

namespace TryCatchExamples.Classes
{
    public class Operations
    {

        public static int StupidNoviceMistake(string value = "ABC")
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(value);
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public static (int? result, bool success) MuchBetter(string value = "ABC") 
            => int.TryParse(value, out var resultValue) ? 
                (resultValue, true) : 
                (null, false);

        public static (string[] list, Exception exception) ReadFileConventional(string fileName)
        {
            try
            {
                return (File.ReadAllLines(fileName), null);
            }
            catch (Exception localException)
            {
                return (null, localException);
            }
        }

        /// <summary>
        /// Assumes caller asserted that the file exists prior to calling this method.
        /// Novice developers tend not to assert if the file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static (string[] list, string message) ReadFileWithWhenFilter(string fileName)
        {
            try
            {
                return (File.ReadAllLines(fileName), null);
            }
            catch (Exception localException) when (localException.Message.Contains("Found"))
            {
                return (null, $"{fileName} is missing dude");
            }
            catch (Exception localException) when (localException.Message.Contains("denied"))
            {
                return (null, $"insufficient rights to {fileName}");
            }
            catch (Exception localException)
            {
                return (null, localException.Message);
            }
        }

        private static string _connectionString = "";
        public static bool RunWithoutIssues = false;

        /// <summary>
        /// Demo for connection timeout
        /// For a real app we would use a log library
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<DataTableResults> ReadProductsTask(CancellationToken ct)
        {
            var result = new DataTableResults() { DataTable = new DataTable() };

            _connectionString = RunWithoutIssues ? "Data Source=.\\sqlexpressISSUE;Initial Catalog=NorthWind2020;Integrated Security=True" : "Data Source=.\\sqlexpress;Initial Catalog=NorthWind2020;Integrated Security=True";

            return await Task.Run(async () =>
            {
                await using var cn = new SqlConnection(_connectionString);
                await using var cmd = new SqlCommand() { Connection = cn };

                cmd.CommandText = Queries.SelectStatement();

                try
                {
                    await cn.OpenAsync(ct);
                }
                catch (TaskCanceledException tce)
                {

                    Exceptions.Write(tce, Exceptions.ExceptionLogType.ConnectionFailure,
                        $"Connection string '{_connectionString}'");

                    result.ConnectionFailed = true;
                    result.ExceptionMessage = "Connection Failed";
                    return result;
                }
                catch (Exception ex)
                {
                    Exceptions.Write(ex, Exceptions.ExceptionLogType.General);
                    result.GeneralException = ex;
                    return result;
                }

                result.DataTable.Load(await cmd.ExecuteReaderAsync(ct));

                return result;

            }, ct);

        }

        /// <summary>
        /// Same as above but with if logic
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeRequestConventional()
        {
            var client = new HttpClient();
            var streamTask = client.GetStringAsync("https://localHost:10000");
            try
            {
                var responseText = await streamTask;
                return responseText;
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("301"))
                {
                    return "Site Moved";
                }
                else if (e.Message.Contains("404"))
                {
                    return "Page Not Found";
                }

                return e.Message;

            }
        }

        /// <summary>
        /// Example using a 'when' to filer a condition, in this case for
        /// a specific type of <see cref="HttpRequestException"/> base on the
        /// exception text.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeRequestWithWhenFilter()
        {
            var client = new HttpClient();
            var streamTask = client.GetStringAsync("https://localHost:10000");
            try
            {
                var responseText = await streamTask;
                return responseText;
            }
            catch (HttpRequestException e) when (e.Message.Contains("301"))
            {
                return "Site Moved";
            }
            catch (HttpRequestException e) when (e.Message.Contains("404"))
            {
                return "Page Not Found";
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Simple example for try/finally without catch where the method is
        /// wrapped in a try-catch by the caller.
        /// </summary>
        public static void TryCast()
        {
            int index = 123;
            string value = "Some string";
            object obj = value;

            try
            {
                // Invalid conversion; obj contains a string, not a numeric type.
                index = (int)obj;

                // The following statement is not run.
                Debug.WriteLine("WriteLine at the end of the try block.");
            }
            finally
            {
                // Report that the finally block is run, and show that the value of
                // index has not been changed.
                Debug.WriteLine("\nIn the finally block in TryCast, i = {0}.\n", index);
            }
        }
    }
}
