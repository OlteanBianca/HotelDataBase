using DataBase.Models.EntityLayer;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.Models
{
    public class UserDAL
    {
        public List<string> GetAllFunctions()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetFunctions", con);
                List<string> functions = new List<string>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    functions.Add(reader.GetString(1));
                }
                reader.Close();
                return functions;
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection con = DALHelper.Connection)
            {

                List<User> result = new List<User>();

                SqlCommand cmd = new SqlCommand("GetUsers", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User
                    {
                        UserID = (int)reader[0],
                        IdFunction = (int)reader[1],
                        UserName = reader.GetString(2),
                        Password = reader.GetString(3)
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public void AddUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdUser = new SqlParameter("@userId", SqlDbType.Int);
                SqlParameter paramUsername = new SqlParameter("@username", user.UserName);
                SqlParameter paramPassword = new SqlParameter("@password", user.Password);
                SqlParameter paramIdFunction = new SqlParameter("@idFunction", user.IdFunction);

                paramIdUser.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramIdFunction);
                cmd.Parameters.Add(paramIdUser);
                con.Open();
                cmd.ExecuteNonQuery();
                user.UserID = paramIdUser.Value as int?;
            }
        }
    }
}
