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
    /// Логика взаимодействия для Add_Type_8.xaml
    /// </summary>
    public partial class Add_Type_8 : Page
    {
        public Frame frame1;
        public Add_Type_8(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        // Переменные для хранения необходимых данных
        string part_3;  // 3 часть текста (выходные данные)
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

            //1 часть текста
            nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));
            nazvanie3 = nazvanie3.Substring(0, nazvanie3.LastIndexOf('/') + (-1));
            part_1 = nazvanie3;
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
        }

        public void Points()
        {
            // Передача текста из RichTextBox
            var textRange_Diss = new TextRange(Diss.Document.ContentStart, Diss.Document.ContentEnd);
            string Diss2 = textRange_Diss.Text;

            double points = 0;
            double points2 = 0;

            // Подсчет баллов за журнал
            if (Diss2 == null || number_of_authors == 0)  // Если одно поле из 2 является пустым, то баллы = 0 
            {                                                                        // 1. Кол-во печатных листов
                points = 0;                                                          // 2. Кол-во авторов
            }
            else
            {
                if (Diss2 == "дис\r\n" || Diss2 == "дис")
                    points = 30;
                else
                    points = 10;

                double itog = points / number_of_authors; // Формула баллов
                double result = Math.Round(itog, 2);    // Сокращение цифр после запятой до 2-х знаков
                point = result;
            }
        }

        public void Addendum_Type8()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            var textRange_Diss = new TextRange(Diss.Document.ContentStart, Diss.Document.ContentEnd);
            string Diss2 = textRange_Diss.Text;

            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `type_8` (`authors`, `diploma`, `number_authors`, `points`) VALUES (@authors, @diploma, @number_authors, @points)", dataBase.getConnection());

            command.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            command.Parameters.Add("@diploma", MySqlDbType.VarChar).Value = Diss2;
            command.Parameters.Add("@number_authors", MySqlDbType.Int32).Value = number_of_authors;
            command.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_8(frame1));
                }
                else
                    frame1.Navigate(new Addendum(frame1));
            }
            else
                MessageBox.Show("Произошла ошибка");
            dataBase.closeConnection();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Parsing_Titles();
            Parsing_Authors();
            Points();
            Addendum_Type8();
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
