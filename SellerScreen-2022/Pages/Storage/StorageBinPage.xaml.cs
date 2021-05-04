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

namespace SellerScreen_2022.Pages.Storage
{
    public partial class StorageBinPage : Page
    {
        private bool reloading = false;
        private readonly Dictionary<short, double> widthId = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthName = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthAvailible = new Dictionary<short, double>();
        private readonly Dictionary<short, double> widthPrice = new Dictionary<short, double>();

        public StorageBinPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.storageData.Bin.Count == 0)
                await LoadBin();
            await BuildBin();
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Viewbox box)
            {
                double header = HeaderGrid.ActualHeight + HeaderGrid.Margin.Top + HeaderGrid.Margin.Bottom;
                double cmdBar = CmdBar.ActualHeight + CmdBar.Margin.Top + CmdBar.Margin.Bottom;
                double columns = ColumnHeaderGrid.ActualHeight + ColumnHeaderGrid.Margin.Top + ColumnHeaderGrid.Margin.Bottom;
                double space = box.ActualHeight - header - cmdBar - columns - BinItemView.Margin.Top - BinItemView.Margin.Bottom - 100;
                if (space < 0)
                {
                    space = 0;
                }

                BinItemView.MaxHeight = space;
            }
        }

        private void ItemId_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthId.ContainsKey(tag))
                {
                    widthId[tag] = txt.ActualWidth;
                }
                else
                {
                    widthId.Add(tag, txt.ActualWidth);
                }

                widthId.OrderBy(key => key.Value);
                HeaderNumberTxt.Width = widthId[0];
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
            if (BinItemView.SelectedItems.Count == 0)
            {
                DelItemBtn.IsEnabled = false;
                RestoreItemBtn.IsEnabled = false;
            }
            else
            {
                DelItemBtn.IsEnabled = true;
                RestoreItemBtn.IsEnabled = true;
            }
        }

        private bool CheckForItems()
        {
            if (BinItemView.Items.Count > 0)
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

        private async void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!reloading)
            {
                reloading = true;
                Storyboard sb = (Storyboard)FindResource("ReloadAnimation");
                sb.Begin();
                await LoadBin();
                await BuildBin();
                sb.Stop();
                reloading = false;
            }
        }

        private async Task BuildBin()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Bauen...";
            BinItemView.Items.Clear();
            foreach (KeyValuePair<string, Product> kvp in MainWindow.storageData.Bin)
            {
                await AddItemToBin(kvp.Value);
            }

            CheckForItems();
            InfoTxt.Visibility = Visibility.Collapsed;
        }

        private async Task<bool> LoadBin()
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Laden...";
            await MainWindow.storageData.LoadBin();
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> RestoreProduct(string key)
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Wiederherstellen...";
            await MainWindow.storageData.RestoreProduct(key);
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private async Task<bool> DeleteProduct(string key)
        {
            InfoTxt.Visibility = Visibility.Visible;
            InfoTxt.Text = "Löschen...";
            await MainWindow.storageData.DeleteProduct(key);
            InfoTxt.Visibility = Visibility.Collapsed;
            return true;
        }

        private void ChangeSelectionModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)ChangeSelectionModeBtn.IsChecked)
            {
                BinItemView.SelectionMode = SelectionMode.Extended;
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
                BinItemView.SelectionMode = SelectionMode.Multiple;
                DoubleAnimation ani = new DoubleAnimation
                {
                    To = 32,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuarticEase()
                };
                HeaderSelectTxt.BeginAnimation(WidthProperty, ani);
            }
        }

        private void ShowItemInfo(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                StorageItemWindow window = new StorageItemWindow(item.Tag.ToString());
                window.ShowDialog();
            }
        }

        private Task AddItemToBin(Product product)
        {
            ItemTempName.Text = product.Name;
            ItemTempAvailible.Text = product.Availible.ToString();
            ItempTempPrice.Text = product.Price.ToString("C");

            ItemTempId.Text = product.Key.ToString();
            ItemTemplate.Tag = product.Key;

            string xamlString = XamlWriter.Save(ItemTemplate);
            StringReader stringReader = new StringReader(xamlString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid item = (Grid)XamlReader.Load(xmlReader);
            MenuItem menuItem = (MenuItem)item.ContextMenu.Items[0];
            menuItem.Click += new RoutedEventHandler(ShowItemInfo);
            menuItem.Tag = product.Key;

            TextBlock txt = (TextBlock)item.Children[0];
            txt.SizeChanged += new SizeChangedEventHandler(ItemId_SizeChanged);
            txt = (TextBlock)item.Children[1];
            txt.SizeChanged += new SizeChangedEventHandler(ItemName_SizeChanged);
            txt = (TextBlock)item.Children[2];
            txt.SizeChanged += new SizeChangedEventHandler(ItemAvailible_SizeChanged);
            txt = (TextBlock)item.Children[3];
            txt.SizeChanged += new SizeChangedEventHandler(ItemPrice_SizeChanged);

            BinItemView.Items.Add(item);
            return Task.CompletedTask;
        }

        private async void DelItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList list = BinItemView.SelectedItems;
                for (int i = 0; i < list.Count; i++)
                {
                    Grid grid = (Grid)list[i];
                    if (await DeleteProduct(grid.Tag.ToString()))
                    {
                        BinItemView.Items.Remove(grid);
                        CheckForItems();
                    }
                    i--;
                }
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
            }
        }

        private async void RestoreItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IList list = BinItemView.SelectedItems;
                for (int i = 0; i < list.Count; i++)
                {
                    Grid grid = (Grid)list[i];
                    if (await RestoreProduct(grid.Tag.ToString()))
                    {
                        BinItemView.Items.Remove(grid);
                        CheckForItems();
                    }
                    i--;
                }
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
            }
        }
    }
}