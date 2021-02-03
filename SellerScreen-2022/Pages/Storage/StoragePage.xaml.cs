using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerScreen_2022.Pages.Storage
{
    /// <summary>
    /// Interaktionslogik für StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        public static double ParentHeight { get; set; }

        public static double[] ItemWidth = new double[5];
        
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

        private void Item_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Grid item)
            {
                for (int i = 0; i < ItemWidth.Length; i++)
                {
                    if (item.ColumnDefinitions[i].ActualWidth < ItemWidth[i])
                    {
                        ItemWidth[i] = item.ColumnDefinitions[i].ActualWidth;

                        switch (i)
                        {
                            case 0:
                                HeaderNumberTxt.Width = ItemWidth[i];
                                break;
                            case 1:
                                //HeaderStatusTxt.Width = ItemWidth[i];
                                break;
                            case 2:
                                //HeaderNameTxt.Width = ItemWidth[i];
                                break;
                            case 3:
                                //HeaderAvailibleTxt.Width = ItemWidth[i];
                                break;
                            case 4:
                                //HeaderPriceTxt.Width = ItemWidth[i];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
