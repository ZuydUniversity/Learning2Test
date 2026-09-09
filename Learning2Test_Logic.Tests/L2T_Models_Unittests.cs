using Learning2Test_DAL;
using Learning2Test_V2;
using System.Reflection;
using System.Collections;

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
    }

    /// <summary>
    /// Unit tests for HolidaySearch class
    /// </summary>
    [TestFixture]
    public class HolidaySearchTests
    {
        private HolidaySearch _holidaySearch;
        private IDestinationRepository _repository;
        private IBookingRepository _bookingRepository;

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
            // Arrange
            // Act
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
            if (clbField == null){ Assert.Fail("checkedListBoxCountries field not found on form"); return; }
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

