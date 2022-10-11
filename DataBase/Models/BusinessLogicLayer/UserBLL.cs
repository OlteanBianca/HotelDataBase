using DataBase.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DataBase.Models.BusinessLogicLayer
{
    internal class UserBLL
    {
        private readonly UserDAL userDAL;
        private readonly List<User> UsersList;

        public UserBLL()
        {
            userDAL = new UserDAL();
            UsersList = userDAL.GetAllUsers();
        }

        public User CheckUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            foreach (User val in UsersList)
            {
                if (val.Password == user.Password && val.UserName == user.UserName)
                {
                    return val;
                }
            }
            return null;
        }

        public void AddUser(object value)
        {
            User user = (value as object[])[1] as User;
            string pass = ((value as object[])[0] as PasswordBox).Password;
            user.Password = pass;

            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                _ = MessageBox.Show("Username and password can't be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                user.IdFunction += 1;
                userDAL.AddUser(user);
            }
        }

        public List<string> GetFunctions()
        {
            return userDAL.GetAllFunctions();
        }

        public List<User> GetReservationUsers(ObservableCollection<Reservation> res)
        {
            List<User> users = new List<User>();

            foreach (Reservation r in res)
            {
                users.Add(UsersList.Find(x => x.UserID == r.UserID));
            }

            return users;
        }
    }
}
