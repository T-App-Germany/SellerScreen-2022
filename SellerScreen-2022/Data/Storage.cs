using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class Storage
    {
        public Storage() { }

        private List<ulong> _Products = new List<ulong>();
        public List<ulong> Products
        {
            get => _Products;
            set => _Products = value;
        }

        private List<ulong> _Bin = new List<ulong>();
        public List<ulong> Bin
        {
            get => _Bin;
            set => _Bin = value;
        }

        public Task Save()
        {
            using FileStream stream = new FileStream(Paths.settingsPath + "Storage.xml", FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(Storage));
            XML.Serialize(stream, this);
            return Task.CompletedTask;
        }

        public static Task<Storage> Load()
        {
            using FileStream stream = new FileStream(Paths.settingsPath + "Storage.xml", FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(Storage));
            return (Task<Storage>)XML.Deserialize(stream);
        }
    }
}
