using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OOP_Lab1;

namespace OOP_Lab1.Tests
{
    [TestClass]
    public class SmartHouseTest
    {
        private SmartHouse _house;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _house = new SmartHouse();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _house = null;
        }

        [TestMethod]
        public void AdressSet_ValidValue_UpdatesProperty()
        {
            // Arrange
            string expectedAddress = "вул. Хрещатик, 10";

            // Act
            _house.Adress = expectedAddress;

            // Assert
            Assert.AreEqual(expectedAddress, _house.Adress);
        }

        [TestMethod]
        public void AdressSet_TooShortValue_ThrowsArgumentException()
        {
            // Arrange
            string invalidAddress = "Дім";

            // Act
            try
            {
                _house.Adress = invalidAddress;

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentException.");
            }
            catch (ArgumentException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RoomsSet_ValidValue_UpdatesProperty()
        {
            // Arrange
            int expectedRooms = 5;

            // Act
            _house.Rooms = expectedRooms;

            // Assert
            Assert.AreEqual(expectedRooms, _house.Rooms);
        }

        [TestMethod]
        public void RoomsSet_ValueGreaterThan30_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int invalidRooms = 35;

            // Act
            try
            {
                _house.Rooms = invalidRooms;

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentOutOfRangeException.");
            }
            catch (ArgumentOutOfRangeException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void OwnersNameSet_ValidValue_UpdatesProperty()
        {
            // Arrange
            string expectedName = "Олександр";

            // Act
            _house.OwnersName = expectedName;

            // Assert
            Assert.AreEqual(expectedName, _house.OwnersName);
        }

        [TestMethod]
        public void OwnersNameSet_EmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string invalidName = "";

            // Act
            try
            {
                _house.OwnersName = invalidName;

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentException.");
            }
            catch (ArgumentException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AverageTemperatureSet_ValidValue_UpdatesProperty()
        {
            // Arrange
            double expectedTemp = 22.5;

            // Act
            _house.AverageTemperature = expectedTemp;

            // Assert
            Assert.AreEqual(expectedTemp, _house.AverageTemperature);
        }

        [TestMethod]
        public void AverageTemperatureSet_TooLowValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double invalidTemp = 10.0;

            // Act
            try
            {
                _house.AverageTemperature = invalidTemp;

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentOutOfRangeException.");
            }
            catch (ArgumentOutOfRangeException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AverageVoltageSet_ValidValue_UpdatesProperty()
        {
            // Arrange
            double expectedVoltage = 220.0;

            // Act
            _house.AverageVoltage = expectedVoltage;

            // Assert
            Assert.AreEqual(expectedVoltage, _house.AverageVoltage);
        }

        [TestMethod]
        public void AverageVoltageSet_TooHighValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double invalidVoltage = 300.0;

            // Act
            try
            {
                _house.AverageVoltage = invalidVoltage;

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentOutOfRangeException.");
            }
            catch (ArgumentOutOfRangeException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void SwitchMode_AwayMode_TurnsOnSecurity()
        {
            // Arrange

            // Act
            _house.SwitchMode(HouseMode.Away);

            // Assert
            Assert.IsTrue(_house.IsSecured);
        }

        [TestMethod]
        public void SwitchMode_HomeMode_TurnsOffSecurity()
        {
            // Arrange
            _house.SwitchMode(HouseMode.Away);

            // Act
            _house.SwitchMode(HouseMode.Home);

            // Assert
            Assert.IsFalse(_house.IsSecured);
        }

        [TestMethod]
        public void AdjustTemperature_ValidChange_ChangesTemperatureCorrectly()
        {
            // Arrange
            _house.AverageTemperature = 20.0;

            // Act
            _house.AdjustTemperature(2.5);

            // Assert
            Assert.AreEqual(22.5, _house.AverageTemperature);
        }

        [TestMethod]
        [DataRow(220.0, true)]
        [DataRow(150.0, false)]
        [DataRow(300.0, false)]
        public void CheckAndStabilizeVoltage_VariousInputs_ReturnsCorrectStatus(double inputVoltage, bool expectedResult)
        {
            // Arrange

            // Act
            bool actualResult = _house.CheckAndStabilizeVoltage(inputVoltage);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [DataRow(0.0, 32.0)]
        [DataRow(100.0, 212.0)]
        [DataRow(-40.0, -40.0)]
        public void ConvertCelsiusToFahrenheit_CalculatesCorrectly(double celsius, double expectedFahrenheit)
        {
            // Arrange

            // Act
            double actualFahrenheit = SmartHouse.ConvertCelsiusToFahrenheit(celsius);

            // Assert
            Assert.AreEqual(expectedFahrenheit, actualFahrenheit);
        }

        [TestMethod]
        public void Parse_EmptyString_ThrowsArgumentNullException()
        {
            // Arrange
            string emptyString = "";

            // Act
            try
            {
                SmartHouse.Parse(emptyString);

                // Assert
                Assert.Fail("Очікувалася помилка ArgumentNullException.");
            }
            catch (ArgumentNullException)
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TryParse_InvalidFormat_ReturnsFalseAndNull()
        {
            // Arrange
            string invalidString = "Зламані;Дані;Не;По;Формату";
            SmartHouse parsedHouse;

            // Act
            bool result = SmartHouse.TryParse(invalidString, out parsedHouse);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(parsedHouse);
        }

        [TestMethod]
        public void AdjustTemperature_WithMode_ChangesTemperatureAndMode()
        {
            // Arrange
            double targetTemp = 25.0;
            HouseMode targetMode = HouseMode.Econom;

            // Act
            _house.AdjustTemperature(targetTemp, targetMode);

            // Assert
            Assert.AreEqual(targetTemp, _house.AverageTemperature);
            Assert.AreEqual(targetMode, _house.CurrentMode);
        }

        [TestMethod]
        public void ToggleSecurity_SetsCorrectStatus()
        {
            // Arrange

            // Act
            _house.ToggleSecurity(true);

            // Assert
            Assert.IsTrue(_house.IsSecured);
        }

        [TestMethod]
        public void GetInfo_ReturnsNonEmptyString()
        {
            // Arrange

            // Act
            string info = _house.GetInfo();

            // Assert
            Assert.IsNotNull(info);
            Assert.IsTrue(info.Contains("Адреса:"));
        }

        [TestMethod]
        public void ToString_ReturnsCorrectlyFormattedString()
        {
            // Arrange
            _house.Adress = "вул. Тестова, 1";
            _house.Rooms = 2;

            // Act
            string result = _house.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("вул. Тестова, 1;2;"));
        }

        [TestMethod]
        public void DecrementCounter_DoesNotThrowException()
        {
            // Arrange 

            // Act
            SmartHouse.DecrementCounter();

            // Assert
            Assert.IsTrue(true);
        }
    }
}