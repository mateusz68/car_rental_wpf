using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class MainMenuWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnExit { get; set; }
        public Action OnLogout { get; set; }

        public Action<int> OnSelect { get; set; }
        private Model.User user;

        public MainMenuWindow(Terminal.Gui.View parent, Model.User user) : base("Menu Główne")
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
            #region User Panel
            var showCarsButton = new Button("Przeglądaj samochody [F1] ")
            {
                X = Pos.Percent(33) - 12,
                Y = Pos.Percent(10),

            };

            var rentCarButton = new Button("Wypożycz samochód [F2] ")
            {
                X = Pos.Percent(66) - 11,
                Y = Pos.Percent(10),
            };

            var historyButton = new Button("Historia wypożyczeń [F3] ")
            {
                X = Pos.Percent(33) - 12,
                Y = Pos.Percent(20),
            };

            var accountButton = new Button("Moje Konto [F4] ")
            {
                X = Pos.Percent(66) - 7,
                Y = Pos.Percent(20),
            };

            var setingsButton = new Button("Ustawienia [F5] ")
            {
                X = Pos.Percent(33) - 7,
                Y = Pos.Percent(30),
            };

            var exitButton = new Button("Wyjdź [F6] ")
            {
                X = Pos.Percent(66) - 5,
                Y = Pos.Percent(30),
            };

            Add(showCarsButton);
            Add(rentCarButton);
            Add(historyButton);
            Add(accountButton);
            Add(setingsButton);
            Add(exitButton);

            StringBuilder carImage = new StringBuilder();
            carImage.AppendLine(@"   yNNNNNNNNNNNNNNNNNNNNNNy   ");
            carImage.AppendLine(@"  .MM                    MM.  ");
            carImage.AppendLine(@"  :Md                    dM:  ");
            carImage.AppendLine(@"  oMy                    yMo  ");
            carImage.AppendLine(@"  yMdssssssssssssssssssssdMy  ");
            carImage.AppendLine(@"omMNdmMMMMMMMMMMMMMMMMMMmdNMmo");
            carImage.AppendLine(@"MMy   /MMMMMMMMMMMMMMMM/   yMM");
            carImage.AppendLine(@"MMh. `sMMMMMMMMMMMMMMMMs` .hMM");
            carImage.AppendLine(@"MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
            carImage.AppendLine(@"MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
            carImage.AppendLine(@"+mMMMMMMMMMMMMMMMMMMMMMMMMMMm+");
            carImage.AppendLine(@"  sMMMm                mMMMs  ");
            carImage.AppendLine(@"  sMMMm                mMMMs  ");
            carImage.AppendLine(@"  :mMN+                +mNd-  ");
            carImage.AppendLine("");

            var imageLabel = new Label(carImage.ToString())
            {
                X = Pos.Percent(38),
                Y = Pos.Percent(40),
            };

            Add(imageLabel);

            #endregion

            #region Logout Button
            var logoutButton = new Button("Wyloguj [F7] ")
            {       
                X = Pos.Center() - 7,
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

            showCarsButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(1);
            };

            rentCarButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(2);
            };

            historyButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(3);
            };

            accountButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(4);
            };

            setingsButton.Clicked += () =>
            {
                //Close();
                OnSelect?.Invoke(5);
            };

            #endregion

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
                        OnSelect?.Invoke(5);
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
        }
    }
}
