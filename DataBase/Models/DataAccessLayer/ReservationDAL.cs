using DataBase.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataBase.Models.DataAccessLayer
{
    internal class ReservationDAL
    {
        public ObservableCollection<Reservation> GetAllReservation()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<Reservation> result = new ObservableCollection<Reservation>();

                SqlCommand cmd = new SqlCommand("GetReservation", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bool canEdit = false;
                    if (reader.GetDateTime(3) > DateTime.Now && reader.GetString(5) != "Paid" && reader.GetString(5) != "Canceled")
                    {
                        canEdit = true;
                    }

                    Reservation u = new Reservation
                    {
                        ReservationID = (int)reader[0],
                        UserID = (int)reader[1],
                        Price = (double)reader[2],
                        DateBeginning = reader.GetDateTime(3),
                        DateEnd = reader.GetDateTime(4),
                        State = reader.GetString(5),
                        IdOffer = reader[6] == DBNull.Value ? null : (int?)reader[6],
                        RoomsReserved = GetAllRoomsReserved((int)reader[0]),
                        AdditionalFeatures = GetAllAdditionalFeaturesReserved((int)reader[0]),
                        CanEdit = canEdit
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public ObservableCollection<RoomsReserved> GetAllRoomsReserved(int id_reservation)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<RoomsReserved> result = new ObservableCollection<RoomsReserved>();

                SqlCommand cmd = new SqlCommand("GetRoomsReserved", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramReservationID = new SqlParameter("@idReservation", id_reservation);
                _ = cmd.Parameters.Add(paramReservationID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RoomsReserved value = new RoomsReserved
                    {
                        IdRoomType = (int)reader[0],
                        NumberReserved = (int)reader[1]
                    };
                    result.Add(value);
                }
                reader.Close();
                return result;
            }
        }

        public List<AdditionalService> GetAllAdditionalFeaturesReserved(int id_reservation)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<AdditionalService> result = new List<AdditionalService>();

                SqlCommand cmd = new SqlCommand("GetAdditionalServicesReserved", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramReservationID = new SqlParameter("@idReservation", id_reservation);
                _ = cmd.Parameters.Add(paramReservationID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AdditionalService value = new AdditionalService
                    {
                        AdditionalServiceID = (int)reader[0],
                        AdditionalServiceName = reader.GetString(1),
                        Price = (double)reader[2]
                    };
                    result.Add(value);
                }
                reader.Close();
                return result;
            }
        }

        public ObservableCollection<RoomsReserved> GetAllFreeRooms(DateTime begin, DateTime end)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<RoomsReserved> result = new ObservableCollection<RoomsReserved>();

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
                    List<int> list = new List<int>();
                    list.AddRange(Enumerable.Range(0, (int)reader[2] + 1));

                    RoomsReserved value = new RoomsReserved
                    {
                        NumberOfFreeRooms = list,
                        RoomType = reader.GetString(1),
                        NumberReserved = 0,
                        Price = GetPrice(begin, end, (int)reader[0]),
                        IdRoomType = (int)reader[0]
                    };
                    result.Add(value);
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
                        (double)reader[0]
                    );
                    result.Add(u);
                }

                double price = 0;
                foreach (Tuple<DateTime, DateTime, double> tuple in result)
                {
                    if (date_b >= tuple.Item1 && date_e <= tuple.Item2)
                    {
                        price += tuple.Item3 * (date_e - date_b).Days;
                    }
                    else if (date_b < tuple.Item1 && date_e > tuple.Item2)
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

        public List<AdditionalService> GetAdditionalServices()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                List<AdditionalService> result = new List<AdditionalService>();

                SqlCommand cmd = new SqlCommand("GetAdditionalServices", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AdditionalService value = new AdditionalService
                    {
                        AdditionalServiceID = (int)reader[0],
                        AdditionalServiceName = reader.GetString(1),
                        Price = (double)reader[2],
                        Reserved = false
                    };
                    result.Add(value);
                }
                reader.Close();
                return result;
            }
        }

        public void AddReservation(Reservation newRes)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddReservation", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdReservation = new SqlParameter("@reservationID", SqlDbType.Int);
                SqlParameter paramIdUser = new SqlParameter("@id_user", newRes.UserID);
                SqlParameter paramPrice = new SqlParameter("@price", newRes.Price);
                SqlParameter paramDateB = new SqlParameter("@date_beginning", newRes.DateBeginning);
                SqlParameter paramDateE = new SqlParameter("@date_end", newRes.DateEnd);
                SqlParameter paramState = new SqlParameter("@state", newRes.State);
                SqlParameter paramIdOffer = new SqlParameter("@id_offer", DBNull.Value);

                if (newRes.IdOffer != null)
                {
                    paramIdOffer = new SqlParameter("@id_offer", newRes.IdOffer);
                }

                paramIdReservation.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramIdReservation);
                cmd.Parameters.Add(paramIdUser);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramDateB);
                cmd.Parameters.Add(paramDateE);
                cmd.Parameters.Add(paramState);
                cmd.Parameters.Add(paramIdOffer);
                con.Open();
                cmd.ExecuteNonQuery();
                newRes.ReservationID = paramIdReservation.Value as int?;
            }
        }

        public void AddRoomReserved(int id_res, int room_id, int amount)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddRoomReserved", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdRes = new SqlParameter("@id_reservation", id_res);
                SqlParameter paramIdRoom = new SqlParameter("@id_room_type", room_id);
                SqlParameter paramAmount = new SqlParameter("@nr_rooms", amount);

                cmd.Parameters.Add(paramIdRes);
                cmd.Parameters.Add(paramIdRoom);
                cmd.Parameters.Add(paramAmount);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddAdditionalServicesReserved(int id_res, int additionalServiceID)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddAdditionalServicesReserved", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdRes = new SqlParameter("@id_reservation", id_res);
                SqlParameter paramIdService = new SqlParameter("@id_additional_service", additionalServiceID);

                cmd.Parameters.Add(paramIdRes);
                cmd.Parameters.Add(paramIdService);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddAllFeaturesReserved(Reservation newRes)
        {
            foreach (RoomsReserved room in newRes.RoomsReserved)
            {
                AddRoomReserved((int)newRes.ReservationID, room.IdRoomType, room.NumberReserved);
            }

            foreach (AdditionalService service in newRes.AdditionalFeatures)
            {
                AddAdditionalServicesReserved((int)newRes.ReservationID, (int)service.AdditionalServiceID);
            }
        }

        public void EditReservation(Reservation res)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("EditReservation", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdRes = new SqlParameter("@id_reservation", res.ReservationID);
                SqlParameter paramState = new SqlParameter("@state", res.State);

                cmd.Parameters.Add(paramIdRes);
                cmd.Parameters.Add(paramState);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
