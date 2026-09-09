using Learning2Test_Models;

namespace Learn2Test_DAL
{
    public class DataAccesLayer
    {
        public List<DestinationCity> AllCities { get; private set; } = new List<DestinationCity>();

        public void CreateSampleCityData()
        {
            var CurrentYear = DateTime.Today.Year;
            AllCities = new List<DestinationCity>();
            // Example data
            AllCities.Add(new DestinationCity("Paris", "France", 2, 40, 0, 0, new DateTime(CurrentYear, 9, 1), new DateTime(CurrentYear, 10, 31)));
            AllCities.Add(new DestinationCity("Nice", "France", 2, 60, 0, 20, new DateTime(CurrentYear, 7, 1), new DateTime(CurrentYear, 9, 15)));
            AllCities.Add(new DestinationCity("Berlin", "Germany", 1, 50, 0, 40, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 10, 1)));
            AllCities.Add(new DestinationCity("Munich", "Germany", 2, 60, 0, 30, new DateTime(CurrentYear, 6, 15), new DateTime(CurrentYear, 9, 30)));
            AllCities.Add(new DestinationCity("Rome", "Italy", 1, 45, 0, 20, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 10, 1)));
            AllCities.Add(new DestinationCity("Venice", "Italy", 2, 55, 0, 55, new DateTime(CurrentYear, 6, 1), new DateTime(CurrentYear, 9, 30)));
            AllCities.Add(new DestinationCity("Maastricht", "Netherlands", 1, 4, 0, 20, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 9, 30)));
            AllCities.Add(new DestinationCity("Lyon", "France", 1, 4, 0, 2, new DateTime(CurrentYear, 9, 1), new DateTime(CurrentYear, 12, 31)));
            AllCities.Add(new DestinationCity("Groningen", "Netherlands", 1, 200, 0, 75, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 10, 31)));
            AllCities.Add(new DestinationCity("Amsterdam", "Netherlands", 1, 45, 0, 2, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 9, 30)));
            AllCities.Add(new DestinationCity("Brussels", "Belgium", 1, 40, 0, 25, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 9, 30)));
            AllCities.Add(new DestinationCity("Berlin", "Germany", 1, 45, 0, 25, new DateTime(CurrentYear, 10, 1), new DateTime(CurrentYear, 12, 24)));
            AllCities.Add(new DestinationCity("Essen", "Germany", 2, 65, 0, 30, new DateTime(CurrentYear, 9, 15), new DateTime(CurrentYear, 11, 11)));
            AllCities.Add(new DestinationCity("Plopsaland", "Belgium", 0, 4, 1, 200, new DateTime(CurrentYear, 5, 1), new DateTime(CurrentYear, 9, 30)));
        }


    }
}
