using System;

namespace SellerScreen_2022.Data
{
    public class Paths
    {
        public static readonly string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\T-App Germany\\SellerScreen-2022\\settings\\";
        public static readonly string productsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\T-App Germany\\SellerScreen-2022\\products\\";
        public static readonly string staticsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\T-App Germany\\SellerScreen-2022\\statics\\";
        public static readonly string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\T-App Germany\\SellerScreen-2022\\temp\\";
        public static readonly string logPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\T-App Germany\\SellerScreen-2022\\log\\";
    }
}
