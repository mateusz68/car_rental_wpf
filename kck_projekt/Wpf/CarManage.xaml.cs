using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
    /// Logika interakcji dla klasy CarManage.xaml
    /// </summary>
    public partial class CarManage : UserControl
    {
        public Controller.AppController MyController { get; set; }
        public StaffMenu staffMenu;
        public CarManage(Controller.AppController MyController, StaffMenu staffMenu)
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
            Model.Car tempCar = staffMenu.ObservableCar[carList.SelectedIndex];
            MyController.manageCars.SaveCar(tempCar, tempCar.CarId);
            staffMenu.updateCarList();
        }

        private void deleteBtnclicked(object sender, RoutedEventArgs e)
        {
            if(carList.SelectedIndex > 0)
            {
                Model.Car tempCar = staffMenu.ObservableCar[carList.SelectedIndex];
                staffMenu.ObservableCar.Remove(tempCar);
                MyController.manageCars.DeleteCar(tempCar.CarId);
            }
        }

        private void addBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            int carPower, carDayPrince, carBail;
            Int32.TryParse(CarPower.Text, out carPower);
            Int32.TryParse(CarDayPrince.Text, out carDayPrince);
            Int32.TryParse(CarBail.Text, out carBail);

            Model.Car tempCar = new Model.Car()
            {
                CarRegNumb = CarRegNumb.Text.ToString(),
                CarColor = Color.Text.ToString(),
                CarPower = carPower,
                CarDayPrince = carDayPrince,
                CarBail = carBail,
                Status = (Model.CarStatus)StatusCombo.SelectedIndex,
                Engine = (Model.CarEngineType)EngineCombo.SelectedIndex,
                Gearbox = (Model.CarGearBoxType)GearBoxCombo.SelectedIndex,
                Model = staffMenu.ObservableCarModel[ModelCombo.SelectedIndex]
            };

            MyController.manageCars.SaveCar(tempCar, -1);
            staffMenu.updateCarList();
        }

        private bool ValidateFields()
        {
            decimal tempDecimal;
            int tempInt;
            if (String.IsNullOrEmpty(CarRegNumb.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole numer rejestracyjny nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(Color.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole kolor nie może być puste!");
                return false;
            }
            if (String.IsNullOrEmpty(CarPower.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole moc nie może być puste!");
                return false;
            }
            if(!Int32.TryParse(CarPower.Text,out tempInt))
            {
                staffMenu.windowManager.ShowMessage("Niepoprawna wartość w polu moc!");
                return false;
            }
            if (String.IsNullOrEmpty(CarDayPrince.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole stawka dzienna nie może być puste!");
                return false;
            }
            if (!decimal.TryParse(CarDayPrince.Text, NumberStyles.Any, new CultureInfo("en-US"), out tempDecimal))
            {
                staffMenu.windowManager.ShowMessage("Niepoprawna wartość w polu stawka dzienna!");
                return false;
            }
            if (String.IsNullOrEmpty(CarBail.Text))
            {
                staffMenu.windowManager.ShowMessage("Pole kaucja nie może być puste!");
                return false;
            }
            if (!decimal.TryParse(CarBail.Text, NumberStyles.Any, new CultureInfo("en-US"), out tempDecimal))
            {
                staffMenu.windowManager.ShowMessage("Niepoprawna wartość w polu kaucja!");
                return false;
            }
            if(StatusCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać status samochodu!");
                return false;
            }
            if (EngineCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać typ silnika samochodu!");
                return false;
            }
            if (GearBoxCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać typ skrzyni biegów samochodu!");
                return false;
            }
            if (ModelCombo.SelectedIndex == -1)
            {
                staffMenu.windowManager.ShowMessage("Musiszy wybrać model samochodu!");
                return false;
            }
            return true;
        }

        private void carSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carList.SelectedIndex == -1)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            saveButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }
    }
}
