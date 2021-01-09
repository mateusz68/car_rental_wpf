using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class StaffMenuWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnExit { get; set; }
        public Action OnLogout { get; set; }

        public Action<int> OnSelect { get; set; }
        private Model.User user;

        public StaffMenuWindow(Terminal.Gui.View parent, Model.User user) : base("Menu Główne")
        {
            this.user = user;
            _parent = parent;
            InitControls();
            InitStyle();
        }

        public void InitStyle()
        {
            X = Pos.Center();
            Y = Pos.Center();
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

            #region Staff Panel
            var staffTitle = new Label("___Panel Pracownika___")
            {
                X = Pos.Center() - 11,
                Y = Pos.Percent(10),
            };


            var reservationManageButton = new Button("Zarządzaj rezerwacjami [F1]")
            {
                X = Pos.Percent(33) - 11,
                Y = Pos.Percent(20),
            };


            var carManageButton = new Button("Zarządzaj samochodami [F2]")
            {
                X = Pos.Percent(66) - 11,
                Y = Pos.Percent(20),
            };


            var modelManageButton = new Button("Zarządzaj modelami [F3]")
            {
                X = Pos.Percent(33) - 9,
                Y = Pos.Percent(30),
            };


            var markManageButton = new Button("Zarządzaj markami [F4]")
            {
                X = Pos.Percent(66) - 9,
                Y = Pos.Percent(30),
            };

            var settingsButton = new Button("Ustawienia Aplikacji [F5]")
            {
                X = Pos.Center() - 10,
                Y = Pos.Percent(40),
            };

            Add(staffTitle);
            Add(reservationManageButton);
            Add(carManageButton);
            Add(modelManageButton);
            Add(markManageButton);
            Add(settingsButton);
            #endregion

            #region Admin Panel
            var adminTitle = new Label("___Panel Administratora___")
            {
                X = Pos.Center() - 13,
                Y = Pos.Percent(50),
            };


            var userManageButton = new Button("Zarządzaj użytkownikami")
            {
                X = Pos.Center() - 12,
                Y = Pos.Percent(60),
            };

            if (user.Rola == Model.UserRole.Admin)
            {
                Add(adminTitle);
                Add(userManageButton);
            }
            #endregion

            #region Logout and Exit Button
            var exitButton = new Button("Wyjdź [F6]")
            {
                X = Pos.Percent(50) - 15,
                Y = Pos.Percent(100) -2,
            };
            Add(exitButton);
            var logoutButton = new Button("Wyloguj [F7]")
            {
                X = Pos.Percent(50) + 5,
                Y = Pos.Percent(100) - 2,
            };
            Add(logoutButton);
            #endregion

            #region bind-button-events


            exitButton.Clicked += () =>
            {
                //Close();
                OnExit?.Invoke();
            };

            logoutButton.Clicked += () =>
            {
                //Close();
                OnLogout?.Invoke();
            };

            reservationManageButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(1);
            };

            carManageButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(2);
            };

            modelManageButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(3);
            };

            markManageButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(4);
            };

            userManageButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(5);
            };

            settingsButton.Clicked += () =>
            {
                OnSelect?.Invoke(6);
            };

            KeyDown += (a) =>
            {
                switch (a.KeyEvent.Key)
                {
                    case Key.F1:
                        OnSelect?.Invoke(1);
                        break;
                    case Key.F2:
                        OnSelect?.Invoke(2);
                        break;
                    case Key.F3:
                        OnSelect?.Invoke(3);
                        break;
                    case Key.F4:
                        OnSelect?.Invoke(4);
                        break;
                    case Key.F5:
                        OnSelect?.Invoke(6);
                        break;
                    case Key.F6:
                        OnExit?.Invoke();
                        break;
                    case Key.F7:
                        OnLogout?.Invoke();
                        break;
                    default:
                        break;
                }
            };

            #endregion
        }
    }
}
