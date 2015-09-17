using Microsoft.Data.Entity;
using _04_Services.Domain.Models;

namespace _04_Services.WebApi
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
