using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom_2023
{
    internal class DataBase
    {
        // Подключение к БД
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)    // Проверяем статус подключения (если подключения нет, то подключаемся)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)    // Проверяем статус подключения (если подключен, то отключаемся)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
