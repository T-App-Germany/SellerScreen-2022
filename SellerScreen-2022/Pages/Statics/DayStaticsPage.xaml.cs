using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerScreen_2022.Pages.Statics
{
    public partial class DayStaticsPage : Page
    {
        private readonly Dictionary<short, double> widthNumber = new();
        private readonly Dictionary<short, double> widthName = new();
        private readonly Dictionary<short, double> widthPrice = new();
        private readonly Dictionary<short, double> widthSold = new();
        private readonly Dictionary<short, double> widthRevenue = new();

        public DayStaticsPage()
        {
            InitializeComponent();
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

                widthSold.OrderBy(key => key.Value);
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

                widthRevenue.OrderBy(key => key.Value);
                HeaderRevenueTxt.Width = widthRevenue[0];
            }
        }
    }
}
