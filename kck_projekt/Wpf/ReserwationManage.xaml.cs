using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy ReserwationManage.xaml
    /// </summary>
    public partial class ReserwationManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;
        public ReserwationManage(Controller.AppController MyController, StaffMenu staffMenu)
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

        private void reservationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(reservationList.SelectedIndex == -1)
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
            if (!ValidateFields())
            {
                return;
            }
            Model.Reservation tempReservation = staffMenu.ObservableReservations[reservationList.SelectedIndex];
            MyController.manageReservation.SaveReservation(tempReservation, tempReservation.ReservationId);
            staffMenu.updateReservationList();
            reservationList.SelectedIndex = -1;
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(reservationList.SelectedIndex > 0)
            {
                Model.Reservation tempReservation = staffMenu.ObservableReservations[reservationList.SelectedIndex];
                staffMenu.ObservableReservations.Remove(tempReservation);
                MyController.manageReservation.DeleteReservation(tempReservation.ReservationId);
            }
            
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            Model.Reservation tempReservation = new Model.Reservation()
            {
                DateFrom = (DateTime)dateFrom.Value,
                DateTo = (DateTime)dateTo.Value,
                Comments = Comments.Text.ToString(),
                Status = Model.ReservationStatus.Verification,
                Car = staffMenu.ObservableCar[CarCombo.SelectedIndex],
                User = staffMenu.ObservableUsers[UserCombo.SelectedIndex]
            };
            MyController.manageReservation.AddReservation(tempReservation);
            reservationList.SelectedIndex = -1;
            staffMenu.updateReservationList();
        }

        private bool ValidateFields()
        {
            if (dateFrom.Value == null)
            {
                staffMenu.windowManager.ShowMessage("Pole data od nie może być puste!");
                return false;
            }
            if (dateTo.Value == null)
            {
                staffMenu.windowManager.ShowMessage("Pole data do nie może być puste!");
                return false;
            }
            if (dateFrom.Value > dateTo.Value)
            {
                staffMenu.windowManager.ShowMessage("Data rozpoczęcia musi być mniejsza niż data zakończenia!");
                return false;
            }
            if (String.IsNullOrEmpty(Comments.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole uwagi nie może być puste!");
                return false;
            }
            if (StatusCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać status rezerwacji!");
                return false;
            }
            if (UserCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać użytkownika!");
                return false;
            }
            if (CarCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać samochód!");
                return false;
            }
            return true;
        }
    }
}
