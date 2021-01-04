using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy UserManage.xaml
    /// </summary>
    public partial class UserManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;
        public UserManage(Controller.AppController MyController, StaffMenu staffMenu)
        {
            InitializeComponent();
            this.MyController = MyController;
            this.staffMenu = staffMenu;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            saveButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private void userSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userList.SelectedIndex == -1)
            {
                saveButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
                addButton.IsEnabled = true;
            }
            else
            {
                saveButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
                addButton.IsEnabled = false;
            }
        }

        private void saveBtnClicked(object sender, RoutedEventArgs e)
        {
            if(!ValidateFields())
            {
                return;
            }
            Model.User tempUser = staffMenu.ObservableUsers[userList.SelectedIndex];
            if (UserPassword.Password != "")
            {
                tempUser.UserPassword = UserPassword.Password;
            }
            MyController.manageUser.SaveUser(tempUser, tempUser.UserId);
            staffMenu.updateUsers();
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(userList.SelectedIndex > 0)
            {
                Model.User tempUser = staffMenu.ObservableUsers[userList.SelectedIndex];
                staffMenu.ObservableUsers.Remove(tempUser);
                MyController.manageUser.DeleteUser(tempUser.UserId);
            }
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            int phoneNumber;
            if(!Int32.TryParse(userPhone.Text,out phoneNumber))
            {
                return;
            }
            if (String.IsNullOrEmpty(UserPassword.Password))
            {
                staffMenu.windowManager.ShowMessage("Pole hasło nie może być puste!");
                return;
            }
            Model.User tempUser = new Model.User() {
                UserName = userName.Text,
                Email = userEmail.Text,
                Name = NameTextBox.Text,
                Surname = userSurname.Text,
                Adres1 = userAdres1.Text,
                Adres2 = userAdres2.Text,
                Adres3 = userAdres3.Text,
                Phone = phoneNumber,
                Rola = (Model.UserRole)rolaCombo.SelectedIndex,
            };
            if (UserPassword.Password != "")
            {
                tempUser.UserPassword = UserPassword.Password;
                var salt = Controller.ManageUsers.GenerateSalt();
                tempUser.UserPassword = Controller.ManageUsers.GenerateHash(UserPassword.Password, salt);
                tempUser.Salt = salt;
            }
            MyController.manageUser.SaveUser(tempUser, -1);
            staffMenu.updateUsers();
        }

        private bool ValidateFields()
        {
            int tempInt;
            if (String.IsNullOrEmpty(userName.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole nazwa użytkownika nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userEmail.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole adres email nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole imię nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userSurname.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole nazwisko nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres1.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole ulica nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres2.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole miejscowość nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userAdres3.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole kod pocztowy nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(userPhone.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole numer telefonu nie może być puste!");
                return false;
            }
            if (!Int32.TryParse(userPhone.Text, out tempInt))
            {
                staffMenu.windowManager.ShowMessage("Błędny numer telefonu!");
                return false;
            }
            if(rolaCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musisz wybrać rolę użytkownika!");
                return false;
            }
            return true;
        }
    }
}
