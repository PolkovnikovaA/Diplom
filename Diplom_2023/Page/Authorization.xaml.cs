using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Frame frame1;
        public Authorization(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;

            Hidden_Menu();
        }

        // Разделение пользователей
        public async void userRole()
        {
            string log = login.Text;

            object item = login.Text;

            string connStr = "server=localhost;port=3306;username=root;password=root;database=xr";
            string sql = "SELECT dolzhnost FROM `users` WHERE `login` = @uL";

            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            MySqlParameter nameParam = new MySqlParameter("@uL", log);

            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add(nameParam);

            string Form_Role = command.ExecuteScalar().ToString();

            switch(Form_Role)
            {
                case "Сотрудник":
                    frame1.Navigate(new Main(frame1, item));
                    break;
            }
            conn.Close();
        }

        private void Authorization_Main(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass_open = password_open.Text;
            string pass_close = password_close.Password;

            DataBase dataBase = new DataBase();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", dataBase.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass_open;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            DataTable dataTable2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            MySqlCommand command2 = new MySqlCommand("SELECT * FROM `users` WHERE `password` = @uP2", dataBase.getConnection());
            command2.Parameters.Add("@uP2", MySqlDbType.VarChar).Value = pass_close;
            adapter2.SelectCommand = command2;
            adapter2.Fill(dataTable2);

            if (dataTable.Rows.Count > 0 || dataTable2.Rows.Count > 0)
                userRole();
            else
                MessageBox.Show("Неверный логин или пароль!");
        }

        // Скрытие меню на странице авторизации
        public void Hidden_Menu()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).rectangle.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_main.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_publication.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_messenger.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_rating.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_help.Visibility = Visibility.Hidden;
                    (window as MainWindow).ellipse_exit.Visibility = Visibility.Hidden;
                    (window as MainWindow).Logo.Visibility = Visibility.Hidden;
                }
            }
        }

        // Открытие пароля
        private void Open_Password(object sender, MouseButtonEventArgs args)
        {
            // Visibility для замка
            Open_Image.Visibility = Visibility.Hidden;
            Close_Image.Visibility = Visibility.Visible;

            // Visibility для пароля
            password_close.Visibility = Visibility.Hidden;
            password_open.Visibility = Visibility.Visible;
            password_open.Text = password_close.Password;
        }

        private void Close_Password(object sender, MouseButtonEventArgs args)
        {
            // Visibility для замка
            Open_Image.Visibility = Visibility.Visible;
            Close_Image.Visibility = Visibility.Hidden;

            // Visibility для пароля
            password_close.Visibility = Visibility.Visible;
            password_open.Visibility = Visibility.Hidden;
            password_close.Password = password_open.Text;
        }
    }
}
