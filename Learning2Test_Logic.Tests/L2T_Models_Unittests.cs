using Learning2Test_DAL;
using Learning2Test_V2;
using System.Collections;
using System.Reflection;

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
            var country = "Frankrijk";
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
            var city = new DestinationCity("Parijs", "Frankrijk", 1, 4, 0, 3, 
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
            var city = new DestinationCity("Nice", "Frankrijk", 2, 6, 0, 4, 
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
            var city = new DestinationCity("Berlijn", "Duitsland", 1, 5, 0, 4, 
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
            var selectedCountries = new List<string> { "Frankrijk" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available.All(c => c.Country == "Frankrijk"), Is.True);
            Assert.That(unavailable.All(c => c.Country == "Frankrijk"), Is.True);
        }

        [Test]
        public void Search_WithMultipleCountries_ReturnsMatchingCities()
        {
            // Arrange
            var startDate = new DateTime(2025, 6, 15);
            var endDate = new DateTime(2025, 6, 20);
            var selectedCountries = new List<string> { "Frankrijk", "Duitsland" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert
            Assert.That(allResults.All(c => c.Country == "Frankrijk" || c.Country == "Duitsland"), Is.True);
            Assert.That(allResults.Any(c => c.Country == "Italië" || c.Country == "Spanje"), Is.False);
        }

        [Test]
        public void Search_WithValidDates_ReturnsAvailableCities()
        {
            // Arrange - Paris is available June 1 - Aug 31 in current year
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 1);
            var endDate = new DateTime(currentYear, 7, 8);
            var selectedCountries = new List<string> { "Frankrijk" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert
            Assert.That(available, Is.Not.Empty);
            Assert.That(available.Any(c => c.Name == "Parijs"), Is.True);
        }

        [Test]
        public void Search_WithDatesOutsideRange_ReturnsUnavailableCities()
        {
            // Arrange - No French cities available in January, all have availability from April onwards
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 1, 1);
            var endDate = new DateTime(currentYear, 1, 8);
            var selectedCountries = new List<string> { "Frankrijk" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 2, 0, selectedCountries);

            // Assert - All French cities should be unavailable in January
            Assert.That(unavailable.Count, Is.GreaterThan(0));
            Assert.That(available.Count, Is.EqualTo(0));
        }

        [Test]
        public void Search_WithTooManyAdults_ExcludesCities()
        {
            // Arrange - Munchen max 45 adults, searching with 50
            var currentYear = DateTime.Now.Year;
            var startDate = new DateTime(currentYear, 7, 1);
            var endDate = new DateTime(currentYear, 7, 8);
            var selectedCountries = new List<string> { "Duitsland" };

            // Act
            var (available, unavailable) = _holidaySearch.Search(startDate, endDate, 50, 0, selectedCountries);
            var allResults = available.Concat(unavailable).ToList();

            // Assert - Munchen (max 45) should not appear, but Berlijn (max 150) should
            Assert.That(allResults.Any(c => c.Name == "Munchen"), Is.False);
            Assert.That(allResults.Any(c => c.Name == "Berlijn"), Is.True);
        }

        [Test]
        public void Search_WithTooFewAdults_ExcludesCities()
        {
            // Arrange - Nice requires min 2 adults, searching with 1
            var startDate = new DateTime(2025, 7, 15);
            var endDate = new DateTime(2025, 7, 20);
            var selectedCountries = new List<string> { "Frankrijk" };

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
            Assert.That(countries.Contains("Frankrijk"), Is.True);
            Assert.That(countries.Contains("Duitsland"), Is.True);
            Assert.That(countries.Contains("Italië"), Is.True);
            Assert.That(countries.Contains("Spanje"), Is.True);
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
            var selectedCountries = new List<string> { "Australië" };

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
            var selectedCountries = new List<string> { "Frankrijk", "Duitsland", "Italië", "Spanje" };

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
            var selectedCountries = new List<string> { "Frankrijk" };

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

    /// <summary>
    /// Unit tests for HolidaySearch Form
    /// 
    /// Het testen van een Form heeft een ingewikkelde structuur, omdat de controls private zijn en niet direct toegankelijk. 
    /// Daarom wordt hier gebruik gemaakt van reflection om de interne controls te benaderen en hun waarden te controleren.
    /// 
    /// Het is dus wel mogelijk om via unittesting de functionaliteit van de Form te testen, 
    /// maar het vereist een diepgaande kennis van de interne structuur van de Form en hoe deze is opgebouwd.
    /// Aangeraden wordt (voor usabillity Engineering) om de gebruikersinerface op andere manieren te testen.
    /// </summary>
    [TestFixture]
    public class HolidaySearchForm_Tests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void HolidaySearchMain_ResetButton_ResetsFormControls()
        {
            // Arrange
            HolidaySearchMain form = new HolidaySearchMain();
            var t = form.GetType();

            var clbField = t.GetField("checkedListBoxCountries", BindingFlags.Instance | BindingFlags.NonPublic);
            if (clbField == null) { Assert.Fail("checkedListBoxCountries field not found on form"); return; }
            var clb = clbField.GetValue(form);
            if (clb == null) { Assert.Fail("checkedListBoxCountries instance is null"); return; }
            var clbNN = clb!;

            var dtStartField = t.GetField("dateTimePickerStart", BindingFlags.Instance | BindingFlags.NonPublic);
            if (dtStartField == null) { Assert.Fail("dateTimePickerStart field not found on form"); return; }
            var dtStart = dtStartField.GetValue(form);
            if (dtStart == null) { Assert.Fail("dateTimePickerStart instance is null"); return; }
            var dtStartNN = dtStart!;

            var dtEndField = t.GetField("dateTimePickerEnd", BindingFlags.Instance | BindingFlags.NonPublic);
            if (dtEndField == null) { Assert.Fail("dateTimePickerEnd field not found on form"); return; }
            var dtEnd = dtEndField.GetValue(form);
            if (dtEnd == null) { Assert.Fail("dateTimePickerEnd instance is null"); return; }
            var dtEndNN = dtEnd!;

            var nudAdultsField = t.GetField("numericUpDownAdults", BindingFlags.Instance | BindingFlags.NonPublic);
            if (nudAdultsField == null) { Assert.Fail("numericUpDownAdults field not found on form"); return; }
            var nudAdults = nudAdultsField.GetValue(form);
            if (nudAdults == null) { Assert.Fail("numericUpDownAdults instance is null"); return; }
            var nudAdultsNN = nudAdults!;

            var nudChildrenField = t.GetField("numericUpDownChildren", BindingFlags.Instance | BindingFlags.NonPublic);
            if (nudChildrenField == null) { Assert.Fail("numericUpDownChildren field not found on form"); return; }
            var nudChildren = nudChildrenField.GetValue(form);
            if (nudChildren == null) { Assert.Fail("numericUpDownChildren instance is null"); return; }
            var nudChildrenNN = nudChildren!;

            var dgvAvailableField = t.GetField("dataGridViewAvailable", BindingFlags.Instance | BindingFlags.NonPublic);
            if (dgvAvailableField == null) { Assert.Fail("dataGridViewAvailable field not found on form"); return; }
            var dgvAvailable = dgvAvailableField.GetValue(form);
            if (dgvAvailable == null) { Assert.Fail("dataGridViewAvailable instance is null"); return; }
            var dgvAvailableNN = dgvAvailable!;

            // Modify state via reflection
            var itemsProp = clbNN.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First(p => p.Name == "Items" && p.GetIndexParameters().Length == 0);
            var items = (IList?)itemsProp.GetValue(clbNN);
            if (items != null && items.Count > 0)
            {
                var setItemChecked = clbNN.GetType().GetMethod("SetItemChecked");
                setItemChecked!.Invoke(clbNN, new object[] { 0, true });
            }

            dtStartNN.GetType().GetProperty("Value").SetValue(dtStartNN, DateTime.Today.AddDays(5));
            dtEndNN.GetType().GetProperty("Value").SetValue(dtEndNN, DateTime.Today.AddDays(6));
            nudAdultsNN.GetType().GetProperty("Value").SetValue(nudAdultsNN, (decimal)3);
            nudChildrenNN.GetType().GetProperty("Value").SetValue(nudChildrenNN, (decimal)2);

            // Add a row to the available grid
            var rowsProp = dgvAvailableNN.GetType().GetProperty("Rows");
            var rows = rowsProp!.GetValue(dgvAvailableNN);
            if (rows == null) Assert.Fail("Rows instance is null");
            var rowsNN = rows!;
            var addMethod = rowsNN.GetType().GetMethod("Add", new Type[] { typeof(object[]) });
            addMethod!.Invoke(rowsNN, new object[] { new object[] { "x" } });

            // Act - invoke reset button handler
            var resetMi = t.GetMethod("buttonReset_Click", BindingFlags.Instance | BindingFlags.NonPublic);
            resetMi!.Invoke(form, new object[] { null, EventArgs.Empty });

            // Assert defaults via reflection
            var dtStartVal = (DateTime)dtStartNN.GetType().GetProperty("Value")!.GetValue(dtStartNN)!;
            var dtEndVal = (DateTime)dtEndNN.GetType().GetProperty("Value")!.GetValue(dtEndNN)!;
            var nudAdultsVal = (decimal)nudAdultsNN.GetType().GetProperty("Value")!.GetValue(nudAdultsNN)!;
            var nudChildrenVal = (decimal)nudChildrenNN.GetType().GetProperty("Value")!.GetValue(nudChildrenNN)!;

            Assert.That(dtStartVal.Date, Is.EqualTo(DateTime.Today));
            Assert.That(dtEndVal.Date, Is.EqualTo(DateTime.Today.AddDays(1)));
            Assert.That(nudAdultsVal, Is.EqualTo((decimal)1));
            Assert.That(nudChildrenVal, Is.EqualTo((decimal)0));

            var itemsPropCheck = clbNN.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First(p => p.Name == "Items" && p.GetIndexParameters().Length == 0);
            var itemsCheck = (IList?)itemsPropCheck.GetValue(clbNN);
            var getItemChecked = clbNN.GetType().GetMethod("GetItemChecked");
            if (itemsCheck != null)
            {
                for (int i = 0; i < itemsCheck.Count; i++)
                {
                    var isChecked = (bool)getItemChecked!.Invoke(clbNN, new object[] { i })!;
                    Assert.That(isChecked, Is.False);
                }
            }

            var rowsPropCheck = dgvAvailableNN.GetType().GetProperty("Rows");
            var rowsCheck = rowsPropCheck!.GetValue(dgvAvailableNN);
            if (rowsCheck == null) Assert.Fail("Rows instance (post-reset) is null");
            var countPropCheck = rowsCheck.GetType().GetProperty("Count");
            var rowCountCheck = (int)countPropCheck!.GetValue(rowsCheck)!;
            Assert.That(rowCountCheck, Is.EqualTo(0));
        }
    }
}

