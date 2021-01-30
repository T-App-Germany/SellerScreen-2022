using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace SellerScreen_2022.Pages.Home
{
    public partial class HomePage : Page
    {
        public static double ParentHeight { get; set; }

        public HomePage()
        {
            InitializeComponent();
        }

        private async Task ChangeDistancePanels()
        {
            double before = 0;
            while (true)
            {
                if (ParentHeight != before)
                {
                    before = ParentHeight;
                    double frame_heigth = ParentHeight;
                    double title_heigth = TitleTxt.ActualHeight + TitleTxt.Margin.Top + TitleTxt.Margin.Bottom;
                    double logo_heigth = LogoPanel.ActualHeight;
                    double space = frame_heigth - logo_heigth;
                    DistancePanel1.Height = space / 2 - title_heigth;
                    DistancePanel2.Height = space / 2 - DownIconPanel.ActualHeight;
                }
                await Task.Delay(50);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeDistancePanels().ConfigureAwait(false);

            var sb = (Storyboard)FindResource("BottomArrowAni");
            BeginStoryboard(sb);
        }
    }
}
