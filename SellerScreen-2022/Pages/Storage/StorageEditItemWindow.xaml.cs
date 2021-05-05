using ModernWpf.Controls;
using SellerScreen_2022.Data;
using System.Globalization;
using System.Windows;

namespace SellerScreen_2022
{
    public partial class StorageEditItemWindow : Window
    {
        public Product prd;

        public StorageEditItemWindow(Product p)
        {
            InitializeComponent();
            AddBox.NumberFormatter = new NumberFormatter1();
            PriceBox.NumberFormatter = new NumberFormatter2();

            prd = p;
            Title = "Produkt: " + p.Key;
            NameBox.Text = p.Name;
            AddBox.Value = 0;
            PriceBox.Value = (double)p.Price;
            StatusBox.SelectedIndex = p.Status ? 0 : 1;
        }

        private class NumberFormatter1 : INumberBoxNumberFormatter
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

        private class NumberFormatter2 : INumberBoxNumberFormatter
        {
            public string FormatDouble(double value)
            {
                return value.ToString("C");
            }

            public double? ParseDouble(string text)
            {
                text = text.Replace(NumberFormatInfo.CurrentInfo.CurrencySymbol, string.Empty);
                if (double.TryParse(text, out double result))
                {
                    return result;
                }
                return null;
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            prd.Name = NameBox.Text;
            prd.Availible += (uint)AddBox.Value;
            prd.Price = (decimal)PriceBox.Value;
            prd.Status = StatusBox.SelectedIndex == 0;
            DialogResult = true;
            Close();
        }
    }
}
