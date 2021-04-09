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
            double space = Vbox.ActualHeight - header - cmdBar - dGrid.Margin.Top - dGrid.Margin.Bottom - ExBox.Margin.Top - ExBox.Margin.Bottom;
            if (space < 0)
            {
                space = 0;
            }

            ExBox.MaxHeight = dGrid.MaxHeight = space / 2;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Paths.tempPath);
            await ErrorList.LoadList().ConfigureAwait(false);
            await LoadErrors();
        }

        private Task LoadErrors()
        {
            LoadProgBar.Visibility = Visibility.Visible;
            if (ErrorList.errors.Count > 0)
            {
                foreach (Errors error in ErrorList.errors)
                {
                    dGrid.Items.Add(error);
                }
            }
            else
            {
                //ExContent.Visibility = Visibility.Collapsed;
                //ExTitle.Text = "Kein Fehler ausgewählt";
            }
            LoadProgBar.Visibility = Visibility.Collapsed;
            dGrid.SelectedIndex = 0;
            return Task.CompletedTask;
        }

        private async void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            Errors error = new Errors
            {
                Page = "Lager",
                Time = DateTime.Now
            };

            try
            {
                File.ReadAllText("C:\\.lol");
            }
            catch (Exception ex)
            {
                error.Msg = ex.Message;
                error.Source = ex.Source;
                error.TargetSite = ex.TargetSite.ToString();
                error.StackTrace = ex.StackTrace;
                error.Type = ex.GetType().ToString();
                await Errors.ShowErrorMsg(error);
            }
            await error.Save();
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dGrid.SelectedItem != null)
            {
                Errors ex = (Errors)dGrid.SelectedItem;
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
                    dGrid.SelectedItem = e.RemovedItems[0];
                }
            }
        }
    }
}
