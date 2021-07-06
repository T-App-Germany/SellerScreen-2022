using ModernWpf;
using SellerScreen_2022.Helper;
using SellerScreen_2022.Languages;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Settings
{
    public partial class GeneralSettingsPage : Page
    {
        public GeneralSettingsPage()
        {
            InitializeComponent();
            ThemeBtns.SelectedIndex = Properties.Settings.Default.AppTheme;
        }

        private void RadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DispatcherHelper.RunOnMainThread(() =>
            {
                switch (ThemeBtns.SelectedIndex)
                {
                    case 0:
                        ThemeManager.Current.ApplicationTheme = null;
                        break;
                    case 1:
                        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                        break;
                    case 2:
                        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                        break;
                    default:
                        ThemeBtns.SelectedIndex = 0;
                        break;
                }
                Properties.Settings.Default.AppTheme = byte.Parse(ThemeBtns.SelectedIndex.ToString());
                Properties.Settings.Default.Save();
            });
        }
    }
}