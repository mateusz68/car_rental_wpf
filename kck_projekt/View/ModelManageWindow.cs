using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class ModelManageWindow : Window
    {
        private readonly Terminal.Gui.View _parent;
        private List<Model.CarModel> models;
        public List<Model.CarModel> SetModel 
        { 
            set
            {
                models = value;
                UpdateComboBox();
            }
        }
        private List<Model.CarMark> marks;
        public Action OnBack { get; set; }
        public Action<(string name, int modelId, int markId)> OnEdit { get; set; }
        public Action<int> OnRemove { get; set; }
        public Action<(string name, int markId)> OnAdd { get; set; }
        private ComboBox modelEditCombo;
        private ComboBox modelDeleteCombo;

        public ModelManageWindow(Terminal.Gui.View parent, List<Model.CarModel> models, List<Model.CarMark> marks) : base("Dodaj Markę")
        {
            _parent = parent;
            this.models = models;
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
        private void UpdateComboBox()
        {
            modelEditCombo.SetSource(models);
            modelDeleteCombo.SetSource(models);
        }

        private void InitControls()
        {

            #region Add Model
            var modelSecionLabel = new Label(2, 1, "Dodaj nowy model:");
            var markModelAddLabel = new Label("Wybierz markę:")
            {
                X = Pos.Left(modelSecionLabel),
                Y = Pos.Top(modelSecionLabel) + 1,
            };
            var markAddCombo = new ComboBox()
            {
                X = Pos.Left(markModelAddLabel),
                Y = Pos.Top(markModelAddLabel) + 1,
                Width = Dim.Fill(),
                ColorScheme = Colors.TopLevel,
                Height = 5,
            };
            markAddCombo.SetSource(marks);

            var modelLabel = new Label("Nazwa Modelu:")
            {
                X = Pos.Left(markAddCombo),
                Y = Pos.Top(markAddCombo) + 1,
            };
            var modelText = new TextField()
            {
                X = Pos.Left(modelLabel),
                Y = Pos.Top(modelLabel) + 1,
                Width = Dim.Fill()
            };

            var addButton = new Button("Dodaj")
            {
                X = Pos.Left(modelText),
                Y = Pos.Top(modelText) + 1
            };
            Add(modelSecionLabel);
            Add(markModelAddLabel);
            Add(markAddCombo);
            Add(modelLabel);
            Add(modelText);
            Add(addButton);
            #endregion

            #region Edit Model
            var editModelSecionLabel = new Label("Edytuj model:")
            {
                X = Pos.Left(addButton),
                Y = Pos.Top(addButton) + 3,
            };
            Add(editModelSecionLabel);

            var modelEditSelectLabel = new Label("Wybierz model który chcesz edytować:")
            {
                X = Pos.Left(editModelSecionLabel),
                Y = Pos.Top(editModelSecionLabel) + 1,
            };
            Add(modelEditSelectLabel);

            modelEditCombo = new ComboBox()
            {
                X = Pos.Left(modelEditSelectLabel),
                Y = Pos.Top(modelEditSelectLabel) + 1,
                Width = Dim.Fill(),
                ColorScheme = Colors.TopLevel,
                Height = 5,
            };
            modelEditCombo.SetSource(models);
            Add(modelEditCombo);

            var modelEditNewValueLabel = new Label("Wprowadź nową nazwę:")
            {
                X = Pos.Left(modelEditCombo),
                Y = Pos.Top(modelEditCombo) + 1,
            };
            Add(modelEditNewValueLabel);

            var modelEditNewValueText = new TextField()
            {
                X = Pos.Left(modelEditNewValueLabel),
                Y = Pos.Top(modelEditNewValueLabel) + 1,
                Width = Dim.Fill(),
            };
            Add(modelEditNewValueText);

            var markEditNewValueLabel = new Label("Wybierz nową markę:")
            {
                X = Pos.Left(modelEditNewValueText),
                Y = Pos.Top(modelEditNewValueText) + 1,
            };
            Add(markEditNewValueLabel);

            var markEditCombo = new ComboBox()
            {
                X = Pos.Left(markEditNewValueLabel),
                Y = Pos.Top(markEditNewValueLabel) + 1,
                Width = Dim.Fill(),
                Height = 5,
                ColorScheme = Colors.TopLevel,
            };
            markEditCombo.SetSource(marks);
            Add(markEditCombo);
            
            var editButton = new Button("Edytuj")
            {
                X = Pos.Left(markEditCombo),
                Y = Pos.Top(markEditCombo) + 1
            };
            Add(editButton);
            #endregion

            #region Delete Model
            var deleteModelSecionLabel = new Label("Usuń model:")
            {
                X = Pos.Left(editButton),
                Y = Pos.Top(editButton) + 3,
            };

            var modelDeleteSelectLabel = new Label("Wybierz model który chcesz usunąć:")
            {
                X = Pos.Left(deleteModelSecionLabel),
                Y = Pos.Top(deleteModelSecionLabel) + 1,
            };

            modelDeleteCombo = new ComboBox()
            {
                X = Pos.Left(modelDeleteSelectLabel),
                Y = Pos.Top(modelDeleteSelectLabel) + 1,
                Width = Dim.Fill(),
                Height = 5,
            };
            modelDeleteCombo.SetSource(models);

            var deleteButton = new Button("Usuń")
            {
                X = Pos.Left(modelDeleteCombo),
                Y = Pos.Top(modelDeleteCombo) + 1
            };

            var backButton = new Button("Cofnij")
            {
                Y = Pos.Percent(100) - 1,
                X = Pos.Center(),
            };
            Add(deleteModelSecionLabel);
            Add(modelDeleteSelectLabel);
            Add(modelDeleteCombo);
            Add(deleteButton);
            Add(backButton);
            #endregion

            #region Value Change
            modelEditCombo.SelectedItemChanged += (a) =>
            {
                markEditCombo.Text = models[a.Item].Mark.MarkName;
            };
            #endregion

            #region bind-button-events
            addButton.Clicked += () =>
            {
                if (markAddCombo.SelectedItem == 0 && string.IsNullOrEmpty(markAddCombo.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wybrano błędną markę.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(modelText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole \"Marka\" nie może być puste", "Ok");
                    return;
                }

                OnAdd?.Invoke((name:modelText.Text.ToString(), markId: marks[markAddCombo.SelectedItem].MarkId));
            };

            editButton.Clicked += () =>
            {
                if (modelEditCombo.SelectedItem == 0 && string.IsNullOrEmpty(modelEditCombo.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wybrano błędny model.", "Ok");
                    return;
                }
                if (markEditCombo.SelectedItem == 0 && string.IsNullOrEmpty(markEditCombo.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Wybrano błędną markę.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(modelEditNewValueText.Text.ToString()))
                {
                    MessageBox.ErrorQuery(25, 8, "Błąd", "Pole \"Marka\" nie może być puste.", "Ok");
                    return;
                }
                OnEdit?.Invoke((name: modelEditNewValueText.Text.ToString(), modelId: models[modelEditCombo.SelectedItem].ModelId, markId: marks[markEditCombo.SelectedItem].MarkId));
                //modelEditNewValueText.Text = "";
                //modelEditCombo.Text = "";
            };

            deleteButton.Clicked += () =>
            {
                if (modelDeleteCombo.SelectedItem == 0 && string.IsNullOrEmpty(modelDeleteCombo.Text.ToString()) || modelDeleteCombo.SelectedItem >= models.Count)
                {
                    MessageBox.ErrorQuery(25, 8, "Usuń", "Błędny wybór", "Ok");
                    return;
                }
                var n = MessageBox.Query(25, 8, "Usuń", "Czy napewno chcesz usunąć model " + models[modelDeleteCombo.SelectedItem], "Anuluj", "Ok");
                if (n == 1)
                {
                    OnRemove?.Invoke(models[modelDeleteCombo.SelectedItem].ModelId);
                    models.Remove(models[modelDeleteCombo.SelectedItem]);
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
