using System.Windows.Controls;

namespace SellerScreen_2022
{
    public partial class StorageEditItemNamePage : Page
    {
        public StorageEditItemNamePage()
        {
            InitializeComponent();
            NameTxt.Text = StorageEditItemWindow.productItem.Name;
            NewNameBox.Text = StorageEditItemWindow.productItemEdited.Name;
        }

        private void NewNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StorageEditItemWindow.SetName(NewNameBox.Text);
        }
    }
}
