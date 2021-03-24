using System.Windows;
using System;
using SellerScreen_2022.Data;

namespace SellerScreen_2022
{
    public partial class StorageItemWindow : Window
    {
        Product product;

        public StorageItemWindow(ulong id)
        {
            Tag = id;
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            product = await Product.Load(ulong.Parse(Tag.ToString()));

            Title = product.Name;
            IdTxt.Text = product.Id.ToString();
            AvailibleTxt.Text = product.Availible.ToString();
            PriceTxt.Text = product.Price.ToString("C");

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
            Clipboard.SetText(product.Id.ToString());
        }
    }
}
