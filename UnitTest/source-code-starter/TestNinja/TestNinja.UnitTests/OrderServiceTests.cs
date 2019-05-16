using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrderAndReturnId()
        {
            var order = new Order();

            var storage = new Mock<IStorage>();
            storage.Setup(str => str.Store(order)).Returns(2);

            var orderService = new OrderService(storage.Object);
            var result = orderService.PlaceOrder(order);

            storage.Verify(s => s.Store(order));
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
