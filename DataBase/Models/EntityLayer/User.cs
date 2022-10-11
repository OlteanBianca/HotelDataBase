
namespace DataBase.Models.EntityLayer
{
    public class User : Base
    {
        private int? userID;
        public int? UserID
        {
            get => userID;
            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }


        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }


        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }


        private int idFunction;
        public int IdFunction
        {
            get => idFunction;
            set
            {
                idFunction = value;
                OnPropertyChanged("IdFunction");
            }
        }
    }
}
