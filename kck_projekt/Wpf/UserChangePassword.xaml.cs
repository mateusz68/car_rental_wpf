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
    /// Logika interakcji dla klasy UserChangePassword.xaml
    /// </summary>
    public partial class UserChangePassword : UserControl
    {
        private Controller.AppController MyController { get; set; }
        private Model.User EditUser { get; set; }
        private UserMenu userMenu;
        public UserChangePassword(Controller.AppController MyController, Model.User current, UserMenu userMenu)
        {
            InitializeComponent();
            this.MyController = MyController;
            this.EditUser = current;
            this.userMenu = userMenu;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            MyController.manageUser.ChangePassword(OldPassword.Text, NewPassword.Text, EditUser.UserId);
        }

        private bool ValidateFields()
        {
            if (String.IsNullOrEmpty(OldPassword.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole imię nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(NewPassword.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole imię nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(NewPasswordRepeat.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole imię nie może być puste!");
                return false;
            }
            if(NewPassword.Text != OldPassword.Text)
            {
                userMenu.WindowManager.ShowMessage("Nowe hasła się nie są takie same!");
                return false;
            }
            return true;

        }
    }
}
