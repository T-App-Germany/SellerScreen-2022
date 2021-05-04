using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernWpf.Controls;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022
{
    public partial class StorageEditItemAvailiblePage : Page
    {
        public StorageEditItemAvailiblePage()
        {
            InitializeComponent();
            AvailibleTxt.Text = StorageEditItemWindow.productItem.Availible.ToString();
            AddBox.NumberFormatter = new CustomNumberFormatter();
        }

        private class CustomNumberFormatter : INumberBoxNumberFormatter
        {
            public string FormatDouble(double value)
            {
                return value.ToString();
            }

            public double? ParseDouble(string text)
            {
                if (uint.TryParse(text, out uint result))
                {
                    return result;
                }
                return null;
            }
        }

        private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            StorageEditItemWindow.productItemEdited.Availible = (uint)sender.Value;
        }
    }
}
