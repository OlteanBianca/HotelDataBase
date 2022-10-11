using DataBase.Models.EntityLayer;
using System.Windows;
using System.Windows.Controls;

namespace DataBase.Views
{
    public partial class EditRooms : Window
    {
        private HotelWindow hotelWindow;
        private readonly User user;

        public EditRooms(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            hotelWindow = new HotelWindow(user);
            hotelWindow.Show();
            Close();
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "features")
            {
                editFeatures.Visibility = editFeatures.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            }
        }
    }
}
