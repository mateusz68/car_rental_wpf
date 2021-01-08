using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt.View
{
    public class AppView : ViewInterface
    {
        public Window Win { get; set; }
        public static Toplevel Top { get; set; }
        private Model.User user;
        private static Toplevel _top;
        private List<Model.CarModel> models;
        private List<Model.CarMark> marks;
        private List<Model.Car> cars;
        private List<Model.Car> avaliableCars;
        public Controller.AppController MyController { get; set; }
        public AppView()
        {
            Application.UseSystemConsole = false;
            Application.Init();
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

        public void CloseWindow()
        {
            Application.Top.RemoveAll();
            Application.RequestStop();
        }

        public void ShowMessage(string text)
        {
            MessageBox.Query(25, 8, "Uwaga", text, "Ok");
        }

        public void ShowMenu()
        {
            if (user.Rola == Model.UserRole.User)
            {
                ShowMainMenu();
            }
            else if (user.Rola == Model.UserRole.Staff || user.Rola == Model.UserRole.Admin)
            {
                ShowStaffMenu();
            }
        }

        public void ShowLogin()
        {
            var loginWindow = new Login(null);
            loginWindow.OnLogin = (loginData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.LoginUser(loginData.name, loginData.password, loginData.remember);
                });
            };
            loginWindow.OnRegister = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(loginWindow);
                ShowRegistration();
            };
            loginWindow.OnExit = () =>
            {
                Application.RequestStop();
            };

            Top = Application.Top;
            Top.Add(loginWindow);
            Application.Run(Top);
        }

        public void ShowRegistration()
        {
            var registrationWindow = new RegistrationWindow(null);
            registrationWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(registrationWindow);
                ShowLogin();
            };
            registrationWindow.OnExit = () =>
            {
                Application.Shutdown();
            };
            registrationWindow.OnRegister = (registerData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.RegisterUser(registerData.name, registerData.password, registerData.email);
                });
            };
            Top = Application.Top;
            Top.Add(registrationWindow);
            Application.Run(Top);
        }

        
        public void ShowMainMenu()
        {
            Application.Top.RemoveAll();
            _top = Application.Top;
            var mainMenuWindow = new MainMenuWindow(null, user)
            {
                OnLogout = () =>
                {
                    MyController.Logout();
                    Application.RequestStop();

                },
                
            };
            _top.Add(mainMenuWindow);
            mainMenuWindow.OnSelect = (int value) =>
            {
                Application.RequestStop();
                Application.Top.Remove(mainMenuWindow);
                switch (value)
                {
                    case 1:
                        ShowCarList();
                        break;
                    case 2:
                        ShowReservationWindow(null);
                        break;
                    case 3:
                        ShowUserReservationList();
                        break;
                    case 4:
                        ShowUserSettings();
                        break;
                    case 5:
                        ShowAppSettings();
                        break;
                    default:
                        break;
                }
            };
            mainMenuWindow.OnExit = () =>
            {
                Application.RequestStop();
            };
            Application.Run(_top);
        }


        public void ShowStaffMenu()
        {
            Application.Top.RemoveAll();
            _top = Application.Top;
            var staffMenuWindow = new StaffMenuWindow(null, user)
            {
                OnLogout = () =>
                {
                    MyController.Logout();
                    Application.RequestStop();
                },

            };
            _top.Add(staffMenuWindow);
            staffMenuWindow.OnSelect = (int value) =>
            {
                Application.RequestStop();
                Application.Top.Remove(staffMenuWindow);
                Debug.WriteLine(value);
                switch (value)
                {
                    case 1:
                        ShowReservationList();
                        break;
                    case 2:
                        ShowCarList();
                        break;
                    case 3:
                        ShowModelManage();
                        break;
                    case 4:
                        ShowMarkManage();
                        break;
                    case 5:
                        ShowUserList();
                        break;
                    case 6:
                        ShowAppSettings();
                        break;
                    default:
                        break;
                }
            };
            staffMenuWindow.OnExit = () =>
            {
                Application.RequestStop();
            };
            Application.Run(_top);
        }

        public void ShowCarList()
        {
            CarListWindow carListWindow;
            if (user.Rola == Model.UserRole.User)
            {
                carListWindow = new CarListWindow(null, avaliableCars, user, MyController);
            }
            else
            {
                carListWindow = new CarListWindow(null, cars, user, MyController);
            }
            carListWindow.OnAdd = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(carListWindow);
                ShowCarManager( null);
            };
            carListWindow.OnEdit = (car) =>
            {
                Application.RequestStop();
                Application.Top.Remove(carListWindow);
                ShowCarManager(car);
            };
            carListWindow.OnRemove = (carId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.DeleteCar(carId);
                });
            };
            carListWindow.OnReservation = (car) =>
            {
                Application.RequestStop();
                Application.Top.Remove(carListWindow);
                ShowReservationWindow(car);
            };
            carListWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(carListWindow);
                ShowMenu();
            };
            Top = Application.Top;
            Top.Add(carListWindow);
            Application.Run(Top);
        }

        public void ShowReservationWindow(Model.Car selectedCar = null)
        {
            var reservationWindow = new ReservationWindow(null, avaliableCars, selectedCar, user, MyController);
            reservationWindow.OnSave = (reservationData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageReservation.AddReservation(reservationData);
                });
            };
            reservationWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(reservationWindow);
                ShowMenu();
            };
            Top = Application.Top;
            Top.Add(reservationWindow);
            Application.Run(Top);
        }


        public void ShowUserReservationList()
        {
            List<Model.Reservation> reserwations = MyController.manageReservation.GetUserReservation(user.UserId);
            var userReservationListWindow = new UserReservationListWindow(null, reserwations);
            userReservationListWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(userReservationListWindow);
                ShowMenu();
            };
            Top = Application.Top;
            Top.Add(userReservationListWindow);
            Application.Run(Top);
        }


        public void ShowUserSettings()
        {
            var userSettingsWindow = new UserSettingsWindow(null, user);
            userSettingsWindow.OnSettingsChange = (settingData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageUser.ChangeUserSettings(settingData.name, settingData.surname, settingData.phone, settingData.adres1, settingData.adres2, settingData.adres3, settingData.userId);
                });
            };
            userSettingsWindow.OnPasswordChange = (passwordData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageUser.ChangePassword(passwordData.oldPassword, passwordData.newPassword, passwordData.userId);
                });
            };
            userSettingsWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(userSettingsWindow);
                ShowMenu();
            };
            Top = Application.Top;
            Top.Add(userSettingsWindow);
            Application.Run(Top);
        }


        public void ShowAppSettings()
        {
            Debug.WriteLine("Jestem w funkcji");
            var appSettingsWindow = new AppSettings(null);
            appSettingsWindow.OnSave = (mode) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    if (mode == 0)
                    {
                        Controller.AppController.AddOrUpdateAppSettings("interfaceType", "wpf");
                    }
                    else
                    {
                        Controller.AppController.AddOrUpdateAppSettings("interfaceType", "console");
                    }
                    ShowMessage("Ustawienia zapisane!");
                });
            };
            appSettingsWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(appSettingsWindow);
                ShowMenu();
            };
            Top = Application.Top;
            Top.Add(appSettingsWindow);
            Application.Run(Top);
        }

        public void ShowCarManager(Model.Car car)
        {
            var carManagerWindow = new CarManageWindow(null, car, models);
            carManagerWindow.OnRemove = (carId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.DeleteCar(carId);
                });
            };
            carManagerWindow.OnSave = (editData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.SaveCar(editData.carData, editData.carId);
                    cars = MyController.manageCars.GetCarList();
                });
            };
            carManagerWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(carManagerWindow);
                ShowCarList();
            };
            Top = Application.Top;
            Top.Add(carManagerWindow);
            Application.Run(Top);
        }

        public void ShowReservationList()
        {
            var users = MyController.manageUser.GetUserList();
            List<Model.Reservation> reserwations = MyController.manageReservation.GetReservationList();
            var reservationListWindow = new ReservationListWindow(null, reserwations, users);
            reservationListWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(reservationListWindow);
                ShowMenu();
            };
            reservationListWindow.OnRemove = (reservationId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageReservation.DeleteReservation(reservationId);
                });
            };
            reservationListWindow.OnAdd = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(reservationListWindow);
                ShowReservationManager(null);
            };
            reservationListWindow.OnEdit = (reservation) =>
            {
                Application.RequestStop();
                Application.Top.Remove(reservationListWindow);
                ShowReservationManager(reservation);
            };
            Top = Application.Top;
            Top.Add(reservationListWindow);
            Application.Run(Top);
        }

        public void ShowReservationManager(Model.Reservation reservation)
        {
            var users = MyController.manageUser.GetUserList();
            var reservationManagerWindow = new ReservationManageWindow(null, reservation, users, cars, MyController);
            reservationManagerWindow.OnRemove = (reservationId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageReservation.DeleteReservation(reservationId);
                });

            };
            reservationManagerWindow.OnSave = (editData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageReservation.SaveReservation(editData.reservationData, editData.reservationId);
                });
            };
            reservationManagerWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(reservationManagerWindow);
                ShowReservationList();
            };
            Top = Application.Top;
            Top.Add(reservationManagerWindow);
            Application.Run(Top);
        }

        #region model and mark magane
        public void ShowMarkManage()
        {
            var markManageWindow = new MarkManageWindow(Application.Top, marks);
            markManageWindow.OnRemove = (id) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.DeleteMark(id);
                });
            };
            markManageWindow.OnAdd = (String markName) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.AddMark(markName);
                    marks = MyController.manageCars.GetMarkList();
                    markManageWindow.MarkManager = marks;
                });
            };
            markManageWindow.OnEdit = (markData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.EditMark(markData.name, markData.id);
                    marks = MyController.manageCars.GetMarkList();
                    markManageWindow.MarkManager = marks;
                });
            };
            markManageWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(markManageWindow);
                ShowMenu();
            };

            Top = Application.Top;
            Top.Add(markManageWindow);
            Application.Run(Top);
        }

        public void ShowModelManage()
        {
            ModelManageWindow modelManageWindow = new ModelManageWindow(Application.Top, models, marks);
            modelManageWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(modelManageWindow);
                ShowMenu();
            };
            modelManageWindow.OnRemove = (id) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.DeleteModel(id);
                });
            };
            modelManageWindow.OnAdd = (addData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.AddModel(addData.name, addData.markId);
                    models = MyController.manageCars.GetModelsList();
                    modelManageWindow.SetModel = models;
                });
            };
            modelManageWindow.OnEdit = (editData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageCars.EditModel(editData.name, editData.modelId, editData.markId);
                    models = MyController.manageCars.GetModelsList();
                    modelManageWindow.SetModel = models;
                });
            };
            Top = Application.Top;
            Top.Add(modelManageWindow);
            Application.Run(Top);
        }
        #endregion

        #region User Admin Manage
        public void ShowUserList()
        {
            List<Model.User> users = MyController.manageUser.GetUserList();
            var userListWindow = new UserListWindow(null, users);
            userListWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(userListWindow);
                ShowMenu();
            };
            userListWindow.OnRemove = (userId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageUser.DeleteUser(userId);
                });
            };
            userListWindow.OnAdd = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(userListWindow);
                ShowUserManager(null);
            };
            userListWindow.OnEdit = (user) =>
            {
                Application.RequestStop();
                Application.Top.Remove(userListWindow);
                ShowUserManager(user);
            };
            Top = Application.Top;
            Top.Add(userListWindow);
            Application.Run(Top);
        }

        public void ShowUserManager(Model.User user)
        {
            var userManagerWindow = new UserManageWindow(null, user);
            userManagerWindow.OnRemove = (userId) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageUser.DeleteUser(userId);
                });
                
            };
            userManagerWindow.OnSave = (editData) =>
            {
                Application.MainLoop.Invoke(() =>
                {
                    MyController.manageUser.SaveUser(editData.userData, editData.userId);
                });
            };
            userManagerWindow.OnBack = () =>
            {
                Application.RequestStop();
                Application.Top.Remove(userManagerWindow);
                ShowUserList();
            };
            Top = Application.Top;
            Top.Add(userManagerWindow);
            Application.Run(Top);
        }
        #endregion
    }
}
