using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для Izm_Type_1.xaml
    /// </summary>
    public partial class Izm_Type_1 : Page
    {
        public Frame frame1;
        public Izm_Type_1(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            Details();
        }

        public int Count()
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

        public void Details()
        {
            

        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Publications(frame1));
        }
    }
}
