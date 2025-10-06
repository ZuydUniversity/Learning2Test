using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Learning2Test
{
    public class HolidaySearch
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Destination> AllDestinations { get; }
        public List<Destination> SelectedDestinations { get; set; }
        
        //Het aantal mensen kan niet direct aangepast worden,
        //alleen vanuit de methoden IncreasePeople en DecreasePeople
        public int People { get; private set; }

        //Initialiseren van alle properties in de constructor
        public HolidaySearch(int people = 0)
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            People = people; 

            SelectedDestinations = new List<Destination>();
            AllDestinations = new List<Destination>
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
        }

        //Als de bestemming niet bestaat, wordt deze toegevoegd aan de lijst van bestemmingen.
        public void AddDestination(Destination newDestination)
        {
            Destination destination = AllDestinations.FirstOrDefault(d => d.Name == newDestination.Name);
            if (destination == null)
                AllDestinations.Add(newDestination);
            else
                throw new InvalidOperationException("Bestemming bestaat al");
        }

        public void AddDestination(string name, int minPeople, int maxPeople, DateTime startDate, DateTime endDate)
        {            
            Destination destination = AllDestinations.FirstOrDefault(d => d.Name == name);
            //Als de bestemming niet bestaat, wordt een nieuwe bestemming gemaakt
            //en deze toegevoegd aan de lijst van bestemmingen.
            if (destination == null)
            {
                Destination newDestination = new Destination(name, minPeople, minPeople, startDate, endDate);
                SelectedDestinations.Add(newDestination);
            }
            else
                throw new InvalidOperationException("Bestemming bestaat al");

        }

        public void IncreasePeople()
        {
            if (People < 15)
                People += 2;
        }

        public void DecreasePeople()
        {
            People--;
        }

        public string Search()
        {
            //Via the GUI is het niet mogelijk om een einddatum te kiezen die voor de startdatum ligt.
            //Maar het is altijd goed om de onderliggende code robust te maken en dit soort logica te valideren.
            //Met "throw new" wordt een foutmelding gegooid die opgevangen kan worden in de bovenliggende laag
            //(in dit geval de GUI of de TestMethode).
            if (EndDate < StartDate)
                throw new InvalidOperationException("Ongeldige invoer. Einddatum eerder dan startdatum.");

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
