using System.Linq;
using NUnit.Framework;
using _04_Services.WebApi;

namespace _04_Services.Tdd.WebApi.Tests.Utils
{
    public class InMemoryDatabaseTests
    {
        /// <summary>
        ///     delete all persons before we start to work with context
        /// </summary>
        [SetUp]
        public void CleanDB()
        {
            if (PersonContext.Persons.Any())
            {
                PersonContext.Persons.RemoveRange(PersonContext.Persons.ToList());
                //dbContext.Persons.ForEachAsync(x => dbContext.Persons.Remove(x));
                PersonContext.SaveChanges();
            }
        }

        protected PersonContext PersonContext { get; private set; }

        public InMemoryDatabaseTests()
        {
            PersonContext = new PersonContext();
        }

        [TestFixtureTearDown]
        public void DisposeContext()
        {
            PersonContext.Dispose();
            PersonContext = null;
        }
    }
}