using System;

namespace TravelAgency
{
    public class Tour
    {
        public object Name { get; }
        public DateTime Date { get; }
        public int NrOfSeats { get; }

        public Tour(string name, DateTime date, int nrOfSeats)
        {
            Name = name;
            Date = date;
            NrOfSeats = nrOfSeats;
        }
    }
}