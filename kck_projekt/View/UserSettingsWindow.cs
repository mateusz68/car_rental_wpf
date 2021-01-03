using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class UserSettingsWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<(string oldPassword, string newPassword, int userId)> OnPasswordChange { get; set; }
        public Action<(string name, string surname, int phone, string adres1, string adres2, string adres3, int userId)> OnSettingsChange { get; set; }
        private Model.User user;
        public UserSettingsWindow(Terminal.Gui.View parent, Model.User user) : base("Ustawienia użytkownika")
        {
            this.user = user;
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
            var userSecionLabel = new Label("Zmień Hasło")
            {
                X = Pos.Center(),
                Y = 1,
            };
            Add(userSecionLabel);
            var oldPasswordLabel = new Label("Stare Hasło:")
            {
                X = 1,
                Y = Pos.Top(userSecionLabel) + 1,
            };
            Add(oldPasswordLabel);
            var oldPasswordText = new TextField("")
            {
                X = Pos.Left(oldPasswordLabel),
                Y = Pos.Top(oldPasswordLabel) + 1,
                Width = Dim.Fill()
            };
            Add(oldPasswordText);

            var newPasswordLabel = new Label("Nowe Hasło:")
            {
                X = Pos.Left(oldPasswordText),
                Y = Pos.Top(oldPasswordText) + 1,
            };
            Add(newPasswordLabel);
            var newPasswordText = new TextField("")
            {
                X = Pos.Left(newPasswordLabel),
                Y = Pos.Top(newPasswordLabel) + 1,
                Width = Dim.Fill()
            };
            Add(newPasswordText);

            var newPasswordRepeatLabel = new Label("Powtórz Hasło:")
            {
                X = Pos.Left(newPasswordText),
                Y = Pos.Top(newPasswordText) + 1,
            };
            Add(newPasswordRepeatLabel);
            var newPasswordRepeatText = new TextField("")
            {
                X = Pos.Left(newPasswordRepeatLabel),
                Y = Pos.Top(newPasswordRepeatLabel) + 1,
                Width = Dim.Fill()
            };
            Add(newPasswordRepeatText);

            var passwordChangeButton = new Button("Zmień Hasło")
            {
                X = Pos.Left(newPasswordRepeatText),
                Y = Pos.Top(newPasswordRepeatText) + 1,
            };
            Add(passwordChangeButton);

            var settingsLabel = new Label("Dane Użytkownika")
            {
                X = Pos.Center(),
                Y = Pos.Top(passwordChangeButton) + 2,
            };
            Add(settingsLabel);

            var nameLabel = new Label("Imię:")
            {
                X = Pos.Left(passwordChangeButton),
                Y = Pos.Top(passwordChangeButton) + 3,
            };
            Add(nameLabel);
            var nameText = new TextField("")
            {
                X = Pos.Left(nameLabel),
                Y = Pos.Top(nameLabel) + 1,
                Width = Dim.Fill()
            };
            Add(nameText);

            var surnameLabel = new Label("Nazwisko:")
            {
                X = Pos.Left(nameText),
                Y = Pos.Top(nameText) + 1,
            };
            Add(surnameLabel);
            var surnameText = new TextField("")
            {
                X = Pos.Left(surnameLabel),
                Y = Pos.Top(surnameLabel) + 1,
                Width = Dim.Fill()
            };
            Add(surnameText);

            var phoneLabel = new Label("Numer Telefonu:")
            {
                X = Pos.Left(surnameText),
                Y = Pos.Top(surnameText) + 1,
            };
            Add(phoneLabel);
            var phoneText = new TextField("")
            {
                X = Pos.Left(phoneLabel),
                Y = Pos.Top(phoneLabel) + 1,
                Width = Dim.Fill()
            };
            Add(phoneText);

            var adres1Label = new Label("Ulica:")
            {
                X = Pos.Left(phoneText),
                Y = Pos.Top(phoneText) + 1,
            };
            Add(adres1Label);

            var adres1Text = new TextField("")
            {
                X = Pos.Left(adres1Label),
                Y = Pos.Top(adres1Label) + 1,
                Width = Dim.Fill()
            };
            Add(adres1Text);

            var adres2Label = new Label("Miejscowość:")
            {
                X = Pos.Left(adres1Text),
                Y = Pos.Top(adres1Text) + 1,
            };
            Add(adres2Label);

            var adres2Text = new TextField("")
            {
                X = Pos.Left(adres2Label),
                Y = Pos.Top(adres2Label) + 1,
                Width = Dim.Fill()
            };
            Add(adres2Text);


            var adres3Label = new Label("Kod pocztowy:")
            {
                X = Pos.Left(adres2Text),
                Y = Pos.Top(adres2Text) + 1,
            };
            Add(adres3Label);

            var adres3Text = new TextField("")
            {
                X = Pos.Left(adres3Label),
                Y = Pos.Top(adres3Label) + 1,
                Width = Dim.Fill()
            };
            Add(adres3Text);

            var adresChangeButton = new Button("Zmień Adres")
            {
                X = Pos.Left(adres3Text),
                Y = Pos.Top(adres3Text) + 1,
            };
            Add(adresChangeButton);
            #endregion


            #region Button
            var backButton = new Button("Cofnij")
            {
                X = Pos.Center(),
                Y = Pos.Percent(100) - 3
            };
            Add(backButton);
            #endregion

            #region Set Data
            if (user != null)
            {
                phoneText.Text = user.Phone.ToString();
                if (!string.IsNullOrEmpty(user.Adres1))
                    adres1Text.Text = user.Adres1;
                if (!string.IsNullOrEmpty(user.Adres2))
                    adres2Text.Text = user.Adres2;
                if (!string.IsNullOrEmpty(user.Adres3))
                    adres3Text.Text = user.Adres3;
                if (!string.IsNullOrEmpty(user.Name))
                    nameText.Text = user.Name;
                if (!string.IsNullOrEmpty(user.Surname))
                    surnameText.Text = user.Surname;
            }
            #endregion

            #region bind-button-events
            int phone;
            adresChangeButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(nameText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole imię nie może być puste.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(surnameText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole nazwisko nie może być puste.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(adres1Text.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole ulica nie może być puste.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(adres2Text.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole miejscowość nie może być puste.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(adres3Text.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole kod pocztowy nie może być puste.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(phoneText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole numer telefonu nie może być puste.", "Ok");
                    return;
                }
                if (!int.TryParse(phoneText.Text.ToString(), out phone) || phoneText.Text.ToString().Length != 9)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wprowadź poprawny numer w polu numer telefonu.", "Ok");
                    return;
                }
                OnSettingsChange?.Invoke((name: nameText.Text.ToString(), surname: surnameText.Text.ToString(), phone: phone, adres1: adres1Text.Text.ToString(), adres2: adres2Text.Text.ToString(), adres3: adres3Text.Text.ToString(), userId: user.UserId));
            };

            passwordChangeButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(oldPasswordText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole stare hasło nie może być puste.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(newPasswordText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole nowe hasło nie może być puste.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(newPasswordRepeatText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole powtórz nowe hasło nie może być puste.", "Ok");
                    return;
                }

                if(newPasswordText.Text.ToString() == newPasswordRepeatText.Text.ToString())
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wprowadzone nowe hasła nie są takie same.", "Ok");
                    return;
                }
                OnPasswordChange?.Invoke((oldPassword: oldPasswordText.Text.ToString(), newPassword: newPasswordText.Text.ToString(), userId: user.UserId));
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
