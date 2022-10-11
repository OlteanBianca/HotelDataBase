using DataBase.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.Models.DataAccessLayer
{
    public class RoomDAL
    {
        public ObservableCollection<Room> GetRooms()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<Room> result = new ObservableCollection<Room>();

                SqlCommand cmd = new SqlCommand("GetRoomTypes", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Room u = new Room
                    {
                        RoomTypeID = (int)reader[0],
                        RoomType = reader.GetString(1),
                        Price = GetPrice(DateTime.Now, DateTime.Now.AddDays(1), (int)reader[0]),
                        NumberOfRooms = (int)reader[2],
                        Features = GetAllFeatures((int)reader[0]),
                        Images = GetAllImages((int)reader[0])
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public double GetPrice(DateTime date_b, DateTime date_e, int id)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<Tuple<DateTime, DateTime, double>> result = new List<Tuple<DateTime, DateTime, double>>();

                SqlCommand cmd = new SqlCommand("GetPrice", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramRoomType = new SqlParameter("@room_type_id", id);
                SqlParameter paramDateB = new SqlParameter("@date_b", date_b);
                SqlParameter paramDateE = new SqlParameter("@date_e", date_e);
                cmd.Parameters.Add(paramRoomType);
                cmd.Parameters.Add(paramDateB);
                cmd.Parameters.Add(paramDateE);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tuple<DateTime, DateTime, double> u = new Tuple<DateTime, DateTime, double>
                    (
                        reader.GetDateTime(1),
                        reader.GetDateTime(2),
                        (double) reader[0]
                    );
                    result.Add(u);
                }

                double price = 0;
                foreach (Tuple<DateTime, DateTime, double> tuple in result)
                {
                    if (date_b > tuple.Item1 && date_e < tuple.Item2)
                    {
                        price += tuple.Item3 * (date_e - date_b).Days;
                    }
                    else if(date_b < tuple.Item1 && date_e > tuple.Item2)
                    {
                        price += tuple.Item3 * (tuple.Item2 - tuple.Item1).Days;
                    }
                    else
                    {
                        if (tuple.Item1 < date_b && tuple.Item2 > date_b)
                        {
                            price += tuple.Item3 * (tuple.Item2 - date_b).Days;
                        }

                        if (tuple.Item1 < date_e && tuple.Item2 > date_e)
                        {
                            price += tuple.Item3 * (date_e - tuple.Item1).Days;
                        }
                    }
                }

                return price;
            }
        }

        public List<Tuple<string, int>> GetAllFeatures()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<Tuple<string, int>> result = new List<Tuple<string, int>>();

                SqlCommand cmd = new SqlCommand("GetFeatures", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Tuple<string, int>(reader.GetString(1), (int)reader[0]));
                }
                reader.Close();
                return result;
            }
        }

        public List<Tuple<string, int>> GetAllFeatures(int roomTypeId)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<Tuple<string, int>> result = new List<Tuple<string, int>>();

                SqlCommand cmd = new SqlCommand("GetRoomsFeatures", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramRoomType = new SqlParameter("@IdType", roomTypeId);
                cmd.Parameters.Add(paramRoomType);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Tuple<string, int>(reader.GetString(1), (int)reader[0]));
                }
                reader.Close();
                return result;
            }
        }

        public List<Tuple<string, int>> GetAllImages(int roomId)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<Tuple<string, int>> result = new List<Tuple<string, int>>();

                SqlCommand cmd = new SqlCommand("GetRoomsImages", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramRoomType = new SqlParameter("@idRoomType", roomId);
                cmd.Parameters.Add(paramRoomType);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Tuple<string, int>(reader.GetString(1), (int)reader[0]));
                }
                reader.Close();
                return result;
            }
        }

        public ObservableCollection<Room> GetFilteredRooms(DateTime begin, DateTime end)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<Room> result = new ObservableCollection<Room>();

                SqlCommand cmd = new SqlCommand("GetFreeRooms", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramDateBegin = new SqlParameter("@date_b", begin);
                SqlParameter paramDateEnd = new SqlParameter("@date_e", end);
                cmd.Parameters.Add(paramDateBegin);
                cmd.Parameters.Add(paramDateEnd);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Room u = new Room
                    {
                        RoomTypeID = (int)reader[0],
                        RoomType = reader.GetString(1),
                        Price = GetPrice(begin, end, (int)reader[0]),
                        NumberOfRooms = (int)reader[2],
                        Features = GetAllFeatures((int)reader[0]),
                        Images = GetAllImages((int)reader[0])
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public void DeleteFeature(int featureId, int roomTypeId)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteFeatureFromRoom", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdFeature = new SqlParameter("@id_feature", featureId);
                SqlParameter paramIdRoom = new SqlParameter("@id_room_type", roomTypeId);

                cmd.Parameters.Add(paramIdFeature);
                cmd.Parameters.Add(paramIdRoom);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddFeatureToRoom(int idFeature, int idRoomType)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddFeatureToRoom", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdFeature = new SqlParameter("@id_feature", idFeature);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", idRoomType);

                cmd.Parameters.Add(paramIdFeature);
                cmd.Parameters.Add(paramIdRoomType);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int AddFeature(string feature)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddFeature", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdFeature = new SqlParameter("@id_feature", SqlDbType.Int);
                SqlParameter paramFeature = new SqlParameter("@feature", feature);

                paramIdFeature.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramFeature);
                cmd.Parameters.Add(paramIdFeature);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdFeature.Value;
            }
        }

        public int AddImageToRoom(string path, int idRoomType)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddImageToRoom", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdImage = new SqlParameter("@id_image", SqlDbType.Int);
                SqlParameter paramPath = new SqlParameter("@path", path);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", idRoomType);

                paramIdImage.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramPath);
                cmd.Parameters.Add(paramIdRoomType);
                cmd.Parameters.Add(paramIdImage);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdImage.Value;
            }
        }

        public void DeleteImage(string path, int idRoomType)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteImageFromRoom", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramPath = new SqlParameter("@path", path);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", idRoomType);

                cmd.Parameters.Add(paramPath);
                cmd.Parameters.Add(paramIdRoomType);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditRoom(int idRoomType, int number)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("EditRoom", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramNumber = new SqlParameter("@number", number);
                SqlParameter paramIdRoomType = new SqlParameter("@id_room_type", idRoomType);

                cmd.Parameters.Add(paramNumber);
                cmd.Parameters.Add(paramIdRoomType);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int AddRoom(Room room)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddRoomType", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdRoomType = new SqlParameter("@room_id", SqlDbType.Int);
                SqlParameter paramNrRooms = new SqlParameter("@nr_rooms", room.NumberOfRooms);
                SqlParameter paramRoomType = new SqlParameter("@room_type", room.RoomType);

                paramIdRoomType.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramRoomType);
                cmd.Parameters.Add(paramNrRooms);
                cmd.Parameters.Add(paramIdRoomType);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdRoomType.Value;
            }
        }
    }
}
