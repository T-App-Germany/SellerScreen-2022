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

        public async Task<bool> Save()
        {
            try
            {
                using FileStream stream = new FileStream(Paths.settingsPath + "Storage.xml", FileMode.Create);
                XmlSerializer XML = new XmlSerializer(typeof(Storage));
                XML.Serialize(stream, this);
                return true;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage_Save", true);
                return false;
            }

        }

        public static async Task<Storage> Load()
        {
            try
            {
                using FileStream stream = new FileStream(Paths.settingsPath + "Storage.xml", FileMode.Open);
                XmlSerializer XML = new XmlSerializer(typeof(Storage));
                return (Storage)XML.Deserialize(stream);
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage_Load", true);
                return null;
            }
        }
    }
}