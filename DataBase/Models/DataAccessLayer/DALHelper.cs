using System.Configuration;
using System.Data.SqlClient;

namespace DataBase.Models
{
    public static class DALHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;

        public static SqlConnection Connection => new SqlConnection(connectionString);
    }
}
