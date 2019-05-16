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
    class MyStackTests
    {
        private MyStack<int> _myStack;

        [SetUp]
        public void SetUp()
        {
            _myStack = new MyStack<int>();
        }

        [Test]
        public void CountProperty_WhenCalled_ReturnsNumberOfListElements()
        {
            // Act
            var result = _myStack.Count;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Push_WhenCalled_ListIsBigger(int obj)
        {
            int countBefore = _myStack.Count;
            _myStack.Push(obj);
            int countAfter = _myStack.Count;

            Assert.That(countAfter > countBefore);
        }

        [Test]
        [TestCase(null)]
        public void Push_NullGiven_ThrowsArgumentNullException(string obj)
        {
            // Arrange 
            MyStack<string> myStack = new MyStack<string>();

            // Act and Assert
            Assert.That(() => { myStack.Push(obj); }, Throws.ArgumentNullException);
        }

        [Test]
        public void Pop_WhenCalled_ListHasOneObjLess()
        {
            // Arrange
            _myStack.Push(2);

            // Act
            int countBefore = _myStack.Count;
            var result = _myStack.Pop();
            int countAfter = _myStack.Count;

            // Assert 
            Assert.That(countAfter < countBefore);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Pop_ListIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => { _myStack.Pop(); }, Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WhenCalled_ReturnsObjOfT()
        {
            _myStack.Push(0);
            var result = _myStack.Peek();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Peek_ListIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => { _myStack.Peek(); }, Throws.InvalidOperationException);
        }
    }
}
