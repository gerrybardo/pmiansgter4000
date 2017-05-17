using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PingMaster4000
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PingBox pingPanel1;
        string ipAddress, ipAddress2;

        public MainWindow()
        {
            InitializeComponent();

            ipAddress = "8.8.8.8";
            ipAddress2 = "8.8.4.4";
            //Erstellt einen neuen Ping (PingBox) mit der IP-Adresse, dem Intervall in Sekunden und der Dauer fuer die Durchschnittsberechnung in Sekunden
            pingPanel1 = new PingBox(ipAddress,1,60,50);
            pingStackpanel.Children.Add(pingPanel1);
            pingPanel1 = new PingBox(ipAddress2,1,30,60);
            pingStackpanel.Children.Add(pingPanel1);


        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
