using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class EditRoomsVM : Base
    {
        private readonly RoomBLL roomBLL;

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
                Visibility = "Visible";
                FeaturesIncluded = roomBLL.GetAllFeaturesIncluded();
                FeaturesNotIncluded = roomBLL.GetAllFeaturesNotIncluded();
                OnPropertyChanged("SelectedRoom");
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



        private ObservableCollection<Room> allRooms;
        public ObservableCollection<Room> AllRooms
        {
            get => allRooms;
            set
            {
                allRooms = value;
                OnPropertyChanged("AllRooms");
            }
        }

        private List<Tuple<string, int>> featuresIncluded;
        public List<Tuple<string, int>> FeaturesIncluded
        {
            get => featuresIncluded;
            set
            {
                featuresIncluded = value;
                OnPropertyChanged("FeaturesIncluded");
            }
        }

        private List<Tuple<string, int>> featuresNotIncluded;
        public List<Tuple<string, int>> FeaturesNotIncluded
        {
            get => featuresNotIncluded;
            set
            {
                featuresNotIncluded = value;
                OnPropertyChanged("FeaturesNotIncluded");
            }
        }



        private ICommand addRoomCommand;
        public ICommand AddRoomCommand
        {
            get
            {
                if (addRoomCommand == null)
                {
                    addRoomCommand = new RelayCommand<Room>(roomBLL.AddRoomType);
                }
                return addRoomCommand;
            }
        }

        private ICommand deleteFeatureCommand;
        public ICommand DeleteFeatureCommand
        {
            get
            {
                if (deleteFeatureCommand == null)
                {
                    deleteFeatureCommand = new RelayCommand<Room>(roomBLL.DeleteFeature);
                }
                return deleteFeatureCommand;
            }
        }

        private ICommand addFeatureCommand;
        public ICommand AddFeatureCommand
        {
            get
            {
                if (addFeatureCommand == null)
                {
                    addFeatureCommand = new RelayCommand<Room>(roomBLL.AddFeature);
                }
                return addFeatureCommand;
            }
        }

        private ICommand deleteImageCommand;
        public ICommand DeleteImageCommand
        {
            get
            {
                if (deleteImageCommand == null)
                {
                    deleteImageCommand = new RelayCommand<Room>(roomBLL.DeleteImage);
                }
                return deleteImageCommand;
            }
        }

        private ICommand addImageCommand;
        public ICommand AddImageCommand
        {
            get
            {
                if (addImageCommand == null)
                {
                    addImageCommand = new RelayCommand<Room>(roomBLL.AddImage);
                }
                return addImageCommand;
            }
        }

        private ICommand editRoomsNumberCommand;
        public ICommand EditRoomsNumberCommand
        {
            get
            {
                if (editRoomsNumberCommand == null)
                {
                    editRoomsNumberCommand = new RelayCommand<Room>(roomBLL.EditRoom);
                }
                return editRoomsNumberCommand;
            }
        }


        public EditRoomsVM()
        {
            roomBLL = new RoomBLL(this);
            AllRooms = roomBLL.GetRooms();
        }
    }
}
