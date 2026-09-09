using Learning2Test_Models;

namespace Learning2Test_DAL
{
    /// <summary>
    /// In-memory implementation of IBookingRepository
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;
        private readonly IDestinationRepository _destinationRepository;
        private int _nextId = 1;

        public BookingRepository(IDestinationRepository destinationRepository)
        {
            _bookings = new List<Booking>();
            _destinationRepository = destinationRepository ?? throw new ArgumentNullException(nameof(destinationRepository));
        }

        public Booking CreateBooking(string bookerName, DateTime bookerBirthDate, DestinationCity destination,
            DateTime startDate, DateTime endDate, int numberOfAdults, int numberOfChildren, decimal totalPrice)
        {
            // Validate capacity
            if (!destination.HasCapacity(numberOfAdults, numberOfChildren))
            {
                throw new InvalidOperationException("Insufficient capacity for this booking");
            }

            // Create the booking
            var booking = new Booking(
                _nextId++,
                bookerName,
                bookerBirthDate,
                destination.Name,
                destination.Country,
                startDate,
                endDate,
                numberOfAdults,
                numberOfChildren,
                totalPrice
            );

            _bookings.Add(booking);

            // Reduce the destination's available capacity
            destination.ReduceCapacity(numberOfAdults, numberOfChildren);

            return booking;
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings.ToList();
        }

        public List<Booking> GetBookingsByDestination(string cityName, string country)
        {
            return _bookings
                .Where(b => b.CityName.Equals(cityName, StringComparison.OrdinalIgnoreCase) &&
                           b.Country.Equals(country, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Booking? GetBookingById(int id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }
    }
}
