using System.Windows;

namespace SellerScreen_2022.Pages.Storage
{
    public partial class StorageDisposeItemWindow : Window
    {
        public bool DisposeAll
        {
            get;
            private set;
        }

        public uint Dispose
        {
            get;
            private set;
        }

        public StorageDisposeItemWindow(uint max, string key)
        {
            InitializeComponent();
            Title = "Produkt: " + key;
            RemoveBox.Maximum = max;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (DisposeMode.SelectedIndex == 0)
            {
                DisposeAll = true;
            }
            else
            {
                Dispose = (uint)RemoveBox.Value;
            }

            DialogResult = true;
            Close();
        }

        private void NumberBox_ValueChanged(ModernWpf.Controls.NumberBox sender, ModernWpf.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (sender.Value == sender.Maximum)
            {
                DisposeMode.SelectedIndex = 0;
            }
            else
            {
                DisposeMode.SelectedIndex = 1;
            }
        }

        private void Btn1_Checked(object sender, RoutedEventArgs e)
        {
            RemoveBox.Value = RemoveBox.Maximum;
        }
    }
}