using System;
using System.Collections.Generic;

namespace TravelAgency
{
    public interface ITourSchedule
    {
        void CreateTour(string name, DateTime date, int nrOfSeats);
        List<Tour> GetToursFor(DateTime date);
    }
}