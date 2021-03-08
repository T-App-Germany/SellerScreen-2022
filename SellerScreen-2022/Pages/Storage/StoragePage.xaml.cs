using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SellerScreen_2022.Pages.Storage
{
    public partial class StoragePage : System.Windows.Controls.Page
    {
        public static double ParentHeight { get; set; }

        Dictionary<short, double> widthNumber = new Dictionary<short, double>();
        Dictionary<short, double> widthName = new Dictionary<short, double>();
        Dictionary<short, double> widthAvailible = new Dictionary<short, double>();
        Dictionary<short, double> widthPrice = new Dictionary<short, double>();

        public StoragePage()
        {
            InitializeComponent();
        }

        private async Task ChangeStorageItemViewHeigth()
        {
            double before = 0;
            while (true)
            {
                if (ParentHeight != before)
                {
                    before = ParentHeight;
                    double title_heigth = HeaderTxt.ActualHeight + HeaderTxt.Margin.Top + HeaderTxt.Margin.Bottom;
                    double logo_heigth = CmdBar.ActualHeight + CmdBar.Margin.Top + CmdBar.Margin.Bottom;
                    double columns_heigth = ColumnHeaderGrid.ActualHeight + ColumnHeaderGrid.Margin.Top + ColumnHeaderGrid.Margin.Bottom;
                    double space = ParentHeight - title_heigth - logo_heigth - columns_heigth - StorageItemView.Margin.Top - StorageItemView.Margin.Bottom;
                    StorageItemView.MaxHeight = space - 30;
                }
                await Task.Delay(50);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeStorageItemViewHeigth().ConfigureAwait(false);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (StorageItemView.SelectionMode == SelectionMode.Multiple)
            {
                StorageItemView.SelectionMode = SelectionMode.Extended;
                HeaderSelectTxt.Width = 0;
                ChangeSelectionModeBtn.Label = "Auswählen";
            }
            else
            {
                StorageItemView.SelectionMode = SelectionMode.Multiple;
                HeaderSelectTxt.Width = 34;
                ChangeSelectionModeBtn.Label = "Auswählen beenden";
            }
        }

        private void ItemId_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthNumber.ContainsKey(tag)) widthNumber[tag] = txt.ActualWidth;
                else widthNumber.Add(tag, txt.ActualWidth);

                widthNumber.OrderBy(key => key.Value);
                HeaderNumberTxt.Width = widthNumber[0];
            }
        }

        private void ItemName_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthName.ContainsKey(tag)) widthName[tag] = txt.ActualWidth;
                else widthName.Add(tag, txt.ActualWidth);

                widthName.OrderBy(key => key.Value);
                HeaderNameTxt.Width = widthName[0];
            }
        }

        private void ItemAvailible_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthAvailible.ContainsKey(tag)) widthAvailible[tag] = txt.ActualWidth;
                else widthAvailible.Add(tag, txt.ActualWidth);

                widthAvailible.OrderBy(key => key.Value);
                HeaderAvailibleTxt.Width = widthAvailible[0];
            }
        }

        private void ItemPrice_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBlock txt)
            {
                short tag = short.Parse(txt.Tag.ToString());
                if (widthPrice.ContainsKey(tag)) widthPrice[tag] = txt.ActualWidth;
                else widthPrice.Add(tag, txt.ActualWidth);

                widthPrice.OrderBy(key => key.Value);
                HeaderPriceTxt.Width = widthPrice[0];
            }
        }
    }
}
