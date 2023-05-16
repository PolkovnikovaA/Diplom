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
    /// Логика взаимодействия для Add_Type_5.xaml
    /// </summary>
    public partial class Add_Type_5 : Page
    {
        public Frame frame1;
        public Add_Type_5(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        // Переменные для хранения необходимых данных
        string part_3;  // 3 часть текста (выходные данные)
        string part_2;  // 2 часть текста (авторы)
        string part_1;  // 1 часть текста (название)
        int number_of_authors;  // Количество авторов
        double point;  // Баллы

        // Парсинг строки названия
        public void Parsing_Titles()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            string nazvanie = Nazvanie2;
            string nazvanie2 = Nazvanie2;
            string nazvanie3 = Nazvanie2;

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

        public void Parsing_Authors()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            // Парсинг1 части названия
            string nazvanie3 = Nazvanie2;

            // 1. Посчитаем сколько символов в первой части
            nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));
            nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));

            // Подсчет авторов:
            // 1. Разделяем слова
            char[] words_authors = { ',' };
            string[] words = nazvanie3.Split(words_authors);
            // 2. Считаем авторов
            int authors = words.Length;
            // 3. Вывод
            number_of_authors = authors;

            string str;

            MySqlCommand command = new MySqlCommand();
            string connectionString, commandString;
            connectionString = "server=localhost;port=3306;username=root;password=root;database=xr";
            MySqlConnection connection = new MySqlConnection(connectionString);
            commandString = "SELECT * FROM users";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
                int count2 = Count();

                while (count2 > 0)
                {
                    reader.Read();
                    str = "";
                    str = Convert.ToString(reader["surname"]);
                    string avt = str;
                    //reader.Close();
                    int j = 0;

                    string q = "";
                povtor:
                    for (int i = j; i < words.Length; i++)
                    {
                        string p = words[i];

                    povtor2:
                        foreach (char aut3 in str)
                        {
                            foreach (char aut2 in p)
                            {
                                if (aut2 == aut3)
                                {
                                    q = q + Convert.ToString(aut2);
                                    if (q == avt)
                                    {
                                        // Добавление в базу данных
                                        Points();
                                        DataBase dataBase2 = new DataBase();
                                        MySqlCommand command2 = new MySqlCommand("UPDATE `users` SET `points` = `points` + @points WHERE `users`.`surname` = @surname", dataBase2.getConnection());

                                        command2.Parameters.Add("@surname", MySqlDbType.VarChar).Value = q;
                                        command2.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

                                        dataBase2.openConnection();
                                        if (command2.ExecuteNonQuery() == 1)
                                        {
                                            MessageBoxResult result2 = MessageBox.Show("Все хорошо");
                                        }
                                        else
                                            MessageBox.Show("Произошла ошибка");
                                        dataBase2.closeConnection();



                                        string zagl = "заглушка";

                                        DataBase dataBase3 = new DataBase();
                                        MySqlCommand command3 = new MySqlCommand("SELECT id FROM users WHERE surname = @surname", dataBase3.getConnection());
                                        command3.Parameters.Add("@surname", MySqlDbType.VarChar).Value = q;
                                        MySqlDataReader reader3;
                                        command3.Connection.Open();
                                        reader3 = command3.ExecuteReader();
                                        reader3.Read();
                                        string user_id = Convert.ToString(reader3["id"]);
                                        reader3.Close();

                                        DataBase dataBase31 = new DataBase();
                                        MySqlCommand command31 = new MySqlCommand("SELECT `id` FROM `type_5` WHERE `authors` = @authors", dataBase31.getConnection());
                                        command31.Parameters.Add("@authors", MySqlDbType.VarChar).Value = zagl;
                                        MySqlDataReader reader31;
                                        command31.Connection.Open();
                                        reader31 = command31.ExecuteReader();
                                        reader31.Read();
                                        string type_id = Convert.ToString(reader31["id"]);
                                        reader31.Close();

                                        DataBase dataBase4 = new DataBase();
                                        MySqlCommand command4 = new MySqlCommand("INSERT INTO `id_type_5` (`id_users`, `id_type_5`) VALUES (@iU, @iT)", dataBase4.getConnection());
                                        command4.Parameters.Add("@iU", MySqlDbType.Int32).Value = Convert.ToInt32(user_id);
                                        command4.Parameters.Add("@iT", MySqlDbType.Int32).Value = Convert.ToInt32(type_id);
                                        dataBase4.openConnection();
                                        if (command4.ExecuteNonQuery() == 1)
                                        {
                                        }
                                        else
                                            MessageBox.Show("Произошла ошибка");
                                        dataBase4.closeConnection();
                                        //str = "";
                                        break;
                                    }
                                    //else
                                    //    str = str.Remove(0, 1);
                                    str = str.Remove(0, 1);
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
                    count2--;
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
        }

        public void Points()
        {
            // Передача текста из RichTextBox
            var textRange_Teor = new TextRange(Teor.Document.ContentStart, Teor.Document.ContentEnd);
            string Teor2 = textRange_Teor.Text;

            double points = 0;
            double points2 = 0;

            // Подсчет баллов за журнал
            if (Teor2 == null || number_of_authors == 0)  // Если одно поле из 2 является пустым, то баллы = 0 
            {                                                                        // 1. Кол-во печатных листов
                points = 0;                                                          // 2. Кол-во авторов
            }
            else
            {
                if (Teor2 == "нет\r\n" || Teor2 == "нет")
                    points = 7.5;
                else
                    points = 5;

                if (number_of_authors < 10)
                    points2 = number_of_authors;
                else
                    points2 = 10;

                double itog = points / points2; // Формула баллов
                double result = Math.Round(itog, 2);    // Сокращение цифр после запятой до 2-х знаков
                point = result;
            }
        }

        public void Addendum_Type5()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            var textRange_Teor = new TextRange(Teor.Document.ContentStart, Teor.Document.ContentEnd);
            string Teor2 = textRange_Teor.Text;

            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `type_5` (`authors`, `theoretical`, `number_authors`, `points`) VALUES (@authors, @theoretical, @number_authors, @points)", dataBase.getConnection());

            command.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            command.Parameters.Add("@theoretical", MySqlDbType.VarChar).Value = Teor2;
            command.Parameters.Add("@number_authors", MySqlDbType.Int32).Value = number_of_authors;
            command.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_5(frame1));
                }
                else
                    frame1.Navigate(new Addendum(frame1));
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase.closeConnection();

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT `id` FROM `type_5` WHERE `authors` = @authors", dataBase31.getConnection());
            command31.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();
            reader31.Read();
            string type_id = Convert.ToString(reader31["id"]);  // id type
            reader31.Close();

            DataBase dataBase22 = new DataBase();
            MySqlCommand command22 = new MySqlCommand("UPDATE `id_type_5` SET `id_type_5` = @id_type_5 WHERE `id_type_5`.`id_type_5` = 1", dataBase22.getConnection());
            command22.Parameters.Add("@id_type_5", MySqlDbType.Int32).Value = Convert.ToInt32(type_id);

            dataBase22.openConnection();
            if (command22.ExecuteNonQuery() == 1)
            {
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase22.closeConnection();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Parsing_Titles();
            Parsing_Authors();
            Points();
            Addendum_Type5();
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
