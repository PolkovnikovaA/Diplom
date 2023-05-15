using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Frame frame1;
        public Main(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;

            Visible_Menu();
            Points();
            Publik();
        }

        // Раскрываем меню на странице авторизации
        public void Visible_Menu()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).rectangle.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_main.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_publication.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_messenger.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_rating.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_help.Visibility = Visibility.Visible;
                    (window as MainWindow).ellipse_exit.Visibility = Visibility.Visible;
                    (window as MainWindow).Logo.Visibility = Visibility.Visible;
                }
            }
        }

        // Подсчет кол-ва строк в таблице
        public int Count()
        {
            string stmt = "SELECT COUNT(*) FROM users";
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

        public int Count2()
        {
            /*DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT id_users FROM id WHERE id_users = @id_users", dataBase31.getConnection());
            command31.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();
            reader31.Read();
            string id = Convert.ToString(reader31["id"]);
            reader31.Close();*/





            string stmt = "SELECT COUNT(*) FROM id";
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

        // Возраст
        public int Year()
        {
            DateTime now = DateTime.Now;
            string polz = Name.Text;

            DataBase dataBase33 = new DataBase();
            MySqlCommand command33 = new MySqlCommand("SELECT birthday FROM users WHERE login = @login", dataBase33.getConnection());
            command33.Parameters.Add("@login", MySqlDbType.VarChar).Value = polz;
            MySqlDataReader reader33;
            command33.Connection.Open();
            reader33 = command33.ExecuteReader();
            reader33.Read();
            DateTime user_id = Convert.ToDateTime(reader33["birthday"]);
            reader33.Close();

            int age = now.Year - user_id.Year;

            return age;
        }

        // Вывод баллов
        public void Points()
        {
            StreamReader reader3 = new StreamReader(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));

            reader3.Close();

            int count2 = Count();
            string str;
            string p = Convert.ToString(pp);

            MySqlCommand command = new MySqlCommand();
            string connectionString, commandString;
            connectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
            MySqlConnection connection = new MySqlConnection(connectionString);
            commandString = "SELECT * FROM users";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            command.Connection.Open();
            reader = command.ExecuteReader();

            while (count2 > 0)
            {
                reader.Read();
                str = "";
                str = Convert.ToString(reader["login"]);

                if (str == p)
                {
                    Name.Text = str;
                    break;
                }
            }
            command.Connection.Close();

            using (var con = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=xr"))
            {
                con.Open();
                using (var cmd = new MySqlCommand("Select points From users WHERE login = @uL", con))
                {
                    cmd.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Name.Text;

                    MySqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        points.Text = String.Format("{0}", reader2["points"]);
                    }
                }
            }

            int age = Year();
            double point = 0;

            double summ = Convert.ToDouble(points.Text);
            if (age < 31)
            {
                point = 1.5;
            }
            else
                point = 1;
            double itog = summ * point;
            points.Text = Convert.ToString(itog);
        }

        //Вывод кол-ва публикаций
        public void Publik()
        {
            int count2 = Count2();

            string polz = Name.Text;

            DataBase dataBase3 = new DataBase();
            MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE login = @login", dataBase3.getConnection());
            command3.Parameters.Add("@login", MySqlDbType.VarChar).Value = polz;
            MySqlDataReader reader3;
            command3.Connection.Open();
            reader3 = command3.ExecuteReader();
            reader3.Read();
            string user_id = Convert.ToString(reader3["id"]);
            reader3.Close();

            int ii = 0;
            int id = 0;
            DataBase dataBase312 = new DataBase();
            MySqlCommand command312 = new MySqlCommand("SELECT id_users FROM id", dataBase312.getConnection());
            //command312.Parameters.Add("@id_users", MySqlDbType.VarChar).Value = user_id;
            MySqlDataReader reader312;
            command312.Connection.Open();
            reader312 = command312.ExecuteReader();
            while (count2 > 0)
            {
                reader312.Read();
                id = Convert.ToInt32(reader312["id_users"]);

                if(Convert.ToInt32(user_id) == id)
                {
                    ii++;
                }
                count2--;
            }
            Public.Text = Convert.ToString(ii);

            if (Public.Text == "")
            {
                Public.Text = Convert.ToString(0);
            }
        }

        private void Addendum(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
