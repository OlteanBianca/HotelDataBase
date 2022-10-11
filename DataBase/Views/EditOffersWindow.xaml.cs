using System.Windows;

namespace DataBase.Views
{
    public partial class EditOffersWindow : Window
    {

        public EditOffersWindow()
        {
            InitializeComponent();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
