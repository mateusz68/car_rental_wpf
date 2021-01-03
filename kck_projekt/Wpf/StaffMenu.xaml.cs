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
    /// Logika interakcji dla klasy StaffMenu.xaml
    /// </summary>
    public partial class StaffMenu : Window
    {
        private CarManage carManage;
        private ReserwationManage reserwationManage;
        public StaffMenu()
        {
            InitializeComponent();
        }

        private void ReservationsButtonClicked(object sender, RoutedEventArgs e)
        {
            if(reserwationManage == null)
            {
                reserwationManage = new ReserwationManage();
            }
            contentControl.Content = reserwationManage;
        }

        private void CarsButtonClicked(object sender, RoutedEventArgs e)
        {
            if(carManage == null)
            {
                carManage = new CarManage();
            }
            contentControl.Content = carManage;
        }

        private void MarksButtonCliced(object sender, RoutedEventArgs e)
        {

        }

        private void ModelsButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
