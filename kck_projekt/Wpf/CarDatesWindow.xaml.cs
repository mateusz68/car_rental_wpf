using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy CarDatesWindow.xaml
    /// </summary>
    public partial class CarDatesWindow : Window
    {
        private Controller.AppController MyController { get; set; }
        public List<string> CarDates { get; set; }
        private int carId;
        public CarDatesWindow(Controller.AppController MyController, int carId)
        {
            InitializeComponent();
            DataContext = this;
            this.MyController = MyController;
            this.carId = carId;
            loadData();
        }

        private void loadData()
        {
            CarDates = MyController.manageReservation.GetCarReservationDates(carId);
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MouseLeftClicked(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
