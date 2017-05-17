using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace PingMaster4000
{
    /// <summary>
    /// Interaktionslogik für pingBox.xaml
    /// </summary>
    public partial class PingBox : UserControl
    {
        BackgroundWorker worker;
        int intervalSeconds, avgRangeSeconds, acceptableAvg;
        string ipAddress;
        List<int> msList;
        bool cancellationPending;

        public PingBox(string ipAddressParam, int intervalSecondsParam, int avgRangeSecondsParam, int acceptableAvgParam)
        {
            InitializeComponent();

            //Schreibt die Constructor Parameter in die globalen Variablen
            intervalSeconds = intervalSecondsParam;
            avgRangeSeconds = avgRangeSecondsParam;
            acceptableAvg = acceptableAvgParam;
            ipAddress = ipAddressParam;

            //Schreibt die IP in den Kopf des Moduls
            ipInfoLabelVal.Content = ipAddress;

            //Schreibt die Tooltips
            ToolTipService.SetShowDuration(pingStatusLabelVal,20000);
            pingStatusLabelVal.ToolTip = "Statusrange:" + Environment.NewLine + acceptableAvg +"ms or better(OK)" + Environment.NewLine + (acceptableAvg+1) 
                +"ms - " + acceptableAvg * 1.2 + "ms(ALARMIG)" + Environment.NewLine + (acceptableAvg * 1.2+1) + "ms and higher(CRITICAL)";

            //Initialisierung der Liste mit den letzten Werten
            msList = new List<int>();

            //Initialisierung des Backgroundworkers
            worker = new BackgroundWorker();
            //Einstiegspunkt (Was soll der Worker ausfuehren)
            worker.DoWork += PingThisIp;
            //Was passiert wenn der Backgroundworker seine Arbeit beendet
            worker.RunWorkerCompleted += RunWorkerCompleted;
            //Der Backgroundworker darf seinen Progress reporten
            worker.WorkerReportsProgress = true;
            //Was soll bei einem Progressreport ausgefuehrt werden
            worker.ProgressChanged +=WorkerProgressChanged;

            
            //startPing();

        }


        /*
         *          ------ Methoden fuer Buttons ------
         *          
         * 
         * 
         * 
         * */

        private void startPingBox_Click(object sender, RoutedEventArgs e)
        {
            startPing();
        }

        private void stopPingBox_Click(object sender, RoutedEventArgs e)
        {
            cancellationPending = true;
        }



        /*
         *
         *                  ----- Methoden zum Backgroundworker! -------
         *                  
         *                  */
        //Prozedur zum Starten des Backgroundworkers
        private void startPing()
        {
            if (!worker.IsBusy)
            {
                cancellationPending = false;

                //Startet den Backgroundworker
                worker.RunWorkerAsync(ipAddress);

                //Gibt an, dass der Ping aktiv ist
                runningLabelVal.Content = "Yes";
                runningLabelVal.Foreground = Brushes.Green;
            }
        }

        //Arbeit des Backgroundworkers
        private void PingThisIp(object sender, DoWorkEventArgs e)
        {
            var ipAddress = e.Argument.ToString();

            //Pingt die gewuenschte IP an solange der Worker laeuft
            do
            {
                using (Ping myPing = new Ping())
                {
                    // Erzeuge Ping und speicher die Results weg
                    var ping = myPing.Send(ipAddress);

                    // Aufruf der UI Update mit String[] 
                    worker.ReportProgress(1, new string[] { ping.RoundtripTime.ToString(), ping.Status.ToString() });
                }
                Thread.Sleep(1000 * intervalSeconds);
            } while (!cancellationPending);


        }

        //Reportet den Progress des Backgroundworkers
        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] timeAndStatus = (string[])e.UserState;
            UpdateUi(timeAndStatus[0],timeAndStatus[1]);
        }


        //Laeuft der Ping(der Backgroundworker) nicht, so wird dies angezeigt
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runningLabelVal.Content = "No";
            runningLabelVal.Foreground = Brushes.Red;
            pingStatusLabelVal.Content = "N/A";
            pingStatusLabelVal.Foreground = Brushes.Black;
            avgMSValLabel.Content = "N/A";
        }

        /*
         *      --- Methoden fuer Berechnungen
         */

        //Berechnung der durchschnittlichen RoundTripTime
        private int CalcUIValues(string time, string status)
        {
            int avgMS, avgRangeElementsInList;

            //Berechnung der Maximalen Anzahl an Listenelementen
            avgRangeElementsInList = avgRangeSeconds / intervalSeconds;

            /*              
            *              Fuegt die Zeiten die in der Durchschnittszeitspanne liegen in die Liste ein
            *             
            */

            //Entfernt die Elemente die aelter sind als die gewuenschte Durschnittsreichweite
            if (msList.Count >= avgRangeElementsInList)
            {
                msList.RemoveAt(0);
            }

            //Falls ein Ping nicht erfolgreich war (Status ist nicht Success und die Zeit wird dann 0ms behinhalten) wird die Timeout Zeit von 5000ms eingetragen
            if (Convert.ToInt32(time) == 0 && !(status == "Success"))
            {
                msList.Add(5000);
            }
            //Ansonsten wird die RTT (RoundTripTime) eingetragen
            else
            {
                msList.Add(Convert.ToInt32(time));
            }

            //Durchschnittsmillisekunden leeren fuer die neu Berechnung
            avgMS = 0;

            //Berechnet die Durchschnittsmillisekunden
            foreach (int val in msList)
            {
                avgMS += val;
            }
            avgMS /= msList.Count;

            return avgMS;
        }


        /*
 *
 *                  ----- Methode fuer das UI Update (wird im Mainthread ausgefuehrt und erhaelt Progress Informationen vom Backgroundworker!) -------
 *                  
 *                  */

        private void UpdateUi(string time, string status)
        {
            //Variablen fuer durchschnittliche ms
            int avgMS = CalcUIValues(time, status);


            
            //Aktualisiert die Anzeige der Durchschnittsmillisekunden
            avgMSValLabel.Content = avgMS + "ms (" + avgRangeSeconds + "sec)";

            //Aktualisiert die Bewertung der Durchschnittsmillisekunden anhand der vorgegeben akzeptablen Werte (acceptableAvg)
            if (avgMS <= acceptableAvg)
            {
                pingStatusLabelVal.Content = "OK";
                pingStatusLabelVal.Foreground = Brushes.Green;

            }
            else if(avgMS <= acceptableAvg*1.2)
            {
                pingStatusLabelVal.Content = "ALARMING";
                pingStatusLabelVal.Foreground = Brushes.Yellow;
            }
            else
            {
                pingStatusLabelVal.Content = "CRITICAL";
                pingStatusLabelVal.Foreground = Brushes.Red;
            }

        }



    }
}
