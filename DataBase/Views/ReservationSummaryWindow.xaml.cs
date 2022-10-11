using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System.Windows;

namespace DataBase.Views
{
    public partial class ReservationSummaryWindow : Window
    {
        public ReservationSummaryWindow(Reservation newReservation)
        {
            InitializeComponent();
            DataContext = new ReservationSummaryVM(newReservation);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
