using Learning2Test_Models;
using Learning2Test_DAL;

namespace Learning2Test_V2
{
    public partial class HolidaySearchMain : Form
    {
        private HolidaySearch holidaySearch;
        private Learning2Test_Models.IDestinationRepository repository;

        public HolidaySearchMain()
        {
            InitializeComponent();

            // Create repository (dependency injection)
            repository = new DestinationRepository();

            // Create HolidaySearch with repository
            holidaySearch = new HolidaySearch(repository, DateTime.Today, DateTime.Today.AddDays(1), 1, 0);

            // Populate countries from available destinations
            var countries = holidaySearch.GetAvailableCountries();
            foreach (var country in countries)
            {
                checkedListBoxCountries.Items.Add(country);
            }

            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today.AddDays(1);
            ResetForm();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;
            int adults = (int)numericUpDownAdults.Value;
            int children = (int)numericUpDownChildren.Value;
            var selectedCountries = checkedListBoxCountries.CheckedItems.Cast<string>().ToList();

            var (available, unavailable) = holidaySearch.Search(startDate, endDate, adults, children, selectedCountries);

            int nights = (endDate - startDate).Days;

            listBoxAvailable.Items.Clear();
            foreach (var city in available)
            {
                var totalPrice = city.CalculateTotalPrice(nights, adults, children);
                var info = $"{city.Name} ({city.Country}) - €{city.PricePerNightPerPerson:F2}/p.p./nacht | Totaal: €{totalPrice:F2} ({nights} nachten) | Cap: {city.MinAdults}-{city.MaxAdults} volw., {city.MinChildren}-{city.MaxChildren} kind.";
                listBoxAvailable.Items.Add(info);
            }

            listBoxUnavailable.Items.Clear();
            foreach (var city in unavailable)
            {
                var info = $"{city.Name} ({city.Country}) - €{city.PricePerNightPerPerson:F2}/p.p./nacht | Beschikbaar: {city.AvailableFrom:dd/MM/yyyy} - {city.AvailableTo:dd/MM/yyyy}";
                listBoxUnavailable.Items.Add(info);
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            holidaySearch.Reset();
            
            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today.AddDays(1);
            numericUpDownAdults.Value = 1;
            numericUpDownChildren.Value = 0;

            for (int i = 0; i < checkedListBoxCountries.Items.Count; i++)
                checkedListBoxCountries.SetItemChecked(i, false);

            listBoxAvailable.Items.Clear();
            listBoxUnavailable.Items.Clear();
        }
    }
}
