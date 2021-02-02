using System;
using System.Collections.Generic;
using System.Linq;
using ModernWpf.Controls;

namespace SellerScreen_2022.Pages.Settings
{
    public partial class SettingsPage : System.Windows.Controls.Page
    {
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("404", typeof(NotFoundPage)),
            ("general", typeof(NotFoundPage)),
            ("about", typeof(AboutPage)),
        };

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
