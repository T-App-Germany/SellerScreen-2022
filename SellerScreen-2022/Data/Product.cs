using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class Product
    {
        public enum ProductProperty
        {
            Name,
            Price,
            Availible,
            Status,
            Id
        }

        public enum ProductHealth
        {
            Ok,
            Warning,
            Error
        }

        public Product(string name, bool status, uint availible, float price, ulong? id = null)
        {
            Id = id == null ? GenId() : (ulong)id;
            Name = name;
            Status = status;
            Availible = availible;
            Price = price;
        }

        private Product() { }

        private ulong _Id;
        public ulong Id
        {
            get => _Id;
            set => _Id = value;
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

        private uint _PurchaseReverses;
        public uint PurchaseReverses
        {
            get => _PurchaseReverses;
            set => _PurchaseReverses = value;
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

        public static ulong GenId()
        {
            return (ulong)DateTime.UtcNow.Ticks / 1000000;
        }

        public static async Task<Product> Load(ulong id)
        {
            try
            {
                using FileStream stream = new FileStream(Paths.productsPath + $"{id}.xml", FileMode.Open);
                XmlSerializer XML = new XmlSerializer(typeof(Product));
                return (Product)XML.Deserialize(stream);
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Product_Load", true);
                return null;
            }
        }

        public async Task<ulong> Save()
        {
            try
            {
                using FileStream stream = new FileStream(Paths.productsPath + $"{Id}.xml", FileMode.Create);
                XmlSerializer XML = new XmlSerializer(typeof(Product));
                XML.Serialize(stream, this);
                return Id;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Product_Save", true);
                return 0;
            }
        }
    }
}