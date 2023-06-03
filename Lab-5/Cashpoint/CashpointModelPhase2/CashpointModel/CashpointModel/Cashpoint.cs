namespace CashpointModel
{
    using System;
    using System.Collections.Generic;

    public sealed class Cashpoint
    {
        private Dictionary<uint, byte> _banknotes = new Dictionary<uint, byte>();

        private List<uint> _granted = new List<uint>();

        private uint _total;

        private int _count = 0;

        public Cashpoint()
        {
            AddBanknote(0);
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
            if (!_banknotes.ContainsKey(value))
            {
                _banknotes.Add(value, 1);
                _total += value;
                _count++;
                CalculateGrants(value);
            }
            else if (_banknotes[value] < 255)
            {
                var currentValue = _banknotes[value];
                currentValue++;
                _banknotes[value] = currentValue;
                _total += value;
                _count++;
                CalculateGrants(value);
            }
        }

        public void AddBanknote(uint value, int numberOfValues)
        {
            if (numberOfValues <= 255)
            {
                for (int i = 0; i < numberOfValues; i++)
                {
                    AddBanknote(value);
                }
            }
        }

        public void RemoveBanknote(uint value)
        {
             if (_banknotes.ContainsKey(value))
            {
                if (_banknotes[value] > 0)
                {
                   var currentValue = _banknotes[value];
                   currentValue--;
                   _banknotes[value] = currentValue;
                   _total -= value;
                   _count--;
                   if (currentValue == 0)
                    {
                        _banknotes.Remove(value);
                    }

                   RemoveGrants(value);
                }
                else
                {
                    _banknotes.Remove(value);
                }
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
            else
            {
                return _granted[(int)value] > 0;
            }
        }

        private void CalculateGrants(uint value)
        {
            _granted = AddItemsToUintList(_granted, Math.Abs((int)(_granted.Count - (_total + 1))));
            _granted[0] = 1;
            for (var i = (int)_total; i >= 0; i--)
            {
                if (_granted[i] > 0 && i + value < _granted.Count)
                {
                    _granted[i + (int)value] += i == 0 ? _banknotes[(uint)i] : value; // if can grant by one banknote assign its count, otherwise value
                }
            }
        }

        private void RemoveGrants(uint value)
        {
            _granted[(int)value] -= 1;

            for (int i = 0; i < _granted.Count; i++)
            {
                if (_granted[i] > 0)
                {
                    if (_banknotes.ContainsKey((uint)i) && _banknotes[(uint)i] != _granted[i] && i != value)
                    {
                        _granted[i] -= value;
                    }
                    else if (_granted[i] % value == 0)
                    {
                        _granted[i] -= value;
                    }

                    if (i > _total)
                    {
                        _granted[i] = 0;
                    }
                }
            }
        }

        private List<uint> AddItemsToUintList(List<uint> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(0);
            }

            return list;
        }
    }
}