using System;
using Microsoft.Data.Entity;
using _04_Services.Domain.Models;
using _04_Services.Domain.Tests;
using DbContext = Microsoft.Data.Entity.DbContext;

namespace _04_Services.WebApi
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public void Seed()
        {
            var names =
                "Olga Roberson, Bob Simpson,Jackie Fletcher,Travis Pittman,Vera Rivera, Erin Turner,Leona Davis, Ralph Greene,Roger Ballard, Lula Rodgers,Raul Pierce, Doris Vaughn," +
                "Norma Hanson, Bernice Haynes,Marie Saunders, Deborah Roy,Cindy Smith, Christopher Powell,Drew Santos, Ruby Burton," +
                "Benny Phillips, Harriet Owens,Jay Carson, Doreen Copeland,Stacy Crawford, Jerald Sanders,Jennie Townsend, Patti Palmer," +
                "Tracy Hayes, Bobbie Christensen,Ignacio Alvarez, Colin Strickland,Kristy Nunez, Anna Holloway,Sherri Wade, Beth Curtis," +
                "Laura Potter, Tiffany Franklin,Lorenzo Cunningham, Calvin Brewer,Jean Young, Lionel Adkins,Gregory Pope, Miriam Hardy," +
                "Donnie Robinson, Mike Lowe,Lawrence Higgins, Sidney Reid,Candice Garner, Lois Ross,Marjorie Maldonado, Terri Ramirez,Ira West," +
                "Elias Waters,Charles Malone, Irene Hicks,Danny Hampton, Emma Reeves,Rose Riley, Elijah Colon,Grant Lyons," +
                "Sylvia Payne,Teresa Evans, Jessie Keller,Shannon Nichols, Steven Mullins,Terrell Parsons, Alma Morris,Rebecca Norton, Tanya Burns," +
                "Chris Washington, Keith Abbott,Lyle Mclaughlin, Lela Hart,Tony Rodriguez, Marc Cox,Erica Bishop, Mathew Campbell," +
                "Dan Elliott, Eddie Jones,Joseph Hunter, Courtney Webb,Jody Watson, Ismael Curry,Margie Wright, Bernadette Diaz,Salvatore Phelps," +
                "Kim Sandoval,Marilyn Guzman, Ollie Williamson,Megan Mcdaniel, Jerome Conner,Blanche Sims, Irvin Love,Ramona Lloyd," +
                "Evelyn Mason,Justin Collins, Sherman Green,Darrin Marsh, Tommy Becker";

            foreach (var fullName in names.Split(','))
            {
                Persons.Add(TestDomain.CreatePerson(fullName.Trim()));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
