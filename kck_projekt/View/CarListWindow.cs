using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class CarListWindow: Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action OnAdd { get; set; }
        public Action<Model.Car> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<Model.Car> OnReservation { get; set; }
        public Action<Model.Car> OnSelect { get; set; }
        private List<Model.Car> cars;
        private Model.User user;
        private Controller.AppController myController;
        public CarListWindow(Terminal.Gui.View parent, List<Model.Car> car_list, Model.User user, Controller.AppController controler) : base("Wypożyczalnia")
        {
            _parent = parent;
            this.myController = controler;
            this.cars = car_list;
            this.user = user;
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
            var carList = new ListView(source: cars)
            {
                Y = 1,
                X = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() -5,
            };
            Add(carList);

            var reservButton = new Button("Zarezerwuj wybrany samochód", true)
            {
                X = Pos.Center(),
                Y = Pos.Percent(100) - 3
            };
            

            var dateShowButton = new Button("Sprawdź terminy")
            {
                X = Pos.Left(reservButton) - 25,
                Y = Pos.Percent(100) - 3
            };
            

            var detailsButton = new Button("Dane Szczegółowe")
            {
                X = Pos.Right(reservButton) + 5,
                Y = Pos.Percent(100) - 3
            };
            


            var editButton = new Button("Edytuj")
            {
                X = Pos.Center(),
                Y = Pos.Percent(100) - 2
            };
            
            var addButton = new Button("Dodaj")
            {
                X = Pos.Left(editButton) - 15,
                Y = Pos.Percent(100) - 2
            };
            
            var deleteButton = new Button("Usuń")
            {
                X = Pos.Right(editButton) + 5,
                Y = Pos.Percent(100) - 2
            };

            if (user.Rola == Model.UserRole.User)
            {
                Add(reservButton);
                Add(dateShowButton);
                Add(detailsButton);
            }

            if (user.Rola == Model.UserRole.Staff || user.Rola == Model.UserRole.Admin)
            {
                Add(editButton);
                Add(addButton);
                Add(deleteButton);
            }


            var backButton = new Button("Cofnij")
            {
                X = Pos.Center() - 5,
                Y = Pos.Percent(100) - 1
            };
            Add(backButton);


            //carList.OpenSelectedItem += (a) =>
            //{
            //    carList.FocusNext();
            //    var tempCar = cars[carList.SelectedItem];
            //    MessageBox.Query(25, 11, "Szczegóły", $"Model: {tempCar.Model}\nKolor: {tempCar.CarColor}\nMoc: {tempCar.CarPower}\nSzkrzynia Biegów: {Controller.HelpMethods.GetEnumDescription(tempCar.Gearbox)}\nSilnik: {Controller.HelpMethods.GetEnumDescription(tempCar.Engine)}\nKosz wypożyczenia: {tempCar.CarDayPrince} PLN/Dzień\nKaucja: {tempCar.CarBail} PLN\nObecny Status: {Controller.HelpMethods.GetEnumDescription(tempCar.Status)}", "Ok");
            //};

            #region bind-button-events
            detailsButton.Clicked += () =>
            {
                var tempCar = cars[carList.SelectedItem];
                MessageBox.Query(25, 11, "Szczegóły", $"Model: {tempCar.Model}\nKolor: {tempCar.CarColor}\nMoc: {tempCar.CarPower}\nSzkrzynia Biegów: {Controller.HelpMethods.GetEnumDescription(tempCar.Gearbox)}\nSilnik: {Controller.HelpMethods.GetEnumDescription(tempCar.Engine)}\nKosz wypożyczenia: {tempCar.CarDayPrince} PLN/Dzień\nKaucja: {tempCar.CarBail} PLN\nObecny Status: {Controller.HelpMethods.GetEnumDescription(tempCar.Status)}", "Ok");
            };

            editButton.Clicked += () =>
            {
                OnEdit?.Invoke(cars[carList.SelectedItem]);
            };

            addButton.Clicked += () =>
            {
                OnAdd?.Invoke();
            };

            deleteButton.Clicked += () =>
            {
                
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz usunąć wybrany samochód?", "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(cars[carList.SelectedItem].CarId);
                }
                cars.Remove(cars[carList.SelectedItem]);
            };

            reservButton.Clicked += () =>
            {
                OnReservation?.Invoke(cars[carList.SelectedItem]);
            };

            backButton.Clicked += () =>
            {
                OnBack?.Invoke();
            };

            dateShowButton.Clicked += () =>
            {
                var dateWindow = new Window("Zajęte Terminy")
                {
                    Width = Dim.Percent(75),
                    Height = Dim.Percent(50),
                    Y = Pos.Center(),
                    X = Pos.Center(),
                };
                Add(dateWindow);
                var dateExitButton = new Button("Zamknij")
                {
                    X = Pos.Center(),
                    Y = Pos.Percent(100) - 2,
                };
                dateWindow.Add(dateExitButton);
                var dateList = new ListView()
                {
                    Width = Dim.Fill(),
                    Height = Dim.Fill() - 3,
                };
                dateWindow.Add(dateList);
                dateList.SetSource(myController.manageReservation.GetCarReservationDates(cars[carList.SelectedItem].CarId));
                dateExitButton.Clicked += () =>
                {
                    Remove(dateWindow);
                };
            };
            #endregion
        }

    }
}
