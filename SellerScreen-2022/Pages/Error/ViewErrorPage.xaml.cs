using SellerScreen_2022.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Error
{
    public partial class ViewErrorPage : Page
    {
        public ViewErrorPage()
        {
            InitializeComponent();
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ErrorContainer.MaxWidth = ContentGrid.ActualWidth;

            double header = HeaderGrid.ActualHeight + HeaderGrid.Margin.Top + HeaderGrid.Margin.Bottom;
            double cmdBar = CmdBar.ActualHeight + CmdBar.Margin.Top + CmdBar.Margin.Bottom;
            double space = Vbox.ActualHeight - header - cmdBar - ErrorDataGrid.Margin.Top - ErrorDataGrid.Margin.Bottom - ExBox.Margin.Top - ExBox.Margin.Bottom;
            if (space < 0)
            {
                space = 0;
            }

            ExBox.MaxHeight = ErrorDataGrid.MaxHeight = space / 2;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Paths.tempPath);
            await ErrorList.Load().ConfigureAwait(false);
            await LoadErrors();
        }

        private async Task LoadErrors()
        {
            LoadProgBar.Visibility = Visibility.Visible;
            if (ErrorList.errors.Count > 0)
            {
                foreach (Errors error in ErrorList.errors)
                {
                    if (!ErrorDataGrid.Items.Contains(error))
                    {
                        ErrorDataGrid.Items.Add(error);
                    }
                }
                ErrorDataGrid.SelectedIndex = 0;

                ErrorContainer.Visibility = Visibility.Visible;
                EmptyTxt.Visibility = Visibility.Collapsed;
            }
            else
            {
                await ErrorList.Load();
                if (ErrorList.errors.Count > 0)
                {
                    ErrorDataGrid.Items.Clear();
                    foreach (Errors error in ErrorList.errors)
                    {
                        ErrorDataGrid.Items.Add(error);
                    }

                    ErrorDataGrid.SelectedIndex = 0;
                    ErrorContainer.Visibility = Visibility.Visible;
                    EmptyTxt.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ErrorContainer.Visibility = Visibility.Collapsed;
                    EmptyTxt.Visibility = Visibility.Visible;
                }
            }
            LoadProgBar.Visibility = Visibility.Collapsed;
        }

        private void ErrorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ErrorDataGrid.SelectedItem != null)
            {
                Errors ex = (Errors)ErrorDataGrid.SelectedItem;
                ExTitle.Text = ex.Type;

                ExMsg.Text = ex.Msg != null ? ex.Msg.ToString() : "null";
                ExSourceTxt.Text = ex.Source != null ? ex.Source.ToString() : "null";
                ExTsTxt.Text = ex.TargetSite != null ? ex.TargetSite.ToString() : "null";
                ExsStTxt.Text = ex.StackTrace != null ? ex.StackTrace.ToString() : "null";

                ExContent.Visibility = Visibility.Visible;
            }
            else
            {
                ExContent.Visibility = Visibility.Collapsed;
                ExTitle.Text = "Kein Fehler ausgewählt";
                if (e.RemovedItems.Count > 0)
                {
                    ErrorDataGrid.SelectedItem = e.RemovedItems[0];
                }
            }
        }
    }
}
