using System;
using System.Collections.Generic;

namespace OOP_Lab6
{
    public class SmartHouseManager
    {
        private List<SmartHouse> _houses;

        public SmartHouseManager()
        {
            _houses = new List<SmartHouse>();
        }

        public int Count => _houses.Count;

        public void AddHouse(SmartHouse house)
        {
            if (house == null) throw new ArgumentNullException(nameof(house));
            _houses.Add(house);
        }

        public bool RemoveHouseAt(int index)
        {
            if (index < 0 || index >= _houses.Count) return false;

            SmartHouse.DecrementCounter();
            _houses.RemoveAt(index);
            return true;
        }

        public int RemoveHousesByOwner(string ownerName)
        {
            if (string.IsNullOrWhiteSpace(ownerName)) return 0;

            List<SmartHouse> toRemove = _houses.FindAll(h =>
                h.OwnersName != null &&
                h.OwnersName.Equals(ownerName, StringComparison.OrdinalIgnoreCase));

            foreach (var house in toRemove)
            {
                SmartHouse.DecrementCounter();
                _houses.Remove(house);
            }

            return toRemove.Count;
        }

        public List<SmartHouse> GetAllHouses()
        {
            return new List<SmartHouse>(_houses);
        }

        public SmartHouse GetHouseAt(int index)
        {
            if (index < 0 || index >= _houses.Count) return null;
            return _houses[index];
        }

        public List<SmartHouse> SearchHouses(Predicate<SmartHouse> criteria)
        {
            return _houses.FindAll(criteria);
        }
    }
}