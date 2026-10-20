using System;
    using OOP_Lab1;

    public class SmartHouse
    {
      public string adress;
      public int rooms;
      public string ownersName;
      public HouseMode currentMode;
      public double averageTemperature;
      public double averageVoltage;
      public bool isSecured;
      public DateTime dateOfLastServiceCheck;

        public void SwitchMode(HouseMode newMode) // виїзд з дому - вмик. охорону,  приїзд - вимкн. охорону
        {
            currentMode = newMode;
            if (currentMode == HouseMode.Away || currentMode == HouseMode.Vacation) isSecured = true; 
            else if (currentMode == HouseMode.Home) isSecured = false; 
        }

        public void AdjustTemperature(double temperatureChange) {
            averageTemperature += temperatureChange;
        } 

        public void ToggleSecurity(bool status) => isSecured = status; // ручне керування охороною

        public bool CheckAndStabilizeVoltage(double newVoltage)
        {
            averageVoltage = newVoltage;
            return averageVoltage >= 198.0 && averageVoltage <= 242.0;
        }
    }
