using ModernWpf.Controls;
using SellerScreen_2022.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Xml;
using static SellerScreen_2022.Data.Product;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Storage
{
    public partial class StoragePage : Page
    {
        private readonly Dictionary<short, double> widthNumber = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthName = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthAvailible = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthPrice = new Dictionary<short, double>();

        private readonly Dictionary<ulong, Product> Products = new Dictionary<ulong, Product>();
        private readonly Dictionary<ulong, Product> Bin = new Dictionary<ulong, Product>();
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
            await LoadStorage();
            await BuildStorage();

            //Process proc = Process.GetCurrentProcess();
            //MessageBox.Show(proc.PrivateMemorySize64.ToString());
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
                DoubleAnimation ani = new DoubleAnimation
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
                DoubleAnimation ani = new DoubleAnimation
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
            foreach (KeyValuePair<ulong, Product> kvp in Products)
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
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                for (int i = 0; i < storage.Products.Count; i++)
                {
                    try
                    {
                        Product product = await Load(storage.Products[i]);
                        Products.Add(product.Id, product);
                    }
                    catch (Exception)
                    {

                    }
                }

                for (int i = 0; i < storage.Bin.Count; i++)
                {
                    try
                    {
                        Product product = await Load(storage.Bin[i]);
                        Bin.Add(product.Id, product);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> SaveStorage()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Speichern...";
            try
            {
                Data.Storage storage = new Data.Storage();
                foreach (KeyValuePair<ulong, Product> kvp in Products)
                {
                    storage.Products.Add(kvp.Value.Id);
                }

                foreach (KeyValuePair<ulong, Product> kvp in Bin)
                {
                    storage.Bin.Add(kvp.Value.Id);
                }

                await storage.Save();
            }
            catch (Exception)
            {
                return false;
            }

            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> RecycelProduct(ulong id)
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bereinigen...";
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                Bin.Add(id, Products[id]);
                Products.Remove(id);
                storage.Products.Remove(id);
                storage.Bin.Add(id);
                await storage.Save();
            }
            catch (Exception)
            {
                return false;
            }

            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> SaveProduct(Product product)
        {
            try
            {
                await product.Save();
            }
            catch (Exception)
            {
                return false;
            }
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

        private void AddItemToStorage(Product product)
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
            ItemTemplate.Tag = product.Id;

            string[] tag = { product.Id.ToString(), "" };

            string xamlString = XamlWriter.Save(ItemTemplate);
            StringReader stringReader = new StringReader(xamlString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid item = (Grid)XamlReader.Load(xmlReader);
            MenuItem menuItem = (MenuItem)item.ContextMenu.Items[0];
            menuItem.Click += new RoutedEventHandler(ShowItemInfo);
            for (int i = 2; i < item.ContextMenu.Items.Count; i++)
            {
                menuItem = (MenuItem)item.ContextMenu.Items[i];
                menuItem.Click += new RoutedEventHandler(EditItemDialog);
            }
            menuItem = null;

            TextBlock txt = (TextBlock)item.Children[0];
            txt.SizeChanged += new SizeChangedEventHandler(ItemId_SizeChanged);
            txt = (TextBlock)item.Children[2];
            txt.SizeChanged += new SizeChangedEventHandler(ItemName_SizeChanged);
            txt = (TextBlock)item.Children[3];
            txt.SizeChanged += new SizeChangedEventHandler(ItemAvailible_SizeChanged);
            txt = (TextBlock)item.Children[4];
            txt.SizeChanged += new SizeChangedEventHandler(ItemPrice_SizeChanged);
            txt = null;

            tag[1] = StorageItemView.Items.Add(item).ToString();
            item.ContextMenu.Tag = tag;

            CheckForItems();
        }

        private async void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product("n/a", false, 0, 0);
            if (await SaveProduct(product))
            {
                Products.Add(product.Id, product);
                if (await SaveStorage())
                {
                    AddItemToStorage(product);
                }
            }
        }

        private async void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            IList list = StorageItemView.SelectedItems;
            for (int i = 0; i < list.Count; i++)
            {
                Grid grid = (Grid)list[i];
                ulong id = ulong.Parse(grid.Tag.ToString());
                if (await RecycelProduct(id))
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
                ulong tag = ulong.Parse(grid.Tag.ToString());
                Product product = await Load(tag);
                product.Status = true;
                await product.Save();

                Grid item = (Grid)StorageItemView.Items[StorageItemView.Items.IndexOf(grid)];
                FontIcon icon = (FontIcon)item.Children[1];
                switch (CheckProductHealth(Products[tag]))
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
                Product product = await Load(ulong.Parse(item.Tag.ToString()));
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
                ulong id = ulong.Parse(tag[0]);
                StorageItemWindow window = new StorageItemWindow(id);
                window.ShowDialog();
            }
        }

        private void EditItemDialog(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                ContextMenu menu = (ContextMenu)item.Parent;
                string[] tag = (string[])menu.Tag;
                ulong id = ulong.Parse(tag[0]);
                Products.TryGetValue(id, out Product product);
                StorageEditItemWindow window = new StorageEditItemWindow(item.Tag.ToString(), product);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    product.Name = window.productItemReturn.Name;
                    product.Availible += window.productItemReturn.Availible;
                    product.Price = window.productItemReturn.Price;
                    Products[id] = product;
                    product.Save();

                    Grid grid = (Grid)StorageItemView.Items.GetItemAt(int.Parse(tag[1]));
                    TextBlock txt = (TextBlock)grid.Children[2];
                    txt.Text = product.Name;
                    txt = (TextBlock)grid.Children[3];
                    txt.Text = product.Availible.ToString();
                    txt = (TextBlock)grid.Children[4];
                    txt.Text = product.Price.ToString("C");

                    FontIcon icon = (FontIcon)grid.Children[1];
                    switch (CheckProductHealth(product))
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
    }
}