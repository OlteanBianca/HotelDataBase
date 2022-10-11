using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class HotelVM : Base
    {
        private readonly RoomBLL roomBLL;

        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string visibilityAdministrator;
        public string VisibilityAdministrator
        {
            get => visibilityAdministrator ?? "Collapsed";
            set
            {
                visibilityAdministrator = value;
                OnPropertyChanged("VisibilityManager");
            }
        }

        private string visibilityClient;
        public string VisibilityClient
        {
            get => visibilityClient ?? "Collapsed";
            set
            {
                visibilityClient = value;
                OnPropertyChanged("VisibilityClient");
            }
        }


        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        private ICommand filterRoomsCommand;
        public ICommand FilterRoomsCommand
        {
            get
            {
                if (filterRoomsCommand == null)
                {
                    filterRoomsCommand = new RelayCommand<Room>(roomBLL.GetFilteredRooms);
                }
                return filterRoomsCommand;
            }
        }


        public HotelVM(User currentUser)
        {
            roomBLL = new RoomBLL(this);
            Rooms = roomBLL.GetRooms();
            user = currentUser;

            if (user != null && user.IdFunction == 1)
            {
                VisibilityAdministrator = "Visible";
                VisibilityClient = "Visible";
            }
            else if (user != null)
            {
                VisibilityClient = "Visible";
            }
        }
    }
}
