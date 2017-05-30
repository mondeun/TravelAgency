using System;

namespace TravelAgency.Exceptions
{
    internal class BookingException : Exception
    {
        public BookingException(string message) : base(message)
        {
        }
    }
}