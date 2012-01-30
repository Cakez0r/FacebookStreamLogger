using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FacebookToDB
{
    public class MySQLStorage : IDBStorage
    {
        const int DUPLICATE_KEY_ERROR_CODE = 1062;
        const string SQL_WRITE_COMMAND = "INSERT INTO `Entries` (`PostID`, `ActorID`, `Message`, `CreatedTime`) VALUES ('{0}', {1}, '{2}', '{3}')";

        MySqlConnection m_connection;

        public MySQLStorage(string host, string username, string password, string databaseName, int port)
        {
            string connectionString = string.Format("server={0};user={1};database={2};port={3};password={4};", host, username, databaseName, port, password);
            m_connection = new MySqlConnection(connectionString);
            Logger.Info("Connection to MySQL database...");
            try
            {
                m_connection.Open();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to connect to MySQL database! Exception detail: " + ex.ToString());
                return;
            }

            Logger.Info("... Connected!");
        }

        public void CreateStreamEntry(FacebookStreamEntry entry)
        {
            try
            {
                string sqlCommand = string.Format(SQL_WRITE_COMMAND, entry.PostID, entry.ActorID, MySqlHelper.EscapeString(entry.Message), entry.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                MySqlCommand command = new MySqlCommand(sqlCommand, m_connection);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                //Ignore duplicate key errors
                if (ex.Number != DUPLICATE_KEY_ERROR_CODE)
                {
                    throw ex;
                }
            }
        }
    }
}
