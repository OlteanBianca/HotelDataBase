using DataBase.Models;
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

    class SignInVM : Base
    {
        private UserBLL userBLL;

        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }


        public SignInVM()
        {
            userBLL = new UserBLL();
            User = new User
            {
                UserName = "",
                Password = ""
            };
        }

        public User CheckUser()
        {
            return userBLL.CheckUser(user);
        }
    }
}
