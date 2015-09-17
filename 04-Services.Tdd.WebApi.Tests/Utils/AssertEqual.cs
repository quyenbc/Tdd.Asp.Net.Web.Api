using NUnit.Framework;

namespace _04_Services.Tdd.WebApi.Tests.Utils
{
    /// <summary>
    /// Extension for Assert on Domain Object 
    /// 
    /// Use this assertion only for domain Object without IEquatable Interface
    /// 
    /// For Object with IEquatable interface use Assert.AreEqual
    /// </summary>
    public class AssertDomainObject : Assert
    {
        /// <summary>
        /// Asserts that expected and actual objects are equal
        /// </summary>
        /// <remarks>Do not use with IEquatable objects</remarks>
        /// <typeparam name="TObject">generic object type</typeparam>
        /// <param name="expected">expected object</param>
        /// <param name="actual">actual object</param>
        public static void AreEqual<TObject>(TObject expected, TObject actual)
        {
            //use fluent assertion to assert that:
            // - all declared object properties are equal
            // - exclude Date* properties -> NHibernate cut off milliseconds
            //actual.ShouldBeEquivalentTo(expected);

            //actual.ShouldBeEquivalentTo(expected, options => options
            //    .Excluding(info => info.PropertyPath.Contains("Date"))
            //    .IgnoringCyclicReferences());

            //TODO: there is still space for improvement. Exclusion is done for All "Date" Property
            //so we should test them without milliseconds later on!


            //test the Date separately
            //NHibernate Pitfalls: DateTime Type Loses Milliseconds!
        }
    }
}
