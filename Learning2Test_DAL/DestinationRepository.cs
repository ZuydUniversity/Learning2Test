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
            _cities.Add(new DestinationCity("Parijs", "Frankrijk", 1, 45, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 8, 31), 195.00m));
            _cities.Add(new DestinationCity("Parijs", "Frankrijk", 1, 45, 0, 30, new DateTime(currentYear, 10, 1), new DateTime(currentYear, 12, 31), 95.00m));
            _cities.Add(new DestinationCity("Nice", "Frankrijk", 2, 60, 0, 20, new DateTime(currentYear, 7, 1), new DateTime(currentYear, 9, 15), 110.00m));
            _cities.Add(new DestinationCity("Lyon", "Frankrijk", 1, 50, 0, 40, new DateTime(currentYear, 5, 15), new DateTime(currentYear, 10, 1), 75.00m));
            _cities.Add(new DestinationCity("Marseille", "Frankrijk", 2, 80, 1, 50, new DateTime(currentYear, 6, 15), new DateTime(currentYear, 9, 30), 85.00m));

            // German destinations
            _cities.Add(new DestinationCity("Berlijn", "Duitsland", 1, 150, 0, 40, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 10, 1), 80.00m));
            _cities.Add(new DestinationCity("Munchen", "Duitsland", 1, 45, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 9, 15), 105.00m));
            _cities.Add(new DestinationCity("Hamburg", "Duitsland", 2, 60, 0, 20, new DateTime(currentYear, 5, 15), new DateTime(currentYear, 9, 30), 90.00m));
            _cities.Add(new DestinationCity("Essen", "Duitsland", 1, 160, 0, 0, new DateTime(currentYear, 9, 15), new DateTime(currentYear, 11, 30), 142.00m));

            // Italian destinations
            _cities.Add(new DestinationCity("Rome", "Italië", 1, 165, 0, 40, new DateTime(currentYear, 4, 1), new DateTime(currentYear, 10, 31), 120.00m));
            _cities.Add(new DestinationCity("Venetië", "Italië", 2, 145, 0, 20, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 9, 30), 135.00m));
            _cities.Add(new DestinationCity("Florence", "Italië", 1, 50, 0, 30, new DateTime(currentYear, 4, 15), new DateTime(currentYear, 10, 15), 100.00m));

            // Spanish destinations
            _cities.Add(new DestinationCity("Barcelona", "Spanje", 2, 200, 1, 160, new DateTime(currentYear, 5, 1), new DateTime(currentYear, 10, 31), 95.00m));
            _cities.Add(new DestinationCity("Madrid", "Spanje", 1, 60, 0, 40, new DateTime(currentYear, 4, 1), new DateTime(currentYear, 11, 30), 85.00m));
            _cities.Add(new DestinationCity("Seville", "Spanje", 2, 50, 0, 30, new DateTime(currentYear, 6, 1), new DateTime(currentYear, 9, 30), 70.00m));
        }

        public List<DestinationCity> GetAllDestinations()
        {
            List<DestinationCity> _temp = _cities.ToList();
            _temp.Add(new DestinationCity("MoonBase1", "Moon", 1, 5000, 0, 3000, new DateTime(1998, 6, 1), new DateTime(2998, 9, 30), 70.00m));
            return _temp; //_cities.ToList();
        }

        public DestinationCity? GetByNameAndCountry(string name, string country)
        {
            return _cities.FirstOrDefault(c => 
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                c.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
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
            // Use GetAllDestinations() so any transient additions (like Moon) are included
            return GetAllDestinations()
                .Select(c => c.Country)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }
    }
}
