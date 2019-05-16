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
    class MathTests
    {
        private Fundamentals.Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Fundamentals.Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnsSum()
        {
            // Arrange
            // var math = new Fundamentals.Math();
            // Act
            var result = _math.Add(1,2);
            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1,2,2)]
        [TestCase(2,1,2)]
        [TestCase(2,2,2)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(1, new int[] { 1 })]
        [TestCase(10, new int[] { 1, 3, 5, 7, 9 })]
        [TestCase(-10, new int[0])]
        public void GetOddNumbers_WhenCalled_ReturnsOddNumbersFrom0ToGivenValue(int givenValue, IEnumerable<int> expectedValue)
        {
            var result = _math.GetOddNumbers(givenValue);

            Assert.That(result, Is.EquivalentTo(expectedValue));
        }
    }
}
