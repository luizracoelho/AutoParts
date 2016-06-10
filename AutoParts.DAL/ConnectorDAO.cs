using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AutoParts.DAL
{
    public class ConnectorDAO
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
        }
    }
}
