using Learning2Test_Models;
using Learning2Test_DAL;

namespace Learning2Test_V2
{
    public partial class HolidaySearchMain : Form
    {
        private HolidaySearch holidaySearch;
        private Learning2Test_Models.IDestinationRepository repository;
        private Learning2Test_Models.IBookingRepository bookingRepository;

        public HolidaySearchMain()
        {
            InitializeComponent();

            // Create repository (dependency injection)
            repository = new DestinationRepository();
            bookingRepository = new BookingRepository(repository);

            // Create HolidaySearch with repository
            holidaySearch = new HolidaySearch(repository, bookingRepository, DateTime.Today, DateTime.Today.AddDays(1), 1, 0);

            // Populate countries from available destinations
            var countries = holidaySearch.GetAvailableCountries();
            foreach (var country in countries)
            {
                checkedListBoxCountries.Items.Add(country);
            }

            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today.AddDays(1);

            InitializeDataGridViews();
            ResetForm();
        }

        private void InitializeDataGridViews()
        {
            // Setup Available DataGridView columns
            dataGridViewAvailable.Columns.Add("Name", "Stad");
            dataGridViewAvailable.Columns.Add("Country", "Land");
            dataGridViewAvailable.Columns.Add("PricePerNight", "€/p.p./nacht");
            dataGridViewAvailable.Columns.Add("TotalPrice", "Totaal €");
            dataGridViewAvailable.Columns.Add("Nights", "Nachten");
            dataGridViewAvailable.Columns.Add("CurrentCapacity", "Beschikbare plaatsen");
            dataGridViewAvailable.Columns.Add("MaxCapacity", "Max. Capaciteit");

            // Setup Unavailable DataGridView columns
            dataGridViewUnavailable.Columns.Add("Name", "Stad");
            dataGridViewUnavailable.Columns.Add("Country", "Land");
            dataGridViewUnavailable.Columns.Add("PricePerNight", "€/p.p./nacht");
            dataGridViewUnavailable.Columns.Add("AvailableFrom", "Beschikbaar vanaf");
            dataGridViewUnavailable.Columns.Add("AvailableTo", "Beschikbaar tot");
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

            // Populate Available grid
            dataGridViewAvailable.Rows.Clear();
            foreach (var city in available)
            {
                var totalPrice = city.CalculateTotalPrice(nights, adults, children);
                var currentCapacity = $"{city.CurrentAvailableAdults} volw., {city.CurrentAvailableChildren} kind.";
                var maxCapacity = $"{city.MinAdults}-{city.MaxAdults} volw., {city.MinChildren}-{city.MaxChildren} kind.";

                dataGridViewAvailable.Rows.Add(
                    city.Name,
                    city.Country,
                    $"€{city.PricePerNightPerPerson:F2}",
                    $"€{totalPrice:F2}",
                    nights,
                    currentCapacity,
                    maxCapacity
                );
            }

            // Populate Unavailable grid
            dataGridViewUnavailable.Rows.Clear();
            foreach (var city in unavailable)
            {
                dataGridViewUnavailable.Rows.Add(
                    city.Name,
                    city.Country,
                    $"€{city.PricePerNightPerPerson:F2}",
                    city.AvailableFrom.ToString("dd/MM/yyyy"),
                    city.AvailableTo.ToString("dd/MM/yyyy")
                );
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

            dataGridViewAvailable.Rows.Clear();
            dataGridViewUnavailable.Rows.Clear();
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            // Check if a destination is selected
            if (dataGridViewAvailable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een bestemming uit de beschikbare steden.", 
                    "Geen selectie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get selected row
            var selectedRow = dataGridViewAvailable.SelectedRows[0];
            string cityName = selectedRow.Cells[0].Value.ToString();
            string country = selectedRow.Cells[1].Value.ToString();

            // Find the destination
            var destination = repository.GetAllDestinations()
                .FirstOrDefault(d => d.Name == cityName && d.Country == country);

            if (destination == null)
            {
                MessageBox.Show("Bestemming niet gevonden.", "Fout", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get booking details from search criteria
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;
            int adults = (int)numericUpDownAdults.Value;
            int children = (int)numericUpDownChildren.Value;
            int nights = (endDate - startDate).Days;
            decimal totalPrice = destination.CalculateTotalPrice(nights, adults, children);

            // Open booking form
            using (var bookingForm = new BookingForm(destination, startDate, endDate, adults, children, totalPrice))
            {
                if (bookingForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Create the booking
                        var booking = bookingRepository.CreateBooking(
                            bookingForm.BookerName,
                            bookingForm.BookerBirthDate,
                            destination,
                            startDate,
                            endDate,
                            adults,
                            children,
                            totalPrice
                        );

                        MessageBox.Show(
                            $"Boeking succesvol!\n\n" +
                            $"Boekingsnummer: {booking.Id}\n" +
                            $"Naam: {booking.BookerName}\n" +
                            $"Bestemming: {booking.CityName}, {booking.Country}\n" +
                            $"Periode: {booking.StartDate:dd/MM/yyyy} - {booking.EndDate:dd/MM/yyyy}\n" +
                            $"Totaalprijs: €{booking.TotalPrice:F2}",
                            "Boeking bevestigd",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // Refresh the search to show updated capacity
                        buttonSearch_Click(sender, e);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show($"Boeking mislukt: {ex.Message}", "Fout", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
