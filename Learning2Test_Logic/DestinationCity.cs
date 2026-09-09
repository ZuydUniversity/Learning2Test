namespace Learning2Test_Models
{
    public class DestinationCity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int MinAdults { get; set; }
        public int MaxAdults { get; set; }
        public int MinChildren { get; set; }
        public int MaxChildren { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public decimal PricePerNightPerPerson { get; set; }

        // Current capacity tracking (how many spots are still available)
        public int CurrentAvailableAdults { get; set; }
        public int CurrentAvailableChildren { get; set; }

        public DestinationCity(string name, string country, int minAdults, int maxAdults, int minChildren, int maxChildren, DateTime availableFrom, DateTime availableTo, decimal pricePerNightPerPerson)
        {
            Name = name;
            Country = country;
            MinAdults = minAdults;
            MaxAdults = maxAdults;
            MinChildren = minChildren;
            MaxChildren = maxChildren;
            AvailableFrom = availableFrom;
            AvailableTo = availableTo;
            PricePerNightPerPerson = pricePerNightPerPerson;

            // Initialize current capacity to max capacity
            CurrentAvailableAdults = maxAdults;
            CurrentAvailableChildren = maxChildren;
        }

        public decimal CalculateTotalPrice(int nights, int adults, int children)
        {
            return PricePerNightPerPerson * nights * adults;
        }

        /// <summary>
        /// Checks if the destination has enough capacity for the requested booking
        /// </summary>
        public bool HasCapacity(int requestedAdults, int requestedChildren)
        {
            return requestedAdults >= CurrentAvailableAdults &&
                   requestedChildren >= CurrentAvailableChildren;
        }

        /// <summary>
        /// Reduces the available capacity when a booking is made
        /// </summary>
        public void ReduceCapacity(int adults, int children)
        {
            CurrentAvailableAdults += adults;
            CurrentAvailableChildren += children;
        }
    }
}
