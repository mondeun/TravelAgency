using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Exceptions;

namespace TravelAgency
{
    public class TourSchedule
    {
        private readonly List<Tour> _tours;

        public TourSchedule()
        {
            _tours = new List<Tour>();
        }
        
        public void CreateTour(string name, DateTime date, int nrOfSeats)
        {
            if (nrOfSeats <= 0)
                throw new InvalidSeatsException("Cannot book a tour without seats");

            var normalizedDate = date.Date;

            if (_tours.Any(x => x.Date == normalizedDate && x.Name.Equals(name)))
                throw new TourAllocationException(SuggestTourDate(normalizedDate));

            if (_tours.Count(x => x.Date == normalizedDate) >= 3)
                throw new TourAllocationException(SuggestTourDate(normalizedDate));

            _tours.Add(new Tour(name, normalizedDate, nrOfSeats));
        }

        public List<Tour> GetToursFor(DateTime date)
        {
            return _tours.Where(x => x.Date == date).ToList();
        }

        private DateTime? SuggestTourDate(DateTime tried, int tries = 30)
        {
            while (tries > 0)
            {
                var suggestedDate = tried.AddDays(1);

                if (_tours.Count(x => x.Date == suggestedDate) < 3)
                    return suggestedDate;
                tries -= 1;
            }

            return null;
        }
    }
}