using System;

namespace TravelAgency.Exceptions
{
    public class InvalidSeatsException : Exception
    {
        public InvalidSeatsException(string message) : base(message)
        {
            
        }
    }
}
