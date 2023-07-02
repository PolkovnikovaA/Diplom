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
    /// Логика взаимодействия для Addendum.xaml
    /// </summary>
    public partial class Addendum : Page
    {
        public Frame frame1;
        public Addendum(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        private void Addendum_Add_Type_1(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_1(frame1));
        }

        private void Addendum_Add_Type_2(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_2(frame1));
        }

        private void Addendum_Add_Type_3(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_3(frame1));
        }

        private void Addendum_Add_Type_4(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_4(frame1));
        }

        private void Addendum_Add_Type_5(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_5(frame1));
        }

        private void Addendum_Add_Type_6(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_6(frame1));
        }

        private void Addendum_Add_Type_7(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_7(frame1));
        }

        private void Addendum_Add_Type_8(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Add_Type_8(frame1));
        }

        private void Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Publications(frame1));
        }
    }
}
