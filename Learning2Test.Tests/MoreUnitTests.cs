using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning2Test.Tests
{
    [TestClass]
    public class MoreUnitTests
    {
        [TestMethod]
        ///<summary>
        /// Tests that the Destination constructor correctly sets the Name, MinPeople
        /// and MaxPeople properties when provided with valid arguments.
        ///</summary>
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var name = "Paris";
            var minPeople = 2;
            var maxPeople = 5;

            // Act
            var dest = new Destination(name, minPeople, maxPeople);

            // Assert
            Assert.AreEqual(name, dest.Name);
            Assert.AreEqual(minPeople, dest.MinPeople);
            Assert.AreEqual(maxPeople, dest.MaxPeople);
        }

        [TestMethod]
        /// <summary>
        /// Tests that the ToString() method of the Destination class
        /// returns the Name property as its string representation.
        /// </summary>
        public void ToString_ReturnsName()
        {
            // Arrange
            var dest = new Destination("Rome", 1, 4);

            // Act
            var result = dest.ToString();

            // Assert
            Assert.AreEqual("Rome", result);
        }

        [TestMethod]
        public void HolidaySearch_InitializesWithDefaults()
        {
            var hs = new HolidaySearch();
            Assert.AreEqual(DateTime.Today, hs.StartDate);
            Assert.AreEqual(DateTime.Today, hs.EndDate);
            Assert.AreEqual(0, hs.People);
            Assert.IsNotNull(hs.SelectedDestinations);
            Assert.AreEqual(0, hs.SelectedDestinations.Count);
            Assert.IsNotNull(hs.AllDestinations);
            Assert.IsTrue(hs.AllDestinations.Count > 0); // Should have some predefined destinations
        }


        [TestMethod]
        public void IncreasePeople_Basic()
        {
            int people = 2;
            var hs = new HolidaySearch(people);            
            hs.IncreasePeople();
            Assert.AreEqual(3, hs.People);
        }

        [TestMethod]
        public void IncreasePeople_UpToMax15()
        {
            int people = 14;
            var hs = new HolidaySearch(people);

            hs.IncreasePeople();
            Assert.AreEqual(15, hs.People);

            hs.IncreasePeople();
            Assert.AreEqual(15, hs.People); // Should not exceed 15
        }

        [TestMethod]
        public void DecreasePeople_Basic1()
        {
            int people = 5;
            var hs = new HolidaySearch(people);

            hs.DecreasePeople();
            Assert.AreEqual(4, hs.People);
        }

        [TestMethod]
        public void DecreasePeople_Min0()
        {
            int people = 1;
            var hs = new HolidaySearch(people);
            
            hs.DecreasePeople();
            Assert.AreEqual(0, hs.People);

            hs.DecreasePeople();
            Assert.AreEqual(0, hs.People);
        }

        [TestMethod]
        public void Search_NoSelectedDestinations_ReturnsSelecteerBestemming()
        {
            var hs = new HolidaySearch();
            hs.SelectedDestinations = null;
            var result = hs.Search();
            Assert.IsTrue(result.Contains("Moon"));

            hs.SelectedDestinations = new List<Destination>();
            result = hs.Search();
            Assert.AreEqual("Selecteer minimaal 1 bestemming", result);
        }

        [TestMethod]
        public void Search_NoValidDestinations_ReturnsGeenOptiesGevonden()
        {
            int people = 20; // Exceeds all max, no destination should match
            var hs = new HolidaySearch(people);
            hs.SelectedDestinations = new List<Destination> { hs.AllDestinations[0] };
            var result = hs.Search();
            Assert.AreEqual("Geen opties gevonden", result);
        }

        [TestMethod]
        public void AddDestination_NewDestination_AddsToAllDestinations()
        {
            var hs = new HolidaySearch();
            int initialCount = hs.AllDestinations.Count;
            var newDest = new Destination("NewPlace", 1, 5);
            hs.AddDestination(newDest);
            Assert.AreEqual(initialCount + 1, hs.AllDestinations.Count);
            Assert.IsTrue(hs.AllDestinations.Contains(newDest));
        }

        [TestMethod]
        public void AddDestination_DuplicateDestination_DoesNotAdd()
        {
            var hs = new HolidaySearch();
            var existingDest = hs.AllDestinations[0];
            
            //There should be an exception thrown with message "Bestemming bestaat al."
            var ex = Assert.ThrowsException<InvalidOperationException>(() => hs.AddDestination(existingDest));
            Assert.AreEqual("Bestemming bestaat al.", ex.Message);
        }

        [TestMethod]
        public void Search_ValidDestinationByPeopleAndDate_ReturnsDestinationName()
        {
            int people = 3; // Exceeds all max, no destination should match
            var hs = new HolidaySearch(people);
            var dest = new Destination("Test", 1, 5, DateTime.Today, DateTime.Today.AddDays(1));
            hs.AddDestination(dest);
            hs.SelectedDestinations = new List<Destination> { dest };
            hs.StartDate = DateTime.Today;
            hs.EndDate = DateTime.Today.AddDays(1);

            var result = hs.Search();
            Assert.IsTrue(result.Contains("Test"), "Expected destination name to be in the result for valid people and date range.");
        }

        [TestMethod]
        public void Search_EndDateBeforeStartDate_ThrowsException()
        {
            int people = 3;
            var hs = new HolidaySearch(people);
            var dest = new Destination("Test", 1, 5, DateTime.Today, DateTime.Today.AddDays(1));
            hs.SelectedDestinations = new List<Destination> { dest };
            
            hs.StartDate = DateTime.Today.AddDays(2);
            hs.EndDate = DateTime.Today; // End date before start date

            //There should be an exception thrown with message "Ongeldige invoer. Einddatum eerder dan startdatum"
            var ex = Assert.ThrowsException<InvalidOperationException>(() => hs.Search());
            Assert.AreEqual("Ongeldige invoer. Einddatum eerder dan startdatum.", ex.Message);
        }

        [TestMethod]
        public void Reset_SetsDefaults()
        {
            int people = 3;
            var hs = new HolidaySearch(people);

            hs.StartDate = new DateTime(2025, 1, 1);
            hs.EndDate = new DateTime(2025, 1, 2);

            hs.SelectedDestinations.Add(new Destination("X", 1, 2));

            hs.Reset();

            Assert.AreEqual(DateTime.Today, hs.StartDate);
            Assert.AreEqual(DateTime.Today, hs.EndDate);
            Assert.AreEqual(0, hs.People);
            Assert.AreEqual(0, hs.SelectedDestinations.Count);
        }
    }
}