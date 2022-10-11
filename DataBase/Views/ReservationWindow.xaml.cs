using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System.Windows;

namespace DataBase.Views
{
    public partial class ReservationWindow : Window
    {
        private ReservationSummaryWindow summaryWindow;
        private readonly User user;
        private ViewWindow viewWindow;

        public ReservationWindow(User user, Offers offer)
        {
            InitializeComponent();
            this.user = user;
            DataContext = new ReservationVM(offer);

            if (offer != null)
            {
                label.Visibility = Visibility.Collapsed;
                item.Visibility = Visibility.Collapsed;
                search.Visibility = Visibility.Collapsed;
                begin.IsEnabled = false;
                end.IsEnabled = false;
            }
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            Reservation res = (DataContext as ReservationVM).GetReservation(user);

            if (res.RoomsReserved.Count != 0)
            {
                summaryWindow = new ReservationSummaryWindow(res);
                summaryWindow.Show();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Please select a room!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectOfferClick(object sender, RoutedEventArgs e)
        {
            viewWindow = new ViewWindow(user, "offers");
            viewWindow.Show();
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
