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
    /// Logika interakcji dla klasy CarModelManage.xaml
    /// </summary>
    public partial class CarModelManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;

        public CarModelManage(Controller.AppController MyController, StaffMenu staffMenu)
        {
            InitializeComponent();
            this.MyController = MyController;
            this.staffMenu = staffMenu;
        }

        private void saveBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            Model.CarModel tempModel = staffMenu.ObservableCarModel[carModelList.SelectedIndex];
            MyController.manageCars.EditModel(ModelName.Text, tempModel.ModelId, staffMenu.ObservableCarMark[MarkCombo.SelectedIndex].MarkId);
            staffMenu.updateCarModel();
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(carModelList.SelectedIndex > 0)
            {
                Model.CarModel tempModel = staffMenu.ObservableCarModel[carModelList.SelectedIndex];
                staffMenu.ObservableCarModel.Remove(tempModel);
                MyController.manageCars.DeleteModel(tempModel.ModelId);
            }
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if(!ValidateFields())
            {
                return;
            }
            MyController.manageCars.AddModel(ModelName.Text, staffMenu.ObservableCarMark[MarkCombo.SelectedIndex].MarkId);
            staffMenu.updateCarModel();
        }

        private bool ValidateFields()
        {
            if (String.IsNullOrEmpty(ModelName.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole nazwa nie może być puste!");
                return false;
            }
            if(MarkCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musisz wybrać markę!");
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            saveButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private void carModelSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carModelList.SelectedIndex == -1)
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
