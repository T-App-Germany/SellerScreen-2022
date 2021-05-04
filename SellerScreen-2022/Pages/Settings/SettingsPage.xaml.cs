using System;
using System.Collections.Generic;
using System.Linq;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;

namespace SellerScreen_2022.Pages.Settings
{
    public partial class SettingsPage : System.Windows.Controls.Page
    {
        private readonly List<(string Tag, Type Page)> _pages = new()
        {
            ("general", typeof(GeneralSettingsPage)),
            ("about", typeof(AboutPage)),
        };
        private NavigationTransitionInfo _transitionInfo = null;
        private short activeIndex = -1;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsNavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (!string.IsNullOrEmpty(navItemTag))
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }

            if (!(_page is null))
            {
                short index = (short)_pages.IndexOf(_pages.FirstOrDefault(p => p.Tag.Equals(navItemTag)));
                if (index > activeIndex)
                {
                    _transitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };
                }
                else
                {
                    _transitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft };
                }

                if (_transitionInfo == null)
                {
                    ContentFrame.Navigate(_page, null);
                }
                else
                {
                    ContentFrame.Navigate(_page, null, _transitionInfo);
                }

                NavigationViewItem item = (NavigationViewItem)SettingsNavView.SelectedItem;
                activeIndex = index;
                SettingsNavView.SelectedItem = SettingsNavView.MenuItems[activeIndex];
            }
        }

        private void SettingsNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                if (!(args.SelectedItemContainer.Tag is null))
                {
                    var navItemTag = args.SelectedItemContainer.Tag.ToString();
                    SettingsNavView_Navigate(navItemTag);
                }
                else
                {
                    SettingsNavView_Navigate("");
                }
            }
        }
    }
}
