using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace SellerScreen_2022.Pages.Home
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)FindResource("BottomArrowAni");
            BeginStoryboard(sb);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Grid grid)
            {
                double frame = grid.ActualHeight;
                double title = TitleTxt.ActualHeight + TitleTxt.Margin.Top + TitleTxt.Margin.Bottom;
                double logo = LogoPanel.ActualHeight;
                double space = (frame - logo) / 2;
                if (space < title)
                {
                    space = title;
                }

                DistancePanel1.Height = space - title;
                if (space < DownIconPanel.ActualHeight)
                {
                    space = DownIconPanel.ActualHeight;
                }

                DistancePanel2.Height = space - DownIconPanel.ActualHeight;
            }
        }
    }
}