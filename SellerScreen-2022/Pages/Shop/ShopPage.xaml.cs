using ModernWpf.Controls;
using SellerScreen_2022.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Xml;
using static SellerScreen_2022.Data.Product;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Shop
{
    public partial class ShopPage : Page
    {
        private readonly Dictionary<short, double> widthNumber = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthName = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthAvailible = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthPrice = new Dictionary<short, double>();

        private bool reloading;

        public ShopPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.storageData.Products.Count == 0)
            {
                await MainWindow.storageData.LoadStorage();
            }

            await BuildShop();
            CancelPurchaseBtn_Click(sender, e);
        }

        private void ItemId_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthNumber.ContainsKey(tag))
                {
                    widthNumber[tag] = txt.ActualWidth;
                }
                else
                {
                    widthNumber.Add(tag, txt.ActualWidth);
                }

                widthNumber.OrderBy(key => key.Value);
                HeaderNumberTxt.Width = widthNumber[0];
            }
        }

        private void ItemName_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthName.ContainsKey(tag))
                {
                    widthName[tag] = txt.ActualWidth;
                }
                else
                {
                    widthName.Add(tag, txt.ActualWidth);
                }

                widthName.OrderBy(key => key.Value);
                HeaderNameTxt.Width = widthName[0];
            }
        }

        private void ItemAvailible_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthAvailible.ContainsKey(tag))
                {
                    widthAvailible[tag] = txt.ActualWidth;
                }
                else
                {
                    widthAvailible.Add(tag, txt.ActualWidth);
                }

                widthAvailible.OrderBy(key => key.Value);
                HeaderAvailibleTxt.Width = widthAvailible[0];
            }
        }

        private void ItemPrice_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthPrice.ContainsKey(tag))
                {
                    widthPrice[tag] = txt.ActualWidth;
                }
                else
                {
                    widthPrice.Add(tag, txt.ActualWidth);
                }

                widthPrice.OrderBy(key => key.Value);
                HeaderPriceTxt.Width = widthPrice[0];
            }
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Viewbox box)
            {
                double header = HeaderGrid.ActualHeight + HeaderGrid.Margin.Top + HeaderGrid.Margin.Bottom;
                double cmdBar = CmdBar.ActualHeight + CmdBar.Margin.Top + CmdBar.Margin.Bottom;
                double columns = ColumnHeaderGrid.ActualHeight + ColumnHeaderGrid.Margin.Top + ColumnHeaderGrid.Margin.Bottom;
                double space = box.ActualHeight - header - cmdBar - columns - ShopItemView.Margin.Top - ShopItemView.Margin.Bottom - 100;
                if (space < 0)
                {
                    space = 0;
                }

                ShopItemView.MaxHeight = space;
            }
        }

        private bool CheckForItems()
        {
            if (ShopItemView.Items.Count > 0)
            {
                EmptyTxt.Visibility = Visibility.Collapsed;
                ListViewGrid.Visibility = Visibility.Visible;
                return true;
            }
            else
            {
                EmptyTxt.Visibility = Visibility.Visible;
                ListViewGrid.Visibility = Visibility.Collapsed;
                return false;
            }
        }

        private ProductHealth CheckProductHealth(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name) && product.Availible > 10)
            {
                return ProductHealth.Ok;
            }
            else if (!string.IsNullOrEmpty(product.Name) && product.Availible > 0)
            {
                return ProductHealth.Warning;
            }
            else
            {
                return ProductHealth.Error;
            }
        }

        private async Task BuildShop()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bauen...";
            ShopItemView.Items.Clear();
            foreach (KeyValuePair<string, Product> kvp in MainWindow.storageData.Products)
            {
                await AddItemToStorage(kvp.Value);
            }

            CheckForItems();

            InfoTxt.Visibility = Visibility.Collapsed;
        }

        private Task AddItemToStorage(Product product)
        {
            if (product.Status && CheckProductHealth(product) <= (ProductHealth)1)
            {
                ItemTempName.Text = product.Name;
                ItemTempAvailible.Text = product.Availible.ToString();
                ItemTempPrice.Text = product.Price.ToString("C");

                switch (CheckProductHealth(product))
                {
                    case ProductHealth.Ok:
                        ItemTempIcon.Glyph = TempIconOk.Glyph;
                        ItemTempIcon.Foreground = TempIconOk.Foreground;
                        ItemTempIcon.ToolTip = TempIconOk.ToolTip;
                        break;
                    case ProductHealth.Warning:
                        ItemTempIcon.Glyph = TempIconWarn.Glyph;
                        ItemTempIcon.Foreground = TempIconWarn.Foreground;
                        ItemTempIcon.ToolTip = TempIconWarn.ToolTip;
                        break;
                }

                ItemTempNumber.Text = (ShopItemView.Items.Count + 1).ToString();
                ItemTemplate.Tag = product.Key;

                NumberBox nBox = new NumberBox()
                {
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline,
                    Margin = new Thickness(10, 0, 0, 0),
                    Minimum = 0,
                    Width = 150,
                    Value = 0
                };
                Grid.SetColumn(nBox, 5);

                string xamlString = XamlWriter.Save(ItemTemplate);
                StringReader stringReader = new StringReader(xamlString);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                Grid item = (Grid)XamlReader.Load(xmlReader);

                TextBlock txt = (TextBlock)item.Children[0];
                txt.SizeChanged += new SizeChangedEventHandler(ItemId_SizeChanged);
                txt = (TextBlock)item.Children[2];
                txt.SizeChanged += new SizeChangedEventHandler(ItemName_SizeChanged);
                txt = (TextBlock)item.Children[3];
                txt.SizeChanged += new SizeChangedEventHandler(ItemAvailible_SizeChanged);
                nBox.Maximum = Convert.ToDouble(txt.Text);
                txt = (TextBlock)item.Children[4];
                txt.SizeChanged += new SizeChangedEventHandler(ItemPrice_SizeChanged);
                item.Children.Add(nBox);

                ShopItemView.Items.Add(item);
            }
            CheckForItems();
            return Task.CompletedTask;
        }

        private void NewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerBtn.Visibility = Visibility.Collapsed;
            ReversePurchaseBtn.Visibility = Visibility.Collapsed;
            RetourBtn.Visibility = Visibility.Collapsed;
            PayBtn.Visibility = Visibility.Visible;
            CancelPurchaseBtn.Visibility = Visibility.Visible;
            TotalPricePanel.Visibility = Visibility.Visible;
            ClearCardBtn.Visibility = Visibility.Visible;
            DoubleAnimation ani = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()
            };
            ani.Completed += OpeningAni_Completed;
            ListViewGrid.IsEnabled = true;
            if (ListViewGrid.Effect == null)
            {
                BlurEffect ef = new BlurEffect() { Radius = 10 };
                ListViewGrid.Effect = ef;
            }
            ListViewGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, ani);
        }

        private void ReversePurchaseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RetourBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerBtn.Visibility = Visibility.Visible;
            ReversePurchaseBtn.Visibility = Visibility.Visible;
            RetourBtn.Visibility = Visibility.Visible;
            PayBtn.Visibility = Visibility.Collapsed;
            CancelPurchaseBtn.Visibility = Visibility.Collapsed;
            TotalPricePanel.Visibility = Visibility.Collapsed;
            ClearCardBtn.Visibility = Visibility.Collapsed;
            DoubleAnimation ani = new DoubleAnimation()
            {
                To = 10,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()
            };
            BlurEffect ef = new BlurEffect() { Radius = 0 };
            ListViewGrid.IsEnabled = false;
            ListViewGrid.Effect = ef;
            ef.BeginAnimation(BlurEffect.RadiusProperty, ani);
        }

        private void ClearCardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpeningAni_Completed(object sender, EventArgs e)
        {
            ListViewGrid.Effect = null;
        }

        private async void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!reloading)
            {
                reloading = true;
                Storyboard sb = (Storyboard)FindResource("ReloadAnimation");
                sb.Begin();
                await MainWindow.storageData.LoadStorage();
                await BuildShop();
                sb.Stop();
                reloading = false;
            }
        }
    }
}
