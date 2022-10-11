using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models.EntityLayer
{
    public class RoomsReserved : Base
    {
        private int idRoomType;
        public int IdRoomType
        {
            get => idRoomType;
            set
            {
                idRoomType = value;
                OnPropertyChanged("IdRoomType");
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


        private int numberReserved;
        public int NumberReserved
        {
            get => numberReserved;
            set
            {
                numberReserved = value;
                OnPropertyChanged("NumberReserved");
            }
        }


        private List<int> numberOfFreeRooms;
        public List<int> NumberOfFreeRooms
        {
            get => numberOfFreeRooms;
            set
            {
                numberOfFreeRooms = value;
                OnPropertyChanged("NumberOfFreeNumber");
            }
        }
    }
}
