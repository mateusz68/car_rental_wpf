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
    /// Logika interakcji dla klasy CarManage.xaml
    /// </summary>
    public partial class CarManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;
        public CarManage(Controller.AppController MyController, StaffMenu staffMenu)
        {
            InitializeComponent();
            this.MyController = MyController;
            this.staffMenu = staffMenu;
        }

        private void saveBtnClicked(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {

        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {

        }

        private void carSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            carList.ItemsSource = staffMenu.CarsList;
            ModelCombo.ItemsSource = staffMenu.CarModelList;
        }
    }
}
