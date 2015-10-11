using System;
using _04_Services.Domain.Models;

namespace _04_Services.Domain.Tests
{
    public class TestDomain
    {
        public static Person CreatePerson(int prefixTestCase, string prefixObject = "per")
        {
            return new Person
            {
                FirstName = GetPropertyValue(prefixObject, prefixTestCase, nameof(Person.FirstName)),
                LastName = GetPropertyValue(prefixObject, prefixTestCase, nameof(Person.LastName)),
                Age = (byte) prefixTestCase,
                Born = DateTime.Today
            };
        }

        public static Person CreatePerson(string name)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            var randomBornDate = DateTime.Today.AddDays(- random.Next(360 * 82));
            var names = name.Split(' ');

            return new Person
            {
                FirstName = names[0],
                LastName = names[1],
                Age = (byte)(DateTime.Today.Year - randomBornDate.Year),
                Born = randomBornDate,
            };
        }

        private static string GetPropertyValue(string prefixObject, int prefixTestCase, string propertyName)
        {
            return $"{prefixObject}.{prefixTestCase}.{propertyName}";
        }
    }
}