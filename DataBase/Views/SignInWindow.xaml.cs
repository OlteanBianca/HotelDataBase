using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System.Windows;

namespace DataBase.Views
{
    public partial class SignInWindow : Window
    {
        private NewUserWindow newUserWindow;
        private HotelWindow hotelWindow;

        public SignInWindow()
        {
            InitializeComponent();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            (DataContext as SignInVM).User.Password = password.Password;
            User user = (DataContext as SignInVM).CheckUser();

            if (user != null)
            {
                hotelWindow = new HotelWindow(user);
                hotelWindow.Show();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Username or password invalid!\n Please try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ContinueAsGuestClick(object sender, RoutedEventArgs e)
        {
            hotelWindow = new HotelWindow(null);
            hotelWindow.Show();
            Close();
        }

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            newUserWindow = new NewUserWindow();
            newUserWindow.Show();
            Close();
        }

    }
}
