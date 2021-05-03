using ModernWpf.Controls;
using System.Windows;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022
{
    public partial class StorageEditItemRemovePage : Page
    {
        public StorageEditItemRemovePage()
        {
            InitializeComponent();
            RemoveBox.NumberFormatter = new CustomNumberFormatter();
        }

        private class CustomNumberFormatter : INumberBoxNumberFormatter
        {
            public string FormatDouble(double value)
            {
                return value.ToString();
            }

            public double? ParseDouble(string text)
            {
                if (int.TryParse(text, out int result))
                {
                    return result;
                }
                return null;
            }
        }

        private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            StorageEditItemWindow.productItemEdited.Availible = StorageEditItemWindow.productItemEdited.Availible - (int)sender.Value;
            Btn2.IsChecked = true;
        }

        private void Btn1_Checked(object sender, RoutedEventArgs e)
        {
            StorageEditItemWindow.productItemEdited.Availible = 0;
        }
    }
}
