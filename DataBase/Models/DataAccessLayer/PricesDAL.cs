using DataBase.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models.DataAccessLayer
{
    class PricesDAL
    {
        public ObservableCollection<Prices> GetAllPrices()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<Prices> result = new ObservableCollection<Prices>();

                SqlCommand cmd = new SqlCommand("GetPrices", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Prices u = new Prices
                    {
                        Id = (int)reader[0],
                        DateBeginning = reader.GetDateTime(1),
                        DateEnd = reader.GetDateTime(2),
                        Price = (double)reader[3],
                        RoomTypeID = (int)reader[4],
                        RoomType = GetRoomType((int)reader[4])
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public int AddPrice(Prices newPrice)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddPrice", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdPrice = new SqlParameter("@id_price", SqlDbType.Int);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", newPrice.RoomTypeID);
                SqlParameter paramDateB = new SqlParameter("@date_b", newPrice.DateBeginning);
                SqlParameter paramDateE = new SqlParameter("@date_e", newPrice.DateEnd);
                SqlParameter paramPrice = new SqlParameter("@price", newPrice.Price);

                paramIdPrice.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramIdRoomType);
                cmd.Parameters.Add(paramIdPrice);
                cmd.Parameters.Add(paramDateB);
                cmd.Parameters.Add(paramDateE);
                cmd.Parameters.Add(paramPrice);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdPrice.Value;
            }
        }

        public void DeletePrice(int idPrice)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeletePrice", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramIdPrice = new SqlParameter("@id_price", idPrice);

                cmd.Parameters.Add(paramIdPrice);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditPrice(Prices editPrice)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("EditPrice", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdPrice = new SqlParameter("@id_price", editPrice.Id);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", editPrice.RoomTypeID);
                SqlParameter paramDateB = new SqlParameter("@date_b", editPrice.DateBeginning);
                SqlParameter paramDateE = new SqlParameter("@date_e", editPrice.DateEnd);
                SqlParameter paramPrice = new SqlParameter("@price", editPrice.Price);

                cmd.Parameters.Add(paramIdRoomType);
                cmd.Parameters.Add(paramIdPrice);
                cmd.Parameters.Add(paramDateB);
                cmd.Parameters.Add(paramDateE);
                cmd.Parameters.Add(paramPrice);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetRoomType(int id)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetRoomTypeById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdRoomType = new SqlParameter("@id_room", id);

                cmd.Parameters.Add(paramIdRoomType);

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                return reader.GetString(0);
            }
        }

        public List<Tuple<string, int>> GetRoomTypes()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<Tuple<string, int>> result = new List<Tuple<string, int>>();

                SqlCommand cmd = new SqlCommand("GetRoomTypes", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tuple<string, int> u = new Tuple<string, int>(reader.GetString(1), (int)reader[0]);

                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }
    }
}
