using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class RegistrationWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnExit { get; set; }
        public Action OnBack { get; set; }
        public Action<(string name, string password, string email)> OnRegister { get; set; }

        public RegistrationWindow(Terminal.Gui.View parent) : base("Registration", 5)
        {
            _parent = parent;
            InitControls();
            InitStyle();
        }

        public void InitStyle()
        {
            X = Pos.Center();
            Width = Dim.Percent(50);
            Height = 21;
        }

        public void Close()
        {
            Application.RequestStop();
            _parent?.Remove(this);
        }

        private void InitControls()
        {
            #region nickname
            var nameLabel = new Label(0, 0, "Login");
            var nameText = new TextField("")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Top(nameLabel) + 1,
                Width = Dim.Fill()
            };
            Add(nameLabel);
            Add(nameText);
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
                Secret = true
            };
            Add(passwordLabel);
            Add(passwordText);
            #endregion

            #region password2
            var passwordRepeatLabel = new Label("Powtórz Hasło")
            {
                X = Pos.Left(passwordText),
                Y = Pos.Top(passwordText) + 1
            };
            var passwordRepeatText = new TextField("")
            {
                X = Pos.Left(passwordRepeatLabel),
                Y = Pos.Top(passwordRepeatLabel) + 1,
                Width = Dim.Fill(),
                Secret = true
            };
            Add(passwordRepeatLabel);
            Add(passwordRepeatText);
            #endregion

            #region email
            var emailLabel = new Label("Email")
            {
                X = Pos.Left(passwordRepeatText),
                Y = Pos.Top(passwordRepeatText) + 1
            };
            var emailText = new TextField("")
            {
                X = Pos.Left(emailLabel),
                Y = Pos.Top(emailLabel) + 1,
                Width = Dim.Fill()
            };
            Add(emailLabel);
            Add(emailText);
            #endregion

            #region buttons
            var loginButton = new Button("Rejestruj", true)
            {
                X = Pos.Left(emailText),
                Y = Pos.Top(emailText) + 1
            };

            var exitButton = new Button("Wyjdź")
            {
                X = Pos.Right(loginButton) + 5,
                Y = Pos.Top(loginButton)
            };

            var backButton = new Button("Cofnij")
            {
                X = Pos.Right(exitButton) + 5,
                Y = Pos.Top(exitButton)
            };

            Add(loginButton);
            Add(exitButton);
            Add(backButton);
            #endregion

            #region bind-button-events
            loginButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(nameText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Login nie może być pusty.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(passwordText.Text.ToString()) || string.IsNullOrEmpty(passwordRepeatText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Hasło nie może być pusty.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(emailText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Email nie może być pusty.", "Ok");
                    return;
                }

                if (passwordText.Text.ToString() != passwordRepeatText.Text.ToString())
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Hasła nie pasują do siebie.", "Ok");
                    return;
                }

                OnRegister?.Invoke((name: nameText.Text.ToString(), password: passwordText.Text.ToString(), email: emailText.Text.ToString()));

                //Close();
            };

            exitButton.Clicked += () =>
            {
                OnExit?.Invoke();
                Close();
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
