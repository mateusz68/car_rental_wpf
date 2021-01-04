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
    public partial class StaffMenu : Window, INotifyPropertyChanged
    {
        private CarManage carManage;
        private ReserwationManage reserwationManage;
        private CarMarkManage carMarkManage;
        private CarModelManage carModelManage;
        private UserManage userManage;
        public Controller.AppController MyController { get; set; }
        public Wpf.WindowManager windowManager { get; set; }

        #region define observable collections
        private ObservableCollection<Model.Reservation> observableReservations;
        public ObservableCollection<Model.Reservation> ObservableReservations
        {
            get { return observableReservations; }
            set
            {
                observableReservations = value;
                NotifyPropertyChanged("ObservableReservations");
            }
        }
        private ObservableCollection<Model.Car> observableCar;
        public ObservableCollection<Model.Car> ObservableCar
        {
            get { return observableCar; }
            set
            {
                observableCar = value;
                NotifyPropertyChanged("ObservableCar");
            }
        }
        private ObservableCollection<Model.CarModel> observableCarModel;
        public ObservableCollection<Model.CarModel> ObservableCarModel
        {
            get { return observableCarModel; }
            set
            {
                observableCarModel = value;
                NotifyPropertyChanged("ObservableCarModel");
            }
        }

        private ObservableCollection<Model.CarMark> observableCarMark;
        public ObservableCollection<Model.CarMark> ObservableCarMark
        {
            get { return observableCarMark; }
            set
            {
                observableCarMark = value;
                NotifyPropertyChanged("ObservableCarMark");
            }
        }

        private ObservableCollection<Model.User> observableUsers;
        public ObservableCollection<Model.User> ObservableUsers
        {
            get { return observableUsers; }
            set
            {
                observableUsers = value;
                NotifyPropertyChanged("ObservableUsers");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion
        public StaffMenu(Controller.AppController MyController, Wpf.WindowManager windowManager)
        {
            InitializeComponent();
            DataContext = this;
            this.MyController = MyController;
            loadData();
            this.windowManager = windowManager;
            if(windowManager.user.Rola == Model.UserRole.Admin)
            {
                userButton.IsEnabled = true;
            }
        }

        #region update and load data
        public void loadData()
        {
            ObservableCar = new ObservableCollection<Model.Car>(MyController.manageCars.GetCarList());
            ObservableCarModel = new ObservableCollection<Model.CarModel>(MyController.manageCars.GetModelsList());
            ObservableCarMark = new ObservableCollection<Model.CarMark>(MyController.manageCars.GetMarkList());
            ObservableReservations = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetReservationList());
            ObservableUsers = new ObservableCollection<Model.User>(MyController.manageUser.GetUserList());

        }

        public void updateReservationList()
        {
            observableReservations = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetReservationList());
        }

        public void updateCarList()
        {
            ObservableCar = new ObservableCollection<Model.Car>(MyController.manageCars.GetCarList());
        }

        public void updateCarModel()
        {
            ObservableCarModel = new ObservableCollection<Model.CarModel>(MyController.manageCars.GetModelsList());
        }

        public void updateCarMark()
        {
            ObservableCarMark = new ObservableCollection<Model.CarMark>(MyController.manageCars.GetMarkList());
        }

        public void updateReservations()
        {
            ObservableReservations = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetReservationList());
        }

        public void updateUsers()
        {
            ObservableUsers = new ObservableCollection<Model.User>(MyController.manageUser.GetUserList());
        }

        #endregion

        private void ReservationsButtonClicked(object sender, RoutedEventArgs e)
        {
            if (reserwationManage == null)
            {
                reserwationManage = new ReserwationManage(MyController, this);
            }
            contentControl.Content = reserwationManage;
        }

        private void CarsButtonClicked(object sender, RoutedEventArgs e)
        {
            if (carManage == null)
            {
                carManage = new CarManage(MyController, this);
            }
            contentControl.Content = carManage;
        }

        private void MarksButtonCliced(object sender, RoutedEventArgs e)
        {
            if (carMarkManage == null)
            {
                carMarkManage = new CarMarkManage(MyController, this);
            }
            contentControl.Content = carMarkManage;
        }

        private void ModelsButtonClicked(object sender, RoutedEventArgs e)
        {
            if (carModelManage == null)
            {
                carModelManage = new CarModelManage(MyController, this);
            }
            contentControl.Content = carModelManage;
        }

        private void UserButtonClicked(object sender, RoutedEventArgs e)
        {
            if (userManage == null)
            {
                userManage = new UserManage(MyController, this);
            }
            contentControl.Content = userManage;
        }
    }
}
