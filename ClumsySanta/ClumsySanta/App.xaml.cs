using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace ClumsySanta
{
    public partial class App : Application
    {


        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            this.DispatcherUnhandledException += Application_DispatcherUnhandledException;
            InitializeComponent();
        }


        // WPF global unhandled exception handler
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (this.MainWindow == null)
            {
                var nav = new NavigationWindow();
                this.MainWindow = nav;

                //nav.Source = new Uri("ChooseLevelPage.xaml", UriKind.Relative);
                nav.Source = new Uri("GamePlayEngine2.xaml", UriKind.Relative);
            }
            this.MainWindow.Show();
        }
    }
}