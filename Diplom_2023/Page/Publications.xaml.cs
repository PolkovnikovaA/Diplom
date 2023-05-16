using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom_2023
{
    /// <summary>
    /// Логика взаимодействия для Publications.xaml
    /// </summary>
    public partial class Publications : Page
    {
        public Frame frame1;
        public Publications(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;

            List_type_1();
            List_type_2();
            List_type_3();
            List_type_4();
            List_type_5();

            List_type_7();
            List_type_8();
        }

        public int Count1()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_1";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count11()
        {
            string stmt = "SELECT COUNT(*) FROM type_1";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_1()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count1();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_1", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_1 FROM id_type_1 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_1"]);

                int count1 = Count11();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_1", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_1 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType2.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count2()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_2";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count22()
        {
            string stmt = "SELECT COUNT(*) FROM type_2";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_2()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open(); 
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count2();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_2", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if(user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }
            
            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_2 FROM id_type_2 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while(o>0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_2"]);

                int count1 = Count22();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_2", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_2 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType3.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count3()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_3";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count33()
        {
            string stmt = "SELECT COUNT(*) FROM type_3";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_3()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count3();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_3", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_3 FROM id_type_3 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_3"]);

                int count1 = Count33();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_3", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_3 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType4.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count4()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_4";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count44()
        {
            string stmt = "SELECT COUNT(*) FROM type_4";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_4()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count4();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_4", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_4 FROM id_type_4 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_4"]);

                int count1 = Count44();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_4", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_4 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType5.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count5()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_5";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count55()
        {
            string stmt = "SELECT COUNT(*) FROM type_5";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_5()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count5();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_5", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_5 FROM id_type_5 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_5"]);

                int count1 = Count55();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_5", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_5 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType6.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count7()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_7";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count77()
        {
            string stmt = "SELECT COUNT(*) FROM type_7";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_7()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count7();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_7", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_7 FROM id_type_7 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_7"]);

                int count1 = Count77();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_7", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_7 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType8.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        public int Count8()
        {
            string stmt = "SELECT COUNT(*) FROM id_type_8";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        public int Count88()
        {
            string stmt = "SELECT COUNT(*) FROM type_8";
            int count = 0;

            using (MySqlConnection thisConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                using (MySqlCommand cmd = new MySqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count;
        }

        // Вывод в лист
        public void List_type_8()
        {
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));
            string p = Convert.ToString(pp);

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = p;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);   // id пользователя (авторизации)
            reader3.Close();

            int count = Count8();
            DataBase dataBase37 = new DataBase();
            MySqlCommand command37 = new MySqlCommand("SELECT id_users FROM id_type_8", dataBase37.getConnection());
            MySqlDataReader reader37;
            command37.Connection.Open();
            reader37 = command37.ExecuteReader();
            int o = 0;
            while (count > 0)
            {
                reader37.Read();
                string user_id2 = Convert.ToString(reader37["id_users"]);

                if (user_id2 == user_id)
                {
                    o++;
                }

                count--;
            }

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_type_8 FROM id_type_8 WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();

            while (o > 0)
            {
                reader31.Read();
                string id = Convert.ToString(reader31["id_type_8"]);

                int count1 = Count88();
                DataBase dataBase310 = new DataBase();
                MySqlCommand command310 = new MySqlCommand("SELECT id FROM type_8", dataBase310.getConnection());
                MySqlDataReader reader310;
                command310.Connection.Open();
                reader310 = command310.ExecuteReader();
                while (count1 > 0)
                {
                    reader310.Read();
                    string list = Convert.ToString(reader310["id"]);

                    if (id == list)
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        String str = "SELECT * FROM type_8 WHERE id = @I";
                        cmd.CommandText = str;
                        cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = id;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                        cmd.Connection = con;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        this.LViewType9.Items.Add(ds.Tables[0].DefaultView);
                        con.Close();
                    }
                    count1--;
                }
                o--;
            }
        }

        private void Addendum(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }

        // Вывод на печать
        private void Print(object sender, RoutedEventArgs e)
        {

        }
    }
}
