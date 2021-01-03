using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class MarkManageWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        private List<Model.CarMark> marks;
        public List<Model.CarMark> MarkManager
        {
            set
            {
                marks = value;
                UpdateComboBox();
            }
        }
        public Action OnBack { get; set; }
        public Action<(string name, int id)> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<string> OnAdd { get; set; }
        private ComboBox markEditCombo;
        private ComboBox markDeleteCombo;
        public MarkManageWindow(Terminal.Gui.View parent, List<Model.CarMark> marks) : base("Dodaj Markę")
        {
            _parent = parent;
            this.marks = marks;
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

        public void UpdateComboBox()
        {
            markEditCombo.SetSource(marks);
            markDeleteCombo.SetSource(marks);
        }

        private void InitControls()
        {

            #region Add Mark
            var markSecionLabel = new Label(2, 1, "Dodaj nową markę:");
            var markLabel = new Label("Nazwa Marki:")
            {
                X = Pos.Left(markSecionLabel),
                Y = Pos.Top(markSecionLabel) + 1,
            };
            var markText = new TextField("")
            {
                X = Pos.Left(markLabel),
                Y = Pos.Top(markLabel) + 1,
                Width = Dim.Fill()
            };

            var addButton = new Button("Dodaj", true)
            {
                X = Pos.Left(markText),
                Y = Pos.Top(markText) + 1
            };
            Add(markSecionLabel);
            Add(markLabel);
            Add(markText);
            markText.FocusFirst();
            Add(addButton);
            #endregion

            #region Edit Mark
            var editMarkSecionLabel = new Label("Edytuj markę:")
            {
                X = Pos.Left(addButton),
                Y = Pos.Top(addButton) + 3,
            };

            var markEditSelectLabel = new Label("Wybierz markę którą chcesz edytować:")
            {
                X = Pos.Left(editMarkSecionLabel),
                Y = Pos.Top(editMarkSecionLabel) + 1,
            };

            markEditCombo = new ComboBox()
            {
                X = Pos.Left(markEditSelectLabel),
                Y = Pos.Top(markEditSelectLabel) + 1,
                Width = Dim.Fill(),
                ColorScheme = Colors.TopLevel,
            };
            markEditCombo.SetSource(marks);

            var markEditNewValueLabel = new Label("Wprowadź nową wartość:")
            {
                X = Pos.Left(markEditCombo),
                Y = Pos.Top(markEditCombo) + 1,
            };

            var markEditNewValueText = new TextField()
            {
                X = Pos.Left(markEditNewValueLabel),
                Y = Pos.Top(markEditNewValueLabel) + 1,
                Width = Dim.Fill(),
            };

            var editButton = new Button("Edytuj")
            {
                X = Pos.Left(markEditNewValueText),
                Y = Pos.Top(markEditNewValueText) + 1
            };
            Add(editMarkSecionLabel);
            Add(markEditSelectLabel);
            Add(markEditCombo);
            Add(markEditNewValueLabel);
            Add(markEditNewValueText);
            Add(editButton);
            #endregion

            #region Delete Mark
            var deleteMarkSecionLabel = new Label("Usuń markę:")
            {
                X = Pos.Left(editButton),
                Y = Pos.Top(editButton) + 3,
            };

            var markDeleteSelectLabel = new Label("Wybierz markę którą chcesz usunąć:")
            {
                X = Pos.Left(deleteMarkSecionLabel),
                Y = Pos.Top(deleteMarkSecionLabel) + 1,
            };

            markDeleteCombo = new ComboBox()
            {
                X = Pos.Left(markDeleteSelectLabel),
                Y = Pos.Top(markDeleteSelectLabel) + 1,
                Width = Dim.Fill(),
            };
            markDeleteCombo.SetSource(marks);

            var deleteButton = new Button("Usuń")
            {
                X = Pos.Left(markDeleteCombo),
                Y = Pos.Top(markDeleteCombo) + 1
            };

            var backButton = new Button("Cofnij")
            {
                Y = Pos.Percent(100) - 1,
                X = Pos.Center(),
            };
            Add(deleteMarkSecionLabel);
            Add(markDeleteSelectLabel);
            Add(markDeleteCombo);
            Add(deleteButton);
            Add(backButton);
            #endregion


            #region bind-button-events
            addButton.Clicked += () =>
            {
                if (string.IsNullOrEmpty(markText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole \"Marka\" nie może być puste", "Ok");
                    return;
                }

                OnAdd?.Invoke(markText.Text.ToString());
            };

            editButton.Clicked += () =>
            {
                if (markEditCombo.SelectedItem == 0 && string.IsNullOrEmpty(markEditCombo.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wybrano błędną markę.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(markEditNewValueText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole \"Marka\" nie może być puste.", "Ok");
                    return;
                }
                OnEdit?.Invoke((name: markEditNewValueText.Text.ToString(), id: marks[markEditCombo.SelectedItem].MarkId));
            };

            deleteButton.Clicked += () =>
            {
                if (markDeleteCombo.SelectedItem == 0 && string.IsNullOrEmpty(markDeleteCombo.Text.ToString()) || markDeleteCombo.SelectedItem >= marks.Count)
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wybrano błędną markę.", "Ok");
                    return;
                }
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz usunąć markę " + marks[markDeleteCombo.SelectedItem], "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(marks[markDeleteCombo.SelectedItem].MarkId);
                    marks.Remove(marks[markDeleteCombo.SelectedItem]);
                }
            };

            backButton.Clicked += () =>
            {
                
                OnBack?.Invoke();
                //Close();

            };
            #endregion
        }
    }
}
