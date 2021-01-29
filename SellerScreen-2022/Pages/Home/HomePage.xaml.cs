using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerScreen_2022.Pages.Home
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ChangeDistancePanels(object sender, SizeChangedEventArgs e)
        {
            if (sender is Frame frm)
            {
                double frame_heigth = frm.Height;
                double title_heigth = TitleTxt.ActualHeight + TitleTxt.Margin.Top + TitleTxt.Margin.Bottom;
                double logo_heigth = LogoPanel.ActualHeight;
                double space = frame_heigth - title_heigth - logo_heigth;
                DistancePanel1.Height = space / 2;
                DistancePanel2.Height = space / 2;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent != null)
            {
                Frame frm = (Frame)Parent;
                frm.SizeChanged += new SizeChangedEventHandler(ChangeDistancePanels);
            }
        }
    }
}
