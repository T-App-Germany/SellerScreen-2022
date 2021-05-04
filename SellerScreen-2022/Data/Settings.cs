using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Data
{
    [Serializable]
    public class Settings
    {
        public Settings() { }

        private bool _AutoUpdateChecker;
        public bool AutoUpdateChecker
        {
            get => _AutoUpdateChecker;
            set => _AutoUpdateChecker = value;
        }

        public async Task<bool> Save()
        {
            try
            {
                File.WriteAllText(Paths.settingsPath + "Settings.json", JsonConvert.SerializeObject(this, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Settings_Save", true);
                return false;
            }
        }

        public static async Task<Settings> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Paths.settingsPath + "Settings.json"));
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Settings_Load", true);
                return null;
            }
        }
    }
}