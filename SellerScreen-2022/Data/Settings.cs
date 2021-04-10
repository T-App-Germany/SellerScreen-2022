using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                using FileStream stream = new FileStream(Paths.settingsPath + "Settings.xml", FileMode.Create);
                XmlSerializer XML = new XmlSerializer(typeof(Settings));
                XML.Serialize(stream, this);
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
                using FileStream stream = new FileStream(Paths.settingsPath + "Settings.xml", FileMode.Open);
                XmlSerializer XML = new XmlSerializer(typeof(Settings));
                return (Settings)XML.Deserialize(stream);
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Settings_Load", true);
                return null;
            }
        }
    }
}