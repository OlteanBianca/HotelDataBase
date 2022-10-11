using DataBase.Models.DataAccessLayer;
using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class OffersBLL
    {
        private readonly EditOffersVM VM;
        private readonly OffersDAL offersDAL;

        public OffersBLL()
        {
            offersDAL = new OffersDAL();
        }

        public OffersBLL(EditOffersVM edit)
        {
            offersDAL = new OffersDAL();
            VM = edit;
        }

        public ObservableCollection<Offers> GetOffers()
        {
            return offersDAL.GetAllOffers();
        }

        public void AddOffer()
        {
            Offers newOffer = new Offers
            {
                DateBeginning = DateTime.Now,
                DateEnd = DateTime.Now.AddDays(1),
                Price = 0
            };

            VM.Offers.Add(newOffer);
        }

        public void DeleteOffer(object value)
        {
            if (value is Offers offer)
            {
                offersDAL.DeleteOffer((int)offer.OfferID);
                _ = VM.Offers.Remove(offer);

                _ = MessageBox.Show("Offer deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _ = MessageBox.Show("Please select an offer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditOffer(object value)
        {
            if ((value as object[])[0] is Tuple<string, int> type && (value as object[])[1] is Offers offer)
            {
                offer.RoomType = type.Item1;
                offer.RoomTypeID = type.Item2;
                if (offer.DateBeginning < offer.DateEnd && offer.Price > 0 && offer.Name != "")
                {
                    if (offer.OfferID != null)
                    {
                        offersDAL.EditOffer(offer);
                    }
                    else
                    {
                        offer.OfferID = offersDAL.AddOffer(offer);
                    }
                    _ = MessageBox.Show("Offer edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _ = MessageBox.Show("Offer is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                _ = MessageBox.Show("Please select an offer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Tuple<string, int>> GetRoomTypes()
        {
            return offersDAL.GetRoomTypes();
        }

    }
}
