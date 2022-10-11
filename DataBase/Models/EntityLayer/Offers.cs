using System;

namespace DataBase.Models.EntityLayer
{
    public class Offers : Base
    {
        private int? offerID;
        public int? OfferID
        {
            get => offerID;
            set
            {
                offerID = value;
                OnPropertyChanged("OfferID");
            }
        }


        private int roomTypeID;
        public int RoomTypeID
        {
            get => roomTypeID;
            set
            {
                roomTypeID = value;
                OnPropertyChanged("RoomType");
            }
        }


        private string roomType;
        public string RoomType
        {
            get => roomType;
            set
            {
                roomType = value;
                OnPropertyChanged("RoomType");
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


        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
