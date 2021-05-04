using ModernWpf.Controls;

namespace SellerScreen_2022
{
    public partial class StorageEditItemPricePage : System.Windows.Controls.Page
    {
        public StorageEditItemPricePage()
        {
            InitializeComponent();
            PriceBox.Text = StorageEditItemWindow.productItem.Price.ToString("C");
            NewPriceBox.Value = (double)StorageEditItemWindow.productItemEdited.Price;
            NewPriceBox.NumberFormatter = new CustomNumberFormatter();
        }

        private class CustomNumberFormatter : INumberBoxNumberFormatter
        {
            public string FormatDouble(double value)
            {
                return value.ToString("C");
            }

            public double? ParseDouble(string text)
            {
                if (double.TryParse(text, out double result))
                {
                    return result;
                }
                return null;
            }
        }

        private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            StorageEditItemWindow.productItemEdited.Price = (decimal)sender.Value;
        }
    }
}
