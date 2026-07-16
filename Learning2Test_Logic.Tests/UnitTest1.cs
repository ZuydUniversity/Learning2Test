using NUnit.Framework;
using Learning2Test_Models;
using Learning2Test_DAL;

namespace Learning2Test_Models.Tests
{
    /// <summary>
    /// Unit tests for DestinationCity class
    /// </summary>
    [TestFixture]
    public class DestinationCityTests
    {
        [Test]
        public void Constructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var name = "Paris";
            var country = "France";
            var minAdults = 1;
            var maxAdults = 4;
            var minChildren = 0;
            var maxChildren = 3;
            var availableFrom = new DateTime(2025, 6, 1);
            var availableTo = new DateTime(2025, 8, 31);
            var price = 95.00m;

            // Act
            var city = new DestinationCity(name, country, minAdults, maxAdults, minChildren, maxChildren, availableFrom, availableTo, price);

            // Assert
            Assert.That(city.Name, Is.EqualTo(name));
            Assert.That(city.Country, Is.EqualTo(country));
            Assert.That(city.MinAdults, Is.EqualTo(minAdults));
            Assert.That(city.MaxAdults, Is.EqualTo(maxAdults));
            Assert.That(city.MinChildren, Is.EqualTo(minChildren));
            Assert.That(city.MaxChildren, Is.EqualTo(maxChildren));
            Assert.That(city.AvailableFrom, Is.EqualTo(availableFrom));
            Assert.That(city.AvailableTo, Is.EqualTo(availableTo));
            Assert.That(city.PricePerNightPerPerson, Is.EqualTo(price));
        }

        [Test]
        public void CalculateTotalPrice_WithTwoAdultsThreeNights_ReturnsCorrectPrice()
        {
            // Arrange
            var city = new DestinationCity("Paris", "France", 1, 4, 0, 3, 
                new DateTime(2025, 6, 1), new DateTime(2025, 8, 31), 100.00m);
            var nights = 3;
            var adults = 2;
            var children = 0;

            // Act
            var totalPrice = city.CalculateTotalPrice(nights, adults, children);

            // Assert
            // 100 * 3 nights * 2 adults = 600
            Assert.That(totalPrice, Is.EqualTo(600.00m));
        }

        [Test]
        public void CalculateTotalPrice_WithFamilyOfFour_ReturnsCorrectPrice()
        {
            // Arrange
            var city = new DestinationCity("Nice", "France", 2, 6, 0, 4, 
                new DateTime(2025, 7, 1), new DateTime(2025, 9, 15), 110.00m);
            var nights = 7;
            var adults = 2;
            var children = 2;

            // Act
            var totalPrice = city.CalculateTotalPrice(nights, adults, children);

            // Assert
            // 110 * 7 nights * 4 people = 3080
            Assert.That(totalPrice, Is.EqualTo(3080.00m));
        }

        [Test]
        public void CalculateTotalPrice_WithZeroNights_ReturnsZero()
        {
            // Arrange
            var city = new DestinationCity("Berlin", "Germany", 1, 5, 0, 4, 
                new DateTime(2025, 5, 1), new DateTime(2025, 10, 1), 80.00m);

            // Act
            var totalPrice = city.CalculateTotalPrice(0, 2, 1);

            // Assert
            Assert.That(totalPrice, Is.EqualTo(0.00m));
        }
    }

    /// <summary>
    /// Unit tests for HolidaySearch class
    /// </summary>
    [TestFixture]
    public class HolidaySearchTests
    {
        private HolidaySearch _holidaySearch;
        private Learning2Test_Models.IDestinationRepository _repository;
        private Learning2Test_Models.IBookingRepository _bookingRepository;

        [SetUp]
        public void SetUp()
        {
            _repository = new DestinationRepository();
            _bookingRepository = new BookingRepository(_repository);
            _holidaySearch = new HolidaySearch(_repository, _bookingRepository, DateTime.Today, DateTime.Today.AddDays(7), 2, 0);
        }

        [Test]
        public void Constructor_WithRepository_InitializesCorrectly()
        {
            // Assert
            Assert.That(_holidaySearch, Is.Not.Null);
            Assert.That(_holidaySearch.NumberOfAdults, Is.EqualTo(2));
            Assert.That(_holidaySearch.NumberOfChildren, Is.EqualTo(0));
        }

        [Test]
        public void Search_WithFranceSelected_ReturnsOnlyFrenchCities()
        {
            // Arrange
            var startDate = new DateTime(2025, 7, 1);
            var endDate = new DateTime(2025, 7, 8);
            var selectedCountries = new List<string> { "France" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available.All(c => c.Country == "France"), Is.True);
            Assert.That(unavailable.All(c => c.Country == "France"), Is.True);
        }

        [Test]
        public void Search_WithMultipleCountries_ReturnsMatchingCities()
        {
            // Arrange
            var startDate = new DateTime(2025, 6, 15);
            var endDate = new DateTime(2025, 6, 20);
            var selectedCountries = new List<string> { "France", "Germany" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert
            Assert.That(allResults.All(c => c.Country == "France" || c.Country == "Germany"), Is.True);
            Assert.That(allResults.Any(c => c.Country == "Italy" || c.Country == "Spain"), Is.False);
        }

        [Test]
        public void Search_WithValidDates_ReturnsAvailableCities()
        {
            // Arrange - Paris is available June 1 - Aug 31 in current year
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 1);
            var endDate = new DateTime(currentYear, 7, 8);
            var selectedCountries = new List<string> { "France" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available, Is.Not.Empty);
            Assert.That(available.Any(c => c.Name == "Paris"), Is.True);
        }

        [Test]
        public void Search_WithDatesOutsideRange_ReturnsUnavailableCities()
        {
            // Arrange - No French cities available in January, all have availability from April onwards
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 1, 1);
            var endDate = new DateTime(currentYear, 1, 8);
            var selectedCountries = new List<string> { "France" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert - All French cities should be unavailable in January
            Assert.That(unavailable.Count, Is.GreaterThan(0));
            Assert.That(available.Count, Is.EqualTo(0));
        }

        [Test]
        public void Search_WithTooManyAdults_ExcludesCities()
        {
            // Arrange - Munich max 45 adults, searching with 50
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 1);
            var endDate = new DateTime(currentYear, 7, 8);
            var selectedCountries = new List<string> { "Germany" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 50, 0, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert - Munich (max 45) should not appear, but Berlin (max 150) should
            Assert.That(allResults.Any(c => c.Name == "Munich"), Is.False);
            Assert.That(allResults.Any(c => c.Name == "Berlin"), Is.True);
        }

        [Test]
        public void Search_WithTooFewAdults_ExcludesCities()
        {
            // Arrange - Nice requires min 2 adults, searching with 1
            var startDate = new DateTime(2025, 7, 15);
            var endDate = new DateTime(2025, 7, 20);
            var selectedCountries = new List<string> { "France" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 1, 0, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert - Nice (min 2 adults) should not appear
            Assert.That(allResults.Any(c => c.Name == "Nice"), Is.False);
        }

        [Test]
        public void GetAvailableCountries_ReturnsDistinctSortedList()
        {
            // Act
            var countries = _holidaySearch.GetAvailableCountries();

            // Assert
            Assert.That(countries, Is.Not.Empty);
            Assert.That(countries, Is.Unique);
            Assert.That(countries, Is.Ordered);
            Assert.That(countries.Contains("France"), Is.True);
            Assert.That(countries.Contains("Germany"), Is.True);
            Assert.That(countries.Contains("Italy"), Is.True);
            Assert.That(countries.Contains("Spain"), Is.True);
        }

        [Test]
        public void Reset_RestoresDefaultValues()
        {
            // Arrange - modify properties
            _holidaySearch = new HolidaySearch(_repository, _bookingRepository, new DateTime(2025, 6, 1), new DateTime(2025, 6, 15), 4, 2);

            // Act
            _holidaySearch.Reset();

            // Assert
            Assert.That(_holidaySearch.StartDate, Is.EqualTo(DateTime.Today));
            Assert.That(_holidaySearch.EndDate, Is.EqualTo(DateTime.Today.AddDays(1)));
            Assert.That(_holidaySearch.NumberOfAdults, Is.EqualTo(1));
            Assert.That(_holidaySearch.NumberOfChildren, Is.EqualTo(0));
        }

        [Test]
        public void Search_WithNoCountriesSelected_ReturnsEmptyResults()
        {
            // Arrange
            var startDate = new DateTime(2025, 7, 1);
            var endDate = new DateTime(2025, 7, 8);
            var selectedCountries = new List<string>();

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available, Is.Empty);
            Assert.That(unavailable, Is.Empty);
        }

        [Test]
        public void Search_WithNonExistentCountry_ReturnsEmptyResults()
        {
            // Arrange
            var startDate = new DateTime(2025, 7, 1);
            var endDate = new DateTime(2025, 7, 8);
            var selectedCountries = new List<string> { "Australia" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available, Is.Empty);
            Assert.That(unavailable, Is.Empty);
        }

        [Test]
        public void Search_WithExcessiveGuestCount_ReturnsEmptyOrLimitedResults()
        {
            // Arrange - Cities support varying capacities, test with 250 adults which exceeds all
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 1);
            var endDate = new DateTime(currentYear, 7, 8);
            var selectedCountries = new List<string> { "France", "Germany", "Italy", "Spain" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 250, 0, selectedCountries);

            // Assert
            // No cities can handle 250 adults (max is Barcelona at 200)
            var totalResults = available.Count + unavailable.Count;
            Assert.That(totalResults, Is.EqualTo(0));
        }

        [Test]
        public void Search_WithChildrenExceedingMax_FiltersCitiesCorrectly()
        {
            // Arrange - Nice max children is 20, searching with 25
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 15);
            var endDate = new DateTime(currentYear, 7, 20);
            var selectedCountries = new List<string> { "France" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 25, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert - Nice (max 20 children) should not appear, only higher capacity cities
            Assert.That(allResults.Any(c => c.Name == "Nice"), Is.False);
            // Marseille (max 50 children) should appear
            Assert.That(allResults.Any(c => c.Name == "Marseille"), Is.True);
        }

        [Test]
        public void TotalGuests_CalculatesCorrectly()
        {
            // Arrange
            var bookingRepo = new BookingRepository(_repository);
            var search = new HolidaySearch(_repository, bookingRepo, DateTime.Today, DateTime.Today.AddDays(7), 3, 2);

            // Act & Assert
            Assert.That(search.TotalGuests, Is.EqualTo(5));
        }

        [Test]
        public void TotalNights_CalculatesCorrectly()
        {
            // Arrange
            var startDate = new DateTime(2025, 7, 1);
            var endDate = new DateTime(2025, 7, 8);
            var bookingRepo = new BookingRepository(_repository);
            var search = new HolidaySearch(_repository, bookingRepo, startDate, endDate, 2, 0);

            // Act & Assert
            Assert.That(search.TotalNights, Is.EqualTo(7));
        }
    }
}

