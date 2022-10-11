
namespace DataBase.Models.EntityLayer
{
    public class AdditionalService : Base
    {
        private int? additionalServiceID;
        public int? AdditionalServiceID
        {
            get => additionalServiceID;
            set
            {
                additionalServiceID = value;
                OnPropertyChanged("AdditionalServiceID");
            }
        }


        private string additionalService;
        public string AdditionalServiceName
        {
            get => additionalService;
            set
            {
                additionalService = value;
                OnPropertyChanged("AdditionalServiceName");
            }
        }


        private double price;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }


        private bool reserved;
        public bool Reserved
        {
            get => reserved;
            set
            {
                reserved = value;
                OnPropertyChanged("Reserved");
            }
        }
    }
}
