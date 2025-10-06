using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Learning2Test
{
    public partial class Form1 : Form
    {
        private HolidaySearch holidaySearch = new HolidaySearch();

        public Form1()
        {
            InitializeComponent();
            listBoxDestinations.Items.AddRange(holidaySearch.AllDestinations.ToArray());
            dateTimePickerStart.MinDate = DateTime.Today;
            dateTimePickerEnd.MinDate = DateTime.Today;

            // Optional: Set initial values if needed
            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today.AddDays(1);
            ResetForm();
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            holidaySearch.IncreasePeople();
            UpdatePeopleTextBox();
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            holidaySearch.DecreasePeople();
            UpdatePeopleTextBox();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            UpdateModelFromForm();
            labelResults.Text = holidaySearch.Search();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            holidaySearch.Reset();
            dateTimePickerStart.Value = holidaySearch.StartDate;
            dateTimePickerEnd.Value = holidaySearch.EndDate;
            listBoxDestinations.ClearSelected();
            UpdatePeopleTextBox();
            labelResults.Text = "";
        }

        private void UpdatePeopleTextBox()
        {
            textBoxPeople.Text = holidaySearch.People.ToString();
        }

        private void UpdateModelFromForm()
        {
            holidaySearch.StartDate = dateTimePickerStart.Value;
            holidaySearch.EndDate = dateTimePickerEnd.Value;
            holidaySearch.SelectedDestinations = listBoxDestinations.SelectedItems
                .OfType<Destination>()
                .ToList();
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.MinDate = dateTimePickerStart.Value;
            if (dateTimePickerEnd.Value <= dateTimePickerStart.Value)
            {
                dateTimePickerEnd.Value = dateTimePickerStart.Value;
            }
        }

        private void btnBestemmingToevoegen_Click(object sender, EventArgs e)
        {
            //TODO: Aanpassen zodat een nieuwe bestemming wordt toegevoegd met gegevens van de gebruiker
            //Huidige implementatie voegt een willekeurige bestemming toe

            // 1. Create a new random destination
            var rnd = new Random();
            string[] names = { "Nice", "Valencia", "Porto", "Edinburgh", "Naples", "Seville", "Krakow", "Tallinn", "Riga", "Vilnius" };
            string name = names[rnd.Next(names.Length)];
            int minPeople = rnd.Next(1, 5);
            int maxPeople = rnd.Next(minPeople, minPeople + 5);
            DateTime start = DateTime.Today.AddDays(rnd.Next(0, 60));
            DateTime end = start.AddDays(rnd.Next(3, 15));


            var newDest = new Destination(name, minPeople, maxPeople, start, end);
            holidaySearch.AddDestination(newDest);

            // 1. Show popup message
            MessageBox.Show("Bestemming '" + name + "' toegevoegd aan de lijst", "Toegevoegd", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 3. Refresh the listBoxDestinations
            listBoxDestinations.Items.Clear();
            listBoxDestinations.Items.AddRange(holidaySearch.AllDestinations.ToArray());
        }
    }
}
