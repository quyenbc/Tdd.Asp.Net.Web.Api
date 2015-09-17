using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using _04_Services.Domain.Models;

namespace _04_Services.WebApi.Controllers
{
    /// <summary>
    ///   C  HTTP POST maps to Create
    ///   R  HTTP GET maps to Read
    ///   U  HTTP PUT maps to Update
    ///   D  HTTP DELETE maps to Delete

    ///    The service we’ll be building here supports the following API(the CRUD operations)

    ///Operation HTTP Method Relative URI
    ///     Get all members             |GET	    |   /api/members
    ///     Create a new member         |POST       |   /api/members/{object}
    ///     Get member                  |GET        |   /api/members/{:Id}
    ///     Update member               |PUT	    |   /api/members/{object}
    ///     Delete member               |DELETE	    |   /api/members/{object}
    /// </summary>
    public class PersonController : ApiController
    {
        /// <summary>
        /// Crud - Create a new Person and save to Db and return he saved object
        /// </summary>
        /// <returns>saved object</returns>
        [HttpPost]
        public HttpResponseMessage Create(Person person)
        {
            if (person == null)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.NoContent, "Invalid object!");
            }

            using (var dbContext = new PersonContext())
            {
                dbContext.Persons.Add(person);
                dbContext.SaveChanges();
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, person);
        }

        /// <summary>
        /// cRud - Read the Persons from Db and return
        /// </summary>
        /// <returns>Person list</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (var dbContext = new PersonContext())
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, dbContext.Persons.ToList());
            }
        }

        /// <summary>
        /// cRud - Read the Person from Db and return
        /// </summary>
        /// <returns>Person by Id</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            using (var dbContext = new PersonContext())
            {
                var result = dbContext.Persons.FirstOrDefault(p => p.Id == id);

                if (result == null)
                {
                    this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "requested person not found!");
                }

                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// crUd - Update the Person and save to Db and return the saved object
        /// </summary>
        /// <returns>saved object</returns>
        [HttpPut]
        public HttpResponseMessage Update(Person person)
        {
            if (person == null)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.NoContent, "requested person is invalid!");
            }

            using (var dbContext = new PersonContext())
            {
                var personToUpdate = dbContext.Persons.SingleOrDefault(p => p.Id == person.Id);
                if (personToUpdate == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "requested person not found!");
                }

                Mapper.CreateMap<Person, Person>();
                Mapper.Map(person, personToUpdate);
                dbContext.SaveChanges();

                return this.Request.CreateResponse(HttpStatusCode.OK, personToUpdate);
            }

            
        }

        /// <summary>
        /// cruD - Delete the Person from Db
        /// </summary>
        /// <returns>http ok</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            using (var dbContext = new PersonContext())
            {
                var person = dbContext.Persons.FirstOrDefault(p => p.Id == id);

                if (person == null)
                {
                   return  this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "requested person not found!");
                }
                dbContext.Persons.Remove(person);
                dbContext.SaveChanges();
                
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
