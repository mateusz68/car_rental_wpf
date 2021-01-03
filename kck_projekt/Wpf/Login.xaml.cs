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
    /// Logika interakcji dla klasy Logowanie.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Controller.AppController MyController { get; set; }
        public Login(Controller.AppController MyController)
        {
            InitializeComponent();
            this.MyController = MyController;
        }

        private void openRegisterWindow(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register(MyController);
            registerWindow.ShowDialog();
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void loginButtonClicked(object sender, RoutedEventArgs e)
        {
            if ((bool)rememberCheckBox.IsChecked)
            {
                MyController.LoginUser(emailTextBox.Text.ToString(), passwordBox.Password.ToString(), true);
            }
            else
            {
                MyController.LoginUser(emailTextBox.Text.ToString(), passwordBox.Password.ToString(), false);
            }
            
        }
    }
}
