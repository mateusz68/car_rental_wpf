using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class UserListWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<Model.User> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action OnAdd { get; set; }
        private List<Model.User> users;
        public UserListWindow(Terminal.Gui.View parent, List<Model.User> userList) : base("Zarządzaj użytkownikami")
        {
            this.users = userList;
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
            var userList = new ListView(source: users)
            {
                Y = 1,
                X = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 5,
            };
            Add(userList);

            var editButton = new Button("Edytuj")
            {
                X = Pos.Center() - 6,
                Y = Pos.Percent(100) - 3
            };
            Add(editButton);

            var addButton = new Button("Usuń")
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
            userList.FocusFirst();
            userList.OpenSelectedItem += (a) =>
            {
                var tempUser = users[userList.SelectedItem];
                MessageBox.Query(25, 11, "Szczegóły", $"Nazwa użytkownika: {tempUser.UserName}\nEmail: {tempUser.Email}\nNumer Telefonu: {tempUser.Phone}\nUlica: {tempUser.Adres1}\nMiejscowość: {tempUser.Adres2}\nKod pocztowy: {tempUser.Adres3}\nStatus: {Controller.HelpMethods.GetEnumDescription(tempUser.Rola)}", "Ok");
                return;
            };

            #region button operation
            addButton.Clicked += () =>
            {
                OnAdd?.Invoke();
            };

            editButton.Clicked += () =>
            {
                OnEdit?.Invoke(users[userList.SelectedItem]);
            };

            deleteButton.Clicked += () =>
            {
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz usunąć wybranego użytkownika?", "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(users[userList.SelectedItem].UserId);
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
