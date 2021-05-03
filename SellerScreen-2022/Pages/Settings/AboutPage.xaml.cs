using System.Windows;
using System.Windows.Controls;

namespace SellerScreen_2022.Pages.Settings
{
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();

            LicenseTxtBlock.Text = Properties.Resources.LICENSE;
        }

        private void ShowLicenseBtn_Click(object sender, RoutedEventArgs e)
        {
            LicenseDialog.ShowAsync();
        }

        private void LookForUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateDialog.ShowAsync();
        }
    }
}