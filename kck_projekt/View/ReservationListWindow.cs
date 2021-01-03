using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class ReservationListWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<Model.Reservation> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action OnAdd { get; set; }
        private List<Model.Reservation> reservations;
        List<Model.User> users;
        public ReservationListWindow(Terminal.Gui.View parent, List<Model.Reservation> reservationList, List<Model.User> users) : base("Zarządzaj rezerwacjami")
            
        {
            this.users = users;
            this.reservations = reservationList;
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
            #region Init Elements
            var reservationList = new ListView(source: reservations)
            {
                Y = 1,
                X = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 5,
            };
            Add(reservationList);

            var editButton = new Button("Edytuj")
            {
                X = Pos.Center() - 6,
                Y = Pos.Percent(100) - 3
            };
            Add(editButton);

            var addButton = new Button("Dodaj")
            {
                X = Pos.Center() - 20,
                Y = Pos.Percent(100) - 3
            };
            Add(addButton);


            var deleteButton = new Button("Usuń")
            {
                X = Pos.Center() + 8,
                Y = Pos.Percent(100) - 3
            };
            Add(deleteButton);

            var backButton = new Button("Cofnij")
            {
                X = Pos.Center() - 6,
                Y = Pos.Percent(100) - 1
            };
            Add(backButton);

            #endregion
            reservationList.FocusFirst();
            reservationList.OpenSelectedItem += (a) =>
            {
                var tempReservation = reservations[reservationList.SelectedItem];
                MessageBox.Query(25, 12, "Szczegóły", $"Użytkownik: {tempReservation.User}\nSamochód: {tempReservation.Car}\nData od: {tempReservation.DateFrom}\nData do: {tempReservation.DateTo}\nUwagi: {tempReservation.Comments}\nKoszt: {tempReservation.Cost} PLN\nKaucja: {tempReservation.Car.CarBail} PLN\nStatus: {Controller.HelpMethods.GetEnumDescription(tempReservation.Status)}", "Ok");
                return;
            };

            #region button operation
            addButton.Clicked += () =>
            {
                OnAdd?.Invoke();
            };

            editButton.Clicked += () =>
            {
                OnEdit?.Invoke(reservations[reservationList.SelectedItem]);
            };

            deleteButton.Clicked += () =>
            {
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz usunąć wybraną rezerwację?", "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(reservations[reservationList.SelectedItem].ReservationId);
                    reservations.Remove(reservations[reservationList.SelectedItem]);
                }
            };

            backButton.Clicked += () =>
            {
                OnBack?.Invoke();
            };

            #endregion

        }

    }
}
