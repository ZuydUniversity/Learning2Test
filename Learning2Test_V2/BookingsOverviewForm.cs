using Learning2Test_Models;

namespace Learning2Test_V2
{
    public partial class BookingsOverviewForm : Form
    {
        private IBookingRepository _bookingRepository;

        public BookingsOverviewForm(IBookingRepository bookingRepository)
        {
            InitializeComponent();
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));

            InitializeDataGridView();
            LoadBookings();
        }

        private void InitializeDataGridView()
        {
            // Setup DataGridView columns
            dataGridViewBookings.Columns.Add("BookingId", "Boeking #");
            dataGridViewBookings.Columns.Add("BookerName", "Naam");
            dataGridViewBookings.Columns.Add("BookerBirthDate", "Geboortedatum");
            dataGridViewBookings.Columns.Add("Destination", "Bestemming");
            dataGridViewBookings.Columns.Add("StartDate", "Startdatum");
            dataGridViewBookings.Columns.Add("EndDate", "Einddatum");
            dataGridViewBookings.Columns.Add("Nights", "Nachten");
            dataGridViewBookings.Columns.Add("Guests", "Gasten");
            dataGridViewBookings.Columns.Add("TotalPrice", "Totaalprijs");
            dataGridViewBookings.Columns.Add("BookingDate", "Geboekt op");

            // Make booking ID column narrower
            dataGridViewBookings.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewBookings.Columns[0].Width = 80;
        }

        private void LoadBookings()
        {
            dataGridViewBookings.Rows.Clear();

            var bookings = _bookingRepository.GetAllBookings();

            if (bookings.Count == 0)
            {
                labelNoBookings.Visible = true;
                return;
            }

            labelNoBookings.Visible = false;

            foreach (var booking in bookings.OrderByDescending(b => b.BookingDate))
            {
                var guests = $"{booking.NumberOfAdults} volw., {booking.NumberOfChildren} kind.";

                dataGridViewBookings.Rows.Add(
                    booking.Id,
                    booking.BookerName,
                    booking.BookerBirthDate.ToString("dd/MM/yyyy"),
                    $"{booking.CityName}, {booking.Country}",
                    booking.StartDate.ToString("dd/MM/yyyy"),
                    booking.EndDate.ToString("dd/MM/yyyy"),
                    booking.TotalNights,
                    guests,
                    $"€{booking.TotalPrice:F2}",
                    booking.BookingDate.ToString("dd/MM/yyyy HH:mm")
                );
            }

            labelTotalBookings.Text = $"Totaal aantal boekingen: {bookings.Count}";
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
