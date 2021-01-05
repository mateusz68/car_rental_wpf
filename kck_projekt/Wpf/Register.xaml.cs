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

namespace kck_projekt.Wpf
{
    /// <summary>
    /// Logika interakcji dla klasy Rejestracja.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Controller.AppController MyController { get; set; }
        public Register(Controller.AppController MyController)
        {
            InitializeComponent();
            this.MyController = MyController;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mouseLeftClicked(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void closeBtnClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
