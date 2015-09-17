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

        private static string GetPropertyValue(string prefixObject, int prefixTestCase, string propertyName)
        {
            return $"{prefixObject}.{prefixTestCase}.{propertyName}";
        }
    }
}