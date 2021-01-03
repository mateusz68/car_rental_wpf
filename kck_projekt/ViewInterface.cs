using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt
{
    public interface ViewInterface
    {
        void SetController(Controller.AppController controller);
        void GetCarData();
        void SetUser(Model.User user);
        void CloseWindow();
        void ShowMessage(string text);
        void ShowMenu();
        void ShowLogin();
        void ShowRegistration();
        void ShowMainMenu();
        void ShowStaffMenu();
    }
}
