using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency;

namespace TravelAgencyTests
{
    public class TourScheduleStub : ITourSchedule
    {
        public List<Tour> Tours;
        public List<DateTime> TimesGetToursForIsCalled;

        public TourScheduleStub()
        {
            Tours = new List<Tour>();
            TimesGetToursForIsCalled = new List<DateTime>();
        }

        public void CreateTour(string name, DateTime date, int nrOfSeats)
        {
        }

        public List<Tour> GetToursFor(DateTime date)
        {
            TimesGetToursForIsCalled.Add(date);

            return Tours;
        }
    }
}
