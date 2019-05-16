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
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _DemeritPointsCalculator;

        [SetUp]
        public void SetUp()
        {
            _DemeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(65, 0)]
        [TestCase(69, 0)]
        [TestCase(55, 0)]
        [TestCase(75, 2)]
        [TestCase(105, 8)]
        [TestCase(0, 0)]
        public void CalculateDemeritPoints_WhenCalledWithValueGreaterThanZero_ReturnsValidValueOfDPts(int givenSpeed, int expectedPoints)
        {
            var result = _DemeritPointsCalculator.CalculateDemeritPoints(givenSpeed);

            Assert.That(result, Is.EqualTo(expectedPoints));
        }

        [Test]
        [TestCase(-2)]
        [TestCase(-1)]
        public void CalculateDemeritPoints_SpeedIsNegative_ThrowsAOORE(int givenSpeed)
        {
            Assert.That(() => { _DemeritPointsCalculator.CalculateDemeritPoints(givenSpeed); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}
