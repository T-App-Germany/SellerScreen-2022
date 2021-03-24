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

        public Product(string name, bool status, int availible, double price, ulong? id = null)
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

        private int _Availible;
        public int Availible
        {
            get => _Availible;
            set => _Availible = value;
        }

        private double _Price;
        public double Price
        {
            get => _Price;
            set => _Price = value;
        }

        public static ulong GenId()
        {
            return (ulong)DateTime.UtcNow.Ticks / 1000000;
        }

        public static Task<Product> Load(ulong id)
        {
            using FileStream stream = new FileStream(Paths.productsPath + $"{id}.xml", FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(Product));
            return Task.FromResult((Product)XML.Deserialize(stream));
        }

        public Task<ulong> Save()
        {
            using FileStream stream = new FileStream(Paths.productsPath + $"{Id}.xml", FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(Product));
            XML.Serialize(stream, this);
            return Task.FromResult(Id);
        }
    }
}
