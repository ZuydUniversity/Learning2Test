namespace Learning2Test_Models
{
    /// <summary>
    /// Repository interface for managing bookings
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Creates a new booking
        /// </summary>
        /// <returns>The created booking with assigned ID</returns>
        Booking CreateBooking(string bookerName, DateTime bookerBirthDate, DestinationCity destination, 
            DateTime startDate, DateTime endDate, int numberOfAdults, int numberOfChildren, decimal totalPrice);

        /// <summary>
        /// Gets all bookings
        /// </summary>
        List<Booking> GetAllBookings();

        /// <summary>
        /// Gets bookings for a specific destination
        /// </summary>
        List<Booking> GetBookingsByDestination(string cityName, string country);

        /// <summary>
        /// Gets a booking by ID
        /// </summary>
        Booking? GetBookingById(int id);
    }
}
