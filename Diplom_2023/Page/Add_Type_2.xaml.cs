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
    /// Логика взаимодействия для Add_Type_2.xaml
    /// </summary>
    public partial class Add_Type_2 : Page
    {
        public Frame frame1;
        public Add_Type_2(Frame frame)
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
                                             // 2. Вычтим количество символов из 3 части
            nazvanie = nazvanie.Remove(0, length);

            // Подсчет авторов:
            // 1. Разделяем слова
            char[] words_authors = { ',' };
            string[] words = nazvanie.Split(words_authors);
            // 2. Считаем авторов
            int authors = words.Length;
            // 3. Вывод
            number_of_authors = authors;
        }

        public void Points()
        {
            // Передача текста из RichTextBox
            var textRange_Printed_sheets = new TextRange(Printed_sheets.Document.ContentStart, Printed_sheets.Document.ContentEnd);
            string Printed_sheets2 = textRange_Printed_sheets.Text;

            double points = 0;

            // Подсчет баллов за журнал
            if (Convert.ToInt32(Printed_sheets2) == 0 || number_of_authors == 0)  // Если одно поле из 2 является пустым, то баллы = 0 
            {                                                                        // 1. Кол-во печатных листов
                points = 0;                                                          // 2. Кол-во авторов
            }
            else
            {
                points = (Convert.ToInt32(Printed_sheets2) * 30) / number_of_authors;   // Формула баллов
                double result = Math.Round(points, 2);    // Сокращение цифр после запятой до 2-х знаков
                point = result;
            }
        }

        public void Addendum_Type2()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            var textRange_Printed_sheets = new TextRange(Printed_sheets.Document.ContentStart, Printed_sheets.Document.ContentEnd);
            string Printed_sheets2 = textRange_Printed_sheets.Text;

            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `type_2` (`authors`, `printed_sheets`, `number_authors`, `points`) VALUES (@authors, @printed_sheets, @number_authors, @points)", dataBase.getConnection());

            command.Parameters.Add("@authors", MySqlDbType.VarChar).Value = Nazvanie2;
            command.Parameters.Add("@printed_sheets", MySqlDbType.Int32).Value = Convert.ToInt32(Printed_sheets2);
            command.Parameters.Add("@number_authors", MySqlDbType.Int32).Value = number_of_authors;
            command.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_2(frame1));
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
            Addendum_Type2();
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
