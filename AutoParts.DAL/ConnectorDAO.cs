using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AutoParts.DAL
{
    public class ConnectorDAO
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
        }
    }
}
