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
            //using(var context = new Model.AppContext())
            //{
            //    var user = new User { UserName = "Test201122", UserPassword = "test21010" };
            //    context.Users.Add(user);
            //    context.SaveChanges();

            //}

            //using (var context = new Model.AppContext())
            //{
            //    var user = context.Users
            //        .Where(r => r.UserId == 10)
            //        .First();
            //    Console.WriteLine(user.UserName);
            //    Console.ReadLine();


            //}
            //Application.Init();
            //string mode = ConfigurationManager.AppSettings["mode"];
            //FreeConsole();
            ViewInterface view;
            Model.AppContext model = new Model.AppContext();

            if (true)
            {
                view = new View.AppView();
            }
            else
            {
                FreeConsole();
                view = new Wpf.WindowManager();
            }

            Controller.AppController controller = new Controller.AppController(model, view);
            //View.AppView view = new View.AppView();



        }
    }
}
