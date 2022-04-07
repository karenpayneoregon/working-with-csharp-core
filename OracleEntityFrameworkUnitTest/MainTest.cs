using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OracleEntityFrameworkUnitTest.Base;
using OracleEntityFrameworkUnitTest.Classes;
using OracleNorthWindLibrary.Data;

namespace OracleEntityFrameworkUnitTest
{
    [TestClass]
    public partial class MainTest : EmploymentEngineTestBase
    {
        /// <summary>
        /// A simple template showing how to test, in this case if we
        /// obtained all records from Customers table using a conventional
        /// method (connection and command objects) to read data against EF Core.
        /// </summary>
        /// <remarks>
        /// Originally written synchronous then asynchronous which
        /// shaved half a second in execution time.
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.Read)]
        public async Task CountTest()
        {
            await using var context = new NorthwindContext();
            var customersCount = await context.Customers.CountAsync();

            // trade this with 91, note time elapsed time difference
            var (rowCount, _) = await CustomersCount();
            Assert.AreEqual(customersCount, rowCount);

            // for kicks, this adds 200 milliseconds
            // showing that the first EF Core query was a warm-up and of
            // course the conventional method took time too
            //var dummy = await context.Categories.ToListAsync();

        }

        /// <summary>
        /// Requires ConnectionWithSaveInterceptor
        ///
        /// In this example we disallow a Customer.Region to = KP,
        /// if so abort save changes.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Interceptor)]
        [Ignore]
        public async Task SaveChangesInterceptor()
        {
            await using var context = new NorthwindContext();
            var customer = await context.Customers.FindAsync(1);

            customer.Region = "Canada";
            int affected = await context.SaveChangesAsync();
            Debug.WriteLine(affected);
        }

        /// <summary>
        /// Requires AuditInterceptor setup in the DbContext
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
        [TestTraits(Trait.Interceptor)]
        public async Task AuditInterceptor()
        {
            await using var context = new NorthwindContext();
            var customer = context.Customers.FirstOrDefault();
            customer.Region = "KP";
            await context.SaveChangesAsync();
        }

        [TestMethod]
        [TestTraits(Trait.ChangeTracker)]
        public void AnnihilationListPopulate()
        {
            SingleEmployee();
            Debug.WriteLine(dbContext.ChangeTracker.DebugView.LongView);
        }




    }
}
