using System;
using System.Collections.Generic;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class DayStatics
    {
        private Dictionary<string, SoldProduct> _soldProducts = new();
        public Dictionary<string, SoldProduct> SoldProducts
        {
            get => _soldProducts;
            set => _soldProducts = value;
        }

        public uint Sold
        {
            get
            {
                uint i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += p.Sold;
                }
                return i;
            }
        }

        public double Revenue
        {
            get
            {
                double i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += p.Sold * p.Price;
                }
                return i;
            }
        }

        public uint Redemptions
        {
            get
            {
                uint i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += p.Redemptions;
                }
                return i;
            }
        }

        public uint Disposals
        {
            get
            {
                uint i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += p.Disposals;
                }
                return i;

            }
        }

    }

    [Serializable]
    public class MonthStatics
    {

    }

    [Serializable]
    public class YearStatics
    {

    }

    [Serializable]
    public class TotalStatics
    {

    }
}
