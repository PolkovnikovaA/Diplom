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
    /// Логика взаимодействия для Add_Type_6.xaml
    /// </summary>
    public partial class Add_Type_6 : Page
    {
        public Frame frame1;
        public Add_Type_6(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        // Переменные для хранения необходимых данных
        double point;  // Баллы

        public void Points()
        {
            // Передача текста из RichTextBox
            var textRange_work = new TextRange(work.Document.ContentStart, work.Document.ContentEnd);
            string work2 = textRange_work.Text;

            var textRange_Semestr = new TextRange(Semestr.Document.ContentStart, Semestr.Document.ContentEnd);
            string Semestr2 = textRange_Semestr.Text;

            double points = 0;

            // Подсчет баллов за журнал
            if (work2 == null)  // Если одно поле является пустым, то баллы = 0 
            {                                                                    
                points = 0;                                                          
            }
            else
            {
                if (work2 == "лекции\r\n" || work2 == "лекции")
                    points = 20;
                else if (work2 == "практика\r\n" || work2 == "практика")
                    points = 10;
                else
                    points = 5;

                double itog = Convert.ToDouble(Semestr2) * points; // Формула баллов
                double result = Math.Round(itog, 2);    // Сокращение цифр после запятой до 2-х знаков
                point = result;
            }
        }

        public void Addendum_Type6()
        {
            // Передача текста из RichTextBox
            var textRange = new TextRange(Nazvanie.Document.ContentStart, Nazvanie.Document.ContentEnd);
            string Nazvanie2 = textRange.Text;

            var textRange_work = new TextRange(work.Document.ContentStart, work.Document.ContentEnd);
            string work2 = textRange_work.Text;

            var textRange_Semestr = new TextRange(Semestr.Document.ContentStart, Semestr.Document.ContentEnd);
            string Semestr2 = textRange_Semestr.Text;

            // Добавление в базу данных
            DataBase dataBase = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `type_6` (`name`, `view`, `number_authors`, `points`) VALUES (@name, @view, @number_authors, @points)", dataBase.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Nazvanie2;
            command.Parameters.Add("@view", MySqlDbType.VarChar).Value = work2;
            command.Parameters.Add("@number_authors", MySqlDbType.Int32).Value = Convert.ToInt32(Semestr2);
            command.Parameters.Add("@points", MySqlDbType.Double).Value = Convert.ToDouble(point);

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBoxResult result = MessageBox.Show("Данные успешно добавлены\nХотите остаться на этой странице?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    frame1.Navigate(new Add_Type_6(frame1));
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
            Points();
            Addendum_Type6();
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Addendum(frame1));
        }
    }
}
