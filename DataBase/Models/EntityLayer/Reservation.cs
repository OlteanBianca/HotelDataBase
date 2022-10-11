using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataBase.Models.EntityLayer
{
    public class Reservation : Base
    {
        private int? reservationID;
        public int? ReservationID
        {
            get => reservationID;
            set
            {
                reservationID = value;
                OnPropertyChanged("ReservationID");
            }
        }


        private int userID;
        public int UserID
        {
            get => userID;
            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }


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


        private double price;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }


        private string state;
        public string State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }


        private int? idOffer;
        public int? IdOffer
        {
            get => idOffer;
            set
            {
                idOffer = value;
                OnPropertyChanged("IdOffer");
            }
        }


        private bool canEdit;
        public bool CanEdit
        {
            get => canEdit;
            set
            {
                canEdit = value;
                OnPropertyChanged("CanEdit");
            }
        }


        private ObservableCollection<RoomsReserved> roomsReserved;
        public ObservableCollection<RoomsReserved> RoomsReserved
        {
            get => roomsReserved;
            set
            {
                roomsReserved = value;
                OnPropertyChanged("RoomsReserved");
            }
        }


        private List<AdditionalService> additionalFeatures;
        public List<AdditionalService> AdditionalFeatures
        {
            get => additionalFeatures;
            set
            {
                additionalFeatures = value;
                OnPropertyChanged("AdditionalFeatures");
            }
        }
    }
}
