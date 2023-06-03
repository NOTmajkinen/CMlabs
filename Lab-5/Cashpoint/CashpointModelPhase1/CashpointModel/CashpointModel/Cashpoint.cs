namespace CashpointModel
{
    using System;
    using System.Collections.Generic;

    public sealed class Cashpoint
    {
        private readonly List<uint> _banknotes = new List<uint>();

        private List<bool> _granted = new List<bool>();

        private uint _total;

        private int _count = 0;

        public Cashpoint()
        {
            CalculateGrants(0);
        }

        public uint Total
        {
            get
            {
                return _total;
            }
        }

        public void AddBanknote(uint value)
        {
            _banknotes.Add(value);
            _total += value;
            _count++;
            CalculateGrants(value);
        }

        public void AddBanknote(uint value, int numberOfValues)
        {
            for (int i = 0; i < numberOfValues; i++)
            {
                AddBanknote(value);
            }
        }

        public void RemoveBanknote(uint value)
        {
             if (_banknotes.Remove(value))
            {
                _total -= value;
                _count--;
                RemoveGrants(value);
            }
        }

        public void RemoveBanknote(uint value, int numberOfValues)
        {
            for (int i = 0; i < numberOfValues; i++)
            {
                RemoveBanknote(value);
            }
        }

        public bool CanGrant(uint value)
        {
            if (value > _total)
            {
                return false;
            }
            else if (_granted.Count >= value)
            {
                return _granted[(int)value];
            }
            else
            {
                CalculateGrants(value);
                return _granted[(int)value];
            }
        }

        private void CalculateGrants(uint value)
        {
            _granted = AddItemsToBoolList(_granted, Math.Abs((int)(_granted.Count - (_total + 1))));
            _granted[0] = true;

            for (var i = (int)_total; i >= 0; i--)
            {
                if (_granted[i] && i + value < _granted.Count)
                {
                    _granted[i + (int)value] = true;
                }
            }
        }

        private void RemoveGrants(uint value)
        {
            _granted.Clear();

            for (int i = 0; i < _banknotes.Count; i++)
            {
                CalculateGrants(_banknotes[i]);
            }
        }

        private List<bool> AddItemsToBoolList(List<bool> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(false);
            }

            return list;
        }
    }
}