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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameName.Navigate(new Authorization(FrameName));
        }

        private async void Menu_Main(object sender, MouseButtonEventArgs e)
        {
            object item = textBlock1.Text;
            FrameName.Navigate(new Main(FrameName, item));
        }

        private void Menu_Publications(object sender, MouseButtonEventArgs e)
        {
            FrameName.Navigate(new Publications(FrameName));
        }

        private void Menu_Messagea(object sender, MouseButtonEventArgs e)
        {
            FrameName.Navigate(new Messages(FrameName));
        }

        private void Menu_Rating(object sender, MouseButtonEventArgs e)
        {
            FrameName.Navigate(new Rating(FrameName));
        }

        private void Menu_Help(object sender, MouseButtonEventArgs e)
        {
            FrameName.Navigate(new Help(FrameName));
        }

        private void Menu_Exit(object sender, MouseButtonEventArgs e)
        {
            FrameName.Navigate(new Authorization(FrameName));
        }
    }
}
