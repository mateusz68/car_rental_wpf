using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class Login : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnExit { get; set; }
        public Action OnRegister { get; set; }
        public Action<(string name, string password, bool remember)> OnLogin { get; set; }

        public Login(Terminal.Gui.View parent) : base("Wypożyczalnia")
        {
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
            #region loginWindow
            var loginWindow = new Window("Logowanie", 5)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
                Width = Dim.Percent(50),
                Height = 18,
            };
            #endregion
            #region nickname
            var nameLabel = new Label(0, 0, "Login");
            var nameText = new TextField("")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Top(nameLabel) + 1,
                Width = Dim.Fill()
            };
            loginWindow.Add(nameLabel);
            loginWindow.Add(nameText);
            #endregion

            #region password
            var passwordLabel = new Label("Hasło")
            {
                X = Pos.Left(nameText),
                Y = Pos.Top(nameText) + 1
            };
            var passwordText = new TextField("")
            {
                X = Pos.Left(passwordLabel),
                Y = Pos.Top(passwordLabel) + 1,
                Width = Dim.Fill(),
                Secret = true,
            };
            loginWindow.Add(passwordLabel);
            loginWindow.Add(passwordText);
            #endregion

            #region remember
            var rememberCheckBox = new CheckBox("Zapamiętaj")
            {
                X = Pos.Left(passwordText),
                Y = Pos.Top(passwordText) + 1
            };
            loginWindow.Add(rememberCheckBox);
            #endregion


            #region buttons
            var loginButton = new Button("Zaloguj", true)
            {
                X = Pos.Left(rememberCheckBox),
                Y = Pos.Top(rememberCheckBox) + 1
            };

            var exitButton = new Button("Wyjdź")
            {
                X = Pos.Right(loginButton) + 5,
                Y = Pos.Top(loginButton)
            };

            var registerButton = new Button("Rejestracja")
            {
                X = Pos.Right(exitButton) + 5,
                Y = Pos.Top(exitButton)
            };

            loginWindow.Add(loginButton);
            loginWindow.Add(exitButton);
            loginWindow.Add(registerButton);
            #endregion

            Add(loginWindow);

            #region bind-button-events
            loginButton.Clicked += () =>
            {
                if (nameText.Text.ToString().TrimStart().Length == 0)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Login nie może być pusty.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(passwordText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Hasło nie może być puste.", "Ok");
                    return;
                }

                bool remember = false;
                if (rememberCheckBox.Checked)
                {
                    remember = true;
                }

                OnLogin?.Invoke((name: nameText.Text.ToString(), password: passwordText.Text.ToString() , remember));
            };

            exitButton.Clicked += () =>
            {
                OnExit?.Invoke();
                //Close();
            };

            registerButton.Clicked += () =>
            {
                OnRegister?.Invoke();
                //Close();
            };
            #endregion
        }

    }
}
