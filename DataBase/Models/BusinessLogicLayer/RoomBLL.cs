using DataBase.Models.DataAccessLayer;
using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class RoomBLL : Base
    {
        private readonly RoomDAL roomDAL;
        private readonly HotelVM VM;
        private readonly EditRoomsVM editVM;

        public RoomBLL(HotelVM hotelVM)
        {
            roomDAL = new RoomDAL();
            VM = hotelVM;
        }

        public RoomBLL(EditRoomsVM editRoomsVM)
        {
            roomDAL = new RoomDAL();
            editVM = editRoomsVM;
        }

        public ObservableCollection<Room> GetRooms()
        {
            return roomDAL.GetRooms();
        }

        public void GetFilteredRooms(object date)
        {
            if ((date as object[])[0] != null && (date as object[])[1] != null)
            {
                DateTime begin = (DateTime)(date as object[])[0];
                DateTime end = (DateTime)(date as object[])[1];

                if (begin < end)
                {
                    VM.Rooms = roomDAL.GetFilteredRooms(begin, end);
                }
            }
            else
            {
                VM.Rooms = GetRooms();
            }
        }

        public void AddRoomType(object value)
        {
            editVM.Visibility = "Visible";
            editVM.SelectedRoom = new Room
            {
                RoomType = value as string
            };
        }

        public void DeleteFeature(object value)
        {
            if (value is Tuple<string, int> delete)
            {
                roomDAL.DeleteFeature(delete.Item2, (int)editVM.SelectedRoom.RoomTypeID);
                editVM.SelectedRoom.Features = roomDAL.GetAllFeatures((int)editVM.SelectedRoom.RoomTypeID);
                _ = editVM.FeaturesIncluded.Remove(delete);
                editVM.FeaturesNotIncluded.Add(delete);

                _ = MessageBox.Show("Feature deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _ = MessageBox.Show("Please select a feature!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddFeature(object value)
        {
            if (editVM.SelectedRoom.NumberOfRooms > 0)
            {
                if ((value as object[])[1] is string newFeature && newFeature != "")
                {
                    if (editVM.FeaturesIncluded.Find(x => x.Item1 == newFeature) == null && editVM.FeaturesNotIncluded.Find(x => x.Item1 == newFeature) == null)
                    {
                        int id = roomDAL.AddFeature(newFeature);
                        roomDAL.AddFeatureToRoom(id, (int)editVM.SelectedRoom.RoomTypeID);
                        editVM.SelectedRoom.Features = roomDAL.GetAllFeatures((int)editVM.SelectedRoom.RoomTypeID);

                        Tuple<string, int> feature = new Tuple<string, int>(newFeature, id);
                        editVM.FeaturesIncluded.Add(feature);
                        _ = MessageBox.Show("Feature added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                if ((value as object[])[0] is Tuple<string, int> existingFeature)
                {
                    if (!editVM.FeaturesIncluded.Contains(existingFeature))
                    {
                        roomDAL.AddFeatureToRoom(existingFeature.Item2, (int)editVM.SelectedRoom.RoomTypeID);
                        editVM.SelectedRoom.Features = roomDAL.GetAllFeatures((int)editVM.SelectedRoom.RoomTypeID);
                        editVM.FeaturesIncluded.Add(existingFeature);
                        _ = editVM.FeaturesNotIncluded.Remove(existingFeature);
                        _ = MessageBox.Show("Feature added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                _ = MessageBox.Show("Please select number of rooms!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddImage(object value)
        {
            if (editVM.SelectedRoom.NumberOfRooms > 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"c:\users\bienc\desktop\facultate\anul 2-sem 2\mvp\tema 3\DataBase\DataBase\Resources\FreeImage\"
                };
                _ = openFileDialog.ShowDialog();

                if (openFileDialog.FileName != "")
                {
                    string path = "/DataBase;component/Resources/FreeImage/" + Path.GetFileName(openFileDialog.FileName);

                    if (editVM.SelectedRoom.Images == null)
                    {
                        editVM.SelectedRoom.Images = new List<Tuple<string, int>>();
                    }

                    if (editVM.SelectedRoom.Images.Find(x => x.Item1 == path) == null)
                    {
                        int id = roomDAL.AddImageToRoom(path, (int)editVM.SelectedRoom.RoomTypeID);

                        ObservableCollection<Room> list = roomDAL.GetRooms();
                        foreach (Room r in list)
                        {
                            if (r.RoomTypeID == editVM.SelectedRoom.RoomTypeID)
                            {
                                editVM.SelectedRoom = r;
                            }
                        }
                        _ = MessageBox.Show("Image added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _ = MessageBox.Show("Image already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                _ = MessageBox.Show("Please select number of rooms!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteImage(object value)
        {
            if (value is Tuple<string, int> delete)
            {
                roomDAL.DeleteImage(delete.Item1, (int)editVM.SelectedRoom.RoomTypeID);
                editVM.SelectedRoom.Images = roomDAL.GetAllImages((int)editVM.SelectedRoom.RoomTypeID);

                _ = MessageBox.Show("Image deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _ = MessageBox.Show("Please select an image!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditRoom(object value)
        {
            if (value is string word)
            {
                int number = Convert.ToInt32(word);

                if (editVM.SelectedRoom.RoomTypeID == null)
                {
                    AddRoom(number);
                    return;
                }
                roomDAL.EditRoom((int)editVM.SelectedRoom.RoomTypeID, number);
                _ = MessageBox.Show("Number changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void AddRoom(int nr)
        {
            editVM.SelectedRoom.NumberOfRooms = nr;
            editVM.SelectedRoom.RoomTypeID = roomDAL.AddRoom(editVM.SelectedRoom);
        }

        public List<Tuple<string, int>> GetAllFeaturesIncluded()
        {
            List<Tuple<string, int>> list = roomDAL.GetAllFeatures();

            if (editVM.SelectedRoom.Features == null)
            {
                editVM.SelectedRoom.Features = new List<Tuple<string, int>>();
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (!editVM.SelectedRoom.Features.Contains(list[i]))
                {
                    _ = list.Remove(list[i]);
                    i--;
                }
            }
            return list;
        }

        public List<Tuple<string, int>> GetAllFeaturesNotIncluded()
        {
            List<Tuple<string, int>> list = roomDAL.GetAllFeatures();

            for (int i = 0; i < list.Count; i++)
            {
                if (editVM.SelectedRoom.Features.Contains(list[i]))
                {
                    _ = list.Remove(list[i]);
                    i--;
                }
            }
            return list;
        }
    }
}
