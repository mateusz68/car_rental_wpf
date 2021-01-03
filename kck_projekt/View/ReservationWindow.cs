using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class ReservationWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<Model.Reservation> OnSave { get; set; }
        private List<Model.Car> cars;
        private Model.User user;
        private Model.Car selectedCar;
        Controller.AppController controller;
        public ReservationWindow(Terminal.Gui.View parent, List<Model.Car> cars, Model.Car selectedCar, Model.User user, Controller.AppController controller) : base("Dodaj rezerwację")
        {
            this.controller = controller;
            this.cars = cars;
            this.user = user;
            this.selectedCar = selectedCar;
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
            #region Fields
            var userSecionLabel = new Label(1, 1, "Rezerwacja:");
            Add(userSecionLabel);
            var dateFromLabel = new Label("Data od:")
            {
                X = Pos.Left(userSecionLabel),
                Y = Pos.Top(userSecionLabel) + 1,
            };
            Add(dateFromLabel);

            var fromTime = new TimeField()
            {
                X = Pos.Left(dateFromLabel),
                Y = Pos.Top(dateFromLabel) + 1,
            };
            Add(fromTime);

            var fromDate = new DateField()
            {
                X = Pos.Right(fromTime) + 5,
                Y = Pos.Top(fromTime),
            };
            Add(fromDate);

            var dateToLabel = new Label("Data do:")
            {
                X = Pos.Left(fromTime),
                Y = Pos.Top(fromTime) + 1,
            };
            Add(dateToLabel);

            var toTime = new TimeField()
            {
                X = Pos.Left(dateToLabel),
                Y = Pos.Top(dateToLabel) + 1,
            };
            Add(toTime);

            var toDate = new DateField()
            {
                X = Pos.Right(toTime) + 5,
                Y = Pos.Top(toTime),
            };
            Add(toDate);

            var commentsLabel = new Label("Uwagi:")
            {
                X = Pos.Left(toTime),
                Y = Pos.Top(toTime) + 1,
            };
            Add(commentsLabel);
            var commentsText = new TextField("")
            {
                X = Pos.Left(commentsLabel),
                Y = Pos.Top(commentsLabel) + 1,
                Width = Dim.Fill()
            };
            Add(commentsText);

            var carLabel = new Label("Samochód:")
            {
                X = Pos.Left(commentsText),
                Y = Pos.Top(commentsText) + 1,
            };
            Add(carLabel);

            var carList = new ListView()
            {
                X = Pos.Left(carLabel),
                Y = Pos.Top(carLabel) + 1,
                Width = Dim.Fill(),
                Height = 5
            };
            carList.SetSource(cars);
            Add(carList);

            var datewLabel = new Label("Zajęte terminy:")
            {
                X = Pos.Left(carList),
                Y = Pos.Top(carList) + 6,
            };
            Add(datewLabel);

            var dateList = new ListView()
            {
                X = Pos.Left(datewLabel),
                Y = Pos.Top(datewLabel) + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 5,
            };
            Add(dateList);
            #endregion


            #region Button
            var saveButton = new Button("Rezerwuj", true)
            {
                X = Pos.Center() - 20,
                Y = Pos.Percent(100) - 2,

            };
            Add(saveButton);

            var backButton = new Button("Anuluj")
            {
                X = Pos.Center() + 5,
                Y = Pos.Top(saveButton)
            };
            Add(backButton);
            #endregion

            carList.SelectedItemChanged += (a) =>
            {
                dateList.SetSource(controller.manageReservation.GetCarReservationDates(cars[carList.SelectedItem].CarId));
            };

            #region Set Data
            if (selectedCar != null)
            {
                carList.SelectedItem = cars.FindIndex(c => c.CarId == selectedCar.CarId);
            }
            #endregion

            fromTime.Time = DateTime.Now.TimeOfDay;
            fromDate.Date = DateTime.Now;
            toTime.Time = DateTime.Now.TimeOfDay;
            toDate.Date = DateTime.Now.AddDays(3);

            #region bind-button-events
            saveButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(commentsText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole uwagi nie może być puste.", "Ok");
                    return;
                }
                var dateFrom = fromDate.Date.Date + fromTime.Time;
                var dateTo = toDate.Date.Date + toTime.Time;
                if (dateFrom > dateTo)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Data rozpoczęcia musi być mniejsza niż data zakończenia.", "Ok");
                    return;
                }
                if (dateFrom < DateTime.Now)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Data rozpoczęcia musi być większa od obecnej daty.", "Ok");
                    return;
                }
                Model.Reservation newReservationData = new Model.Reservation()
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    Comments = commentsText.Text.ToString(),
                    Status = (int)Model.ReservationStatus.Verification,
                    User = user,
                    Car = cars[carList.SelectedItem]
                };
                OnSave?.Invoke(newReservationData);
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
