using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class CarManageWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<(string name, int id)> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<(Model.Car carData, int carId)> OnSave { get; set; }
        private Model.Car car;
        private List<Model.CarModel> models;
        public CarManageWindow(Terminal.Gui.View parent, Model.Car car, List<Model.CarModel> models) : base("Edycja Samochodu")
        {
            this.car = car;
            this.models = models;
            _parent = parent;
            InitControls();
            InitStyle();
        }

        public void InitStyle()
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();
        }
        public void Close()
        {
            Application.RequestStop();
            _parent?.Remove(this);
        }

        private void InitControls()
        {
            #region Manage Car
            var carSecionLabel = new Label(2, 1, "Samochód:");
            Add(carSecionLabel);
            var regnumbLabel = new Label("Numer Rejestracyjny:")
            {
                X = Pos.Left(carSecionLabel),
                Y = Pos.Top(carSecionLabel) + 1,
            };
            Add(regnumbLabel);
            var regnumbText = new TextField("")
            {
                X = Pos.Left(regnumbLabel),
                Y = Pos.Top(regnumbLabel) + 1,
                Width = Dim.Fill()
            };
            Add(regnumbText);

            var colorLabel = new Label("Kolor:")
            {
                X = Pos.Left(regnumbText),
                Y = Pos.Top(regnumbText) + 1,
            };
            Add(colorLabel);
            var colorText = new TextField("")
            {
                X = Pos.Left(colorLabel),
                Y = Pos.Top(colorLabel) + 1,
                Width = Dim.Fill()
            };
            Add(colorText);

            var powerLabel = new Label("Moc:")
            {
                X = Pos.Left(colorText),
                Y = Pos.Top(colorText) + 1,
            };
            Add(powerLabel);
            var powerText = new TextField("")
            {
                X = Pos.Left(powerLabel),
                Y = Pos.Top(powerLabel) + 1,
                Width = Dim.Fill()
            };
            Add(powerText);

            var dayCostLabel = new Label("Stawka wynajmu za dzień:")
            {
                X = Pos.Left(powerText),
                Y = Pos.Top(powerText) + 1,
            };
            Add(dayCostLabel);
            var dayCostText = new TextField("")
            {
                X = Pos.Left(dayCostLabel),
                Y = Pos.Top(dayCostLabel) + 1,
                Width = Dim.Fill()
            };
            Add(dayCostText);

            var bailLabel = new Label("Kaucja:")
            {
                X = Pos.Left(dayCostText),
                Y = Pos.Top(dayCostText) + 1,
            };
            Add(bailLabel);
            var bailText = new TextField("")
            {
                X = Pos.Left(bailLabel),
                Y = Pos.Top(bailLabel) + 1,
                Width = Dim.Fill()
            };
            Add(bailText);

            var statusLabel = new Label("Status Samochodu:")
            {
                X = Pos.Left(bailText),
                Y = Pos.Top(bailText) + 1,
            };
            Add(statusLabel);
            var statusList = new ListView()
            {
                X = Pos.Left(statusLabel),
                Y = Pos.Top(statusLabel) + 1,
                Width = Dim.Fill(),
                Height = 1,
                ColorScheme = Colors.Dialog,
            };
            statusList.SetSource(Controller.HelpMethods.GetDescriptions<Model.CarStatus>());
            Add(statusList);

            var engineLabel = new Label("Typ silnika:")
            {
                X = Pos.Left(statusList),
                Y = Pos.Top(statusList) + 1,
            };
            Add(engineLabel);
            var engineList = new ListView()
            {
                X = Pos.Left(engineLabel),
                Y = Pos.Top(engineLabel) + 1,
                Width = Dim.Fill(),
                Height = 1,
                ColorScheme = Colors.Dialog,
            };
            engineList.SetSource(Controller.HelpMethods.GetDescriptions<Model.CarEngineType>());
            Add(engineList);

            var gearboxLabel = new Label("Typ skrzyni biegów:")
            {
                X = Pos.Left(engineList),
                Y = Pos.Top(engineList) + 1,
            };
            Add(gearboxLabel);
            var gearboxList = new ListView()
            {
                X = Pos.Left(gearboxLabel),
                Y = Pos.Top(gearboxLabel) + 1,
                Width = Dim.Fill(),
                Height = 1,
                ColorScheme = Colors.Dialog,
            };
            gearboxList.SetSource(Controller.HelpMethods.GetDescriptions<Model.CarGearBoxType>());
            Add(gearboxList);

            var modelLabel = new Label("Model samochodu:")
            {
                X = Pos.Left(gearboxList),
                Y = Pos.Top(gearboxList) + 1,
            };
            Add(modelLabel);
            //var modelCombo = new ComboBox()
            //{
            //    X = Pos.Left(modelLabel),
            //    Y = Pos.Top(modelLabel) + 1,
            //    Width = Dim.Fill(),
            //    Height = 3
            //};
            //modelCombo.SetSource(models);
            //Add(modelCombo);

            var modelList = new ListView()
            {
                X = Pos.Left(modelLabel),
                Y = Pos.Top(modelLabel) + 1,
                Width = Dim.Fill(),
                Height = 4,
                ColorScheme = Colors.Dialog,
            };
            modelList.SetSource(models);
            Add(modelList);

            #endregion
            var saveButton = new Button("Zapisz", true)
            {
                X = Pos.Center(),
                Y = Pos.Percent(100) - 2,
                
            };
            Add(saveButton);
            #region Button
            var deleteButton = new Button("Usuń")
            {
                X = Pos.Left(saveButton) - 13,
                Y = Pos.Top(saveButton),
            };
            if(car != null)
            {
                Add(deleteButton);
            }


            var backButton = new Button("Cofnij")
            {
                X = Pos.Right(saveButton) + 5,
                Y = Pos.Top(saveButton)
            };
            Add(backButton);
            #endregion

            #region Set Data
            if(car != null)
            {
                regnumbText.Text = car.CarRegNumb;
                colorText.Text = car.CarColor;
                powerText.Text = car.CarPower.ToString();
                dayCostText.Text = car.CarDayPrince.ToString();
                bailText.Text = car.CarBail.ToString();
                engineList.SelectedItem = (int)car.Engine;
                gearboxList.SelectedItem = (int)car.Gearbox;
                statusList.SelectedItem = (int)car.Status;
                //modelCombo.Text = car.Model.ToString();
                modelList.SelectedItem = models.FindIndex(model => model.ModelName == car.Model.ModelName);
            }
            #endregion

            #region bind-button-events
            int power;
            decimal dayCost, bail;
            saveButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(regnumbText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole numer rejsetracyjny nie może być puste.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(colorText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole kolor nie może być puste.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(powerText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole moc nie może być puste.", "Ok");
                    return;
                }
                if (!int.TryParse(powerText.Text.ToString(),out power))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wprowadź poprawną liczbę całkowitą w polu moc.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(dayCostText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole stawka wyjnajmu nie może być puste.", "Ok");
                    return;
                }
                if (!decimal.TryParse(dayCostText.Text.ToString(), out dayCost))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wprowadź poprawną liczbę w polu stawka wynajmu.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(bailText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole kaucja nie może być puste.", "Ok");
                    return;
                }
                if (!decimal.TryParse(bailText.Text.ToString(), out bail))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wprowadź poprawną liczbę w polu kaucja.", "Ok");
                    return;
                }
                Model.Car newDataCar = new Model.Car()
                {
                    CarRegNumb = regnumbText.Text.ToString(),
                    CarColor = colorText.Text.ToString(),
                    CarPower = power,
                    CarDayPrince = dayCost,
                    CarBail = bail,
                    //Model = models[modelCombo.SelectedItem],
                    Model = models[modelList.SelectedItem],
                    Status = (Model.CarStatus)statusList.SelectedItem,
                    Gearbox = (Model.CarGearBoxType)gearboxList.SelectedItem,
                    Engine = (Model.CarEngineType)engineList.SelectedItem
                };
                int editCarId;
                if(car != null)
                {
                    editCarId = car.CarId;
                }
                else
                {
                    editCarId = -1;
                }
                OnSave?.Invoke((carData: newDataCar, carId: editCarId));
            };

            deleteButton.Clicked += () =>
            {
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz wybrany samochód " + car, "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(car.CarId);
                }
            };

            backButton.Clicked += () =>
            {
                OnBack?.Invoke();
                Close();
            };
            #endregion
        }
    }
}
