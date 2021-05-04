using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class Storage
    {
        public Storage() { }

        private List<string> _Products = new List<string>();
        public List<string> Products
        {
            get => _Products;
            set => _Products = value;
        }

        private List<string> _Bin = new List<string>();
        public List<string> Bin
        {
            get => _Bin;
            set => _Bin = value;
        }

        public async Task<bool> Save()
        {
            try
            {
                File.WriteAllText(Paths.settingsPath + "Storage.json", JsonConvert.SerializeObject(this, Formatting.Indented));
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
                return JsonConvert.DeserializeObject<Storage>(File.ReadAllText(Paths.settingsPath + "Storage.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage_Load", true);
                return null;
            }
        }
    }
}