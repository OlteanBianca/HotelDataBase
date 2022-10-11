using DataBase.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.Models.DataAccessLayer
{
    internal class OffersDAL
    {
        public ObservableCollection<Offers> GetAllOffers()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<Offers> result = new ObservableCollection<Offers>();

                SqlCommand cmd = new SqlCommand("GetOffers", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Offers u = new Offers
                    {
                        OfferID = (int)reader[0],
                        DateBeginning = reader.GetDateTime(1),
                        DateEnd = reader.GetDateTime(2),
                        RoomTypeID = (int)reader[3],
                        Price = (double)reader[4],
                        Name = reader.GetString(5),
                        RoomType = GetRoomType((int)reader[3])
                    };

                    if (u.DateBeginning > DateTime.Now && GetAllFreeRooms(u.DateBeginning, u.DateEnd, u.RoomTypeID) > 0)
                    {
                        result.Add(u);
                    }
                }

                reader.Close();
                return result;
            }
        }

        public int AddOffer(Offers newOffer)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddOffer", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdOffer = new SqlParameter("@offer_id", SqlDbType.Int);
                SqlParameter paramName = new SqlParameter("@name", newOffer.Name);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room", newOffer.RoomTypeID);
                SqlParameter paramDateB = new SqlParameter("@date_b", newOffer.DateBeginning);
                SqlParameter paramDateE = new SqlParameter("@date_e", newOffer.DateEnd);
                SqlParameter paramPrice = new SqlParameter("@price", newOffer.Price);

                paramIdOffer.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramIdRoomType);
                cmd.Parameters.Add(paramIdOffer);
                cmd.Parameters.Add(paramDateB);
                cmd.Parameters.Add(paramDateE);
                cmd.Parameters.Add(paramPrice);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdOffer.Value;
            }
        }

        public void DeleteOffer(int idOffer)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteOffer", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramIdOffer = new SqlParameter("@id_offer", idOffer);

                cmd.Parameters.Add(paramIdOffer);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditOffer(Offers editOffer)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("EditOffer", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdOffer = new SqlParameter("@id_offer", editOffer.OfferID);
                SqlParameter paramName = new SqlParameter("@name", editOffer.Name);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", editOffer.RoomTypeID);
                SqlParameter paramDateB = new SqlParameter("@date_b", editOffer.DateBeginning);
                SqlParameter paramDateE = new SqlParameter("@date_e", editOffer.DateEnd);
                SqlParameter paramPrice = new SqlParameter("@price", editOffer.Price);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramIdRoomType);
                cmd.Parameters.Add(paramIdOffer);
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

        public int GetAllFreeRooms(DateTime begin, DateTime end, int id)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetFreeRoomsById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramDateBegin = new SqlParameter("@date_b", begin);
                SqlParameter paramDateEnd = new SqlParameter("@date_e", end);
                SqlParameter paramId = new SqlParameter("@id", id);
                cmd.Parameters.Add(paramDateBegin);
                cmd.Parameters.Add(paramDateEnd);
                cmd.Parameters.Add(paramId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                return (int)reader[0];
            }
        }
    }
}
