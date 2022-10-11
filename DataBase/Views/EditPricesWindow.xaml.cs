using System.Windows;

namespace DataBase.Views
{
    public partial class EditPricesWindow : Window
    {
        public EditPricesWindow()
        {
            InitializeComponent();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
