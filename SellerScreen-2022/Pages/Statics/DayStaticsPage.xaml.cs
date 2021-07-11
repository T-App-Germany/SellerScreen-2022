using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
using System.Xml;

namespace SellerScreen_2022.Pages.Statics
{
    public partial class DayStaticsPage : Page
    {
        private readonly Dictionary<short, double> widthNumber = new();
        private readonly Dictionary<short, double> widthName = new();
        private readonly Dictionary<short, double> widthPrice = new();
        private readonly Dictionary<short, double> widthSold = new();
        private readonly Dictionary<short, double> widthRevenue = new();

        private bool reloading;
        private readonly List<ISeries> RevenueSeries = new();
        private readonly List<ISeries> SoldSeries = new();

        public DayStaticsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DayDatePicker.SelectedDate = MainWindow.selectedStaticsDates[0];
        }

        private async Task<bool> LoadDayStatic(DateTime date)
        {
            if (File.Exists(Paths.staticsPath + $"{date.Ticks}.json"))
            {
                DayStatics day = await DayStatics.Load(date);
                if (MainWindow.dayStatics.ContainsKey(date))
                {
                    MainWindow.dayStatics[date] = day;
                }
                else
                {
                    MainWindow.dayStatics.Add(date, day);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> BuildDayStatic(DateTime date)
        {
            if (MainWindow.dayStatics.TryGetValue(date, out DayStatics day))
            {
                DayLoadingRing.IsActive = true;
                DayItemView.Items.Clear();
                RevenueTxt.Text = day.Revenue.ToString("C");
                LossesTxt.Text = day.Losses.ToString("C");
                SoldTxt.Text = day.Sold.ToString();
                CancellationsTxt.Text = day.Cancellations.ToString();
                RedemptionsTxt.Text = day.Redemptions.ToString();
                DisposalsTxt.Text = day.Disposals.ToString();

                RevenueSeries.Clear();
                SoldSeries.Clear();
                foreach (SoldProduct p in day.SoldProducts.Values)
                {
                    _ = AddItemToChart(p);
                    await AddItemToList(p);
                }

                DayLoadingRing.IsActive = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private Task AddItemToChart(SoldProduct p)
        {
            SoldSeries.Add(new PieSeries<double> { Name = p.Name, Values = new List<double> { p.Sold }, TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue}" });
            RevenueSeries.Add(new PieSeries<decimal> { Name = p.Name, Values = new List<decimal> { p.Sold * p.Price }, TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue:C}" });

            RevenueChart.Series = RevenueSeries;
            SoldChart.Series = SoldSeries;
            return Task.CompletedTask;
        }

        private Task AddItemToList(SoldProduct p)
        {
            ItemTempName.Text = p.Name;
            ItemTempSold.Text = p.Sold.ToString();
            ItemTempPrice.Text = p.Price.ToString("C");
            ItemTempRevenue.Text = (p.Price * p.Sold).ToString("C");

            ItemTempNumber.Text = (DayItemView.Items.Count + 1).ToString();
            ItemTemplate.Tag = p.Key;

            string xamlString = XamlWriter.Save(ItemTemplate);
            StringReader stringReader = new(xamlString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid item = (Grid)XamlReader.Load(xmlReader);

            TextBlock txt = (TextBlock)item.Children[0];
            txt.SizeChanged += new SizeChangedEventHandler(ItemId_SizeChanged);
            txt = (TextBlock)item.Children[1];
            txt.SizeChanged += new SizeChangedEventHandler(ItemName_SizeChanged);
            txt = (TextBlock)item.Children[2];
            txt.SizeChanged += new SizeChangedEventHandler(ItemPrice_SizeChanged);
            txt = (TextBlock)item.Children[3];
            txt.SizeChanged += new SizeChangedEventHandler(ItemSold_SizeChanged);
            txt = (TextBlock)item.Children[4];
            txt.SizeChanged += new SizeChangedEventHandler(ItemRevenue_SizeChanged);

            DayItemView.Items.Add(item);
            return Task.CompletedTask;
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

                _ = widthNumber.OrderBy(key => key.Value);
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

                _ = widthName.OrderBy(key => key.Value);
                HeaderNameTxt.Width = widthName[0];
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

                _ = widthPrice.OrderBy(key => key.Value);
                HeaderPriceTxt.Width = widthPrice[0];
            }
        }

        private void ItemSold_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthSold.ContainsKey(tag))
                {
                    widthSold[tag] = txt.ActualWidth;
                }
                else
                {
                    widthSold.Add(tag, txt.ActualWidth);
                }

                _ = widthSold.OrderBy(key => key.Value);
                HeaderSoldTxt.Width = widthSold[0];
            }
        }

        private void ItemRevenue_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthRevenue.ContainsKey(tag))
                {
                    widthRevenue[tag] = txt.ActualWidth;
                }
                else
                {
                    widthRevenue.Add(tag, txt.ActualWidth);
                }

                _ = widthRevenue.OrderBy(key => key.Value);
                HeaderRevenueTxt.Width = widthRevenue[0];
            }
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SView.MaxHeight = ContentVbox.ActualHeight;
            SView.MaxWidth = ContentVbox.ActualWidth;
        }

        private async void DayDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = DayDatePicker.SelectedDate.Value.Date;
            MainWindow.selectedStaticsDates[0] = date;
            DayLoadingRing.IsActive = true;
            DayDatePicker.IsEnabled = false;
            SView.Visibility = Visibility.Visible;
            DayNotFoundLbl.Visibility = Visibility.Collapsed;
            if (!MainWindow.dayStatics.ContainsKey(date))
            {
                if (await LoadDayStatic(date))
                {
                    _ = await BuildDayStatic(date);
                }
                else
                {
                    SView.Visibility = Visibility.Hidden;
                    DayNotFoundLbl.Visibility = Visibility.Visible;
                }
            }
            else
            {
                _ = await BuildDayStatic(date);
            }
            DayDatePicker.IsEnabled = true;
            DayLoadingRing.IsActive = false;
        }

        private async void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!reloading)
            {
                reloading = true;
                DayDatePicker.IsEnabled = false;
                Storyboard sb = (Storyboard)FindResource("ReloadAnimation");
                sb.Begin();
                if (await LoadDayStatic(DayDatePicker.SelectedDate.Value.Date))
                {
                    SView.Visibility = Visibility.Visible;
                    DayNotFoundLbl.Visibility = Visibility.Collapsed;
                    _ = await BuildDayStatic(DayDatePicker.SelectedDate.Value.Date);
                }
                else
                {
                    SView.Visibility = Visibility.Hidden;
                    DayNotFoundLbl.Visibility = Visibility.Visible;
                }

                sb.Stop();
                DayDatePicker.IsEnabled = true;
                reloading = false;
            }
        }
    }
}
