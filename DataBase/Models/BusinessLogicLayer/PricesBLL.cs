using DataBase.Models.DataAccessLayer;
using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class PricesBLL
    {
        private readonly PricesDAL pricesDAL;
        private readonly EditPricesVM VM;

        public PricesBLL(EditPricesVM edit)
        {
            pricesDAL = new PricesDAL();
            VM = edit;
        }

        public ObservableCollection<Prices> GetPrices()
        {
            return pricesDAL.GetAllPrices();
        }

        public List<Tuple<string, int>> GetRoomTypes()
        {
            return pricesDAL.GetRoomTypes();
        }

        public void AddPrice()
        {
            Prices newPrice = new Prices
            {
                DateBeginning = DateTime.Now,
                DateEnd = DateTime.Now.AddDays(1),
                Price = 0
            };

            VM.Prices.Add(newPrice);
        }

        public void DeletePrice(object value)
        {
            if (value is Prices price)
            {
                pricesDAL.DeletePrice((int)price.Id);
                _ = VM.Prices.Remove(price);

                _ = MessageBox.Show("Price deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _ = MessageBox.Show("Please select a price!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void EditPrice(object value)
        {
            if ((value as object[])[0] is Tuple<string, int> type && (value as object[])[1] is Prices price)
            {
                if (price.DateEnd > DateTime.Now)
                {
                    price.RoomType = type.Item1;
                    price.RoomTypeID = type.Item2;
                    if (price.DateBeginning < price.DateEnd && price.Price > 0)
                    {
                        if (price.Id != null)
                        {
                            pricesDAL.EditPrice(price);
                        }
                        else
                        {
                            price.Id = pricesDAL.AddPrice(price);
                        }
                        _ = MessageBox.Show("Price edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    _ = MessageBox.Show("Price is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _ = MessageBox.Show("You can't edit a price for a period that passed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _ = MessageBox.Show("Please select a price!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
