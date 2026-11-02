using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OOP_Lab6;

namespace OOP_Lab6.Tests
{
    [TestClass]
    public class SmartHouseManagerTests
    {
        private SmartHouseManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new SmartHouseManager();
        }

        [TestMethod]
        public void AddHouse_ValidHouse_IncreasesCount()
        {
            // Arrange
            SmartHouse house = new SmartHouse();

            // Act
            _manager.AddHouse(house);

            // Assert
            Assert.AreEqual(1, _manager.Count);
        }

        [TestMethod]
        public void RemoveHouseAt_ValidIndex_RemovesHouseAndReturnsTrue()
        {
            // Arrange
            _manager.AddHouse(new SmartHouse());

            // Act
            bool result = _manager.RemoveHouseAt(0);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _manager.Count);
        }

        [TestMethod]
        public void RemoveHouseAt_InvalidIndex_ReturnsFalse()
        {
            // Arrange
            _manager.AddHouse(new SmartHouse());

            // Act
            bool result = _manager.RemoveHouseAt(5);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, _manager.Count);
        }

        [TestMethod]
        public void RemoveHousesByOwner_ExistingOwner_RemovesAndReturnsCount()
        {
            // Arrange
            _manager.AddHouse(new SmartHouse("вул. Тест, 1", "Іван"));
            _manager.AddHouse(new SmartHouse("вул. Тест, 2", "Марія"));
            _manager.AddHouse(new SmartHouse("вул. Тест, 3", "Іван"));

            // Act
            int removedCount = _manager.RemoveHousesByOwner("Іван");

            // Assert
            Assert.AreEqual(2, removedCount);
            Assert.AreEqual(1, _manager.Count);
            Assert.AreEqual("Марія", _manager.GetHouseAt(0).OwnersName);
        }

        [TestMethod]
        public void SearchHouses_ValidCriteria_ReturnsMatchingHouses()
        {
            // Arrange
            _manager.AddHouse(new SmartHouse("вул. Київська, 1", "Петро"));
            _manager.AddHouse(new SmartHouse("вул. Львівська, 2", "Ольга"));

            // Act
            List<SmartHouse> results = _manager.SearchHouses(h => h.OwnersName == "Ольга");

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Ольга", results[0].OwnersName);
        }

        [TestMethod]
        public void GetAllHouses_ReturnsCopyOfList()
        {
            // Arrange
            _manager.AddHouse(new SmartHouse());

            // Act
            var houses = _manager.GetAllHouses();

            // Assert
            Assert.AreEqual(1, houses.Count);
        }
    }
}