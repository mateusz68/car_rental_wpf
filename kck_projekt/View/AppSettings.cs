using NStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class AppSettings : Window
    {
        private readonly Terminal.Gui.View _parent;
        public Action OnBack { get; set; }
        public Action<int> OnSave { get; set; }
        public AppSettings(Terminal.Gui.View parent) : base("Ustawienia aplikacji")
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
            #region Set Data
            string type = ConfigurationManager.AppSettings["interfaceType"];
            int interfaceMode = 0;
            if (type != null)
            {
                if (type.Equals("wpf"))
                {
                    interfaceMode = 0;
                }
                else
                {
                    interfaceMode = 1;
                }
            }
            #endregion
            #region Manage User Fields
            var appInterfaceLabel = new Label("Tryb aplikacji")
            {
                X = Pos.Center(),
                Y = 1,
            };
            Add(appInterfaceLabel);
            ustring[] radioElements = new ustring[] { "Graficzny", "Konsolowy" };
            var radioInterfaceType = new RadioGroup(radioElements, interfaceMode)
            {
                X = 1,
                Y = Pos.Top(appInterfaceLabel)
            };
            Add(radioInterfaceType);

            #endregion


            #region Button
            var saveButton = new Button("Zapisz")
            {
                X = Pos.Center() - 10,
                Y = Pos.Percent(100) - 3
            };
            Add(saveButton);
            var backButton = new Button("Cofnij")
            {
                X = Pos.Center() + 3,
                Y = Pos.Percent(100) - 3
            };
            Add(backButton);
            #endregion

            #region bind-button-events
            saveButton.Clicked += () =>
            {
                OnSave?.Invoke(radioInterfaceType.SelectedItem);
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
