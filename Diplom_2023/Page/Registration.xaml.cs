using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Frame frame1;
        public Registration(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;

            Combo.Items.Add("");
            Combo.Items.Add("Сотрудник");
            Combo.Items.Add("Ученый секретарь");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `surname`, `name`, `patronymic`, `dolzhnost`, points) VALUES (@login, @password, @surname, @name, @patronymic, @dolzhnost, @points)", dataBase.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Log.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Pas.Password;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = Fam.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Nam.Text;
            command.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = Ot.Text;
            command.Parameters.Add("@dolzhnost", MySqlDbType.VarChar).Value = Combo.Text;
            //command.Parameters.Add("@birthday", MySqlDbType.Date).Value = 2002-06-02;
            command.Parameters.Add("@points", MySqlDbType.Double).Value = 0;

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Результат выполнения", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Registration(frame1));
                }
                else
                    frame1.Navigate(new Main(frame1));
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase.closeConnection();
        }
    }
}
