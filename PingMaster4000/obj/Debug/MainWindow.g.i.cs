#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5C6CB6EF44B062216A3A8EA83E8DB6A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using PingMaster4000;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PingMaster4000
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 9 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid controlGrid;

#line default
#line hidden


#line 20 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid abschlussOben;

#line default
#line hidden


#line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label controlLabel;

#line default
#line hidden


#line 43 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pingStackpanel;

#line default
#line hidden


#line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid controlGridButtons;

#line default
#line hidden


#line 77 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startButton;

#line default
#line hidden


#line 78 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stopButton;

#line default
#line hidden


#line 79 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removeButton;

#line default
#line hidden


#line 80 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;

#line default
#line hidden


#line 81 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;

#line default
#line hidden


#line 82 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label versionLabel;

#line default
#line hidden


#line 85 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkall;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PingMaster4000;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.PingMaster4000 = ((PingMaster4000.MainWindow)(target));
                    return;
                case 2:
                    this.controlGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 3:
                    this.abschlussOben = ((System.Windows.Controls.Grid)(target));
                    return;
                case 4:
                    this.controlLabel = ((System.Windows.Controls.Label)(target));
                    return;
                case 5:
                    this.pingStackpanel = ((System.Windows.Controls.StackPanel)(target));
                    return;
                case 6:
                    this.controlGridButtons = ((System.Windows.Controls.Grid)(target));
                    return;
                case 7:
                    this.startButton = ((System.Windows.Controls.Button)(target));

#line 77 "..\..\MainWindow.xaml"
                    this.startButton.Click += new System.Windows.RoutedEventHandler(this.startButton_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.stopButton = ((System.Windows.Controls.Button)(target));

#line 78 "..\..\MainWindow.xaml"
                    this.stopButton.Click += new System.Windows.RoutedEventHandler(this.stopButton_Click);

#line default
#line hidden
                    return;
                case 9:
                    this.removeButton = ((System.Windows.Controls.Button)(target));

#line 79 "..\..\MainWindow.xaml"
                    this.removeButton.Click += new System.Windows.RoutedEventHandler(this.removeButton_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.addButton = ((System.Windows.Controls.Button)(target));

#line 80 "..\..\MainWindow.xaml"
                    this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);

#line default
#line hidden
                    return;
                case 11:
                    this.closeButton = ((System.Windows.Controls.Button)(target));

#line 81 "..\..\MainWindow.xaml"
                    this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);

#line default
#line hidden
                    return;
                case 12:
                    this.versionLabel = ((System.Windows.Controls.Label)(target));
                    return;
                case 13:
                    this.checkall = ((System.Windows.Controls.CheckBox)(target));

#line 85 "..\..\MainWindow.xaml"
                    this.checkall.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window PingMaster4000;
    }
}

