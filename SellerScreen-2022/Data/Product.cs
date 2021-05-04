using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class Product
    {
        public enum ProductHealth
        {
            Ok,
            Warning,
            Error
        }

        public Product(string name, bool status, uint availible, float price, string key = null)
        {
            Key = key ?? GenKey();
            Name = name;
            Status = status;
            Availible = availible;
            Price = price;
        }

        private Product() { }

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

        private bool _Status;
        public bool Status
        {
            get => _Status;
            set => _Status = value;
        }

        private uint _Availible;
        public uint Availible
        {
            get => _Availible;
            set => _Availible = value;
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

        private float _Revenue;
        public float Revenue
        {
            get => _Revenue;
            set => _Revenue = value;
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

        public string GenKey()
        {
            byte[] codebytes = new byte[8];
            string code;

            do
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(codebytes);
                }
                code = BitConverter.ToString(codebytes).ToLower().Replace("-", "");
            } while (File.Exists(Paths.staticsPath + $"{code}.json"));

            Key = code;
            return code;
        }

        public static async Task<Product> Load(string key)
        {
            try
            {
                return JsonConvert.DeserializeObject<Product>(File.ReadAllText(Paths.productsPath + $"{key}.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Product_Load", true);
                return null;
            }
        }

        public async Task<string> Save()
        {
            try
            {
                File.WriteAllText(Paths.productsPath + $"{Key}.json", JsonConvert.SerializeObject(this, Formatting.Indented));
                return Key;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Product_Save", true);
                return "";
            }
        }
    }
}