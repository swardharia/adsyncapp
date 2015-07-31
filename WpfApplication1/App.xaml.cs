using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region EXCEPTION HANDLER
        public App ()
            : base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException ( object sender , System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
            string errorMessage = string.Format( "An unhandled exception occurred: {0}" , e.Exception.Message );
            MessageBox.Show( errorMessage , "Error" , MessageBoxButton.OK , MessageBoxImage.Error );
            e.Handled = true;
        }
        #endregion
    }
}
