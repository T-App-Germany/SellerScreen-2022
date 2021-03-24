using System;
using System.IO;
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

        public void Save()
        {
            using FileStream stream = new FileStream(Paths.settingsPath + "Settings.xml", FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(Settings));
            XML.Serialize(stream, this);
        }

        public static Settings Load()
        {
            using FileStream stream = new FileStream(Paths.settingsPath + "Settings.xml", FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(Settings));
            return (Settings)XML.Deserialize(stream);
        }
    }
}
