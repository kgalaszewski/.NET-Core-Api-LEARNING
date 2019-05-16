using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        [TestCase(3, "Fizz")]
        [TestCase(0, "FizzBuzz")]
        [TestCase(1,"1")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(5, "Buzz")]
        public void GetOutput_WhenCalled_ReturnFizzBuzzGameOutput(int number, string expectedOutput)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
