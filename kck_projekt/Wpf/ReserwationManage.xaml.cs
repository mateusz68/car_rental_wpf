using System;
using System.Collections.Generic;
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

        private void saveBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            Model.Reservation tempReservation = staffMenu.ObservableReservations[reservationList.SelectedIndex];
            reservationList.SelectedIndex = -1;
            MyController.manageReservation.SaveReservation(tempReservation, tempReservation.ReservationId);
            staffMenu.updateReservationList();
            reservationList.Items.Refresh();
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(reservationList.SelectedIndex > 0)
            {
                //Model.Reservation tempReservation = staffMenu.ObservableReservations[reservationList.SelectedIndex];
                int reservationId = staffMenu.ObservableReservations[reservationList.SelectedIndex].ReservationId;
                //reservationList.Items.RemoveAt(reservationList.SelectedIndex);
                Debug.WriteLine(reservationList.SelectedIndex);
                Debug.WriteLine(reservationList.SelectedItem);
                //Debug.WriteLine(tempReservation);
                //staffMenu.ObservableReservations.RemoveAt(reservationList.SelectedIndex);
                staffMenu.ObservableReservations.RemoveAt(reservationList.SelectedIndex);
                reservationList.ItemsSource = staffMenu.ObservableReservations;
                reservationList.Items.Refresh();
                reservationList.SelectedIndex = -1;
                //staffMenu.updateReservationList();
                MyController.manageReservation.DeleteReservation(reservationId);
                //staffMenu.updateReservationList();
                //reservationList.ItemsSource = staffMenu.ObservableReservations;
                //reservationList.Items.Refresh();
            }
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            Debug.WriteLine("Po ifie");
            Model.Reservation tempReservation = new Model.Reservation()
            {
                DateFrom = (DateTime)dateFrom.Value,
                DateTo = (DateTime)dateTo.Value,
                Comments = Comments.Text.ToString(),
                Status = Model.ReservationStatus.Verification,
                Car = staffMenu.ObservableCar[CarCombo.SelectedIndex],
                User = staffMenu.ObservableUsers[UserCombo.SelectedIndex]
            };
            Debug.WriteLine("Po tworzeniu obiektu");
            reservationList.SelectedIndex = -1;
            Debug.WriteLine("po zmianie indeksu");
            MyController.manageReservation.AddReservation(tempReservation);
            Debug.WriteLine("Po dodaniu");
            staffMenu.updateReservationList();
            Debug.WriteLine("Po aktualizacji");
            reservationList.Items.Refresh();
            reservationList.ItemsSource = staffMenu.ObservableReservations;
            Debug.WriteLine("Po refresh");
        }

        private void reservationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reservationList.SelectedIndex == -1)
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

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(reservationList.ItemsSource).Refresh();
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Model.Reservation).Comments.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
