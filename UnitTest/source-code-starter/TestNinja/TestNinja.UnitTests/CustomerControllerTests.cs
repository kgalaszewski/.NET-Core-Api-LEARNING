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
    class CustomerControllerTests
    {
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(0)]
        [TestCase(null)]
        public void GetCustomer_IdIsZero_ReturnsNotFound(int id)
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(id);

            Assert.That(result, Is.InstanceOf<ActionResult>());
        }
    }
}
