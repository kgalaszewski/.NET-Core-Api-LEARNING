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
    class HtmlFormatterTests
    {
        [Test]
        [TestCase("","<strong></strong>")]
        [TestCase("a","<strong>a</strong>")]
        [TestCase("asd", "<strong>asd</strong>")]
        public void FormatAsBold_WhenCalled_ReturnsStrongString(string givenString, string outputString)
        {
            var htmlFormatter = new HtmlFormatter();

            var result = htmlFormatter.FormatAsBold(givenString);

            Assert.That(result, Is.EqualTo(outputString));
        }
    }
}
