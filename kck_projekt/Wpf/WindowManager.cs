using kck_projekt.Model;
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
        private Model.User user;
        private List<Model.CarModel> models;
        private List<Model.CarMark> marks;
        private List<Model.Car> cars;
        private List<Model.Car> avaliableCars;
        private Controller.AppController appController;
        public WindowManager()
        {
            InitializeWindows();
        }
        static void InitializeWindows()
        {
            WinApp = new Application();
            
        }

        [STAThread]
        public void start()
        {
            System.Windows.Application app = new System.Windows.Application();
            app.Run(new Login());
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
            /*WinApp.Run(MainWindow = new Login());*/ // note: blocking call
            WinApp.Run(MainWindow = new StaffMenu());
        }

        public void CloseWindow()
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string text)
        {
            throw new NotImplementedException();
        }

        public void ShowMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowRegistration()
        {
            throw new NotImplementedException();
        }

        public void ShowMainMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowStaffMenu()
        {
            throw new NotImplementedException();
        }
    }
}
