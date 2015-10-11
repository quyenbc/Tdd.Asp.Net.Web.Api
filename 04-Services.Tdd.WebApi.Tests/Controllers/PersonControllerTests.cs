using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using _04_Services.Domain.Models;
using _04_Services.Domain.Tests;
using _04_Services.Tdd.WebApi.Tests.Utils;

namespace _04_Services.Tdd.WebApi.Tests.Controllers
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
    [TestFixture]
    public class PersonControllerTests : InMemoryDatabaseTests
    {
        /// <summary>
        /// POST => PostAsJsonAsync
        /// </summary>
        [Test]
        public void Create_DummyObject_ResponseIsNotFound()
        {
            var json = JsonConvert.SerializeObject(new { dummy = "post test" });

            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                var personApiUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/create");

                //act
                var response = client.PostAsJsonAsync(personApiUri, json).Result;

                //assert
                Assert.That(response.StatusCode == HttpStatusCode.NoContent, Is.True, "response:" + response);
            }
        }

        /// <summary>
        /// POST => PostAsJsonAsync
        /// </summary>
        [Test]
        public void Create_Person_ResponseIsSuccess()
        {
            var person = TestDomain.CreatePerson(100);

            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                var personApiUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/Create" );

                //act
                var response = client.PostAsJsonAsync(personApiUri, person).Result;
                var returnPerson = response.Content.ReadAsAsync<Person>().Result;

                //assert
                returnPerson.ShouldBeEquivalentTo(person);
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
                Assert.That(returnPerson.Id, Is.GreaterThan(0));

            }
        }

        /// <summary>
        /// POST => PostAsJsonAsync
        /// </summary>
        [Test]
        public void Create_XmlPerson_ResponseIsSuccess()
        {
            var person = TestDomain.CreatePerson(100);

            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Accept.ParseAdd("application/xml");

                var personApiUri = new Uri(client.BaseAddress, "api/" + nameof(Person));

                //act
                var response = client.PostAsJsonAsync(personApiUri, person).Result;
                var returnPerson = response.Content.ReadAsAsync<Person>().Result;

                //assert
                returnPerson.ShouldBeEquivalentTo(person);
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
                Assert.That(returnPerson.Id, Is.GreaterThan(0));

            }
        }
        
        /// <summary>
        /// GET => GetAsync
        /// </summary>
        [Test]
        public void Read_EmptyRequest_ResponseIsSuccess()
        {
            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                //act
                var newUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/Read/");

                var response = client.GetAsync(newUri).Result;

                //assert
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
            }
        }
        
        /// <summary>
        /// GET => GetAsync
        /// </summary>
        [Test]
        public void Read_Person_ResponseIsSuccess()
        {
            //arrange
            var person = TestDomain.CreatePerson(100);
            PersonContext.Persons.AddRange(person);
            PersonContext.SaveChanges();

            using (var client = TestHttpClientFactory.Create())
            {
                var personApiGetByIdUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/Read/{person.Id}");

                //act
                var responseFromGet = client.GetAsync(personApiGetByIdUri).Result;
                var returnPersonFromGet = responseFromGet.Content.ReadAsAsync<Person>().Result;

                //assert
                returnPersonFromGet.ShouldBeEquivalentTo(person);
                Assert.That(responseFromGet.IsSuccessStatusCode, Is.True, "response:" + responseFromGet);
            }
        }

        /// <summary>
        /// GET => GetAsync
        /// </summary>
        [Test]
        public void Read_Persons_ResponseIsSuccess()
        {
            //arrange
            var persons = new List<Person>();
            for (int i = 1; i <= 10; i++)
            {
                persons.Add(TestDomain.CreatePerson(i));
            }

            PersonContext.Persons.AddRange(persons);
            PersonContext.SaveChanges();

            using (var client = TestHttpClientFactory.Create())
            {
                var personApiUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/Read");

                //act
                var responseFromGet = client.GetAsync(personApiUri).Result;
                var returnPersonsFromGet = responseFromGet.Content.ReadAsAsync<List<Person>>().Result;

                //assert
                returnPersonsFromGet.ShouldBeEquivalentTo(persons);
                Assert.That(responseFromGet.IsSuccessStatusCode, Is.True, "response:" + responseFromGet);
            }
        }

        /// <summary>
        /// GET => GetAsync
        /// </summary>
        [Test]
        public void Seed_Persons_ResponseIsSuccess()
        {
            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                var personApiUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/Seed");

                //act
                var responseFromGet = client.GetAsync(personApiUri).Result;
                var returnPersonsFromGet = responseFromGet.Content.ReadAsAsync<List<Person>>().Result;

                //assert
                Assert.That(returnPersonsFromGet.Count(), Is.EqualTo(100));
                Assert.That(responseFromGet.IsSuccessStatusCode, Is.True, "response:" + responseFromGet);
            }
        }

        /// <summary>
        /// PUT => PutAsJsonAsync
        /// </summary>
        [Test]
        public void Update_DummyObject_ResponseNotFound()
        {
            //arrange
            var dummyObject = JsonConvert.SerializeObject(new { dummy = "post test" });

            using (var client = TestHttpClientFactory.Create())
            {
                var personApiGetByIdUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}");

                //act
                var response = client.PutAsJsonAsync(personApiGetByIdUri, dummyObject).Result;

                //assert
                Assert.That(response.StatusCode == HttpStatusCode.NoContent, "response:" + response);
            }
        }

        /// <summary>
        /// PUT => PutAsJsonAsync
        /// </summary>
        [Test]
        public void Update_Person_ResponseIsSuccess()
        {
            //arrange
            var person = TestDomain.CreatePerson(1);
            PersonContext.Persons.AddRange(person);
            PersonContext.SaveChanges();

          
            using (var client = TestHttpClientFactory.Create())
            {
                var personApiUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}");

                //act
                //change the person properties
                person.FirstName = "John";
                person.LastName = "Doe";
                person.Age = 99;
                person.Born = DateTime.Today.AddYears(-99);

                var response = client.PutAsJsonAsync(personApiUri, person).Result;
                var returnPerson = response.Content.ReadAsAsync<Person>().Result;

                //assert
                returnPerson.ShouldBeEquivalentTo(person);
                Assert.That(PersonContext.Persons.Any(), Is.True);
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
            }
        }

        /// <summary>
        /// DELETE => DeleteAsync
        /// </summary>
        [Test]
        public void Delete_Person_ResponseIsSuccess()
        {
            //arrange
            var person = TestDomain.CreatePerson(1);
            PersonContext.Persons.Add(person);
            PersonContext.SaveChanges();

            using (var client = TestHttpClientFactory.Create())
            {
                var personApiGetByIdUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/delete/{person.Id}");

                //act
                var response = client.DeleteAsync(personApiGetByIdUri).Result;

                //assert
                Assert.That(PersonContext.Persons.Any(), Is.False);
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
            }
        }

        /// <summary>
        /// DELETE => DeleteAsync
        /// </summary>
        [Test]
        public void Delete_PersonInValid_ResponseIsNotFound()
        {
            //arrange
            using (var client = TestHttpClientFactory.Create())
            {
                var personApiGetByIdUri = new Uri(client.BaseAddress, $"api/{nameof(Person)}/{999}");

                //act
                var response = client.DeleteAsync(personApiGetByIdUri).Result;

                //assert
                //assert
                Assert.That(response.StatusCode == HttpStatusCode.NotFound, Is.True, "response:" + response);

            }
        }
    }
}