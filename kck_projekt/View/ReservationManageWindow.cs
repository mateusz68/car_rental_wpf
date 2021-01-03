using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class ReservationManageWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<(Model.Reservation reservationData, int reservationId)> OnSave { get; set; }
        private Model.Reservation reservation;
        private List<Model.User> users;
        private List<Model.Car> cars;
        Controller.AppController controller;
        public ReservationManageWindow(Terminal.Gui.View parent, Model.Reservation reservation, List<Model.User> users, List<Model.Car> cars, Controller.AppController controller) : base("Edycja Samochodu")
        {
            this.controller = controller;
            this.reservation = reservation;
            this.cars = cars;
            this.users = users;
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
            #region Manage User Fields
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

            var userLabel = new Label("Użytkownik:")
            {
                X = Pos.Left(commentsText),
                Y = Pos.Top(commentsText) + 1,
            };
            Add(userLabel);
            //var userCombo = new ComboBox()
            //{
            //    X = Pos.Left(userLabel),
            //    Y = Pos.Top(userLabel) + 1,
            //    Width = Dim.Fill()
            //};
            var userList = new ListView()
            {
                X = Pos.Left(userLabel),
                Y = Pos.Top(userLabel) + 1,
                Width = Dim.Fill(),
                Height = 3
            };
            userList.SetSource(users);
            Add(userList);
            //userCombo.SetSource(users);
            //Add(userCombo);
            var carLabel = new Label("Samochód:")
            {
                X = Pos.Left(userList),
                Y = Pos.Top(userList) + 4,
            };
            Add(carLabel);

            var carList = new ListView()
            {
                X = Pos.Left(carLabel),
                Y = Pos.Top(carLabel) + 1,
                Width = Dim.Fill(),
                Height = 3
            };
            carList.SetSource(cars);
            Add(carList);

            var statusLabel = new Label("Status:")
            {
                X = Pos.Left(carLabel),
                Y = Pos.Top(carLabel) + 4,
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
            statusList.SetSource(Controller.HelpMethods.GetDescriptions<Model.ReservationStatus>());
            Add(statusList);

            var datewLabel = new Label("Zajęte terminy:")
            {
                X = Pos.Left(statusList),
                Y = Pos.Top(statusList) + 1,
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

            carList.SelectedItemChanged += (a) =>
            {
                dateList.SetSource(controller.manageReservation.GetCarReservationDates(cars[carList.SelectedItem].CarId));
            };


            #region Button
            var saveButton = new Button("Zapisz", true)
            {
                X = Pos.Center(),
                Y = Pos.Percent(100) - 2,

            };
            Add(saveButton);

            var deleteButton = new Button("Usuń")
            {
                X = Pos.Left(saveButton) - 13,
                Y = Pos.Top(saveButton),
            };
            if (reservation != null)
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
            if (reservation != null)
            {
                fromTime.Time = reservation.DateFrom.TimeOfDay;
                fromDate.Date = reservation.DateFrom.Date;
                toTime.Time = reservation.DateTo.TimeOfDay;
                toDate.Date = reservation.DateTo.Date;
                if (!string.IsNullOrEmpty(reservation.Comments))
                    commentsText.Text = reservation.Comments;
                //userCombo.Text = reservation.User.ToString();
                userList.SelectedItem = users.FindIndex(c => c.UserId == reservation.User.UserId);
                carList.SelectedItem = cars.FindIndex(c => c.CarId == reservation.Car.CarId);
                statusList.SelectedItem = (int)reservation.Status;
            }else{
                fromTime.Time = DateTime.Now.TimeOfDay;
                fromDate.Date = DateTime.Now;
                toTime.Time = DateTime.Now.TimeOfDay;
                toDate.Date = DateTime.Now.AddDays(3);
            }
            #endregion

            #region bind-button-events
            saveButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(commentsText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole uwagi nie może być puste.", "Ok");
                    return;
                }

                var dateTo = toDate.Date.Date + toTime.Time;
                var dateFrom = fromDate.Date.Date + fromTime.Time;
                if (dateFrom > dateTo)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Data rozpoczęcia musi być mniejsza niż data zakończenia.", "Ok");
                    return;
                }

                Model.Reservation newReservationData = new Model.Reservation()
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    Comments = commentsText.Text.ToString(),
                    Status = (Model.ReservationStatus)statusList.SelectedItem,
                    //User = users[userCombo.SelectedItem],
                    User = users[userList.SelectedItem],
                    Car = cars[carList.SelectedItem]
                };
                int editReservationId;
                if (reservation != null)
                {
                    editReservationId = reservation.ReservationId;
                }
                else
                {
                    editReservationId = -1;
                }
                OnSave?.Invoke((reservationData: newReservationData, reservationId: editReservationId));
            };

            deleteButton.Clicked += () =>
            {
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz wybraną rezerwację", "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(reservation.ReservationId);
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
