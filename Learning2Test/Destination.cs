using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning2Test_Logic
{

    /// <summary>
    /// Represents a travel destination with a name and constraints on the number of people.
    /// </summary>
    public class Destination
    {
        public string Name { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        public DateTime? PossibleStartDate { get; set; }
        public DateTime? PossibleEndDate { get; set; }

        public Destination(string name, int minPeople, int maxPeople, DateTime? possibleStartDate = null, DateTime? possibleEndDate = null)
        {
            Name = name;
            MinPeople = minPeople;
            MaxPeople = maxPeople;
            PossibleStartDate = possibleStartDate;
            PossibleEndDate = possibleEndDate;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
