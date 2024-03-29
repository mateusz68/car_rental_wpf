﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        public Controller.AppController MyController { get; set; }
        public Wpf.WindowManager WindowManager { get; set; }
        private UserRentHistory rentHistory;
        private AppSettings appSettings;
        private CarAvailableList carAvailableList;
        private MakeReservation makeReservation;
        private Model.User currentUser;
        #region define observable collections
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

        private ObservableCollection<Model.Reservation> observableUserReservation;
        public ObservableCollection<Model.Reservation> ObservableUserReservation
        {
            get { return observableUserReservation; }
            set
            {
                observableUserReservation = value;
                NotifyPropertyChanged("ObservableUserReservation");
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
        public UserMenu(Controller.AppController MyController, Wpf.WindowManager windowManager)
        {
            InitializeComponent();
            this.MyController = MyController;
            DataContext = this;
            this.WindowManager = windowManager;
            currentUser = windowManager.user;
            LoadData();
            userName.Text = currentUser.UserName;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            DarkModeToggleButton.IsChecked = theme.GetBaseTheme() == BaseTheme.Dark;

            //if (paletteHelper.GetThemeManager() is  themeManager)
            //{
            //    themeManager.ThemeChanged += (_, e) =>
            //    {
            //        DarkModeToggleButton.IsChecked = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
            //    };
            //}
            if (carAvailableList == null)
            {
                carAvailableList = new CarAvailableList(MyController, this);
            }
            contentControl.Content = carAvailableList;
        }

        #region load and update data
        public void LoadData()
        {
            ObservableCar = new ObservableCollection<Model.Car>(MyController.manageCars.GetAvaliableCarList());
            ObservableUserReservation = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetUserReservation(currentUser.UserId));
        }

        public void UpdateUserReservation()
        {
            ObservableUserReservation = new ObservableCollection<Model.Reservation>(MyController.manageReservation.GetUserReservation(currentUser.UserId));
        }

        #endregion


        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);

            paletteHelper.SetTheme(theme);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemUserRentHistory":
                    if (rentHistory == null)
                    {
                        rentHistory = new UserRentHistory();
                    }
                    contentControl.Content = rentHistory;
                    break;
                case "ItemCar":
                    if (carAvailableList == null)
                    {
                        carAvailableList = new CarAvailableList(MyController,this);
                    }
                    contentControl.Content = carAvailableList;
                    break;
                case "ItemAccount":
                    contentControl.Content = new UserAccountDetails(MyController, currentUser, this);
                    break;
                case "ItemPassword":
                    contentControl.Content = new UserChangePassword(MyController, currentUser, this);
                    break;
                default:
                    break;
            }
        }

        private void GridMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            MyController.Logout();
            Application.Current.Shutdown();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsButttonClick(object sender, RoutedEventArgs e)
        {
            if (appSettings == null)
            {
                appSettings = new AppSettings(MyController);
            }
            contentControl.Content = appSettings;
        }

        public void showReservationWindow(int carId)
        {
            if (makeReservation == null)
            {
                makeReservation = new MakeReservation(MyController,this,carId);
            }
            contentControl.Content = makeReservation;
        }

        public void closeReservationWindow()
        {
            if (carAvailableList == null)
            {
                carAvailableList = new CarAvailableList(MyController, this);
            }
            contentControl.Content = carAvailableList;
            if (makeReservation != null)
            {
                makeReservation = null;
            }
        }
    }
}
