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

        private bool ValidateFields()
        {
            if (String.IsNullOrEmpty(loginTextBox.Text))
            {
                ShowMessage("Pole login nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(passwordBox.Password))
            {
                ShowMessage("Pole hasło nie może być puste!");
                return false;
            }
            if (passwordBox.Password != repeatPasswordBox.Password)
            {
                ShowMessage("Hasła nie pasują!");
                return false;
            }
            if (String.IsNullOrEmpty(emailTextBox.Text))
            {
                ShowMessage("Pole email nie może być puste!");
                return false;
            }
            return true;
        }
        public void ShowMessage(string text)
        {
            MessageWindow messageWindow = new MessageWindow(text);
            messageWindow.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            messageWindow.ShowDialog();

        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;
            MyController.manageUser.AddUser(loginTextBox.Text, passwordBox.Password, emailTextBox.Text);
        }
    }
}
