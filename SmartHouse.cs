using System;

namespace OOP_Lab1
{
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

        private static int _objectCounter = 0;

        public static int ObjectCounter { get { return _objectCounter; } }
        public static double MaxAllowedVoltage { get; set; } = 242.0;
        public static double MaxAllowedTemperature { get; set; } = 32.0;

        public static void DecrementCounter()
        {
            if (_objectCounter > 0)
                _objectCounter--;
        }

        public string FirmwareVersion { get; set; } = "v2.4.1";

        public SmartHouse()
        {
            _adress = "вул. Невідома, 1";
            _rooms = 1;
            _ownersName = "Невідомий";
            _currentMode = HouseMode.Home;
            _averageTemperature = 20.0;
            _averageVoltage = 220.0;
            _isSecured = false;
            _dateOfLastServiceCheck = DateTime.Now;

            _objectCounter++;
        }

        public SmartHouse(string adress, string ownersName) : this(adress, 2, ownersName, HouseMode.Home, 21.0, 220.0, false, DateTime.Now)
        {
        }

        public SmartHouse(string adress, int rooms, string ownersName, HouseMode currentMode,
                          double averageTemperature, double averageVoltage, bool isSecured, DateTime dateOfLastServiceCheck)
        {
            Adress = adress;
            Rooms = rooms;
            OwnersName = ownersName;
            CurrentMode = currentMode;
            AverageTemperature = averageTemperature;
            AverageVoltage = averageVoltage;
            IsSecured = isSecured;
            DateOfLastServiceCheck = dateOfLastServiceCheck;

            _objectCounter++;
        }

        public string Adress
        {
            get { return _adress; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5 || value.Length > 60)
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
                if (value < 15.0 || value > MaxAllowedTemperature)
                    throw new ArgumentOutOfRangeException($"Average temperature must be between 15.0 and {MaxAllowedTemperature} degrees Celsius.");
                _averageTemperature = value;
            }
        }

        public double AverageVoltage
        {
            get { return _averageVoltage; }
            set
            {
                if (value < 198.0 || value > MaxAllowedVoltage)
                    throw new ArgumentOutOfRangeException($"Average voltage must be between 198.0 and {MaxAllowedVoltage} volts.");
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

                if (_averageVoltage > 230.0 && _averageVoltage <= MaxAllowedVoltage)
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

        public void AdjustTemperature(double targetTemperature, HouseMode targetMode)
        {
            AverageTemperature = targetTemperature;
            SwitchMode(targetMode);
        }

        public void ToggleSecurity(bool status) => _isSecured = status;

        public bool CheckAndStabilizeVoltage(double newVoltage)
        {
            _averageVoltage = newVoltage;
            return _averageVoltage >= 198.0 && _averageVoltage <= MaxAllowedVoltage;
        }

        public string GetInfo()
        {
            return $"Адреса: {Adress}, Власник: {OwnersName}, Кімнат: {Rooms}, Режим: {CurrentMode}, " +
                   $"Температура: {AverageTemperature:F1}°C, Напруга: {AverageVoltage:F1}В ({VoltageStatus}), " +
                   $"Охорона: {(IsSecured ? "Увімкнена" : "Вимкнена")}, Перевірка: {DateOfLastServiceCheck:yyyy-MM-dd}";
        }

        public override string ToString()
        {
            string tempStr = AverageTemperature.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string voltStr = AverageVoltage.ToString(System.Globalization.CultureInfo.InvariantCulture);

            return $"{Adress};{Rooms};{OwnersName};{CurrentMode};{tempStr};{voltStr};{IsSecured};{DateOfLastServiceCheck:yyyy-MM-dd}";
        }

        public static SmartHouse Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException(nameof(s), "Рядок для парсингу не може бути порожнім.");

            string[] parts = s.Split(';');

            if (parts.Length != 8)
                throw new FormatException($"Некоректний формат рядка. Очікується 8 параметрів, отримано {parts.Length}.");

            string address = parts[0];
            int rooms = int.Parse(parts[1]);
            string owner = parts[2];
            HouseMode mode = Enum.Parse<HouseMode>(parts[3], true);

            double temp = double.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture);
            double voltage = double.Parse(parts[5], System.Globalization.CultureInfo.InvariantCulture);

            bool isSecured = bool.Parse(parts[6]);
            DateTime date = DateTime.Parse(parts[7]);

            return new SmartHouse(address, rooms, owner, mode, temp, voltage, isSecured, date);
        }

        public static bool TryParse(string s, out SmartHouse house)
        {
            house = null;
            bool valid = false;

            try
            {
                house = Parse(s);
                valid = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[Помилка формату]: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"[Помилка порожнього рядка]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Помилка даних об'єкта TryParse]: {ex.Message}");
            }

            return valid;
        }

        public static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }
    }
}