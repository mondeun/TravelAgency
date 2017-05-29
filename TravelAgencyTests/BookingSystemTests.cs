using System;
using System.Collections.Generic;
using NUnit.Framework;
using TravelAgency;
using TravelAgency.Exceptions;

namespace TravelAgencyTests
{
    [TestFixture]
    public class BookingSystemTests
    {
        private TourScheduleStub _tourSchedule;
        private BookingSystem _sut;

        [SetUp]
        public void Setup()
        {
            _tourSchedule = new TourScheduleStub();
            _sut = new BookingSystem(_tourSchedule);
        }

        [Test]
        public void CanCreateBooking()
        {
            _tourSchedule.Tours = new List<Tour>
            {
                new Tour("a safari", new DateTime(2013, 1, 1), 1)
            };
            var passenger = new Passenger
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _sut.CreateBooking("a safari", new DateTime(2013, 1, 1), passenger);
            var bookings = _sut.GetBookingsFor(passenger);

            Assert.AreEqual(1, bookings.Count);
            Assert.AreEqual("a safari", bookings[0].Tour.Name);
            Assert.AreEqual("John", bookings[0].Passenger.FirstName);
            Assert.AreEqual(1, _tourSchedule.TimesGetToursForIsCalled.Count);
        }

        [Test]
        public void BookingNonexistentTourThrowsException()
        {
            Assert.Throws<TourAllocationException>(() => 
                _sut.CreateBooking("failing safari", new DateTime(2013, 1, 1), new Passenger())
            );
        }

        [Test]
        public void BookingFilledTourThrowsException()
        {
            _tourSchedule.Tours = new List<Tour>
            {
                new Tour("a safari", new DateTime(2013, 1, 1), 0)
            };

            Assert.Throws<InvalidSeatsException>(() => 
                _sut.CreateBooking("a safari", new DateTime(2013, 1, 1), new Passenger())
            );
        }
    }
}
