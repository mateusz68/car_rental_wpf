using kck_projekt.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace kck_projekt
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [STAThread]
        static void Main(string[] args)
        {
            ViewInterface view;
            Model.AppContext model = new Model.AppContext();

            string type = ConfigurationManager.AppSettings["interfaceType"];
            if (type != null && type.Equals("wpf"))
            {
                FreeConsole();
                view = new Wpf.WindowManager();
            }
            else
            {
                view = new View.AppView();
            }
            Controller.AppController controller = new Controller.AppController(model, view);
            //View.AppView view = new View.AppView();



        }
    }
}
