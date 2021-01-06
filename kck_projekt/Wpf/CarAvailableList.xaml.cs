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
    /// Logika interakcji dla klasy CarAvailableList.xaml
    /// </summary>
    public partial class CarAvailableList : UserControl
    {
        private UserMenu userMenu;
        public CarAvailableList(UserMenu userMenu)
        {
            InitializeComponent();

            this.userMenu = userMenu;
        }

        private void carSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carList.SelectedIndex == -1)
            {
                rentButton.IsEnabled = false;
                availableButton.IsEnabled = false;
            }
            else
            {
                rentButton.IsEnabled = true;
                availableButton.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rentButton.IsEnabled = false;
            availableButton.IsEnabled = false;
        }
    }
}
