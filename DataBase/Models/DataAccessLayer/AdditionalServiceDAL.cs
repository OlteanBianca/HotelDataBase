using DataBase.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.Models.DataAccessLayer
{
    internal class AdditionalServiceDAL
    {
        public ObservableCollection<AdditionalService> GetAllServices()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                ObservableCollection<AdditionalService> result = new ObservableCollection<AdditionalService>();

                SqlCommand cmd = new SqlCommand("GetAdditionalServices", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AdditionalService u = new AdditionalService
                    {
                        AdditionalServiceID = (int)reader[0],
                        AdditionalServiceName = reader.GetString(1),
                        Price = (double)reader[2],
                    };
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
        }

        public int AddService(AdditionalService newService)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddAdditionalService", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdService = new SqlParameter("@id_service", SqlDbType.Int);
                SqlParameter paramName = new SqlParameter("@name", newService.AdditionalServiceName);
                SqlParameter paramPrice = new SqlParameter("@price", newService.Price);

                paramIdService.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramIdService);
                cmd.Parameters.Add(paramPrice);
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)paramIdService.Value;
            }
        }

        public void DeleteService(int idService)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteAdditionalService", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramIdService = new SqlParameter("@id_service", idService);

                cmd.Parameters.Add(paramIdService);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditService(AdditionalService editService)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("EditAdditionalService", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramIdService = new SqlParameter("@id_service", editService.AdditionalServiceID);
                SqlParameter paramName = new SqlParameter("@name", editService.AdditionalServiceName);
                SqlParameter paramPrice = new SqlParameter("@price", editService.Price);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramIdService);
                cmd.Parameters.Add(paramPrice);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
