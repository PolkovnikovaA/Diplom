using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
            //Publik();
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
            string stmt = "SELECT COUNT(*) FROM type";
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
        }

        // Переменные для хранения необходимых данных
        string part_3;  // 3 часть текста (выходные данные)
        string part_2;  // 2 часть текста (авторы)
        string part_1;  // 1 часть текста (название)
        int number_of_authors;  // Количество авторов
        string russian_foreign;  // Российский или зарубежный журнал
        double point;  // Баллы

        //Вывод кол-ва публикаций
        public void Publik()
        {
            string str;
            string polz = Name.Text;

            MySqlCommand command = new MySqlCommand();
            string connectionString, commandString;
            connectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
            MySqlConnection connection = new MySqlConnection(connectionString);
            commandString = "SELECT * FROM type";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();

                int count2 = Count2();
                int ii = 0;

            povtor3:
                while (count2 > 0)
                {
                    reader.Read();
                    str = "";
                    str = Convert.ToString(reader["authors"]);

                    string nazvanie = str;
                    string nazvanie2 = str;
                    string nazvanie3 = str;

                    // 3 часть текста
                    string[] words_c = nazvanie.Split('/');
                    foreach (var word in words_c)
                    {
                        string c = word;
                        part_3 = c;
                    }

                    // 2 часть текста:
                    // 1. Посчитаем сколько символов в первой части
                    nazvanie = nazvanie.Substring(0, nazvanie.LastIndexOf('/') + (-1));
                    nazvanie2 = nazvanie.Substring(0, nazvanie.LastIndexOf('/') + 2);
                    int length = nazvanie2.Length;   // количество символов в первой части
                                                     // 2. Вычтим количество символов из 3 части
                    nazvanie = nazvanie.Remove(0, length);
                    part_2 = nazvanie;

                    //1 часть текста
                    nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));
                    nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));
                    part_1 = nazvanie3;

                    // Подсчет авторов:
                    // 1. Разделяем слова
                    char[] words_authors = { ',' };
                    string[] words = nazvanie.Split(words_authors);
                    // 2. Считаем авторов
                    int authors = words.Length;

                    int j = 0;

                    string q = "";

                    string dest = "";
                    string dest2 = "";
                    if (polz.Length > 0)
                    {
                        dest = polz.Substring(0, polz.Length - 2);
                        dest2 = polz.Substring(0, polz.Length - 2);
                    }

                povtor:
                    for (int i = j; i < words.Length; i++)
                    {
                        string p = words[i];

                    povtor2:
                        foreach (char aut3 in dest)
                        {
                            foreach (char aut2 in p)
                            {
                                if (aut2 == aut3)
                                {
                                    q = q + Convert.ToString(aut2);
                                    if (q == dest2)
                                    {
                                        ii++;
                                        Public.Text = Convert.ToString(ii);
                                        count2--;
                                        goto povtor3;
                                        //break;
                                    }
                                    dest = dest.Remove(0, 1);
                                    goto povtor2;
                                }
                                else
                                    p = p.Remove(0, 1);
                                if (p == "")
                                {
                                    j++;
                                    goto povtor;
                                }
                            }
                        }
                    }
                    //count2--;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: \r\n{0}", ex.ToString());
            }
            finally
            {
                command.Connection.Close();
            }

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
