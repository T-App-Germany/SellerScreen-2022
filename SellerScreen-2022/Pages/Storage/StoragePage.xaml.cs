using ModernWpf.Controls;
using SellerScreen_2022.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using static SellerScreen_2022.Data.Product;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Storage
{
    public partial class StoragePage : Page
    {
        private readonly Dictionary<short, double> widthNumber = new();
        private readonly Dictionary<short, double> widthName = new();
        private readonly Dictionary<short, double> widthAvailible = new();
        private readonly Dictionary<short, double> widthPrice = new();

        private bool reloading;

        public StoragePage()
        {
            InitializeComponent();
            StorageItemView.Items.Clear();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Paths.productsPath));
            Directory.CreateDirectory(Path.GetDirectoryName(Paths.settingsPath));
            if (MainWindow.storageData.Products.Count == 0)
                await LoadStorage();
            await BuildStorage();
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Viewbox box)
            {
                double header = HeaderGrid.ActualHeight + HeaderGrid.Margin.Top + HeaderGrid.Margin.Bottom;
                double cmdBar = CmdBar.ActualHeight + CmdBar.Margin.Top + CmdBar.Margin.Bottom;
                double columns = ColumnHeaderGrid.ActualHeight + ColumnHeaderGrid.Margin.Top + ColumnHeaderGrid.Margin.Bottom;
                double space = box.ActualHeight - header - cmdBar - columns - StorageItemView.Margin.Top - StorageItemView.Margin.Bottom - 100;
                if (space < 0)
                {
                    space = 0;
                }

                StorageItemView.MaxHeight = space;
            }
        }

        private void ChangeSelectionModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)ChangeSelectionModeBtn.IsChecked)
            {
                StorageItemView.SelectionMode = SelectionMode.Extended;
                DoubleAnimation ani = new()
                {
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuarticEase()
                };
                HeaderSelectTxt.BeginAnimation(WidthProperty, ani);
            }
            else
            {
                StorageItemView.SelectionMode = SelectionMode.Multiple;
                DoubleAnimation ani = new()
                {
                    To = 32,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuarticEase()
                };
                HeaderSelectTxt.BeginAnimation(WidthProperty, ani);
            }
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

        private void StorageItemView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StorageItemView.SelectedItems.Count == 0)
            {
                RemoveItemBtn.IsEnabled = false;
                ActItemBtn.IsEnabled = false;
                DeactItemBtn.IsEnabled = false;
            }
            else
            {
                RemoveItemBtn.IsEnabled = true;
                ActItemBtn.IsEnabled = true;
                DeactItemBtn.IsEnabled = true;
            }
        }

        private bool CheckForItems()
        {
            if (StorageItemView.Items.Count > 0)
            {
                EmptyTxt.Visibility = Visibility.Collapsed;
                ListViewGrid.Visibility = Visibility.Visible;
                ChangeSelectionModeBtn.IsEnabled = true;
                return true;
            }
            else
            {
                EmptyTxt.Visibility = Visibility.Visible;
                ListViewGrid.Visibility = Visibility.Collapsed;
                ChangeSelectionModeBtn.IsEnabled = false;
                ChangeSelectionModeBtn.IsChecked = false;
                return false;
            }
        }

        private Task BuildStorage()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bauen...";
            StorageItemView.Items.Clear();
            foreach (KeyValuePair<string, Product> kvp in MainWindow.storageData.Products)
            {
                AddItemToStorage(kvp.Value);
            }

            CheckForItems();

            InfoTxt.Visibility = Visibility.Collapsed;
            return Task.CompletedTask;
        }

        private async Task<bool> LoadStorage()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Laden...";
            await MainWindow.storageData.LoadStorage();
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> SaveStorage()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Speichern...";
            await MainWindow.storageData.SaveStorage();
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> RecycelProduct(string key)
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bereinigen...";
            await MainWindow.storageData.RecycelProduct(key);
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
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

        private Task AddItemToStorage(Product product, int position = -1)
        {
            ItemTempName.Text = product.Name;
            ItemTempAvailible.Text = product.Availible.ToString();
            ItempTempPrice.Text = product.Price.ToString("C");

            if (product.Status)
            {
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
                    case ProductHealth.Error:
                        ItemTempIcon.Glyph = TempIconError.Glyph;
                        ItemTempIcon.Foreground = TempIconError.Foreground;
                        ItemTempIcon.ToolTip = TempIconError.ToolTip;
                        break;
                }
            }
            else
            {
                ItemTempIcon.Glyph = TempIconOff.Glyph;
                ItemTempIcon.Foreground = TempIconOff.Foreground;
                ItemTempIcon.ToolTip = TempIconOff.ToolTip;
            }

            ItemTempNumber.Text = (StorageItemView.Items.Count + 1).ToString();
            ItemTemplate.Tag = product.Key;

            string[] tag = { product.Key.ToString(), "" };

            string xamlString = XamlWriter.Save(ItemTemplate);
            StringReader stringReader = new(xamlString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid item = (Grid)XamlReader.Load(xmlReader);
            MenuItem menuItem = (MenuItem)item.ContextMenu.Items[0];
            menuItem.Click += new RoutedEventHandler(ShowItemInfo);
            menuItem = (MenuItem)item.ContextMenu.Items[2];
            menuItem.Click += new RoutedEventHandler(EditItemDialog);
            menuItem = (MenuItem)item.ContextMenu.Items[3];
            menuItem.Click += new RoutedEventHandler(DisposeItemDialog);

            TextBlock txt = (TextBlock)item.Children[0];
            txt.SizeChanged += new SizeChangedEventHandler(ItemId_SizeChanged);
            txt = (TextBlock)item.Children[2];
            txt.SizeChanged += new SizeChangedEventHandler(ItemName_SizeChanged);
            txt = (TextBlock)item.Children[3];
            txt.SizeChanged += new SizeChangedEventHandler(ItemAvailible_SizeChanged);
            txt = (TextBlock)item.Children[4];
            txt.SizeChanged += new SizeChangedEventHandler(ItemPrice_SizeChanged);
            txt = null;

            if (position == -1)
            {
                tag[1] = StorageItemView.Items.Add(item).ToString();
            }
            else
            {
                tag[1] = position.ToString();
                StorageItemView.Items.Insert(position, item);
            }
            item.ContextMenu.Tag = tag;

            CheckForItems();
            return Task.CompletedTask;
        }

        private Task EditItemInStorage(int i, Product p)
        {
            Grid item = (Grid)StorageItemView.Items.GetItemAt(i);
            TextBlock txt = (TextBlock)item.Children[0];
            txt.Text = (i + 1).ToString();
            txt.ToolTip = txt.Text;
            txt = (TextBlock)item.Children[2];
            txt.Text = p.Name;
            txt.ToolTip = txt.Text;
            txt = (TextBlock)item.Children[3];
            txt.Text = p.Availible.ToString();
            txt.ToolTip = txt.Text;
            txt = (TextBlock)item.Children[4];
            txt.Text = p.Price.ToString("C");
            txt.ToolTip = txt.Text;

            FontIcon icon = (FontIcon)item.Children[1];
            if (p.Status)
            {
                switch (CheckProductHealth(p))
                {
                    case ProductHealth.Ok:
                        icon.Glyph = TempIconOk.Glyph;
                        icon.Foreground = TempIconOk.Foreground;
                        icon.ToolTip = TempIconOk.ToolTip;
                        break;
                    case ProductHealth.Warning:
                        icon.Glyph = TempIconWarn.Glyph;
                        icon.Foreground = TempIconWarn.Foreground;
                        icon.ToolTip = TempIconWarn.ToolTip;
                        break;
                    case ProductHealth.Error:
                        icon.Glyph = TempIconError.Glyph;
                        icon.Foreground = TempIconError.Foreground;
                        icon.ToolTip = TempIconError.ToolTip;
                        break;
                }
            }
            else
            {
                icon.Glyph = TempIconOff.Glyph;
                icon.Foreground = TempIconOff.Foreground;
                icon.ToolTip = TempIconOff.ToolTip;
            }

            return Task.CompletedTask;
        }

        private async void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = new("n/a", false, 0, 0);
            try
            {
                await product.Save();
                MainWindow.storageData.Products.Add(product.Key, product);
                if (await SaveStorage())
                {
                    await AddItemToStorage(product);
                }
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage", true);
            }
        }

        private async void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            IList list = StorageItemView.SelectedItems;
            for (int i = 0; i < list.Count; i++)
            {
                Grid grid = (Grid)list[i];
                if (await RecycelProduct(grid.Tag.ToString()))
                {
                    StorageItemView.Items.Remove(grid);
                    CheckForItems();
                }
                i--;
            }
        }

        private async void ActItemBtn_Click(object sender, RoutedEventArgs e)
        {
            IList list = StorageItemView.SelectedItems;
            foreach (Grid grid in list)
            {
                Product product = await Load(grid.Tag.ToString());
                product.Status = true;
                await product.Save();

                Grid item = (Grid)StorageItemView.Items[StorageItemView.Items.IndexOf(grid)];
                FontIcon icon = (FontIcon)item.Children[1];
                switch (CheckProductHealth(MainWindow.storageData.Products[grid.Tag.ToString()]))
                {
                    case ProductHealth.Ok:
                        icon.Glyph = TempIconOk.Glyph;
                        icon.Foreground = TempIconOk.Foreground;
                        break;
                    case ProductHealth.Warning:
                        icon.Glyph = TempIconWarn.Glyph;
                        icon.Foreground = TempIconWarn.Foreground;
                        break;
                    case ProductHealth.Error:
                        icon.Glyph = TempIconError.Glyph;
                        icon.Foreground = TempIconError.Foreground;
                        break;
                }
            }
        }

        private async void DeactItemBtn_Click(object sender, RoutedEventArgs e)
        {
            IList list = StorageItemView.SelectedItems;
            foreach (Grid item in list)
            {
                Product product = await Load(item.Tag.ToString());
                product.Status = false;
                await product.Save();

                FontIcon icon = (FontIcon)item.Children[1];
                icon.Glyph = TempIconOff.Glyph;
                icon.Foreground = TempIconOff.Foreground;
                icon.ToolTip = TempIconOff.ToolTip;
            }
        }

        private void ShowItemInfo(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                ContextMenu menu = (ContextMenu)item.Parent;
                string[] tag = (string[])menu.Tag;
                StorageItemWindow window = new(tag[0]);
                window.ShowDialog();
            }
        }

        private async void EditItemDialog(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                ContextMenu menu = (ContextMenu)item.Parent;
                string[] tag = (string[])menu.Tag;
                MainWindow.storageData.Products.TryGetValue(tag[0], out Product product);
                StorageEditItemWindow window = new(product);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    product = window.prd;
                    MainWindow.storageData.Products[tag[0]] = product;
                    _ = SaveStorage();
                    await product.Save();

                    await EditItemInStorage(int.Parse(tag[1]), product);
                }
            }
        }

        private async void DisposeItemDialog(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                ContextMenu menu = (ContextMenu)item.Parent;
                string[] tag = (string[])menu.Tag;
                MainWindow.storageData.Products.TryGetValue(tag[0], out Product product);
                StorageDisposeItemWindow window = new(product.Availible, product.Key);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
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

                    if (!d.SoldProducts.ContainsKey(tag[0]))
                    {
                        d.SoldProducts.Add(tag[0], new SoldProduct(product));
                    }

                    if (window.DisposeAll)
                    {
                        d.SoldProducts[tag[0]].Disposals += product.Availible;
                        t.Disposals += product.Availible;
                        t.Losses -= product.Availible * product.Price;
                        product.Disposals += product.Availible;
                        product.Availible = 0;
                    }
                    else
                    {
                        d.SoldProducts[tag[0]].Disposals += window.Dispose;
                        t.Disposals += window.Dispose;
                        t.Losses -= window.Dispose * product.Price;
                        product.Disposals += window.Dispose;
                        product.Availible -= window.Dispose;
                    }


                    MainWindow.storageData.Products[tag[0]] = product;
                    _ = SaveStorage();
                    _ = d.Save();
                    _ = t.Save();
                    await product.Save();

                    await EditItemInStorage(int.Parse(tag[1]), product);
                }
            }
        }

        private async void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!reloading)
            {
                reloading = true;
                Storyboard sb = (Storyboard)FindResource("ReloadAnimation");
                sb.Begin();
                await LoadStorage();
                await BuildStorage();
                sb.Stop();
                reloading = false;
            }
        }

        private void StorageItemView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StorageItemView.SelectedItem != null && ChangeSelectionModeBtn.IsChecked == false)
            {
                Grid item = (Grid)StorageItemView.SelectedItem;
                StorageItemWindow window = new(item.Tag.ToString());
                window.ShowDialog();
            }
        }
    }
}