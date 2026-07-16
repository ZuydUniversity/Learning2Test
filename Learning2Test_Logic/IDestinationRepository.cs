using System.Collections.Generic;

namespace Learning2Test_Models
{
    /// <summary>
    /// Repository interface for accessing destination data.
    /// Implementations can use in-memory data, databases, or external APIs.
    /// </summary>
    public interface IDestinationRepository
    {
        /// <summary>
        /// Gets all available destination cities.
        /// </summary>
        /// <returns>List of all destination cities in the system.</returns>
        List<DestinationCity> GetAllDestinations();

        /// <summary>
        /// Gets all unique countries from available destinations.
        /// </summary>
        /// <returns>Distinct list of country names, sorted alphabetically.</returns>
        List<string> GetAvailableCountries();
    }
}
