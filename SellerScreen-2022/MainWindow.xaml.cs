using ModernWpf;
using ModernWpf.Controls;
using SellerScreen_2022.Data;
using SellerScreen_2022.Pages;
using SellerScreen_2022.Pages.Error;
using SellerScreen_2022.Pages.Home;
using SellerScreen_2022.Pages.Settings;
using SellerScreen_2022.Pages.Shop;
using SellerScreen_2022.Pages.Statics;
using SellerScreen_2022.Pages.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Settings = SellerScreen_2022.Data.Settings;

namespace SellerScreen_2022
{
    public partial class MainWindow
    {
        public static StorageData storageData = new();

        private readonly List<(string Tag, Type Page)> _pages = new()
        {
            ("404", typeof(NotFoundPage)),
            ("home", typeof(HomePage)),
            ("shop", typeof(ShopPage)),
            ("storage", typeof(StoragePage)),
            ("storage_bin", typeof(StorageBinPage)),
            ("statics", typeof(StaticsPage)),
            ("errors", typeof(ViewErrorPage)),
            ("settings", typeof(SettingsPage)),
        };

        public MainWindow()
        {
            InitializeComponent();
            Paths.CreateAllDirectories().ConfigureAwait(true);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            DispatcherHelper.RunOnMainThread(() =>
            {
                if (this == Application.Current.MainWindow)
                {
                    this.SetPlacement(Properties.Settings.Default.MainWindowPlacement);
                }
            });

            SetLanguageDictionary();
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
                        Properties.Settings.Default.MainWindowPlacement = this.GetPlacement();
                        Properties.Settings.Default.Save();
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
            Storyboard sb = (Storyboard)FindResource("NavViewOpen");
            BeginStoryboard(sb);
        }

        private void MainNavView_PaneClosing(NavigationView sender, NavigationViewPaneClosingEventArgs args)
        {
            Storyboard sb = (Storyboard)FindResource("NavViewClose");
            BeginStoryboard(sb);
        }

        private void MainNavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (!string.IsNullOrEmpty(navItemTag))
            {
                (string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            else
            {
                (string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Tag.Equals("404"));
                _page = item.Page;
            }

            if (!(_page is null))
            {
                ContentFrame.Navigate(_page, null);
            }
        }

        private void MainNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                if (args.IsSettingsSelected)
                {
                    MainNavView_Navigate("settings");
                }
                else if (!(args.SelectedItemContainer.Tag is null))
                {
                    string navItemTag = args.SelectedItemContainer.Tag.ToString();
                    MainNavView_Navigate(navItemTag);
                }
                else
                {
                    MainNavView_Navigate("");
                }
            }
        }

        private void OnSizeButtonClick(object sender, RoutedEventArgs e)
        {
            Height = 768;
            Width = 1024;
            WindowState = WindowState.Normal;
        }

        private async void OnDataButtonClick(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Paths.settingsPath));

            Settings settings = new()
            {
                AutoUpdateChecker = true
            };
            await settings.Save();

            Storage storage = new();
            await storage.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ = ErrorList.Load();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new()
            {
                Source = Thread.CurrentThread.CurrentCulture.ToString() switch
                {
                    "de-DE" => new Uri("..\\Languages\\de-DE.xaml", UriKind.Relative),
                    //"en-US" => new Uri("..\\Languages\\en-US.xaml", UriKind.Relative),
                    _ => new Uri("..\\Languages\\de-DE.xaml", UriKind.Relative),
                }
            };
            Resources.MergedDictionaries.Add(dict);
        }
    }
}
