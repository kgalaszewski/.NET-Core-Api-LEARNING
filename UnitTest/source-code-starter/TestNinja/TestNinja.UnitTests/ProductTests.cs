using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class ProductTests
    {
        Product _product;

        [SetUp]
        public void SetUp()
        {
            _product = new Product() { ListPrice = 10.0f };
        }

        [Test]
        [TestCase(true, 7.0f)]
        [TestCase(false, 10.0f)]
        public void GetPrice_WhenCalled_ReturnsCorrectPrice(bool _isGold, float expectedListPrice)
        {
            var customer = new Customer() { IsGold = _isGold };

            var result = _product.GetPrice(customer);

            Assert.That(result, Is.EqualTo(expectedListPrice));
        }
    }
}
