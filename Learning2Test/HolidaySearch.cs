using System;
using System.Collections.Generic;
using System.Linq;

namespace Learning2Test
{
    public class HolidaySearch
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Destination> AllDestinations { get; } = new List<Destination>
        {
            new Destination("Paris", 1, 4, new DateTime(2025, 6, 1), new DateTime(2025, 8, 31)),
            new Destination("London", 2, 6, new DateTime(2025, 5, 1), new DateTime(2025, 9, 30)),
            new Destination("Rome", 1, 8, new DateTime(2025, 4, 1), new DateTime(2025, 10, 31)),
            new Destination("Berlin", 3, 10, new DateTime(2025, 7, 1), new DateTime(2025, 9, 15)),
            new Destination("Madrid", 2, 5, new DateTime(2025, 3, 15), new DateTime(2025, 7, 31)),
            new Destination("Amsterdam", 1, 3, new DateTime(2025, 5, 15), new DateTime(2025, 8, 15)),
            new Destination("Prague", 2, 7, new DateTime(2025, 6, 10), new DateTime(2025, 9, 10)),
            new Destination("Vienna", 1, 6, new DateTime(2025, 4, 20), new DateTime(2025, 10, 20)),
            new Destination("Lisbon", 2, 8, new DateTime(2025, 5, 10), new DateTime(2025, 9, 25)),
            new Destination("Budapest", 1, 5, new DateTime(2025, 6, 5), new DateTime(2025, 8, 25)),
            new Destination("Barcelona", 2, 6, new DateTime(2025, 5, 1), new DateTime(2025, 9, 30)),
            new Destination("Copenhagen", 1, 4, new DateTime(2025, 6, 1), new DateTime(2025, 8, 31)),
            new Destination("Dublin", 1, 5, new DateTime(2025, 4, 15), new DateTime(2025, 10, 15)),
            new Destination("Florence", 2, 7, new DateTime(2025, 5, 10), new DateTime(2025, 9, 20)),
            new Destination("Munich", 1, 8, new DateTime(2025, 6, 1), new DateTime(2025, 10, 1)),
            new Destination("Stockholm", 2, 6, new DateTime(2025, 5, 20), new DateTime(2025, 8, 20)),
            new Destination("Brussels", 1, 4, new DateTime(2025, 4, 1), new DateTime(2025, 9, 30)),
            new Destination("Zurich", 2, 5, new DateTime(2025, 6, 15), new DateTime(2025, 9, 15)),
            new Destination("Oslo", 1, 3, new DateTime(2025, 5, 1), new DateTime(2025, 8, 31)),
            new Destination("Athens", 2, 8, new DateTime(2025, 4, 10), new DateTime(2025, 10, 10))
        };


        public List<Destination> SelectedDestinations { get; set; } = new List<Destination>();
        public int People { get; set; } = 1;

        public void IncreasePeople()
        {
            if (People < 15)
                People+=2;
        }

        public void DecreasePeople()
        {
            People--;
        }

        public string Search()
        {
            if (SelectedDestinations == null || SelectedDestinations.Count == 0)
                return "- \t" + "Moon";

            // Filter destinations by people count and possible date range
            var valid = SelectedDestinations
                .Where(d => People <= d.MinPeople && People >= d.MaxPeople)
                .Where(d =>
                    (!d.PossibleStartDate.HasValue || StartDate >= d.PossibleStartDate.Value) &&
                    (!d.PossibleEndDate.HasValue || EndDate <= d.PossibleEndDate.Value)
                )
                .ToList();

            if (valid.Count == 0)
                return "Geen opties gevonden";

            return "- \t" + string.Join("\n- \t", valid.Select(d => d.Name));
        }

        public void Reset()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            SelectedDestinations.Clear();
            People = -1;
        }
    }
}
