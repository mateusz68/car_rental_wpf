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
    /// Logika interakcji dla klasy UserAccountDetails.xaml
    /// </summary>
    public partial class UserAccountDetails : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public UserMenu userMenu;
        public Model.User EditUser { get; set; }
        public UserAccountDetails(Controller.AppController MyController, Model.User current, UserMenu userMenu)
        {
            InitializeComponent();
            DataContext = this;
            this.MyController = MyController;
            this.EditUser = current;
            this.userMenu = userMenu;
        }

        private bool ValidateFields()
        {
            int tempInt;
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole imię nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userSurname.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole nazwisko nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres1.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole ulica nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres2.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole miejscowość nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres3.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole kod pocztowy nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userPhone.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole numer telefonu nie może być puste!");
                return false;
            }
            if (!Int32.TryParse(userPhone.Text, out tempInt))
            {
                userMenu.WindowManager.ShowMessage("Błędny numer telefonu!");
                return false;
            }
            return true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            MyController.manageUser.ChangeUserSettings(EditUser.Name, EditUser.Surname, EditUser.Phone, EditUser.Adres1, EditUser.Adres2, EditUser.Adres3, EditUser.UserId);

        }
    }
}
