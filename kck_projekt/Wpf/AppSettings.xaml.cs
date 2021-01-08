using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kck_projekt.Wpf
{
    /// <summary>
    /// Logika interakcji dla klasy AppSettings.xaml
    /// </summary>
    public partial class AppSettings : UserControl
    {
        private Controller.AppController MyController { get; set; }
        public AppSettings(Controller.AppController MyController)
        {
            InitializeComponent();
            this.MyController = MyController;
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if ((bool)wpfRadio.IsChecked)
            {
                Controller.AppController.AddOrUpdateAppSettings("interfaceType", "wpf");
            }
            else
            {
                Controller.AppController.AddOrUpdateAppSettings("interfaceType", "console");
            }
            MessageWindow messageWindow = new MessageWindow("Ustawienia zapisane!");
            messageWindow.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            messageWindow.ShowDialog();

        }

        private void LoadSettings()
        {
            string type = ConfigurationManager.AppSettings["interfaceType"];
            if(type != null)
            {
                if (type.Equals("wpf"))
                {
                    wpfRadio.IsChecked = true;
                }
                else
                {
                    consoleRadio.IsChecked = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }
    }
}
