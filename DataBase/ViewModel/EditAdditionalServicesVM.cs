using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class EditAdditionalServicesVM : Base
    {
        private readonly AdditionalServiceBLL additionalServiceBLL;


        private ObservableCollection<AdditionalService> services;
        public ObservableCollection<AdditionalService> Services
        {
            get => services;
            set
            {
                services = value;
                OnPropertyChanged("Services");
            }
        }

        private ICommand deleteServiceCommand;
        public ICommand DeleteServiceCommand
        {
            get
            {
                if (deleteServiceCommand == null)
                {
                    deleteServiceCommand = new RelayCommand<AdditionalService>(additionalServiceBLL.DeleteAdditionalService);
                }
                return deleteServiceCommand;
            }
        }

        private ICommand addServiceCommand;
        public ICommand AddServiceCommand
        {
            get
            {
                if (addServiceCommand == null)
                {
                    addServiceCommand = new RelayCommand<AdditionalService>(additionalServiceBLL.AddAdditionalService);
                }
                return addServiceCommand;
            }
        }

        private ICommand editServiceCommand;
        public ICommand EditServiceCommand
        {
            get
            {
                if (editServiceCommand == null)
                {
                    editServiceCommand = new RelayCommand<AdditionalService>(additionalServiceBLL.EditAdditionalService);
                }
                return editServiceCommand;
            }
        }


        public EditAdditionalServicesVM()
        {
            additionalServiceBLL = new AdditionalServiceBLL(this);

            services = additionalServiceBLL.GetServices();
        }
    }
}
