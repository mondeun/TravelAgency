using System;

namespace TravelAgency.Exceptions
{
    public class TourAllocationException : Exception
    {
        public DateTime? SuggestDate { get; }

        public TourAllocationException(DateTime? suggestedDate)
        {
            SuggestDate = suggestedDate;
        }
    }
}
