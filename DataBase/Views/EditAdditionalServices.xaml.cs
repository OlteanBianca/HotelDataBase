using System.Windows;

namespace DataBase.Views
{
    public partial class EditAdditionalServices : Window
    {
        public EditAdditionalServices()
        {
            InitializeComponent();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
