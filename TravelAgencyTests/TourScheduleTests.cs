using System;
using System.Collections.Generic;
using NUnit.Framework;
using TravelAgency;
using TravelAgency.Exceptions;

namespace TravelAgencyTests
{
    [TestFixture]
    public class TourScheduleTests
    {
        private TourSchedule _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TourSchedule();
        }

        [Test]
        public void CanCreateNewTour()
        {
            _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1), 20);
            var tours = _sut.GetToursFor(new DateTime(2013, 1, 1));

            Assert.AreEqual(1, tours.Count);
            Assert.AreEqual("New years day safari", tours[0].Name);
            Assert.AreEqual(20, tours[0].NrOfSeats);
        }

        [Test]
        public void ToursAreScheduledByDateOnly()
        {
            _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20);
            var tours = _sut.GetToursFor(new DateTime(2013, 1, 1));

            Assert.AreEqual(1, tours.Count);
        }

        [Test]
        public void GetToursForGivenDayOnly()
        {
            _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20);
            _sut.CreateTour("New years day safari", new DateTime(2014, 1, 13, 10, 15, 0), 20);
            _sut.CreateTour("New years day safari", new DateTime(2015, 2, 1, 10, 15, 0), 20);
            var tours = _sut.GetToursFor(new DateTime(2013, 1, 1));

            Assert.AreEqual(1, tours.Count);
        }

        [Test]
        public void OverbookingIsNotAloud()
        {
            _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20);
            _sut.CreateTour("Travel the north", new DateTime(2013, 1, 1, 10, 15, 0), 20);
            _sut.CreateTour("Holidays special", new DateTime(2013, 1, 1, 10, 15, 0), 20);

            var e = Assert.Throws<TourAllocationException>(() =>
                _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20)
            );
            Assert.AreEqual(new DateTime(2013, 1, 2), e.SuggestDate);
        }

        [Test]
        public void CannotBookSameTourOnSameDate()
        {
            _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20);

            Assert.Throws<TourAllocationException>(() =>
                _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), 20)
            );
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IvalidSeatsThrowException(int seats)
        {
            Assert.Throws<InvalidSeatsException>(() =>
                _sut.CreateTour("New years day safari", new DateTime(2013, 1, 1, 10, 15, 0), seats)
            );
        }

        [Test]
        public void GetBookingsForUnbookedDate()
        {
            var tours = _sut.GetToursFor(new DateTime(2013, 1, 2));

            Assert.AreEqual(0, tours.Count);
        }
    }
}
