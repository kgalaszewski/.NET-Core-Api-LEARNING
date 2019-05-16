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
    class ErrorLoggerTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [TestCase("a")]
        public void Log_WhenValidArgumentIsGiven_LastErrorEqualsGivenArgument(string givenError)
        {
            _logger.Log(givenError);

            Assert.That(_logger.LastError.Equals("a"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Log_WhenInvalidArgumentIsGiven_ThrowsNewArgumentNullException(string givenError)
        {
            Assert.That(() => _logger.Log(givenError), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;

            _logger.ErrorLogged += (sender, args) => { id = args; };
            _logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
