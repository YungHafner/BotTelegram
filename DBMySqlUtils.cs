using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTelegram
{
    public class DBMySqlUtils
    {
        private DBMySqlUtils() { }
        static DBMySqlUtils db;
        public static DBMySqlUtils GetDB()
        {
            if (db == null)
                db = new DBMySqlUtils();
            return db;
        }


        public MySqlConnection
                GetDBConnection()
        {
            InitConnection();

            return conn;
        }
        public bool OpenConnection()
        {
            try
            {
                if (conn == null)
                    InitConnection();
                conn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public void CloseConnection()
        {
            try
            {
                conn.Close(); // закрытие соединения
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        internal MySqlConnection conn = null;
        internal void InitConnection()
        {
            InitConnection(Properties.Settings.Default.host, Properties.Settings.Default.username,
                Properties.Settings.Default.database);
        }
        public void InitConnection(string host, string username, string database)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.UserID = username;
            builder.Database = database;
            builder.Server = host;
            builder.CharacterSet = "utf8";
            builder.ConnectionTimeout = 5;

            conn = new MySqlConnection(builder.GetConnectionString(true));
        }


        private int GetTableInfo(string table, string column)
        {
            int result = 0;
            //SHOW TABLE STATUS WHERE `Name` = 'group'
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand($"SHOW TABLE STATUS WHERE `Name` = '{table}'", conn))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    dr.Read();
                    result = dr.GetInt32(column);
                }
                CloseConnection();
            }
            return result;
        }
        internal void ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        mc.Parameters.AddRange(parameters);
                    mc.ExecuteNonQuery();
                }
                CloseConnection();
            }
        }

        internal int GetNextID(string table)
        {
            string column = "Auto_increment";
            return GetTableInfo(table, column);
        }
        internal int GetRowsCount(string table)
        {
            string column = "Rows";
            return GetTableInfo(table, column);
        }
    }
}

