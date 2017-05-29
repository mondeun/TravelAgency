using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Exceptions;

namespace TravelAgency
{
    public class BookingSystem
    {
        private readonly ITourSchedule _tourSchedule;
        private readonly List<Booking> _bookings;

        public BookingSystem(ITourSchedule tourSchedule)
        {
            _tourSchedule = tourSchedule;
            _bookings = new List<Booking>();
        }

        public void CreateBooking(string tourName, DateTime date, Passenger passenger)
        {
            var normalizedDate = date.Date;

            var tour = _tourSchedule.GetToursFor(normalizedDate).Find(x => x.Name.Equals(tourName));
            if (tour == null)
                throw new TourAllocationException("Tour does not exist");

            var numberOfSeatsTaken = _bookings.Count(x => x.Tour.Name.Equals(tourName) && x.Tour.Date == normalizedDate);
            if (numberOfSeatsTaken >= tour.NrOfSeats)
                throw new InvalidSeatsException("Cannot book a fully booked tour");

            _bookings.Add(new Booking(tour, passenger));
        }

        public List<Booking> GetBookingsFor(Passenger passenger)
        {
            return _bookings.Where(x => x.Passenger.FirstName == passenger.FirstName &&
                                        x.Passenger.LastName == passenger.LastName).ToList();
        }
    }
}