using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class ReservationSummaryVM : Base
    {
        private readonly ReservationBLL reservationBLL;

        private Reservation newReservation;
        public Reservation NewReservation
        {
            get => newReservation;
            set
            {
                newReservation = value;
                OnPropertyChanged("NewReservation");
            }
        }


        private ICommand addReservationCommand;
        public ICommand AddReservationCommand
        {
            get
            {
                if (addReservationCommand == null)
                {
                    addReservationCommand = new RelayCommand<Reservation>(reservationBLL.AddReservation);
                }
                return addReservationCommand;
            }
        }

        public ReservationSummaryVM(Reservation newReservation)
        {
            reservationBLL = new ReservationBLL();
            this.newReservation = newReservation;
        }
    }
}
