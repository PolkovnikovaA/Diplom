using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Diplom_2023
{
    /// <summary>
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void SendEmailAsync()
        {

            //string nazvanie = Email2.Substring(0, Email2.LastIndexOf('/') + (-1));

            var textRange_Mess1 = new TextRange(Mess1.Document.ContentStart, Mess1.Document.ContentEnd);
            string Mess12 = textRange_Mess1.Text;

            StreamReader reader = new StreamReader(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);
            string[] strok = File.ReadAllLines(@"C:\Users\21nas\source\repos\Diplom_2023\Diplom_2023\Login_and_password.txt", System.Text.Encoding.Default);

            string pp = strok[0];

            // 1 часть текста:
            // 1. Посчитаем сколько символов в первой части
            pp = pp.Substring(0, pp.LastIndexOf(' '));

            /*MailAddress from = new MailAddress("", pp);
            MailAddress to = new MailAddress("21nastya.chernaya19@gmail.ru");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = Mess12;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);    //587
            smtp.Credentials = new NetworkCredential("", "MaksimVlasov2020");
            smtp.EnableSsl = true;
            smtp.Send(m);*/
            if (Mess12 == "\r\n")
                MessageBox.Show("Введите сообщение!");
            else
            {
                MessageBox.Show("Письмо отправлено. В ближайшее время с вами свяжутся");
                this.Close();
            }    
                
        }

        private void Mess(object sender, RoutedEventArgs e)
        {
            SendEmailAsync();
        }
    }
}
