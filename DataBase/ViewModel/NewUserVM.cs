using DataBase.Models.BusinessLogicLayer;
using DataBase.Models.EntityLayer;
using DataBase.View;
using System.Collections.Generic;
using System.Windows.Input;

namespace DataBase.ViewModel
{
    internal class NewUserVM : Base
    {
        public List<string> Functions { get; set; }

        private readonly UserBLL userBLL;

        private User newUser;
        public User NewUser
        {
            get => newUser;
            set
            {
                newUser = value;
                OnPropertyChanged("NewUser");
            }
        }


        private ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    addUserCommand = new RelayCommand<User>(userBLL.AddUser);
                }
                return addUserCommand;
            }
        }

        public NewUserVM()
        {
            userBLL = new UserBLL();
            Functions = userBLL.GetFunctions();
            newUser = new User();
        }
    }
}
