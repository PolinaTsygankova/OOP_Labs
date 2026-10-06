using System;

namespace OOP_Lab1
{
    internal class Program
    {
        private static SmartHouse[] houses;
        private static int maxCapacity = 0;
        private static int currentCount = 0;

        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.Clear();
            Console.WriteLine("=== ІНІЦІАЛІЗАЦІЯ СИСТЕМИ ===");

            while (true)
            {
                Console.Write("Введіть максимальну кількість об'єктів N (N > 0): ");
                if (int.TryParse(Console.ReadLine(), out maxCapacity) && maxCapacity > 0)
                {
                    houses = new SmartHouse[maxCapacity];
                    break;
                }

                Console.WriteLine("ПОМИЛКА! Значення N має бути цілим додатним числом!");
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("       СИСТЕМА УПРАВЛІННЯ «SMART HOUSE»      ");
                Console.WriteLine($"       (Заповнено: {currentCount}/{maxCapacity})");
                Console.WriteLine("============================================");
                Console.WriteLine("1 – Додати об'єкт");
                Console.WriteLine("2 – Переглянути всі об'єкти");
                Console.WriteLine("3 – Знайти об'єкт");
                Console.WriteLine("4 – Продемонструвати поведінку");
                Console.WriteLine("5 – Видалити об'єкт");
                Console.WriteLine("0 – Вийти з програми");
                Console.WriteLine("============================================");
                Console.Write("Оберіть пункт меню: ");

                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        ChooseAddMode();
                        break;
                    case "2":
                        ViewAllHouses();
                        Pause();
                        break;
                    case "3":
                        SearchHouse();
                        Pause();
                        break;
                    case "4":
                        DemonstrateBehavior();
                        break;
                    case "5":
                        DeleteHouse();
                        Pause();
                        break;
                    case "0":
                        Console.WriteLine("\nЗавершення роботи програми. До побачення!");
                        return;
                    default:
                        Console.WriteLine("\nПОМИЛКА! Некоректний вибір меню.");
                        Pause();
                        break;
                }
            }
        }

        static void ChooseAddMode()
        {
            Console.Clear();

            if (currentCount >= maxCapacity)
            {
                Console.WriteLine($"ПОМИЛКА! Досягнуто ліміту N ({maxCapacity}). Неможливо додати більше об'єктів!");
                Pause();
                return;
            }

            Console.WriteLine("=== РЕЖИМ ДОДАВАННЯ ОБ'ЄКТА ===");
            Console.WriteLine("1 – Ручне введення даних");
            Console.WriteLine("2 – Автоматичне введення (генерація тестових даних)");
            Console.Write("Ваш вибір: ");

            string mode = Console.ReadLine()?.Trim();

            if (mode == "1") AddHouseManual();
            else if (mode == "2") AddHouseAuto();
            else
            {
                Console.WriteLine("ПОМИЛКА! Некоректний вибір!");
                Pause();
            }
        }

        static void AddHouseManual()
        {
            Console.Clear();
            Console.WriteLine($"=== ДОДАВАННЯ ОБ'ЄКТА (РУЧНЕ) ({currentCount + 1}/{maxCapacity}) ===\n");

            SmartHouse house = new SmartHouse();

            while (true)
            {
                Console.Write("Введіть адресу (5-60 символів): ");
                string input = Console.ReadLine()?.Trim() ?? "";
                try
                {
                    house.Adress = input;
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                }
            }

            while (true)
            {
                Console.Write("Введіть кількість кімнат (1-30): ");
                if (int.TryParse(Console.ReadLine(), out int r))
                {
                    try
                    {
                        house.Rooms = r;
                        break;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ПОМИЛКА! Введіть ціле число!");
                }
            }

            while (true)
            {
                Console.Write("Введіть ім'я власника (2-20 літер): ");
                string input = Console.ReadLine()?.Trim() ?? "";
                try
                {
                    house.OwnersName = input;
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                }
            }

            while (true)
            {
                Console.WriteLine("Оберіть режим: 0 - Home, 1 - Away, 2 - Econom, 3 - Night, 4 - Vacation");
                Console.Write("Ваш вибір: ");

                if (int.TryParse(Console.ReadLine(), out int modeVal) && modeVal >= 0 && modeVal <= 4)
                {
                    house.SwitchMode((HouseMode)modeVal);
                    break;
                }

                Console.WriteLine("ПОМИЛКА! Некоректний номер режиму!");
            }

            while (true)
            {
                Console.Write("Введіть бажану середню температуру (15.0 - 32.0 °C): ");
                if (double.TryParse(Console.ReadLine(), out double temp))
                {
                    try
                    {
                        house.AverageTemperature = temp;
                        break;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ПОМИЛКА! Введіть числове значення!");
                }
            }

            while (true)
            {
                Console.Write("Введіть середню напругу (198.0 - 242.0 В): ");
                if (double.TryParse(Console.ReadLine(), out double volt))
                {
                    try
                    {
                        house.AverageVoltage = volt;
                        break;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ПОМИЛКА! Введіть числове значення!");
                }
            }

            while (true)
            {
                Console.Write("Увімкнути охорону? (1 - Так, 0 - Ні): ");
                string input = Console.ReadLine()?.Trim();

                if (input == "1")
                {
                    house.ToggleSecurity(true);
                    break;
                }

                if (input == "0")
                {
                    house.ToggleSecurity(false);
                    break;
                }

                Console.WriteLine("ПОМИЛКА! Введіть 1 або 0!");
            }

            while (true)
            {
                Console.Write("Введіть дату останнього ТО (рррр-мм-дд): ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime dt) &&
                    dt >= new DateTime(2000, 1, 1) && dt <= DateTime.Now)
                {
                    house.DateOfLastServiceCheck = dt;
                    break;
                }

                Console.WriteLine("ПОМИЛКА! Некоректна дата (від 01.01.2000 до сьогодні)!");
            }

            houses[currentCount] = house;
            currentCount++;

            Console.WriteLine("\nУСПІХ! Об'єкт успішно додано ручним способом!");

            Pause();
        }

        static void AddHouseAuto()
        {
            Random rnd = new Random();

            string[] streetNames =
            {
                "вул. Соборна, ", "вул. Хрещатик, ", "просп. Перемоги, ", "вул. Шевченка, ", "вул. Лесі Українки, "
            };

            string[] ownerNames =
            {
                "Олександр", "Марія", "Володимир", "Олена", "Дмитро", "Анна"
            };

            SmartHouse house = new SmartHouse();

            house.Adress = streetNames[rnd.Next(streetNames.Length)] + rnd.Next(1, 100);
            house.OwnersName = ownerNames[rnd.Next(ownerNames.Length)];
            house.Rooms = rnd.Next(1, 6);
            house.SwitchMode((HouseMode)rnd.Next(0, 5));
            house.AverageTemperature = Math.Round(18.0 + rnd.NextDouble() * 8.0, 1);
            house.AverageVoltage = Math.Round(200.0 + rnd.NextDouble() * 40.0, 1);
            house.ToggleSecurity(rnd.Next(0, 2) == 1);
            house.DateOfLastServiceCheck = DateTime.Now.AddDays(-rnd.Next(10, 500));

            houses[currentCount] = house;
            currentCount++;

            Console.WriteLine("\nУСПІХ! Об'єкт автоматично згенеровано та додано!");
            Console.WriteLine($"Власник: {house.OwnersName} | Адреса: {house.Adress}");

            Pause();
        }

        static void ViewAllHouses()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ВСІХ ОБ'ЄКТІВ ===\n");

            if (currentCount == 0)
            {
                Console.WriteLine("ІНФО! Жодного об'єкта ще не додано.");
                return;
            }

            PrintTableHeaders();

            for (int i = 0; i < currentCount; i++)
            {
                PrintTableRow(i + 1, houses[i]);
            }
        }

        static void SearchHouse()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК ОБ'ЄКТІВ ===\n");

            if (currentCount == 0)
            {
                Console.WriteLine("ІНФО! Список об'єктів порожній.");
                return;
            }

            Console.WriteLine("Оберіть ПЕРШУ характеристику для пошуку:");
            Console.WriteLine("1 – За ім'ям власника");
            Console.WriteLine("2 – За адресою");
            Console.WriteLine("3 – За кількістю кімнат");
            Console.WriteLine("4 – За режимом");
            Console.WriteLine("5 – За температурою");
            Console.WriteLine("6 – За напругою");
            Console.WriteLine("7 – За охороною");
            Console.Write("Ваш вибір: ");

            if (!int.TryParse(Console.ReadLine(), out int criteria1) ||
                criteria1 < 1 || criteria1 > 7)
            {
                Console.WriteLine("\nПОМИЛКА! Некоректний вибір!");
                return;
            }

            Console.WriteLine("\nОберіть ДРУГУ характеристику для пошуку:");
            Console.WriteLine("1 – За ім'ям власника");
            Console.WriteLine("2 – За адресою");
            Console.WriteLine("3 – За кількістю кімнат");
            Console.WriteLine("4 – За режимом");
            Console.WriteLine("5 – За температурою");
            Console.WriteLine("6 – За напругою");
            Console.WriteLine("7 – За охороною");
            Console.Write("Ваш вибір: ");

            if (!int.TryParse(Console.ReadLine(), out int criteria2) ||
                criteria2 < 1 || criteria2 > 7)
            {
                Console.WriteLine("\nПОМИЛКА! Некоректний вибір!");
                return;
            }

            if (criteria1 == criteria2)
            {
                Console.WriteLine("\nПОМИЛКА! Потрібно обрати дві різні характеристики!");
                return;
            }

            Console.WriteLine("\n=== ВВЕДЕННЯ ЗНАЧЕНЬ ДЛЯ ПОШУКУ ===\n");

            string value1 = ReadSearchValue(criteria1);

            if (string.IsNullOrWhiteSpace(value1))
            {
                Console.WriteLine("\nПОМИЛКА! Значення не може бути порожнім!");
                return;
            }

            string value2 = ReadSearchValue(criteria2);

            if (string.IsNullOrWhiteSpace(value2))
            {
                Console.WriteLine("\nПОМИЛКА! Значення не може бути порожнім!");
                return;
            }

            Console.WriteLine("\n=== РЕЗУЛЬТАТИ ПОШУКУ ===\n");

            bool found = false;

            for (int i = 0; i < currentCount; i++)
            {
                SmartHouse house = houses[i];

                if (house == null)
                    continue;

                bool match1 = CheckSearchCriteria(house, criteria1, value1);
                bool match2 = CheckSearchCriteria(house, criteria2, value2);

                if (match1 && match2)
                {
                    if (!found)
                    {
                        PrintTableHeaders();
                    }

                    PrintTableRow(i + 1, house);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("[ІНФО] Об'єктів, що відповідають заданим характеристикам, не знайдено.");
            }
        }

        static string ReadSearchValue(int criteria)
        {
            switch (criteria)
            {
                case 1:
                    Console.Write("Введіть ім'я власника: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 2:
                    Console.Write("Введіть адресу: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 3:
                    Console.Write("Введіть кількість кімнат: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 4:
                    Console.WriteLine("0 - Home, 1 - Away, 2 - Econom, 3 - Night, 4 - Vacation");
                    Console.Write("Введіть номер режиму: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 5:
                    Console.Write("Введіть температуру: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 6:
                    Console.Write("Введіть напругу: ");
                    return Console.ReadLine()?.Trim() ?? "";

                case 7:
                    Console.Write("Введіть охорону (1 - Так, 0 - Ні): ");
                    return Console.ReadLine()?.Trim() ?? "";

                default:
                    return "";
            }
        }

        static bool CheckSearchCriteria(SmartHouse house, int criteria, string value)
        {
            value = value.Trim();

            switch (criteria)
            {
                case 1:
                    return string.Equals(house.OwnersName?.Trim(), value, StringComparison.OrdinalIgnoreCase);

                case 2:
                    return string.Equals(house.Adress?.Trim(), value, StringComparison.OrdinalIgnoreCase);

                case 3:
                    if (int.TryParse(value, out int rooms))
                    {
                        return house.Rooms == rooms;
                    }
                    return false;

                case 4:
                    if (int.TryParse(value, out int modeNumber))
                    {
                        if (modeNumber >= 0 && modeNumber <= 4)
                        {
                            return house.CurrentMode == (HouseMode)modeNumber;
                        }
                    }
                    return false;

                case 5:
                    if (double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double temperature))
                    {
                        return Math.Abs(house.AverageTemperature - temperature) < 0.01;
                    }
                    return false;

                case 6:
                    if (double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double voltage))
                    {
                        return Math.Abs(house.AverageVoltage - voltage) < 0.01;
                    }
                    return false;

                case 7:
                    if (value == "1" || value.Equals("Так", StringComparison.OrdinalIgnoreCase))
                    {
                        return house.IsSecured;
                    }

                    if (value == "0" || value.Equals("Ні", StringComparison.OrdinalIgnoreCase))
                    {
                        return !house.IsSecured;
                    }
                    return false;

                default:
                    return false;
            }
        }

        static void DemonstrateBehavior()
        {
            Console.Clear();

            if (currentCount == 0)
            {
                Console.WriteLine("ПОМИЛКА! Спочатку додайте хоча б один об'єкт!");
                Pause();
                return;
            }

            ViewAllHouses();

            Console.Write("\nВведіть номер об'єкта для демонстрації поведінки: ");

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > currentCount)
            {
                Console.WriteLine("ПОМИЛКА! Некоректний порядковий номер!");
                Pause();
                return;
            }

            SmartHouse target = houses[index - 1];

            while (true)
            {
                Console.Clear();

                Console.WriteLine($"=== ДЕМОНСТРАЦІЯ ПОВЕДІНКИ (Об'єкт №{index}: {target.Adress}) ===");
                Console.WriteLine($"Власник: {target.OwnersName} | Прошивка: {target.FirmwareVersion}");
                Console.WriteLine($"Стан: Режим = {target.CurrentMode}, " +
                                  $"Темп = {target.AverageTemperature:F1}°C, " +
                                  $"Напруга = {target.AverageVoltage:F1}В ({target.VoltageStatus}), " +
                                  $"Охорона = {(target.IsSecured ? "Так" : "Ні")}");

                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("1 – Змінити режим (SwitchMode)");
                Console.WriteLine("2 – Змінити температуру (AdjustTemperature)");
                Console.WriteLine("3 – Керування охороною (ToggleSecurity)");
                Console.WriteLine("4 – Перевірити/налаштувати напругу (CheckAndStabilizeVoltage)");
                Console.WriteLine("0 – Повернутися до головного меню");
                Console.Write("Оберіть дію: ");

                string subChoice = Console.ReadLine()?.Trim();

                switch (subChoice)
                {
                    case "1":
                        Console.WriteLine("\nНовий режим: 0 - Home, 1 - Away, 2 - Econom, 3 - Night, 4 - Vacation");

                        if (int.TryParse(Console.ReadLine(), out int m) && m >= 0 && m <= 4)
                        {
                            target.SwitchMode((HouseMode)m);
                            Console.WriteLine($"[ОК] Новий режим: {target.CurrentMode}. " +
                                              $"Охорона: {(target.IsSecured ? "Увімкнена" : "Вимкнена")}");
                        }
                        else Console.WriteLine("ПОМИЛКА! Некоректний номер!");

                        Pause();
                        break;

                    case "2":
                        Console.Write("\nВведіть зміну температури (наприклад, +2.0 або -1.5): ");

                        if (double.TryParse(Console.ReadLine(), out double tChange))
                        {
                            try
                            {
                                target.AdjustTemperature(tChange);
                                Console.WriteLine($"[ОК] Поточна температура після коригування: {target.AverageTemperature:F1} °C");
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine($"[ПОМИЛКА КЛІМАТУ] {ex.Message}");
                            }
                        }
                        else Console.WriteLine("ПОМИЛКА! Некоректне число!");

                        Pause();
                        break;

                    case "3":
                        Console.Write("\nУвімкнути охорону? (1 - Так, 0 - Ні): ");
                        string s = Console.ReadLine()?.Trim();

                        if (s == "1")
                        {
                            target.ToggleSecurity(true);
                            Console.WriteLine("[ОК] Охорону увімкнено.");
                        }
                        else if (s == "0")
                        {
                            target.ToggleSecurity(false);
                            Console.WriteLine("[ОК] Охорону вимкнено.");
                        }
                        else
                        {
                            Console.WriteLine("ПОМИЛКА! Некоректний ввід!");
                        }

                        Pause();
                        break;

                    case "4":
                        Console.Write("\nВведіть значення напруги для перевірки (наприклад, 220 або 250): ");

                        if (double.TryParse(Console.ReadLine(), out double newVoltage))
                        {
                            try
                            {
                                bool isVoltageNormal = target.CheckAndStabilizeVoltage(newVoltage);
                                if (isVoltageNormal)
                                {
                                    Console.WriteLine($"[OK] Напруга в нормі: {target.AverageVoltage}В ({target.VoltageStatus}).");
                                }
                                else
                                {
                                    Console.WriteLine($"[УВАГА] Небезпечна напруга: {target.AverageVoltage}В ({target.VoltageStatus})! Спрацював захист.");
                                }
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine($"[КРИТИЧНА ПОМИЛКА] {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ПОМИЛКА! Некоректне число!");
                        }

                        Pause();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("ПОМИЛКА! Некоректна опція!");
                        Pause();
                        break;
                }
            }
        }

        static void DeleteHouse()
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛЕННЯ ОБ'ЄКТА ===\n");

            if (currentCount == 0)
            {
                Console.WriteLine("ІНФО! Список порожній, нічого видаляти.");
                return;
            }

            Console.WriteLine("1 – Видалити за порядковим номером");
            Console.WriteLine("2 – Видалити за ім'ям власника");
            Console.Write("Ваш вибір: ");

            string opt = Console.ReadLine()?.Trim();

            if (opt == "1")
            {
                ViewAllHouses();

                Console.Write("\nВведіть номер для видалення: ");

                if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= currentCount)
                {
                    for (int i = idx - 1; i < currentCount - 1; i++)
                    {
                        houses[i] = houses[i + 1];
                    }

                    houses[currentCount - 1] = null;
                    currentCount--;

                    Console.WriteLine("УСПІХ! Об'єкт видалено!");
                }
                else
                {
                    Console.WriteLine("ПОМИЛКА! Об'єкта з таким номером не існує!");
                }
            }
            else if (opt == "2")
            {
                Console.Write("Введіть ім'я власника для видалення об'єктів: ");

                string name = Console.ReadLine()?.Trim() ?? "";
                int removedCount = 0;

                for (int i = 0; i < currentCount; i++)
                {
                    if (!string.IsNullOrEmpty(houses[i].OwnersName) &&
                        houses[i].OwnersName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        for (int j = i; j < currentCount - 1; j++)
                        {
                            houses[j] = houses[j + 1];
                        }

                        houses[currentCount - 1] = null;
                        currentCount--;
                        removedCount++;
                        i--;
                    }
                }

                if (removedCount > 0) Console.WriteLine($"УСПІХ! Видалено об'єктів: {removedCount}");
                else Console.WriteLine("ІНФО! Об'єктів з таким власником не знайдено.");
            }
            else Console.WriteLine("ПОМИЛКА! Некоректний вибір!");
        }

        static void PrintTableHeaders()
        {
            Console.WriteLine(new string('-', 140));
            Console.WriteLine($"| {"№",-2} | {"Власник",-12} | {"Адреса",-22} | {"Кімн",-4} | {"Режим",-8} | {"Темп",-6} | {"Напруга",-7} | {"Статус напруги",-28} | {"Охорона",-7} | {"ТО",-10} |");
            Console.WriteLine(new string('-', 140));
        }

        static void PrintTableRow(int index, SmartHouse h)
        {
            string owner = (h.OwnersName ?? "").Length > 12 ? h.OwnersName.Substring(0, 9) + "..." : (h.OwnersName ?? "");
            string addr = (h.Adress ?? "").Length > 22 ? h.Adress.Substring(0, 19) + "..." : (h.Adress ?? "");

            Console.WriteLine($"| {index,-2} | {owner,-12} | {addr,-22} | {h.Rooms,-4} | " +
                              $"{h.CurrentMode,-8} | {h.AverageTemperature,6:F1} | " +
                              $"{h.AverageVoltage,7:F1} | {h.VoltageStatus,-28} | " +
                              $"{(h.IsSecured ? "Так" : "Ні"),-7} | " +
                              $"{h.DateOfLastServiceCheck:yyyy-MM-dd} |");

            Console.WriteLine(new string('-', 140));
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }
    }
}