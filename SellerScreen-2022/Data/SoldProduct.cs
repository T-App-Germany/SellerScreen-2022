using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class SoldProduct
    {
        public SoldProduct(Product p)
        {
            Key = p.Key;
            Name = p.Name;
            Price = p.Price;
        }

        private SoldProduct() { }

        private string _Key;
        public string Key
        {
            get => _Key;
            set => _Key = value;
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => _Name = value;
        }

        private float _Price;
        public float Price
        {
            get => _Price;
            set => _Price = value;
        }

        private uint _Sold;
        public uint Sold
        {
            get => _Sold;
            set => _Sold = value;
        }

        private uint _Cancellations;
        public uint Cancellations
        {
            get => _Cancellations;
            set => _Cancellations = value;
        }

        private uint _Redemptions;
        public uint Redemptions
        {
            get => _Redemptions;
            set => _Redemptions = value;
        }

        private uint _Disposals;
        public uint Disposals
        {
            get => _Disposals;
            set => _Disposals = value;
        }

        public static async Task<SoldProduct> Load(string key)
        {
            try
            {
                return JsonConvert.DeserializeObject<SoldProduct>(File.ReadAllText(Paths.staticsPath + $"{key}.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "SoldProduct_Load", true);
                return null;
            }
        }

        public async Task<string> Save()
        {
            try
            {
                File.WriteAllText(Paths.staticsPath + $"{Key}.json", JsonConvert.SerializeObject(this, Formatting.Indented));
                return Key;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "SoldProduct_Save", true);
                return "";
            }
        }
    }
}