using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy MakeReservation.xaml
    /// </summary>
    public partial class MakeReservation : UserControl
    {
        private Controller.AppController MyController { get; set; }
        private ObservableCollection<string> datesList;
        public ObservableCollection<string> DatesList
        {
            get { return datesList; }
            set
            {
                datesList = value;
            }
        }
        private UserMenu userMenu;
        private int selectedCar;
        public MakeReservation(Controller.AppController MyController, UserMenu userMenu, int selectedCar)
        {
            InitializeComponent();
            this.MyController = MyController;
            this.userMenu = userMenu;
            this.selectedCar = selectedCar;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model.Car tempCar = userMenu.ObservableCar.Where(x => x.CarId == selectedCar).FirstOrDefault();
            if(tempCar != null)
            {
                CarCombo.SelectedItem = tempCar;
                DatesList = new ObservableCollection<string>(MyController.manageReservation.GetCarReservationDates(selectedCar));
                CarDatesList.ItemsSource = DatesList;
            }
        }

        private void CarChangeCombo(object sender, SelectionChangedEventArgs e)
        {
            if(CarCombo.SelectedIndex > 0 && CarCombo.SelectedIndex < userMenu.ObservableCar.Count())
            {
                DatesList = new ObservableCollection<string>(MyController.manageReservation.GetCarReservationDates(userMenu.ObservableCar[CarCombo.SelectedIndex].CarId));
                CarDatesList.ItemsSource = DatesList;
                CarDatesList.Items.Refresh();
            }
        }

        private void reserveBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            Model.Reservation tempReservation = new Model.Reservation()
            {
                DateFrom = (DateTime)dateFrom.Value,
                DateTo = (DateTime)dateTo.Value,
                Comments = Comments.Text,
                Car = userMenu.ObservableCar[CarCombo.SelectedIndex],
                User = userMenu.WindowManager.user,
                Status = Model.ReservationStatus.Verification,
            };
            MyController.manageReservation.AddReservation(tempReservation);
        }

        private bool ValidateFields()
        {
            if (dateFrom.Value == null)
            {
                userMenu.WindowManager.ShowMessage("Pole data od nie może być puste!");
                return false;
            }
            if (dateTo.Value == null)
            {
                userMenu.WindowManager.ShowMessage("Pole data do nie może być puste!");
                return false;
            }
            if (dateFrom.Value > dateTo.Value)
            {
                userMenu.WindowManager.ShowMessage("Data rozpoczęcia musi być mniejsza niż data zakończenia!");
                return false;
            }
            if (String.IsNullOrEmpty(Comments.Text))
            {
                userMenu.WindowManager.ShowMessage("Pole uwagi nie może być puste!");
                return false;
            }
            if (CarCombo.SelectedIndex == -1)
            {
                userMenu.WindowManager.ShowMessage("Musiszy wybrać samochód!");
                return false;
            }
            return true;
        }

        private void cancelBtnclicked(object sender, RoutedEventArgs e)
        {
            userMenu.closeReservationWindow();
        }
    }
}
