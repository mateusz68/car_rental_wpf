using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Controller
{
    public class AppController
    {
        private int UserId { get; set; }
        private Model.User LoggedUser {get; set;}
        private ViewInterface view;
        private Model.AppContext model;
        public ManageUsers manageUser;
        public ManageCars manageCars;
        public ManageReservation manageReservation;
        public AppController(Model.AppContext model, ViewInterface view)
        {
            this.view = view;
            this.model = model;
            manageUser = new ManageUsers(model, view);
            manageCars = new ManageCars(model, view);
            manageReservation = new ManageReservation(model, view);
            view.SetController(this);
            view.GetCarData();
            startApp();

        }


        public void startApp()
        {
            if (ConfigurationManager.AppSettings["remember"] == "true")
            {
                if (manageUser.UserExists(ConfigurationManager.AppSettings["login"]))
                {
                    var user = manageUser.GetUserHash(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);
                    if (user != null)
                    {
                        LoggedUser = user;
                        view.SetUser(LoggedUser);
                        
                        showMenu();
                        return;
                    }
                }
            }
            view.ShowLogin();
        }


        public void showMenu()
        {
            if(LoggedUser.Rola == Model.UserRole.User)
            {
                view.ShowMainMenu();
            }else if(LoggedUser.Rola == Model.UserRole.Staff || LoggedUser.Rola == Model.UserRole.Admin)
            {
                view.ShowStaffMenu();
            }
        }

        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: \"{1}\"", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }


        public void RegisterUser(string name, string password, string email)
        {
            if (!manageUser.UserExists(name))
            {
                manageUser.AddUser(name, password, email);
                //view.ShowMessage("Zarejestrowano pomyślnie!");
                view.CloseWindow();
                view.ShowLogin();
                return;
            }
            view.ShowMessage("Użytkownik istnieje!");
            
        }

        public void LoginUser(string name, string password, bool remember)
        {
            if (manageUser.UserExists(name))
            {
                var user = manageUser.GetUser(name, password);
                if (user != null)
                {
                    if (remember)
                    {
                        var salt = manageUser.GetUserSalt(name);
                        var hashPassword = ManageUsers.GenerateHash(password, salt);
                        AddOrUpdateAppSettings("remember", "true");
                        AddOrUpdateAppSettings("login", name);
                        AddOrUpdateAppSettings("password", hashPassword);
                    }
                    LoggedUser = user;
                    view.ShowMessage("Zalogowano poprawnie!");
                    view.CloseWindow();
                    view.SetUser(LoggedUser);
                    showMenu();
                    return;
                }
            }
            view.ShowMessage("Wprowadzono błędne dane logowania! Spróbuj ponownie.");
        }

        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public void Logout()
        {
            AddOrUpdateAppSettings("remember", "false");
            AddOrUpdateAppSettings("login", "");
            AddOrUpdateAppSettings("password", "");
        }
    }
}
