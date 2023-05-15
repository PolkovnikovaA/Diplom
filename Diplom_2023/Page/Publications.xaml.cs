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
    /// Логика взаимодействия для Publications.xaml
    /// </summary>
    public partial class Publications : Page
    {
        public Frame frame1;
        public Publications(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;

            List();
        }

        // Вывод в лист
        public void List()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
             adapter.Fill(dataSet);
            LViewTours_Type_1.DataContext = dataSet.Tables[0];


            /*DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `type_1`");
            command.CommandText = string.Format("SELECT * FROM `type_1`", LViewTours_Type_1.SelectedItem);*/

            //LViewTours_Type_1.ItemsSource = ;
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
