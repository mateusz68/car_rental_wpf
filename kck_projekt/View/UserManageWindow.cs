using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class UserManageWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<(string name, int id)> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<(Model.User userData, int userId)> OnSave { get; set; }
        private Model.User user;
        public UserManageWindow(Terminal.Gui.View parent, Model.User user) : base("Edycja Samochodu")
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
            var userSecionLabel = new Label(1, 1, "Użytkownik:");
            Add(userSecionLabel);
            var userNameLabel = new Label("Login:")
            {
                X = Pos.Left(userSecionLabel),
                Y = Pos.Top(userSecionLabel) + 1,
            };
            Add(userNameLabel);
            var userNameText = new TextField("")
            {
                X = Pos.Left(userNameLabel),
                Y = Pos.Top(userNameLabel) + 1,
                Width = Dim.Fill()
            };
            Add(userNameText);

            var passwordLabel = new Label("Hasło:")
            {
                X = Pos.Left(userNameText),
                Y = Pos.Top(userNameText) + 1,
            };
            Add(passwordLabel);
            var passwordText = new TextField("")
            {
                X = Pos.Left(passwordLabel),
                Y = Pos.Top(passwordLabel) + 1,
                Width = Dim.Fill()
            };
            Add(passwordText);

            var emailLabel = new Label("Email:")
            {
                X = Pos.Left(passwordText),
                Y = Pos.Top(passwordText) + 1,
            };
            Add(emailLabel);
            var emailText = new TextField("")
            {
                X = Pos.Left(emailLabel),
                Y = Pos.Top(emailLabel) + 1,
                Width = Dim.Fill()
            };
            Add(emailText);

            var nameLabel = new Label("Imię:")
            {
                X = Pos.Left(emailText),
                Y = Pos.Top(emailText) + 1,
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

            var roleLabel = new Label("Rola:")
            {
                X = Pos.Left(adres3Text),
                Y = Pos.Top(adres3Text) + 1,
            };
            Add(roleLabel);
            var roleList = new ListView()
            {
                X = Pos.Left(roleLabel),
                Y = Pos.Top(roleLabel) + 1,
                Width = Dim.Fill(),
                Height = 1,
                ColorScheme = Colors.Dialog,
            };
            roleList.SetSource(Controller.HelpMethods.GetDescriptions<Model.UserRole>());
            Add(roleList);
            #endregion


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
            if (user != null)
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
            if (user != null)
            {
                userNameText.Text = user.UserName;
                emailText.Text = user.Email;
                phoneText.Text = user.Phone.ToString();
                if(!string.IsNullOrEmpty(user.Adres1))
                    adres1Text.Text = user.Adres1;
                if (!string.IsNullOrEmpty(user.Adres2))              
                    adres2Text.Text = user.Adres2;
                if (!string.IsNullOrEmpty(user.Adres3))
                    adres3Text.Text = user.Adres3;
                if (!string.IsNullOrEmpty(user.Name))
                    nameText.Text = user.Name;
                if (!string.IsNullOrEmpty(user.Surname))
                    surnameText.Text = user.Surname;
                roleList.SelectedItem = (int)user.Rola;
            }
            #endregion

            #region bind-button-events
            int phone;
            saveButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(userNameText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole login nie może być puste.", "Ok");
                    return;
                }
                if(user == null)
                {
                    if (string.IsNullOrEmpty(passwordText.Text.ToString()))
                    {
                        MessageBox.ErrorQuery(25, 8, "Błąd", "Pole hasło nie może być puste.", "Ok");
                        return;
                    }
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
                Model.User newUserData = new Model.User()
                {
                    UserName = userNameText.Text.ToString(),
                    Email = emailText.Text.ToString(),
                    Name = nameText.Text.ToString(),
                    Surname = surnameText.Text.ToString(),
                    Adres1 = adres1Text.Text.ToString(),
                    Adres2 = adres2Text.Text.ToString(),
                    Adres3 = adres3Text.Text.ToString(),
                    Phone = phone,
                };
                if (string.IsNullOrEmpty(passwordText.Text.ToString()))
                {
                    var salt = Controller.ManageUsers.GenerateSalt();
                    newUserData.UserPassword = Controller.ManageUsers.GenerateHash(passwordText.Text.ToString(), salt);
                    newUserData.Salt = salt;
                }
                else
                {
                    newUserData.UserPassword = null;
                }
                int editUserId;
                if (user != null)
                {
                    editUserId = user.UserId;
                }
                else
                {
                    editUserId = -1;
                }
                OnSave?.Invoke((userData: newUserData, userId: editUserId));
            };

            deleteButton.Clicked += () =>
            {
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz wybranego użytkownika " + user, "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(user.UserId);
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
