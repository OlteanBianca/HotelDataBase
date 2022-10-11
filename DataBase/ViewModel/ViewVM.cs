using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class ViewVM : Base
    {
        private readonly OffersBLL offersBLL;
        private readonly ReservationBLL reservationBLL;

        private string visibilityR;
        public string VisibilityR
        {
            get => visibilityR ?? "Hidden";
            set
            {
                visibilityR = value;
                OnPropertyChanged("VisibilityR");
            }
        }

        private string visibilityO;
        public string VisibilityO
        {
            get => visibilityO ?? "Hidden";
            set
            {
                visibilityO = value;
                OnPropertyChanged("VisibilityO");
            }
        }

        private ObservableCollection<Offers> offers;
        public ObservableCollection<Offers> Offers
        {
            get => offers;
            set
            {
                offers = value;
                OnPropertyChanged("Offers");
            }
        }

        private ObservableCollection<Reservation> reservations;
        public ObservableCollection<Reservation> Reservations
        {
            get => reservations;
            set
            {
                reservations = value;
                OnPropertyChanged("Reservations");
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand<Reservation>(reservationBLL.EditReservation);
                }
                return saveCommand;
            }
        }


        public ViewVM(User user, string page)
        {
            reservationBLL = new ReservationBLL();
            if (page == "reservations")
            {
                reservations = user.IdFunction == 3 ? reservationBLL.GetUserReservations((int)user.UserID)
                    : reservationBLL.GetReservations();
                VisibilityR = "Visible";
            }
            else
            {
                offersBLL = new OffersBLL();
                VisibilityO = "Visible";

                offers = offersBLL.GetOffers();
            }   
        }
    }
}
