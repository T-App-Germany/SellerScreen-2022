using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerScreen_2022
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TabView.NewItemFactory = () =>
            {
                var newItem = new TabItem { Header = "New Document" };
                TabItemHelper.SetIcon(newItem, new SymbolIcon(Symbol.Document));
                return newItem;
            };
        }
    }
}
