﻿using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public string Message { get; set; }
        public MessageWindow(string message)
        {
            InitializeComponent();
            DataContext = this;
            this.Message = message;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MouseLeftClicked(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
