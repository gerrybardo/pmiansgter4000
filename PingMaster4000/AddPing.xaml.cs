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
using System.Data;
using System.Net;

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
            if (ipaddressTextbox.Text == "Mandatory")
            {
                ipaddressTextbox.Clear();
            }
        }

        private void ipaddressTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ipaddressTextbox.Text == "")
            {
                ipaddressTextbox.Text = "Mandatory";
            }
        }

        private void timeErrorTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (timeErrorTextbox.Text == "")
            {
                timeErrorTextbox.Text = "Default(Variable)";
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
            if (rangeTextbox.Text == "Default(Variable)")
            {
                rangeTextbox.Clear();
            }
        }

        private void intervalTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (intervalTextbox.Text == "Default(Variable)")
            {
                intervalTextbox.Clear();
            }
            
        }

        private void timeErrorTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (timeErrorTextbox.Text == "Default(Variable)")
            {
                timeErrorTextbox.Clear();
            }
        }

        private void intervalTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (intervalTextbox.Text == "")
            {
                intervalTextbox.Text = "Default(Variable)";
            }
        }
       
        private void addPing_Click(object sender, RoutedEventArgs e)
        {
            int range, interval, timeError,configId;
            string ip= "";
            timeError = 60;
            configId = 0;
            int autostart = 0;
            IPAddress ipAddress;

            if(IPAddress.TryParse(ipaddressTextbox.Text,out ipAddress)) {
                ip = ipAddress.ToString();
            }else if (validateAddress(ipaddressTextbox.Text))
            {
                ip = ipaddressTextbox.Text;
            }
            else 
            {
                errorMessage.Visibility = Visibility.Visible;
            }

            if (autoStartCheckbox.IsChecked == true)
            {
                autostart = 1;
            }
            else
            {
                autostart = 0;
            }

            if (int.TryParse(this.intervalTextbox.Text, out interval))
            {
                if (interval <= 0)
                {
                    interval = 1;
                }
            }
            else
            {
                interval = 1;
            };

            if (int.TryParse(this.timeErrorTextbox.Text, out timeError))
            {
                if (timeError <= 0)
                {
                    timeError = 60;
                }
            }
            else
            {
                timeError = 60;
            };

            if (int.TryParse(this.rangeTextbox.Text, out range))
            {
                if (range <= 0)
                {
                    range = 60;
                }
            }
            else
            {
                range = 60;
            };


            if (ip != "")
            {
                errorMessage.Visibility = Visibility.Hidden;
                ((MainWindow)this.Owner).configCreate(ip, interval, range, timeError, autostart);
                DataTable DT = ((MainWindow)this.Owner).GetDataTable("tbl_config", "ORDER BY Config_ID DESC LIMIT 1");
                ((MainWindow)this.Owner).fillPingpanelFromDataTable(DT);
                DT.Dispose();
            }

        }
        public bool validateAddress(string address)
        {
            string[] topLVLDomainArray;
            string topLVLDomain;
            topLVLDomainArray = address.Split('.');
            if (topLVLDomainArray.Length <=1) {
                return false;
            }
            topLVLDomain = topLVLDomainArray[topLVLDomainArray.Length - 1];
            
            if (((MainWindow)this.Owner).countDataTable("tbl_top_level_domain", String.Format("WHERE endung = '{0}'", topLVLDomain)) != 1)
            {
                return false;
            }
            foreach(string part in topLVLDomainArray)
            {

                if(part == "")
                {
                    return false;
                }
                foreach(char c in part)
                {
                    if (!((c >= 48  && c <= 57)|| (c >= 65 && c <= 90)|| (c >= 67 && c <= 122)||(c == 45)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
