using Learning2Test_Models;

namespace Learning2Test_DAL
{
    /// <summary>
    /// In-memory implementation of IDestinationRepository with hardcoded data.
    /// This can be replaced with a database implementation in the future.
    /// </summary>
    public class DestinationRepository : IDestinationRepository
    {
        private readonly List<DestinationCity> _cities;

        public DestinationRepository()
        {
            _cities = new List<DestinationCity>();
            InitializeData();
        }

        /// <summary>
        /// Initializes hardcoded destination data.
        /// In a future database implementation, this would be replaced with database queries.
        /// </summary>
        private void InitializeData()
        {
            var currentYear = DateTime.Now.Year;

            // French destinations
            _cities.Add(new DestinationCity("Paris", "France", 1, 45, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 8, 31), 195.00m));
            _cities.Add(new DestinationCity("Paris", "France", 1, 45, 0, 30, new DateTime(currentYear, 10, 1), new DateTime(currentYear, 12, 31), 95.00m));
            _cities.Add(new DestinationCity("Nice", "France", 2, 60, 0, 20, new DateTime(currentYear, 7, 1), new DateTime(currentYear, 9, 15), 110.00m));
            _cities.Add(new DestinationCity("Lyon", "France", 1, 50, 0, 40, new DateTime(currentYear, 5, 15), new DateTime(currentYear, 10, 1), 75.00m));
            _cities.Add(new DestinationCity("Marseille", "France", 2, 80, 1, 50, new DateTime(currentYear, 6, 15), new DateTime(currentYear, 9, 30), 85.00m));

            // German destinations
            _cities.Add(new DestinationCity("Berlin", "Germany", 1, 150, 0, 40, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 10, 1), 80.00m));
            _cities.Add(new DestinationCity("Munich", "Germany", 1, 45, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 9, 15), 105.00m));
            _cities.Add(new DestinationCity("Hamburg", "Germany", 2, 60, 0, 20, new DateTime(currentYear, 5, 15), new DateTime(currentYear, 9, 30), 90.00m));
            _cities.Add(new DestinationCity("Essen", "Germany", 1, 160, 0, 0, new DateTime(currentYear, 9, 15), new DateTime(currentYear, 11, 30), 142.00m));

            // Italian destinations
            _cities.Add(new DestinationCity("Rome", "Italy", 1, 165, 0, 40, new DateTime(currentYear, 4, 1), new DateTime(currentYear, 10, 31), 120.00m));
            _cities.Add(new DestinationCity("Venice", "Italy", 2, 145, 0, 20, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 9, 30), 135.00m));
            _cities.Add(new DestinationCity("Florence", "Italy", 1, 50, 0, 30, new DateTime(currentYear, 4, 15), new DateTime(currentYear, 10, 15), 100.00m));

            // Spanish destinations
            _cities.Add(new DestinationCity("Barcelona", "Spain", 2, 200, 1, 160, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 10, 31), 95.00m));
            _cities.Add(new DestinationCity("Madrid", "Spain", 1, 60, 0, 40, new DateTime(currentYear, 4, 1), new DateTime(currentYear, 11, 30), 85.00m));
            _cities.Add(new DestinationCity("Seville", "Spain", 2, 50, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 9, 30), 70.00m));
        }

        public List<DestinationCity> GetAllDestinations()
        {
            return _cities.ToList(); // Return a copy to prevent external modification
        }

        public DestinationCity? GetByName(string name)
        {
            return _cities.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<DestinationCity> GetByCountry(string country)
        {
            return _cities.Where(c => c.Country.Equals(country, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<string> GetAvailableCountries()
        {
            return _cities
                .Select(c => c.Country)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }
    }
}
