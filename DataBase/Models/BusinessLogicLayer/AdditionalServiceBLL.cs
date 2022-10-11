using DataBase.Models.DataAccessLayer;
using DataBase.Models.EntityLayer;
using DataBase.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class AdditionalServiceBLL
    {
        private readonly AdditionalServiceDAL additionalServiceDAL;
        private readonly EditAdditionalServicesVM VM;

        public AdditionalServiceBLL(EditAdditionalServicesVM edit)
        {
            additionalServiceDAL = new AdditionalServiceDAL();
            VM = edit;
        }

        public ObservableCollection<AdditionalService> GetServices()
        {
            return additionalServiceDAL.GetAllServices();
        }

        public void AddAdditionalService()
        {
            AdditionalService newOffer = new AdditionalService
            {
                Price = 0
            };

            VM.Services.Add(newOffer);
        }

        public void DeleteAdditionalService(object value)
        {
            if (value is AdditionalService service)
            {
                additionalServiceDAL.DeleteService((int)service.AdditionalServiceID);
                _ = VM.Services.Remove(service);

                _ = MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _ = MessageBox.Show("Please select a service!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void EditAdditionalService(object value)
        {
            if (value is AdditionalService service)
            {
                if (service.Price > 0 && service.AdditionalServiceName != "")
                {
                    if (service.AdditionalServiceID != null)
                    {
                        additionalServiceDAL.EditService(service);
                    }
                    else
                    {
                        service.AdditionalServiceID = additionalServiceDAL.AddService(service);
                    }
                    _ = MessageBox.Show("Service edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                _ = MessageBox.Show("Service is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _ = MessageBox.Show("Please select a Service!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
    }
}
