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
    /// Logika interakcji dla klasy CarMarkManage.xaml
    /// </summary>
    public partial class CarMarkManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;
        public CarMarkManage(Controller.AppController MyController, StaffMenu staffMenu)
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
            Model.CarMark tempMark = staffMenu.ObservableCarMark[carMarkList.SelectedIndex];
            MyController.manageCars.EditMark(tempMark.MarkName, tempMark.MarkId);
            staffMenu.updateCarMark();
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(carMarkList.SelectedIndex > 0)
            {
                Model.CarMark tempMark = staffMenu.ObservableCarMark[carMarkList.SelectedIndex];
                staffMenu.ObservableCarMark.Remove(tempMark);
                MyController.manageCars.DeleteMark(tempMark.MarkId);
            }

        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            MyController.manageCars.AddMark(MarkName.Text);
            staffMenu.updateCarMark();

        }

        private bool ValidateFields()
        {
            if (String.IsNullOrEmpty(MarkName.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole nazwa nie może być puste!");
                return false;
            }
            return true;
        }

        private void carMarkSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carMarkList.SelectedIndex == -1)
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
    }
}
