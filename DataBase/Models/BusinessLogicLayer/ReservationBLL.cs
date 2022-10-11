using DataBase.Models.DataAccessLayer;
using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class ReservationBLL
    {
        private readonly ReservationDAL reservationDAL;
        private readonly ReservationVM VM;

        public ReservationBLL(ReservationVM reservationVM)
        {
            reservationDAL = new ReservationDAL();
            VM = reservationVM;
        }

        public ReservationBLL()
        {
            reservationDAL = new ReservationDAL();
        }

        public double GetPrice(Reservation newRes)
        {
            double price = 0;

            foreach (AdditionalService a in newRes.AdditionalFeatures)
            {
                price += a.Price;
            }
            foreach (RoomsReserved r in newRes.RoomsReserved)
            {
                price += r.NumberReserved * r.Price;
            }
            return price;
        }

        public double GetPriceWithOffer(Reservation newRes, Offers offer)
        {
            double price = offer.Price;

            foreach (AdditionalService a in newRes.AdditionalFeatures)
            {
                price += a.Price;
            }
            return price;
        }

        public void AddReservation(object value)
        {
            reservationDAL.AddReservation(value as Reservation);
            reservationDAL.AddAllFeaturesReserved(value as Reservation);

            _ = MessageBox.Show("Reservation added successfully.\n Thank you for trusting us!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void EditReservation(object value)
        {
            ObservableCollection<Reservation> updatedRes = value as ObservableCollection<Reservation>;

            ObservableCollection<Reservation> oldRes = reservationDAL.GetAllReservation();

            for (int i = 0; i < updatedRes.Count; i++)
            {
                if (updatedRes[i].State != oldRes[i].State)
                {
                    if (updatedRes[i].State == "Active" || updatedRes[i].State == "Canceled" || updatedRes[i].State == "Paid")
                    {
                        reservationDAL.EditReservation(updatedRes[i]);
                    }
                    else
                    {
                        _ = MessageBox.Show("Please insert a valid state!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }
        }

        public ObservableCollection<Reservation> GetReservations()
        {
            return reservationDAL.GetAllReservation();
        }

        public ObservableCollection<Reservation> GetUserReservations(int userId)
        {
            ObservableCollection<Reservation> res = reservationDAL.GetAllReservation();

            _ = res.Where(var => var.UserID != userId).ToList().All(i => res.Remove(i));

            return res;
        }

        public void SearchFreeRooms(object date)
        {
            if ((date as object[])[0] != null && (date as object[])[1] != null)
            {
                DateTime begin = (DateTime)(date as object[])[0];
                DateTime end = (DateTime)(date as object[])[1];

                if (begin < end)
                {
                    VM.AllRoomsReserved = reservationDAL.GetAllFreeRooms(begin, end);

                    if (VM.AllRoomsReserved.Count != 0)
                    {
                        VM.Visibility = "Visible";
                    }
                    else
                    {
                        _ = MessageBox.Show("There is no availability for those dates! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public List<AdditionalService> GetAdditionalServices()
        {
            return reservationDAL.GetAdditionalServices();
        }

        public List<AdditionalService> FilterAdditionalServices()
        {
            _ = VM.AdditionalServices.RemoveAll(var => var.Reserved == false);
            return VM.AdditionalServices;
        }

        public ObservableCollection<RoomsReserved> FilterRoomsReserved()
        {
            _ = VM.AllRoomsReserved.Where(var => var.NumberReserved == 0).ToList().All(i => VM.AllRoomsReserved.Remove(i));
            return VM.AllRoomsReserved;
        }

    }
}
