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
        }

        public decimal CalculateTotalPrice(int nights, int adults, int children)
        {
            int totalGuests = adults + children;
            return PricePerNightPerPerson * nights * totalGuests;
        }
    }
}
