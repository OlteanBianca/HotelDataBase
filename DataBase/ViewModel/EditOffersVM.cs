using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class EditOffersVM : Base
    {
        private readonly OffersBLL offersBLL;

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

        private List<Tuple<string, int>> allRooms;
        public List<Tuple<string, int>> AllRooms
        {
            get => allRooms;
            set
            {
                allRooms = value;
                OnPropertyChanged("AllRooms");
            }
        }

        private ICommand deleteOfferCommand;
        public ICommand DeleteOfferCommand
        {
            get
            {
                if (deleteOfferCommand == null)
                {
                    deleteOfferCommand = new RelayCommand<Offers>(offersBLL.DeleteOffer);
                }
                return deleteOfferCommand;
            }
        }

        private ICommand addOfferCommand;
        public ICommand AddOfferCommand
        {
            get
            {
                if (addOfferCommand == null)
                {
                    addOfferCommand = new RelayCommand<Offers>(offersBLL.AddOffer);
                }
                return addOfferCommand;
            }
        }

        private ICommand editOfferCommand;
        public ICommand EditOfferCommand
        {
            get
            {
                if (editOfferCommand == null)
                {
                    editOfferCommand = new RelayCommand<Offers>(offersBLL.EditOffer);
                }
                return editOfferCommand;
            }
        }



        public EditOffersVM()
        {
            offersBLL = new OffersBLL(this);

            offers = offersBLL.GetOffers();
            allRooms = offersBLL.GetRoomTypes();
        }
    }
}
