using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using SellerScreen_2022.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SellerScreen_2022
{
    public partial class StorageEditItemWindow : Window
    {
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("0", typeof(StorageEditItemNamePage)),
            ("1", typeof(StorageEditItemPricePage)),
            ("2", typeof(StorageEditItemAvailiblePage)),
            ("3", typeof(StorageEditItemRemovePage)),
        };

        private NavigationTransitionInfo _transitionInfo = null;
        private short activeIndex = -1;
        public static Product productItem;
        public static Product productItemEdited;
        public Product productItemReturn;

        public StorageEditItemWindow(string pageTag, Product product)
        {
            InitializeComponent();
            productItem = product;
            productItemEdited = product;
            Frame_Navigate(pageTag);
            Title = "Produkt: " + product.Id;
        }

        private void Frame_Navigate(string navItemTag)
        {
            Type _page = null;
            if (!string.IsNullOrEmpty(navItemTag))
            {
                (string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag.ToLower()));
                _page = item.Page;
            }

            if (!(_page is null))
            {
                if (short.Parse(navItemTag) > activeIndex)
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

                MainNavView.SelectedItem = MainNavView.MenuItems[int.Parse(navItemTag.ToString())];
            }
        }

        private void MainNavView_SelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
        {
            if (e.SelectedItemContainer != null)
            {

                if (!(e.SelectedItemContainer.Tag is null))
                {
                    string navItemTag = e.SelectedItemContainer.Tag.ToString();
                    Frame_Navigate(navItemTag);
                }
                else
                {
                    Frame_Navigate("");
                }
                NavigationViewItem item = (NavigationViewItem)MainNavView.SelectedItem;
                activeIndex = short.Parse(item.Tag.ToString());
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            productItemReturn = productItemEdited;
            Close();
        }

        public static void SetName(string name)
        {
            productItemEdited.Name = name;
        }
    }
}
