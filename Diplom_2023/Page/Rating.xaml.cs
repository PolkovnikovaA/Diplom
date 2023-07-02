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
    /// Логика взаимодействия для Rating.xaml
    /// </summary>
    public partial class Rating : Page
    {
        public Frame frame1;
        public Rating(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            List_type_1();
        }

        public int Count1()
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

        // Вывод в лист
        public void List_type_1()
        {
            int count = Count1();
            DataBase dataBase310 = new DataBase();
            MySqlCommand command310 = new MySqlCommand("SELECT id FROM users", dataBase310.getConnection());
            MySqlDataReader reader310;
            command310.Connection.Open();
            reader310 = command310.ExecuteReader();

            while(count > 0)
            {
                reader310.Read();
                string list = Convert.ToString(reader310["id"]);

                MySqlConnection con = new MySqlConnection();
                MySqlDataAdapter ad = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                String str = "SELECT * FROM users WHERE id = @I";
                cmd.CommandText = str;
                cmd.Parameters.Add("@I", MySqlDbType.VarChar).Value = list;
                ad.SelectCommand = cmd;
                con.ConnectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
                cmd.Connection = con;
                DataSet ds = new DataSet();
                ad.Fill(ds);
                this.LViewType2.Items.Add(ds.Tables[0].DefaultView);
                con.Close();
                count--;
            }




        }
    }
}
