using System.Windows;
using System;
using SellerScreen_2022.Data;

namespace SellerScreen_2022
{
    public partial class StorageItemWindow : Window
    {
        Product product;

        public StorageItemWindow(string key)
        {
            Tag = key;
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            product = await Product.Load(Tag.ToString());

            Title = product.Name;
            IdTxt.Text = product.Key.ToString();
            AvailibleTxt.Text = product.Availible.ToString();
            PriceTxt.Text = product.Price.ToString("C");
            SoldTxt.Text = product.Sold.ToString();
            RevenueTxt.Text = product.Revenue.ToString("C");
            CancellationsTxt.Text = product.Cancellations.ToString();
            RedemptionsTxt.Text = product.Redemptions.ToString();
            DisposalsTxt.Text = product.Disposals.ToString();

            switch (product.Status)
            {
                case true:
                    StatusTxt.Text = "Aktiv";
                    break;

                case false:
                    StatusTxt.Text = "Inaktiv";
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(product.Key.ToString());
        }
    }
}
