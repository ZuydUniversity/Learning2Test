using Learning2Test_Models;

namespace Learning2Test_V2
{
    public partial class BookingForm : Form
    {
        public string BookerName { get; private set; }
        public DateTime BookerBirthDate { get; private set; }

        private DestinationCity _destination;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _adults;
        private int _children;
        private decimal _totalPrice;

        public BookingForm(DestinationCity destination, DateTime startDate, DateTime endDate, 
            int adults, int children, decimal totalPrice)
        {
            InitializeComponent();

            _destination = destination;
            _startDate = startDate;
            _endDate = endDate;
            _adults = adults;
            _children = children;
            _totalPrice = totalPrice;

            // Set initial date to a reasonable past date (e.g., 18 years ago)
            dateTimePickerBirthDate.Value = DateTime.Today.AddYears(-18);
            dateTimePickerBirthDate.MaxDate = DateTime.Today.AddYears(-18); // Must be at least 18
            dateTimePickerBirthDate.MinDate = DateTime.Today.AddYears(-120); // Reasonable max age

            DisplayBookingDetails();
        }

        private void DisplayBookingDetails()
        {
            int nights = (_endDate - _startDate).Days;

            labelBookingInfo.Text = $"Boeking Details:\n\n" +
                $"Bestemming: {_destination.Name}, {_destination.Country}\n" +
                $"Periode: {_startDate:dd/MM/yyyy} - {_endDate:dd/MM/yyyy} ({nights} nachten)\n" +
                $"Gasten: {_adults} volwassenen, {_children} kinderen\n" +
                $"Totaalprijs: €{_totalPrice:F2}";
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Vul alstublieft uw naam in.", "Validatiefout", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return;
            }

            if (dateTimePickerBirthDate.Value > DateTime.Today.AddYears(-18))
            {
                MessageBox.Show("U moet minimaal 18 jaar oud zijn om te boeken.", "Validatiefout", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BookerName = textBoxName.Text.Trim();
            BookerBirthDate = dateTimePickerBirthDate.Value.Date;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
