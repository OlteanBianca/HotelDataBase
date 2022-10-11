using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models.EntityLayer
{
    public class Room : Base
    {
        private int? roomTypeID;
        public int? RoomTypeID
        {
            get => roomTypeID;
            set
            {
                roomTypeID = value;
                OnPropertyChanged("RoomTypeID");
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


        private int numberOfRooms;
        public int NumberOfRooms
        {
            get => numberOfRooms;
            set
            {
                numberOfRooms = value;
                OnPropertyChanged("RoomsNumber");
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


        private List<Tuple<string, int>> features;
        public List<Tuple<string, int>> Features
        {
            get => features;
            set
            {
                features = value;
                OnPropertyChanged("Features");
            }
        }


        private List<Tuple<string, int>> images;
        public List<Tuple<string, int>> Images
        {
            get => images;
            set
            {
                images = value;
                OnPropertyChanged("Images");
            }
        }
    }
}
