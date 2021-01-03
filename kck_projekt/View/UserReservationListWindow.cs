using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class UserReservationListWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        private List<Model.Reservation> reservations;
        public UserReservationListWindow(Terminal.Gui.View parent, List<Model.Reservation> reservationList) : base("Lista moich rezerwacji")

        {
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

            var detailsButton = new Button("Szczegóły")
            {
                X = Pos.Center() -10,
                Y = Pos.Percent(100) - 3
            };
            Add(detailsButton);

            var backButton = new Button("Cofnij")
            {
                X = Pos.Center() + 10,
                Y = Pos.Percent(100) - 3
            };
            Add(backButton);

            #endregion

            #region List Click
            //reservationList.FocusFirst();
            //reservationList.OpenSelectedItem += (a) =>
            //{
            //    var tempReservation = reservations[reservationList.SelectedItem];
            //    MessageBox.Query(25, 12, "Szczegóły", $"Samochód: {tempReservation.Car}\nData od: {tempReservation.DateFrom}\nData do: {tempReservation.DateTo}\nUwagi: {tempReservation.Comments}\nKoszt: {tempReservation.Cost} PLN\nKaucja: {tempReservation.Car.CarBail} PLN\nStatus: {Controller.HelpMethods.GetEnumDescription(tempReservation.Status)}", "Ok");
            //};
            #endregion

            #region button operation
            detailsButton.Clicked += () =>
            {
                var tempReservation = reservations[reservationList.SelectedItem];
                MessageBox.Query(25, 12, "Szczegóły", $"Samochód: {tempReservation.Car}\nData od: {tempReservation.DateFrom}\nData do: {tempReservation.DateTo}\nUwagi: {tempReservation.Comments}\nKoszt: {tempReservation.Cost} PLN\nKaucja: {tempReservation.Car.CarBail} PLN\nStatus: {Controller.HelpMethods.GetEnumDescription(tempReservation.Status)}", "Ok");
            };

            backButton.Clicked += () =>
            {
                OnBack?.Invoke();
            };

            #endregion

        }

    }
}
