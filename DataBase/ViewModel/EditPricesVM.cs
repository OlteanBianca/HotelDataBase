using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBase.ViewModel
{
   
    class EditPricesVM : Base
    {
        private PricesBLL pricesBLL;

        private ObservableCollection<Prices> prices;
        public ObservableCollection<Prices> Prices
        {
            get => prices;
            set
            {
                prices = value;
                OnPropertyChanged("Prices");
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

        private ICommand deletePriceCommand;
        public ICommand DeletePriceCommand
        {
            get
            {
                if (deletePriceCommand == null)
                {
                    deletePriceCommand = new RelayCommand<Prices>(pricesBLL.DeletePrice);
                }
                return deletePriceCommand;
            }
        }

        private ICommand addPriceCommand;
        public ICommand AddPriceCommand
        {
            get
            {
                if (addPriceCommand == null)
                {
                    addPriceCommand = new RelayCommand<Offers>(pricesBLL.AddPrice);
                }
                return addPriceCommand;
            }
        }

        private ICommand editPriceCommand;
        public ICommand EditPriceCommand
        {
            get
            {
                if (editPriceCommand == null)
                {
                    editPriceCommand = new RelayCommand<Prices>(pricesBLL.EditPrice);
                }
                return editPriceCommand;
            }
        }


        public EditPricesVM()
        {
            pricesBLL = new PricesBLL(this);

            prices = pricesBLL.GetPrices();
            allRooms = pricesBLL.GetRoomTypes();
        }

    }
}
