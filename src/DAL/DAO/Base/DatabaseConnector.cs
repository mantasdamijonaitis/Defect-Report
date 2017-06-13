using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DAO.Base
{
    public class DatabaseConnector
    {
        private const string ConnectionString = "Data Source=localhost;port=3306;Initial Catalog=test;Pooling=false;SslMode=None;Convert Zero Datetime=True";
        private static MySqlConnection connection;

        public static MySqlDataReader ExecuteSql(string query)
        {
            connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            return command.ExecuteReader();
        }

    }
}
