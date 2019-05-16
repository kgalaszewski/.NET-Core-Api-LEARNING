using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_Admin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();
            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            // Assert

            // 3 spososby zapisywania w NUnit
            // Assert.IsTrue(result);
            // Assert.That(result == true);
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_UserThatItWasMadeBy_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };
            // Act
            var result = reservation.CanBeCancelledBy(user);
            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUser_ReturnsFalse()
        {
            // Arrange
            var reservation = new Reservation();
            // Act
            var result = reservation.CanBeCancelledBy(new User());
            // Assert
            Assert.That(result, Is.False);
        }
    }
}
