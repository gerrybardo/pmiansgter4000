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
using System.Windows.Shapes;

namespace PingMaster4000
{
    /// <summary>
    /// Interaktionslogik für AddPing.xaml
    /// </summary>
    public partial class AddPing : Window
    {
        public AddPing()
        {
            InitializeComponent();
        }

        /*
         *      Buttons & Texboxen
         * 
         */
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ipaddressTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ipaddressTextbox.Clear();
        }

        private void ipaddressTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ipaddressTextbox.Text == "")
            {
                ipaddressTextbox.Text = "Mandatory";
            }
        }

        private void rangeTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rangeTextbox.Text == "")
            {
                rangeTextbox.Text = "Default(Variable)";
            }
        }

        private void rangeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            rangeTextbox.Clear();
        }

        private void intervalTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            intervalTextbox.Clear();
        }

        private void intervalTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (intervalTextbox.Text == "")
            {
                intervalTextbox.Text = "Default(Variable)";
            }
        }
    }
}
