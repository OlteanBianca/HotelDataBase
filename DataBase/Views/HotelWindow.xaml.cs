using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System.Windows;

namespace DataBase.Views
{
    public partial class HotelWindow : Window
    {
        private readonly User user;
        private EditRooms editRooms;
        private ViewWindow viewWindow;
        private SignInWindow signInWindow;
        private EditOffersWindow offersWindow;
        private EditPricesWindow pricesWindow;
        private ReservationWindow reservationWindow;
        private EditAdditionalServices additionalServices;

        public HotelWindow(User user)
        {
            DataContext = new HotelVM(user);
            InitializeComponent();
            this.user = user;
        }

        private void ReservationClick(object sender, RoutedEventArgs e)
        {
            if ((DataContext as HotelVM).User != null)
            {
                reservationWindow = new ReservationWindow((DataContext as HotelVM).User, null);
                reservationWindow.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please create an account to book a reservation.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    signInWindow = new SignInWindow();
                    signInWindow.Show();
                    Close();
                }
            }
        }

        private void ViewReservationsClick(object sender, RoutedEventArgs e)
        {
            viewWindow = new ViewWindow((DataContext as HotelVM).User, "reservations");
            viewWindow.Show();
        }

        private void OffersClick(object sender, RoutedEventArgs e)
        {
            viewWindow = new ViewWindow((DataContext as HotelVM).User, "offers");
            viewWindow.Show();
        }

        private void EditRoomsClick(object sender, RoutedEventArgs e)
        {
            editRooms = new EditRooms(user);
            editRooms.Show();
            Close();
        }

        private void EditOffersClick(object sender, RoutedEventArgs e)
        {
            offersWindow = new EditOffersWindow();
            offersWindow.Show();
        }

        private void EditPricesClick(object sender, RoutedEventArgs e)
        {
            pricesWindow = new EditPricesWindow();
            pricesWindow.Show();
        }

        private void EditServicesClick(object sender, RoutedEventArgs e)
        {
            additionalServices = new EditAdditionalServices();
            additionalServices.Show();
        }
    }
}
