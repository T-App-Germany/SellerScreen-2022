using ModernWpf;
using ModernWpf.Controls;
using SellerScreen_2022.Pages;
using SellerScreen_2022.Pages.Home;
using SellerScreen_2022.Pages.Settings;
using SellerScreen_2022.Pages.Storage;
using SellerScreen_2022.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SellerScreen_2022
{
    public partial class MainWindow
    {
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("404", typeof(NotFoundPage)),
            ("home", typeof(HomePage)),
            ("storage", typeof(StoragePage)),
            ("settings", typeof(SettingsPage)),
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            DispatcherHelper.RunOnMainThread(() =>
            {
                if (this == Application.Current.MainWindow)
                {
                    this.SetPlacement(Settings.Default.MainWindowPlacement);
                }
            });

            MainNavView_Navigate("home");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                DispatcherHelper.RunOnMainThread(() =>
                {
                    if (this == Application.Current.MainWindow)
                    {
                        Settings.Default.MainWindowPlacement = this.GetPlacement();
                        Settings.Default.Save();
                    }
                });
            }
        }

        private void OnThemeButtonClick(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.RunOnMainThread(() =>
            {
                if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                }
            });
        }

        private void Window_ActualThemeChanged(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Light)
            {
                Background = (Brush)new BrushConverter().ConvertFrom("#CCFFFFFF");
            }
            else
            {
                Background = (Brush)new BrushConverter().ConvertFrom("#CC282828");
            }
        }

        private void MainNavView_PaneOpening(NavigationView sender, object args)
        {
            var sb = (Storyboard)FindResource("NavViewOpen");
            BeginStoryboard(sb);
        }

        private void MainNavView_PaneClosing(NavigationView sender, NavigationViewPaneClosingEventArgs args)
        {
            var sb = (Storyboard)FindResource("NavViewClose");
            BeginStoryboard(sb);
        }

        private void MainNavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (!string.IsNullOrEmpty(navItemTag))
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals("404"));
                _page = item.Page;
            }

            if (!(_page is null))
            {
                ContentFrame.Navigate(_page);
            }
        }

        private void MainNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                if (args.IsSettingsSelected) MainNavView_Navigate("settings");
                else if (!(args.SelectedItemContainer.Tag is null))
                {
                    var navItemTag = args.SelectedItemContainer.Tag.ToString();
                    MainNavView_Navigate(navItemTag);
                }
                else
                {
                    MainNavView_Navigate("");
                }
            }
        }

        private void acrylicWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HomePage.ParentHeight = ContentFrame.ActualHeight;
            StoragePage.ParentHeight = ContentFrame.ActualHeight;
        }
    }
}
