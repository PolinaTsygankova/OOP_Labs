using System;
using OOP_Lab1;

public class SmartHouse
    {
	    private string _adress;
	    private int _rooms;
	    private string _ownersName;
	    private HouseMode _currentMode;
	    private double _averageTemperature;
	    private double _averageVoltage;
	    private bool _isSecured;
	    private DateTime _dateOfLastServiceCheck;

        public string FirmwareVersion { get; set; } = "v2.4.1";

        public string Adress {
           get { return _adress; }
            set {
            if(string.IsNullOrWhiteSpace(value) || value.Length < 5 || value.Length > 60)
                throw new ArgumentException("Adress cannot be empty or be under 5 or over 60 characters.");
            _adress = value; 
            }
         }

        public int Rooms
        {
            get { return _rooms; }
            set
            {
               if (value < 1 || value > 30)
                   throw new ArgumentOutOfRangeException("Rooms must be between 1 and 30.");
               _rooms = value;
            }
        }

        public string OwnersName
        {
            get { return _ownersName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 20)
                    throw new ArgumentException("Owner's name cannot be empty or be under 2 or over 20 characters.");
                _ownersName = value;
            }
        }
        public HouseMode CurrentMode { get => _currentMode; set => _currentMode = value; }
        public double AverageTemperature
        {
            get { return _averageTemperature; }
            set
            {
                if (value < 15.0 || value > 32.0)
                    throw new ArgumentOutOfRangeException("Average temperature must be between 15.0 and 32.0 degrees Celsius.");
                _averageTemperature = value;
            }
        }   
        public double AverageVoltage {
            get { return _averageVoltage; } 
            set
            {
                if(value < 198.0 || value > 242.0)
                    throw new ArgumentOutOfRangeException("Average voltage must be between 198.0 and 242.0 volts.");
                _averageVoltage = value;
            }
        }

        public string VoltageStatus
        {
            get
            {
                if (_averageVoltage < 198.0)
                    return "Критично низька напруга!";

                if (_averageVoltage >= 198.0 && _averageVoltage < 210.0)
                    return "Низька (але в межах норми)";

                if (_averageVoltage >= 210.0 && _averageVoltage <= 230.0)
                    return "Оптимальна (норма)";

                if (_averageVoltage > 230.0 && _averageVoltage <= 242.0)
                    return "Підвищена (але в межах норми)";

                return "Критично висока напруга!";
            }
        }

        public bool IsSecured { get => _isSecured; private set => _isSecured = value; }
        public DateTime DateOfLastServiceCheck { get => _dateOfLastServiceCheck; set => _dateOfLastServiceCheck = value; }

        public void SwitchMode(HouseMode newMode)
        {
            CurrentMode = newMode;
            ApplySecurityRules(newMode);
        }

        private void ApplySecurityRules(HouseMode mode)
        {
            if (mode == HouseMode.Away || mode == HouseMode.Vacation) _isSecured = true;
            else if (mode == HouseMode.Home) _isSecured = false;
        }
        public void AdjustTemperature(double temperatureChange)
        {
            AverageTemperature += temperatureChange;
        }

        public void ToggleSecurity(bool status) => _isSecured = status; 

         public bool CheckAndStabilizeVoltage(double newVoltage)
         {
            _averageVoltage = newVoltage;
            return _averageVoltage >= 198.0 && _averageVoltage <= 242.0;
         }
}

