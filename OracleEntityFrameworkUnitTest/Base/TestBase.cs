using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OracleNorthWindLibrary.Data;
using OracleNorthWindLibrary.Interfaces;

// ReSharper disable once CheckNamespace
namespace OracleEntityFrameworkUnitTest
{
    [TestClass]
    public class TestBase
    {
        protected List<object> annihilationList;
        protected TestContext TestContextInstance;
        public static IList<TestContext> TestResults;
        protected NorthwindContext dbContext;
        public NorthwindContext DbContext
        {
            get => dbContext;
            set => dbContext = value;
        }

        public TestContext TestContext
        {
            get => TestContextInstance;
            set
            {
                TestContextInstance = value;
                TestResults.Add(TestContext);
            }
        }

        [TestInitialize]
        public void SetupTestBase()
        {
            annihilationList = new List<object>();
            dbContext = new NorthwindContext();
        }
        [TestCleanup]
        public void TeardownTestBase()
        {
            var success = AnnihilateData(annihilationList);
            dbContext.Dispose();
        }

        /// <summary>
        /// Used to revert live database to pre-test data
        /// </summary>
        /// <param name="sandboxBackingList"></param>
        /// <returns></returns>
        public bool AnnihilateData(List<object> sandboxBackingList)
        {
            
            // TODO - implement full functionality

            var exceptions = new List<Exception>();
            DbCleanupResults cleanupResults;

            // ReSharper disable once ConvertToUsingDeclaration
            using (var context = new NorthwindContext())
            {
                cleanupResults = CleanUpTheDatabase(context, sandboxBackingList);
            }

            return true;
        }

        /// <summary>
        /// Container for results of a DbCleanup operation.
        /// </summary>
        private class DbCleanupResults
        {
            public List<Exception> Exceptions { get; set; }
            public List<object> FailedEntities { get; set; }
        }

        private DbCleanupResults CleanUpTheDatabase(DbContext context, List<object> sandboxBackingList)
        {
            var exceptions = new List<Exception>();
            var previousExceptions = new List<Exception>();
            var sandboxedEntities = new List<object>(sandboxBackingList);

            return new DbCleanupResults
            {
                Exceptions = (exceptions.Count == 0) ? exceptions : previousExceptions, // prefer previousExceptions because extra changes can get cached in the entity graph that cause misleading exceptions after multiple passes
                FailedEntities = sandboxedEntities
            };
        }

        /// <summary>
        /// Gets all objects of the given type that exist in the annihilateList.
        /// </summary>
        /// <typeparam name="T">The type of objects to return</typeparam>
        /// <returns></returns>
        protected IEnumerable<T> GetSandboxEntities<T>()
        {
            var returnObject = (
                from item in annihilationList
                where item.GetType() == typeof(T)
                select (T)item
            );

            return returnObject;
        }

        /// <summary>
        /// Adds an entity object to the db context and the annihilateList.
        /// </summary>
        /// <typeparam name="T">An EF entity type</typeparam>
        /// <param name="sandboxEntity">
        /// An EF entity to add to the sandbox.
        /// </param>
        protected T AddSandboxEntity<T>(T sandboxEntity) where T : class, IBaseEntity
        {
            annihilationList.Add(sandboxEntity);
            DbContext.Set<T>().Add(sandboxEntity);
            return sandboxEntity;
        }

        /// <summary>
        /// Adds an entity object to the db context and the annihilateList.
        /// </summary>
        /// <typeparam name="T">An EF entity type</typeparam>
        /// <param name="sandboxEntities">
        /// Enumerable of EF entities to add to the sandbox.
        /// </param>
        protected IEnumerable<T> AddSandboxEntities<T>(IEnumerable<T> sandboxEntities) where T : class, IBaseEntity
        {

            DbContext.AddRange(sandboxEntities);
            return sandboxEntities;
        }

        /// <summary>
        /// Helper method for creating multiple sand boxed entities.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="quantity">The number of entities to create</param>
        /// <param name="entityFactory">A delegate that will make each entity</param>
        /// <returns></returns>
        protected IEnumerable<TEntity> MakeMultiple<TEntity>(int quantity, Func<int, TEntity> entityFactory)
            where TEntity : class, IBaseEntity
        {
            var entities = new List<TEntity>();

            for (int index = 1; index <= quantity; index++)
            {
                entities.Add(AddSandboxEntity(entityFactory(index)));
            }

            return entities;
        }

        /// <summary>
        /// Create a well formed connection string
        /// </summary>
        /// <param name="index">database index</param>
        /// <returns>connection string</returns>
        public static string DevelopmentConnectionString(int index) =>
            "Data Source=aix-aixdev.emp.state.or.us:1521/northwind_demo;" +
            "Persist Security Info=True;Enlist=false;Pooling=true;" +
            $"Statement Cache Size=10;User ID=northwind{index:D2};Password=\"!northwind!DEMO!\"";
    }
}
