using ModernWpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerScreen_2022
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
