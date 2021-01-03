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
            CarCombo.ItemsSource = staffMenu.CarsList;
            UserCombo.ItemsSource = staffMenu.UserList;
            //reservationList.ItemsSource = staffMenu.ReservationsList;
            reservationList.ItemsSource = staffMenu.observableReservation;
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
            Model.Reservation tempReservation = staffMenu.ReservationsList[reservationList.SelectedIndex];
            MyController.manageReservation.SaveReservation(tempReservation, tempReservation.ReservationId);
            staffMenu.updateReservationList();
            reservationList.SelectedIndex = -1;
            reservationList.ItemsSource = staffMenu.ReservationsList;
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            Model.Reservation tempReservation = staffMenu.ReservationsList[reservationList.SelectedIndex];
            reservationList.SelectedIndex = -1;
            staffMenu.observableReservation.Remove(staffMenu.ReservationsList[reservationList.SelectedIndex]);
            //staffMenu.ReservationsList.Remove(staffMenu.ReservationsList[reservationList.SelectedIndex]);
            //reservationList.ItemsSource = staffMenu.ReservationsList;
            MyController.manageReservation.DeleteReservation(tempReservation.ReservationId);
            //staffMenu.updateReservationList();
            //reservationList.ItemsSource = staffMenu.ReservationsList;
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            Model.Reservation tempReservation = new Model.Reservation()
            {
                DateFrom = (DateTime)dateFrom.Value,
                DateTo = (DateTime)dateTo.Value,
                Comments = Comments.Text.ToString(),
                Status = Model.ReservationStatus.Verification,
                Car = staffMenu.CarsList[CarCombo.SelectedIndex],
                User = staffMenu.UserList[UserCombo.SelectedIndex]
            };
            MyController.manageReservation.AddReservation(tempReservation);
            reservationList.SelectedIndex = -1;
            staffMenu.updateReservationList();
            reservationList.ItemsSource = staffMenu.ReservationsList;
        }
    }
}
