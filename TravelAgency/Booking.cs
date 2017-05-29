namespace TravelAgency
{
    public class Booking
    {
        public Tour Tour { get; }
        public Passenger Passenger { get; }

        public Booking(Tour tour, Passenger passenger)
        {
            Tour = tour;
            Passenger = passenger;
        }
    }
}