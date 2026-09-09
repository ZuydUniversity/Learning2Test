namespace Learning2Test_Models
{
    /// <summary>
    /// Represents a holiday booking made by a customer
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }
        public string BookerName { get; set; }
        public DateTime BookerBirthDate { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(int id, string bookerName, DateTime bookerBirthDate, string cityName, string country, 
            DateTime startDate, DateTime endDate, int numberOfAdults, int numberOfChildren, decimal totalPrice)
        {
            Id = id;
            BookerName = bookerName;
            BookerBirthDate = bookerBirthDate;
            CityName = cityName;
            Country = country;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            TotalPrice = totalPrice;
            BookingDate = DateTime.Now;
        }

        public int TotalGuests => NumberOfAdults + NumberOfChildren;
        public int TotalNights => (EndDate - StartDate).Days;
    }
}
