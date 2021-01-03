﻿using kck_projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kck_projekt.Wpf
{
    public class WindowManager: ViewInterface
    {
        public static Application WinApp { get; private set; }
        public static Window MainWindow { get; private set; }
        public Controller.AppController MyController { get; set; }
        private Window currentWindow;
        private Model.User user;
        private List<Model.CarModel> models;
        private List<Model.CarMark> marks;
        private List<Model.Car> cars;
        private List<Model.Car> avaliableCars;
        public WindowManager()
        {
            InitializeWindows();
        }
        static void InitializeWindows()
        {
            WinApp = new Application();
            WinApp.ShutdownMode = ShutdownMode.OnLastWindowClose;
            //WinApp = new App();
        }

        [STAThread]
        public void start()
        {
            //System.Windows.Application app = new System.Windows.Application();
        }

        public void SetController(Controller.AppController controller)
        {
            MyController = controller;
        }

        public void GetCarData()
        {
            models = MyController.manageCars.GetModelsList();
            marks = MyController.manageCars.GetMarkList();
            cars = MyController.manageCars.GetCarList();
            avaliableCars = MyController.manageCars.GetAvaliableCarList();
        }

        public void SetUser(Model.User user)
        {
            this.user = user;
        }

        public void ShowLogin()
        {
            currentWindow = new Login(MyController);
            //currentWindow.Show();

            WinApp.Run(currentWindow);
        }

        public void CloseWindow()
        {
            currentWindow.Hide();
        }

        public void ShowMessage(string text)
        {
            MessageBox.Show(text, "Uwaga");
        }

        public void ShowMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowRegistration()
        {
            currentWindow = new Register(MyController);
            WinApp.Run(currentWindow);
        }

        public void ShowMainMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowStaffMenu()
        {
            
            if(currentWindow == null)
            {
                currentWindow = new StaffMenu(MyController);
                WinApp.Run(currentWindow);
            }
            else
            {
                currentWindow = new StaffMenu(MyController);
                currentWindow.Show();
            }
        }
    }
}
