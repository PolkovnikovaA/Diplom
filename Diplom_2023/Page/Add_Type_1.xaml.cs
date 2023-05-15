using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Add_Type_1.xaml
    /// </summary>
    public partial class Add_Type_1 : Page
    {
        public Frame frame1;
        public Add_Type_1(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        // Переменные для хранения необходимых данных
        string part_3;  // 3 часть текста (выходные данные)
        string part_2;  // 2 часть текста (авторы)
        string part_1;  // 1 часть текста (название)
        int number_of_authors;  // Количество авторов
        string russian_foreign;  // Российский или зарубежный журнал
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

            // Определяем какой журнал: зарубежный или российский
            string str;
            str = nazvanie3;
            str = str.ToLower();    // Перевод в нижний регистр
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            int angl_count = 0, russ_count = 0;
            foreach (byte bt in b)  // Проверка
            {
                if ((bt >= 97) && (bt <= 122)) angl_count++;
                if ((bt >= 192) && (bt <= 239)) russ_count++;
            }
            if (angl_count > russ_count)
                russian_foreign = "з";
            if (angl_count < russ_count)
                russian_foreign = "р";
        }

        public void Points()
        {
            // Передача текста из RichTextBox
            var textRange_Impact = new TextRange(Impact.Document.ContentStart, Impact.Document.ContentEnd);
            string Impact2 = textRange_Impact.Text;

            var textRange_Teor = new TextRange(Teor.Document.ContentStart, Teor.Document.ContentEnd);
            string Teor2 = textRange_Teor.Text;

            double points = 0;
            double points1 = 0;
            double points2 = 0;
            double points3 = 0;

            // Подсчет баллов за журнал
            if (russian_foreign == null || Impact2 == null)  // Если одно поле из 2 является пустым, то баллы = 0 
            {                                                                        // 1. Российский или зарубежный журнал
                points = 0;                                                          // 2. Работа является теоретической
            }
            else
            {
                if (Convert.ToDouble(Impact2) < 0.2)    // Если импакт фактор журнала < 0.2, то баллы = 6
                {
                    points = 6;
                }
                else   // Если импакт фактор журнала > 0.2, то баллы = импакт
                {
                    points = Convert.ToDouble(Impact2);
                }
                if (russian_foreign == "р") // Если журнал является российским, то баллы = 45
                {
                    points1 = 45;
                }
                else   // Если журнал является зарубежным, то баллы = 30
                {
                    points1 = 30;
                }
                if (Teor2 == "нет\r\n") // Если работа не является чисто теоретической, то баллы = 1.5
                {
                    points2 = 1.5;
                }
                else   // Если работа является чисто теоритической, то баллы = 1
                {
                    points2 = 1;
                }
                if (Convert.ToDouble(number_of_authors) < 10) // Если количество авторов < 10, то баллы = количество авторов
                {
                    points3 = Convert.ToDouble(number_of_authors);
                }
                else   // Если количество авторов => 10, то баллы = 10
                {
                    points3 = 10;
                }
                double itog = (points * points1 * points2) / points3; // Формула баллов
                double result = Math.Round(itog, 2);    // Сокращение цифр после запятой до 2-х знаков
                point = result;
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

        public void Parsing_Authors()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            // Парсинг 2 части названия
            string nazvanie = Nazvanie2;
            string nazvanie2 = Nazvanie2;

            // 1. Посчитаем сколько символов в первой части
            nazvanie = nazvanie.Substring(0, nazvanie.LastIndexOf('/') + (-1));
            nazvanie2 = nazvanie.Substring(0, nazvanie.LastIndexOf('/') + 2);
            int length = nazvanie2.Length;   // количество символов в первой части
                                             // 2. Вычтим количество символов из названия
            nazvanie = nazvanie.Remove(0, length);

            // Подсчет авторов:
            // 1. Разделяем слова
            char[] words_authors = { ',' };
            string[] words = nazvanie.Split(words_authors);
            // 2. Считаем авторов
            int authors = words.Length;
            // 3. Вывод
            number_of_authors = authors;

            /*string b = Convert.ToString(words);
            foreach (char c in b)
            {
                Console.WriteLine(c);
            }*/

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
                
                while(count2 > 0)
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
                                        MySqlCommand command31 = new MySqlCommand("SELECT `id` FROM `type` WHERE `authors` = @authors", dataBase31.getConnection());
                                        command31.Parameters.Add("@authors", MySqlDbType.VarChar).Value = zagl;
                                        MySqlDataReader reader31;
                                        command31.Connection.Open();
                                        reader31 = command31.ExecuteReader();
                                        reader31.Read();
                                        string type_id = Convert.ToString(reader31["id"]);
                                        reader31.Close();

                                        DataBase dataBase4 = new DataBase();
                                        MySqlCommand command4 = new MySqlCommand("INSERT INTO `id` (`id_users`, `id_type`) VALUES (@iU, @iT)", dataBase4.getConnection());
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



            // Поиск авторов по базе
            /*string test = " Vladimir N. Polkovnikov";

            DataBase dataBase = new DataBase();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", dataBase.getConnection());
            

            foreach (string aut in words)
            {
                if (aut == test)
                    MessageBox.Show("Нашел");
            }*/

        }

        public void Addendum_Type1()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            var textRange_Impact = new TextRange(Impact.Document.ContentStart, Impact.Document.ContentEnd);
            string Impact2 = textRange_Impact.Text;

            var textRange_Teor = new TextRange(Teor.Document.ContentStart, Teor.Document.ContentEnd);
            string Teor2 = textRange_Teor.Text;

            var textRange_Conclusion = new TextRange(Conclusion.Document.ContentStart, Conclusion.Document.ContentEnd);
            string Conclusion2 = textRange_Conclusion.Text;

            // Поиск авторов по базе


            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `type_1` (`authors`, `ross_zarub`, `theoretical`, `number_authors`, `impact`, `conclusion`, `points`) VALUES (@authors, @ross_zarub, @theoretical, @number_authors, @impact, @conclusion, @points)", dataBase.getConnection());
            
            command.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            command.Parameters.Add("@ross_zarub", MySqlDbType.VarChar).Value = russian_foreign;
            command.Parameters.Add("@theoretical", MySqlDbType.VarChar).Value = Teor2;
            command.Parameters.Add("@number_authors", MySqlDbType.Int32).Value = Convert.ToInt32(number_of_authors);
            command.Parameters.Add("@impact", MySqlDbType.Double).Value = Convert.ToDouble(Impact2);
            command.Parameters.Add("@conclusion", MySqlDbType.VarChar).Value = Conclusion2;
            command.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_1(frame1));
                }
                else
                    frame1.Navigate(new Addendum(frame1));
            }    
            else
                MessageBox.Show("Произошла ошибка");
            dataBase.closeConnection();

            DataBase dataBase2 = new DataBase();
            MySqlCommand command2 = new MySqlCommand("INSERT INTO `type` (`authors`) VALUES (@authors)", dataBase2.getConnection());

            command2.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            dataBase2.openConnection();
            if (command2.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result2 = MessageBox.Show("Все хорошо", "Confirmation", MessageBoxButton.YesNo);
                if (result2 == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_1(frame1));
                }
                else
                    frame1.Navigate(new Addendum(frame1));
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase2.closeConnection();

            DataBase dataBase31 = new DataBase();
            MySqlCommand command31 = new MySqlCommand("SELECT `id` FROM `type` WHERE `authors` = @authors", dataBase31.getConnection());
            command31.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            MySqlDataReader reader31;
            command31.Connection.Open();
            reader31 = command31.ExecuteReader();
            reader31.Read();
            string type_id = Convert.ToString(reader31["id"]);  // id type
            reader31.Close();

            DataBase dataBase22 = new DataBase();
            MySqlCommand command22 = new MySqlCommand("UPDATE `id` SET `id_type` = @id_type WHERE `id`.`id_type` = 1", dataBase22.getConnection());
            command22.Parameters.Add("@id_type", MySqlDbType.Int32).Value = Convert.ToInt32(type_id);

            dataBase22.openConnection();
            if (command22.ExecuteNonQuery() == 1)
            {
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase2.closeConnection();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Parsing_Titles();
            Parsing_Authors();
            Points();
            Raspredelenie();
            Addendum_Type1();
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
