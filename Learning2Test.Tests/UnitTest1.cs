using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Learning2Test_Logic;

namespace Learning2Test_Logic.Tests
{
    [TestClass]
    ///<summary>
    /// The DestinationTests class is a unit test class designed to verify the behavior of the Destination class. 
    /// It uses MSTest attributes ([TestClass], [TestMethod]) to define and organize tests. 
    /// Specifically, it checks:
    /// * The constructor of Destination correctly sets the Name, MinPeople, and MaxPeople properties.
    /// * The ToString() method of Destination returns the expected name.
    /// This ensures that basic object creation and string representation work as intended for the Destination class.
    /// 
    /// More tests needed
    ///</summary>
    public class DestinationTests
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
    }
}

