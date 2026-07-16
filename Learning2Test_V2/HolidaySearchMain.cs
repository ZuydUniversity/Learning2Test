using Learning2Test_Models;

namespace Learning2Test_V2
{
    public partial class HolidaySearchMain : Form
    {
        private HolidaySearch holidaySearch;

        public HolidaySearchMain()
        {
            InitializeComponent();

            holidaySearch = new HolidaySearch(DateTime.Today, DateTime.Today.AddDays(1), 1, 0);

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

            listBoxAvailable.Items.Clear();
            foreach (var city in available)
            {
                var info = $"{city.Name} ({city.Country}) - Capaciteit: {city.MinAdults}-{city.MaxAdults} volw., {city.MinChildren}-{city.MaxChildren} kind. | Beschikbaar: {city.AvailableFrom:dd/MM/yyyy} tot {city.AvailableTo:dd/MM/yyyy}";
                listBoxAvailable.Items.Add(info);
            }

            listBoxUnavailable.Items.Clear();
            foreach (var city in unavailable)
            {
                var info = $"{city.Name} ({city.Country}) - Beschikbaar vanaf {city.AvailableFrom:dd/MM/yyyy} tot {city.AvailableTo:dd/MM/yyyy}";
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
