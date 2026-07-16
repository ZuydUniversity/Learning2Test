using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning2Test_Models
{
    public class HolidaySearch
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int NumberOfAdults { get; private set; }
        public int NumberOfChildren { get; private set; }
        public int TotalGuests { get { return NumberOfAdults + NumberOfChildren; }} 
        public int TotalNights { get { return (EndDate - StartDate).Days; }}

        private HolidaySearchLogic searchLogic;

        public HolidaySearch(DateTime startDate, DateTime endDate, int adults, int children)
        {
            StartDate = startDate;
            EndDate = endDate;
            NumberOfAdults = adults;
            NumberOfChildren = children;
            searchLogic = new HolidaySearchLogic();
        }

        public (List<DestinationCity> available, List<DestinationCity> unavailable) Search(
            DateTime startDate, DateTime endDate, int adults, int children, List<string> selectedCountries)
        {
            return searchLogic.Search(startDate, endDate, adults, children, selectedCountries);
        }

        public List<string> GetAvailableCountries()
        {
            

            return searchLogic.AllCities
                .Select(c => c.Country)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        public void Reset()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(1);
            NumberOfAdults = 1;
            NumberOfChildren = 0;
        }
    }
}
