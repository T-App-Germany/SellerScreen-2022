using ModernWpf.Controls;
using SellerScreen_2022.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private enum SellType
        {
            None,
            Sell,
            Cancel,
            Redemption
        };

        private readonly Dictionary<short, double> widthNumber = new();
        private readonly Dictionary<short, double> widthName = new();
        private readonly Dictionary<short, double> widthAvailible = new();
        private readonly Dictionary<short, double> widthPrice = new();

        private bool reloading;
        private SellType sellType;
        private readonly Dictionary<int, (double, double)> shoppingCard = new();

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
                double cmdBar = SellCmdBar.ActualHeight + SellCmdBar.Margin.Top + SellCmdBar.Margin.Bottom;
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
                NewCustomerBtn.IsEnabled = true;
                return true;
            }
            else
            {
                EmptyTxt.Visibility = Visibility.Visible;
                ListViewGrid.Visibility = Visibility.Collapsed;
                NewCustomerBtn.IsEnabled = false;
                return false;
            }
        }

        private bool CheckForDayStatics(bool fullCheck = false)
        {
            if (File.Exists(Paths.staticsPath + DateTime.Now.Date.Ticks + ".json"))
            {
                if (fullCheck)
                {
                    DayStatics d = DayStatics.Load(DateTime.Now.Date).Result;
                    if (d.Sold == 0)
                    {
                        CancellationBtn.IsEnabled = false;
                        RedemptionBtn.IsEnabled = false;
                        return false;
                    }
                }

                CancellationBtn.IsEnabled = true;
                RedemptionBtn.IsEnabled = true;
                return true;
            }
            else
            {
                CancellationBtn.IsEnabled = false;
                RedemptionBtn.IsEnabled = false;
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
                await AddItemToShop(kvp.Value);
            }

            CheckForItems();
            CheckForDayStatics();

            InfoTxt.Visibility = Visibility.Collapsed;
        }

        private async Task BuildStaticsToShop()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bauen...";
            DayStatics d = await DayStatics.Load(DateTime.Now.Date);
            ShopItemView.Items.Clear();
            foreach (KeyValuePair<string, SoldProduct> kvp in d.SoldProducts)
            {
                Product p = new(kvp.Value.Name, true, kvp.Value.Sold, kvp.Value.Price, kvp.Key);
                await AddItemToShop(p);
            }

            CheckForItems();

            InfoTxt.Visibility = Visibility.Collapsed;
        }

        private Task AddItemToShop(Product product)
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

                NumberBox nBox = new()
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
                nBox.ValueChanged += NBox_ValueChanged;
                Grid.SetColumn(nBox, 5);

                string xamlString = XamlWriter.Save(ItemTemplate);
                StringReader stringReader = new(xamlString);
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

        private void NBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            Grid item = (Grid)sender.Parent;
            TextBlock t = (TextBlock)item.Children[4];
            int i = ShopItemView.Items.IndexOf(item);
            if (shoppingCard.ContainsKey(i))
            {
                shoppingCard[i] = (double.Parse(t.Text.Remove(t.Text.Length - 2)), sender.Value);
            }
            else
            {
                shoppingCard.Add(i, (double.Parse(t.Text.Remove(t.Text.Length - 2)), sender.Value));
            }

            GetTotalPrice();
        }

        private double GetTotalPrice()
        {
            double price = 0;
            foreach ((double d, double i) in shoppingCard.Values)
            {
                price += d * i;
            }
            TotalPriceTxt.Text = price.ToString("C");
            return price;
        }

        private void NewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            sellType = SellType.Sell;
            ShopCmdBar.Visibility = Visibility.Collapsed;
            SellCmdBar.Visibility = Visibility.Visible;
            TotalPricePanel.Visibility = Visibility.Visible;
            PayBtn.Visibility = Visibility.Visible;
            DoCancellationBtn.Visibility = Visibility.Collapsed;
            DoRetourBtn.Visibility = Visibility.Collapsed;
            _ = GetTotalPrice();
            DoubleAnimation ani = new()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()
            };
            ani.Completed += OpeningAni_Completed;
            ListViewGrid.IsEnabled = true;
            if (ListViewGrid.Effect == null)
            {
                BlurEffect ef = new() { Radius = 10 };
                ListViewGrid.Effect = ef;
            }
            ListViewGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, ani);
        }

        private async void CancellationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForDayStatics(true))
            {
                await BuildStaticsToShop();
                NewCustomerBtn_Click(sender, e);
                PayBtn.Visibility = Visibility.Collapsed;
                DoCancellationBtn.Visibility = Visibility.Visible;
                DoRetourBtn.Visibility = Visibility.Collapsed;
                sellType = SellType.Cancel;
            }
            else
            {
                CancellationBtn.IsEnabled = false;
                RedemptionBtn.IsEnabled = false;
            }
        }

        private async void RedemptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForDayStatics(true))
            {
                await BuildStaticsToShop();
                NewCustomerBtn_Click(sender, e);
                PayBtn.Visibility = Visibility.Collapsed;
                DoCancellationBtn.Visibility = Visibility.Collapsed;
                DoRetourBtn.Visibility = Visibility.Visible;
                sellType = SellType.Redemption;
            }
            else
            {
                CancellationBtn.IsEnabled = false;
                RedemptionBtn.IsEnabled = false;
            }
        }

        private async void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            List<int> remove = new();

            if (sellType == SellType.Sell)
            {
                foreach (Grid item in ShopItemView.Items)
                {
                    NumberBox nBox = (NumberBox)item.Children[5];
                    uint sell = Convert.ToUInt32(nBox.Value);

                    if (nBox.Value > 0)
                    {
                        Product p = await Load(item.Tag.ToString());
                        DayStatics d;
                        if (!File.Exists(Paths.staticsPath + $"{DateTime.Now.Date.Ticks}.json"))
                        {
                            d = new();
                        }
                        else
                        {
                            d = await DayStatics.Load(DateTime.Now.Date);
                        }

                        TotalStatics t;
                        if (!File.Exists(Paths.staticsPath + $"{DateTime.Now.Date.Ticks}.json"))
                        {
                            t = new();
                        }
                        else
                        {
                            t = await TotalStatics.Load();
                        }

                        p.Availible -= sell;
                        p.Sold += sell;
                        p.Revenue += Convert.ToDecimal(p.Price * sell);

                        t.Customers++;
                        t.Sold += sell;
                        t.Revenue += Convert.ToDecimal(p.Price * sell);

                        if (!d.SoldProducts.ContainsKey(p.Key))
                        {
                            d.SoldProducts.Add(p.Key, new SoldProduct(p));
                        }
                        d.SoldProducts[p.Key].Sold += sell;

                        await t.Save();
                        await p.Save();
                        await d.Save();

                        TextBlock txt = (TextBlock)item.Children[3];
                        txt.Text = p.Availible.ToString();

                        if (p.Availible < 1) remove.Add(ShopItemView.Items.IndexOf(item));
                    }
                }
                foreach (int i in remove)
                {
                    ShopItemView.Items.RemoveAt(i);
                }
                CheckForItems();
                CheckForDayStatics();
            }
            else if (sellType == SellType.Cancel)
            {
                foreach (Grid item in ShopItemView.Items)
                {
                    NumberBox nBox = (NumberBox)item.Children[5];
                    uint sell = Convert.ToUInt32(nBox.Value);

                    if (nBox.Value > 0)
                    {
                        Product p = await Load(item.Tag.ToString());
                        DayStatics d = await DayStatics.Load(DateTime.Now.Date);
                        TotalStatics t = await TotalStatics.Load();

                        p.Availible += sell;
                        p.Sold -= sell;
                        p.Cancellations += sell;
                        p.Revenue -= Convert.ToDecimal(p.Price * sell);

                        t.Sold -= sell;
                        t.Cancellations += sell;
                        t.Revenue -= Convert.ToDecimal(p.Price * sell);

                        if (!d.SoldProducts.ContainsKey(p.Key))
                        {
                            d.SoldProducts.Add(p.Key, new SoldProduct(p));
                        }
                        d.SoldProducts[p.Key].Sold -= sell;
                        d.SoldProducts[p.Key].Cancellations += sell;

                        await t.Save();
                        await p.Save();
                        await d.Save();

                        TextBlock txt = (TextBlock)item.Children[3];
                        txt.Text = p.Availible.ToString();
                    }
                }

                await BuildShop();
            }
            else if (sellType == SellType.Redemption)
            {
                foreach (Grid item in ShopItemView.Items)
                {
                    NumberBox nBox = (NumberBox)item.Children[5];
                    uint sell = Convert.ToUInt32(nBox.Value);

                    if (nBox.Value > 0)
                    {
                        Product p = await Load(item.Tag.ToString());
                        DayStatics d = await DayStatics.Load(DateTime.Now.Date);
                        TotalStatics t = await TotalStatics.Load();

                        p.Sold -= sell;
                        p.Redemptions += sell;
                        p.Revenue -= Convert.ToDecimal(p.Price * sell);

                        t.Redemptions += sell;
                        t.Sold -= sell;
                        t.Revenue -= Convert.ToDecimal(p.Price * sell);
                        t.Losses -= Convert.ToDecimal(p.Price * sell);

                        if (!d.SoldProducts.ContainsKey(p.Key))
                        {
                            d.SoldProducts.Add(p.Key, new SoldProduct(p));
                        }
                        d.SoldProducts[p.Key].Sold -= sell;
                        d.SoldProducts[p.Key].Redemptions += sell;

                        await t.Save();
                        await p.Save();
                        await d.Save();

                        TextBlock txt = (TextBlock)item.Children[3];
                        txt.Text = p.Availible.ToString();
                    }
                }

                await BuildShop();
            }

            CancelPurchaseBtn_Click(sender, e);
            ClearCardBtn_Click(sender, e);
        }

        private async void CancelPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            ShopCmdBar.Visibility = Visibility.Visible;
            SellCmdBar.Visibility = Visibility.Collapsed;
            TotalPricePanel.Visibility = Visibility.Collapsed;
            DoubleAnimation ani = new()
            {
                To = 10,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()
            };
            BlurEffect ef = new() { Radius = 0 };
            ListViewGrid.IsEnabled = false;
            ListViewGrid.Effect = ef;
            ef.BeginAnimation(BlurEffect.RadiusProperty, ani);

            if (sellType != SellType.Sell) await BuildShop();
            sellType = SellType.None;
        }

        private void ClearCardBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Grid item in ShopItemView.Items)
            {
                NumberBox nBox = (NumberBox)item.Children[5];
                nBox.Value = 0;
            }
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
