using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using _04_Services.Tdd.WebApi.Tests.Utils;
using _04_Services.WebApi.Models;

//using _04_Services.WebApi;

namespace _04_Services.Tdd.WebApi.Tests
{
    [TestFixture]
    public class JournalControllerTests
    {
        [Test]
        public void Get_EmptyRequest_ResponseIsSuccess()
        {
            //arrange
            using (var client = TestHttpClientFactory.Create(nameof(Person)))
            {
                //act
                var response = client.GetAsync("").Result;

                //assert
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
            }
        }

        [Test]
        public void Post_DummyObject_ResponseIsNoContent()
        {
            string json = JsonConvert.SerializeObject(new {dummy = "post test"});

            //arrange
            using (var client = TestHttpClientFactory.Create(nameof(Person)))
            {
                //act
                var response = client.PostAsJsonAsync("", json).Result;

                //assert
                Assert.That(response.StatusCode == HttpStatusCode.NoContent, Is.True, "response:" + response);
            }

        }

        [Test]
        public async void Post_JsonJournalEntryModelObject_ResponseIsSuccess()
        {
            var journalEntryModel = new JournalEntryModel()
            {
                  Distance = 3000,
                  Duration = new TimeSpan(1,25,56),
                  Time = new DateTimeOffset(DateTime.Today),
            //    Born = DateTime.Today,
            };

            //string json2 = JsonConvert.SerializeObject(journalEntryModel);

            var json = new
            {
                time = DateTimeOffset.Now,
                distance = 8500,
                duration = TimeSpan.FromMinutes(44)
            };

            //arrange
            using (var client = TestHttpClientFactory.Create("JournalController"))
            {
                //act
                // var response = client.PostAsJsonAsync().Result;
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                //2. Accept headers in the request
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response =  client.PostAsJsonAsync("", journalEntryModel).Result;
               // var returnPerson = await response.Content.ReadAsAsync<JournalEntryModel>();

                

                //assert
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
                //Assert.That(returnPerson.Id, Is.GreaterThan(0));
            }

        }


        [Test]
        public async void Post_XmlJournalEntryModelObject_ResponseIsSuccess()
        {
            var journalEntryModel = new JournalEntryModel()
            {
                Distance = 3000,
                Duration = new TimeSpan(1, 25, 56),
                Time = new DateTimeOffset(DateTime.Today),
                //    Born = DateTime.Today,
            };

            string json = JsonConvert.SerializeObject(journalEntryModel);

            //arrange
            using (var client = TestHttpClientFactory.Create("JournalController"))
            {
                //act
                // var response = client.PostAsJsonAsync().Result;
                client.DefaultRequestHeaders.Accept.ParseAdd("application/xml");


                var response = await client.PostAsJsonAsync("", json);
                var returnPerson = await response.Content.ReadAsAsync<JournalEntryModel>();



                //assert
                Assert.That(response.IsSuccessStatusCode, Is.True, "response:" + response);
                //Assert.That(returnPerson.Id, Is.GreaterThan(0));
            }

        }
    }
}