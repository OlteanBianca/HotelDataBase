using DataBase.Models.EntityLayer;
using System.Windows;
using DataBase.ViewModel;

namespace DataBase.Views
{
    public partial class ViewWindow : Window
    {
        private ReservationWindow reservationWindow;
        private readonly User user;

        public ViewWindow(User user, string page)
        {
            InitializeComponent();
            DataContext = new ViewVM(user, page);
            this.user = user;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            if (offers.SelectedItem != null)
            {
                reservationWindow = new ReservationWindow(user, offers.SelectedItem as Offers);
                reservationWindow.Show();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Please select an offer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
