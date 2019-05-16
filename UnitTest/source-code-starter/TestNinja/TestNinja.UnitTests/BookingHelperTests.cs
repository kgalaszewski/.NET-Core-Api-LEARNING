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
    class BookingHelperTests
    {
        private Mock<IBookingStorage> _bookingStorage;
        private Booking myBooking;

        [SetUp]
        public void SetUp()
        {
            _bookingStorage = new Mock<IBookingStorage>();
            myBooking = new Booking();
        }

        [Test]
        public void OverlappingBookingsExist_WhenStatusCancelled_ReturnStringEmpty()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking() { Status = "Cancelled" }, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_WhenOverlappingBookingIsNull_ReturnStringEmpty()
        {
            _bookingStorage.Setup(bs => bs.GetActiveBookings(myBooking)).Returns(Enumerable.Empty<Booking>().AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(myBooking, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_WhenOverlappingBookingIsNotNull_ReturnOverlappingReference()
        {
            // Arrange
            myBooking = new Booking()
            {
                ArrivalDate = new DateTime(2014, 1, 1),
                DepartureDate = new DateTime(2014, 1, 1),
            };

            var overlappingBooking = new Booking()
            {
                ArrivalDate = new DateTime(2013, 1, 1),
                DepartureDate = new DateTime(2015, 1, 1),
                Reference = "anyReference"
            };

            _bookingStorage.Setup(bs => bs.GetActiveBookings(myBooking)).Returns(new List<Booking>() { overlappingBooking }.AsQueryable<Booking>());

            // Act
            var result = BookingHelper.OverlappingBookingsExist(myBooking, _bookingStorage.Object);

            //Assert
            Assert.That(result, Is.EqualTo(overlappingBooking.Reference));
        }
    }
}
