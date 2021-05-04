using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Page = System.Windows.Controls.Page;

namespace SellerScreen_2022.Pages.Statics
{
    public partial class StaticsPage : Page
    {
        private readonly List<(string Tag, Type Page)> _pages = new()
        {
            ("total", typeof(NotFoundPage)),
            ("year", typeof(NotFoundPage)),
            ("month", typeof(NotFoundPage)),
            ("day", typeof(NotFoundPage)),
        };
        private NavigationTransitionInfo _transitionInfo = null;
        private short activeIndex = -1;

        public StaticsPage()
        {
            InitializeComponent();
        }

        private void StaticsNavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (!string.IsNullOrEmpty(navItemTag))
            {
                (string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
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

                NavigationViewItem item = (NavigationViewItem)StaticsNavView.SelectedItem;
                activeIndex = index;
                StaticsNavView.SelectedItem = StaticsNavView.MenuItems[activeIndex];
            }
        }

        private void StaticsNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                if (!(args.SelectedItemContainer.Tag is null))
                {
                    string navItemTag = args.SelectedItemContainer.Tag.ToString();
                    StaticsNavView_Navigate(navItemTag);
                }
                else
                {
                    StaticsNavView_Navigate("");
                }
            }
        }
    }
}
