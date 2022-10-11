using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class ReservationVM : Base
    {
        private ReservationBLL reservationBLL;
        private readonly Offers offer;


        private DateTime dateBeginning;
        public DateTime DateBeginning
        {
            get => dateBeginning == null ? DateTime.Now : dateBeginning;
            set
            {
                dateBeginning = value;
                OnPropertyChanged("DateBeginning");
            }
        }

        private DateTime dateEnd;
        public DateTime DateEnd
        {
            get => dateEnd == null ? DateTime.Now : dateEnd;
            set
            {
                dateEnd = value;
                OnPropertyChanged("DateEnd");
            }
        }

        private string visibility;
        public string Visibility
        {
            get => visibility ?? "Hidden";
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private ObservableCollection<RoomsReserved> allRoomsReserved;
        public ObservableCollection<RoomsReserved> AllRoomsReserved
        {
            get => allRoomsReserved;
            set
            {
                allRoomsReserved = value;
                OnPropertyChanged("AllRoomsReserved");
            }
        }

        private List<AdditionalService> additionalServices;
        public List<AdditionalService> AdditionalServices
        {
            get => additionalServices;
            set
            {
                additionalServices = value;
                OnPropertyChanged("AdditionalServices");
            }
        }


        private ICommand searchFreeRoomsCommand;
        public ICommand SearchFreeRoomsCommand
        {
            get
            {
                if (searchFreeRoomsCommand == null)
                {
                    searchFreeRoomsCommand = new RelayCommand<Reservation>(reservationBLL.SearchFreeRooms);
                }
                return searchFreeRoomsCommand;
            }
        }


        public ReservationVM(Offers offer)
        {
            reservationBLL = new ReservationBLL(this);
            this.offer = offer;

            if (offer == null)
            {
                DateBeginning = DateTime.Now;
                DateEnd = DateTime.Now.AddDays(1);
            }
            else
            {
                Visibility = "Visible";
                DateBeginning = offer.DateBeginning;
                DateEnd = offer.DateEnd;
                AllRoomsReserved = new ObservableCollection<RoomsReserved>
                {
                    new RoomsReserved
                    {
                        RoomType = offer.RoomType,
                        IdRoomType = offer.RoomTypeID,
                        NumberReserved = 1
                    }
                };
            }
            additionalServices = reservationBLL.GetAdditionalServices();
        }

        public Reservation GetReservation(User user)
        {
            Reservation newReservation = new Reservation
            {
                DateBeginning = DateBeginning,
                DateEnd = DateEnd,
                State = "Active",
                IdOffer = null,
                AdditionalFeatures = reservationBLL.FilterAdditionalServices(),
                UserID = (int)user.UserID,
                RoomsReserved = reservationBLL.FilterRoomsReserved()
            };

            if (offer != null)
            {
                newReservation.Price = reservationBLL.GetPriceWithOffer(newReservation, offer);
                newReservation.IdOffer = offer.OfferID;
            }
            else
            {
                newReservation.Price = reservationBLL.GetPrice(newReservation);
            }
            return newReservation;
        }
    }
}
