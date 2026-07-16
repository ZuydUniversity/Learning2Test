using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning2Test_Models
{
    /// <summary>
    /// Business logic for holiday search functionality.
    /// Uses repository pattern for data access.
    /// </summary>
    public class HolidaySearch
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int NumberOfAdults { get; private set; }
        public int NumberOfChildren { get; private set; }
        public int TotalGuests { get { return NumberOfAdults + NumberOfChildren; }} 
        public int TotalNights { get { return (EndDate - StartDate).Days; }}
        public List<DestinationCity> available { get; private set; }
        public List<DestinationCity> unavailable { get; private set; }

        /// <summary>
        /// Repository for accessing destination data.
        /// </summary>
        private readonly IDestinationRepository _repository;
        
        /// <summary>
        /// Creates a new HolidaySearch instance with dependency injection
        /// </summary>
        /// <param name="repository">Data repository for destinations</param>
        /// <param name="startDate">Search start date</param>
        /// <param name="endDate">Search end date</param>
        /// <param name="adults">Number of adults</param>
        /// <param name="children">Number of children</param>
        public HolidaySearch(IDestinationRepository repository, DateTime startDate, DateTime endDate, int adults, int children)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            StartDate = startDate;
            EndDate = endDate;
            NumberOfAdults = adults;
            NumberOfChildren = children;
        }

        /// <summary>
        /// Searches for available and unavailable destinations based on criteria
        /// </summary>
        public (List<DestinationCity> available, List<DestinationCity> unavailable) Search(
            DateTime startDate, DateTime endDate, int adults, int children, List<string> selectedCountries)
        {
            var allCities = _repository.GetAllDestinations();

            List<DestinationCity> filtered = allCities
                .Where(c =>
                    selectedCountries.Contains(c.Country) &&
                    adults >= c.MinAdults && adults <= c.MaxAdults &&
                    children >= c.MinChildren && children <= c.MaxChildren)
                .ToList();

            available = filtered
                .Where(c => startDate >= c.AvailableFrom && endDate <= c.AvailableTo)
                .ToList();

            unavailable = filtered
                .Where(c => !(startDate >= c.AvailableFrom && endDate <= c.AvailableTo))
                .ToList();

            return (available, unavailable);
        }

        /// <summary>
        /// Gets all unique countries from the repository
        /// </summary>
        public List<string> GetAvailableCountries()
        { 
            return _repository.GetAvailableCountries();
        }

        /// <summary>
        /// Resets search parameters to default values
        /// </summary>
        public void Reset()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(1);
            NumberOfAdults = 1;
            NumberOfChildren = 0;
        }
    }
}

