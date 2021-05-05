using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class DayStatics
    {
        public DayStatics() { }

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

        public decimal Revenue
        {
            get
            {
                decimal i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += Convert.ToDecimal(p.Sold * p.Price);
                }
                return i;
            }
        }

        public decimal Losses
        {
            get
            {
                decimal i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i -= Convert.ToDecimal(p.Redemptions * p.Price);
                }
                return i;
            }
        }

        public uint Cancellations
        {
            get
            {
                uint i = 0;
                foreach (SoldProduct p in _soldProducts.Values)
                {
                    i += p.Cancellations;
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

        public static async Task<DayStatics> Load(DateTime date)
        {
            try
            {
                return JsonConvert.DeserializeObject<DayStatics>(File.ReadAllText(Paths.staticsPath + $"{date.Date.Ticks}.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Statics.Day_Load", true);
                return null;
            }
        }

        public async Task<bool> Save(DateTime date = new DateTime())
        {
            try
            {
                if (date == default) date = DateTime.Now.Date;
                File.WriteAllText(Paths.staticsPath + $"{date.Date.Ticks}.json", JsonConvert.SerializeObject(this, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Statics.Day_Save", true);
                return false;
            }
        }

    }

    [Serializable]
    public class MonthStatics
    {
        public MonthStatics() { }

    }

    [Serializable]
    public class YearStatics
    {
        public YearStatics() { }

    }

    [Serializable]
    public class TotalStatics
    {
        public TotalStatics() { }
        
        private uint _customers;
        public uint Customers
        {
            get => _customers;
            set => _customers = value;
        }

        private uint _sold;
        public uint Sold
        {
            get => _sold;
            set => _sold = value;
        }

        private decimal _revenue;
        public decimal Revenue
        {
            get => _revenue;
            set => _revenue = value;
        }

        private decimal _losses;
        public decimal Losses
        {
            get => _losses;
            set => _losses = value;
        }

        private uint _cancellations;
        public uint Cancellations
        {
            get => _cancellations;
            set => _cancellations = value;
        }

        private uint _redemptions;
        public uint Redemptions
        {
            get => _redemptions;
            set => _redemptions = value;
        }

        private uint _disposals;
        public uint Disposals
        {
            get => _disposals;
            set => _disposals = value;
        }


        private List<SoldProduct> _topRevenue;
        public List<SoldProduct> TopRevenue
        {
            get => _topRevenue;
            set => _topRevenue = value;
        }

        private List<SoldProduct> _topSold;
        public List<SoldProduct> TopSold
        {
            get => _topSold;
            set => _topSold = value;
        }

        public static async Task<TotalStatics> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<TotalStatics>(File.ReadAllText(Paths.staticsPath + $"total.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Statics.Total_Load", true);
                return null;
            }
        }

        public async Task<bool> Save()
        {
            try
            {
                File.WriteAllText(Paths.staticsPath + $"total.json", JsonConvert.SerializeObject(this, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Statics.Total_Save", true);
                return false;
            }
        }
    }
}
