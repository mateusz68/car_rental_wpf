using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        public Controller.AppController MyController { get; set; }
        public List<Model.Car> CarsList { get; set; }
        public List<Model.CarMark> CarMarkList { get; set; }
        public List<Model.CarModel> CarModelList { get; set; }
        public List<Model.User> UserList { get; set; }
        public List<Model.Reservation> ReservationsList { get; set; }
        public ObservableCollection<Model.Reservation> observableReservation;
        public StaffMenu(Controller.AppController MyController)
        {
            InitializeComponent();
            this.MyController = MyController;
            loadData();
        }

        public void loadData()
        {
            CarsList = MyController.manageCars.GetCarList();
            CarMarkList = MyController.manageCars.GetMarkList();
            CarModelList = MyController.manageCars.GetModelsList();
            UserList = MyController.manageUser.GetUserList();
            ReservationsList = MyController.manageReservation.GetReservationList();
            observableReservation = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetReservationList());

        }

        public void updateReservationList()
        {
            observableReservation = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetReservationList());
            ReservationsList = MyController.manageReservation.GetReservationList();
        }

        private void ReservationsButtonClicked(object sender, RoutedEventArgs e)
        {
            if(reserwationManage == null)
            {
                reserwationManage = new ReserwationManage(MyController, this);
            }
            contentControl.Content = reserwationManage;
        }

        private void CarsButtonClicked(object sender, RoutedEventArgs e)
        {
            if(carManage == null)
            {
                carManage = new CarManage(MyController, this);
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
