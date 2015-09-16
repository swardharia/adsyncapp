using System;
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
using System.IO;
using System.Threading;
using Microsoft.Win32;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Threading;

using WpfApplication1.Properties;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        

        string username1,password1,xc;
        Thread ad ;
        int er = 1;
        DirectoryEntry entry;
        DirectorySearcher search;
        Window3 w3;
        string serverad = "localhost";
        
        #region INITIALIZE WINDOW
        public MainWindow ()
        {
            InitializeComponent();

            if ( Settings.Default[ "exit" ].ToString() == "1" )
            {
                w3 = new Window3();
                w3.Show();
                this.Close();
            }

            else
            {
                var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey key = Registry.CurrentUser.OpenSubKey( path , true );
                key.SetValue( "ADsyncAPP" , System.IO.Path.GetDirectoryName( System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName )+"\\ADsyncApp.exe" );

                lbl.Visibility = Visibility.Hidden;
                username1 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                lbl.Content = username1;
                lbl.Visibility = Visibility.Visible;
                string pat = GetTempPath();
                BusyBar.IsBusy = false;
                System.IO.File.WriteAllText( pat + "\\My Log File.txt" , string.Empty );

                MessageBox.Show( Settings.Default[ "exit" ].ToString() );
            }
            
        } 
        #endregion

        #region CONNECT BUTTON
        private void Button_Click ( object sender , RoutedEventArgs e )
        {
            try
            {
                    ad = new Thread( connectad );

                    ad.Start();
                    BusyBar.IsBusy = true;

                    if ( er == 0 )
                    {
                        string connectedmsg = System.String.Format( "{0:G}: {1}." , System.DateTime.Now , "Connected" );
                        LogM(connectedmsg);
                        w3 = new Window3();
                        w3.Show();
                        this.Close();
                    }

                     
            }

            catch ( Exception ex )
            {
                MessageBox.Show("Exception just occurred: " + ex, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        } 
        #endregion

        #region CONNECT AD
        private void connectad ()
        {
            try
            {
                // CONNECT TO ACTIVE DIRECTORY
                //**********************************************************************************************************************//

                // username : string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //LDAP://localhost:398
                //password1 = "Swar123";
                string alDomains = DomainManager.DomainName;
                string domainnam = GetDomainDN(alDomains);

                entry = new DirectoryEntry( "LDAP://localhost/" + domainnam );
                search = new DirectorySearcher( entry );
                //----------------------------------------------------------------------------------------------------------------------//
                xc = entry.Properties.Count.ToString();

                er = 0;

                this.Dispatcher.Invoke( ( Action ) ( () =>
                {
                    BusyBar.IsBusy = false;
                } ) );
               
                //----------------------------------------------------------------------------------------------------------------------//

                
            }

            catch ( Exception ex )
            {
                this.Dispatcher.Invoke( ( Action ) ( () =>
                {
                    BusyBar.IsBusy = false;
                } ) );
                MessageBox.Show( "Problems connecting to Active - Directory..\n" +ex.Message, "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning );
                er = 1;
            }
        }
        #endregion

        #region BUSY SIGNAL
        private void Busy ()
        {
            this.Dispatcher.Invoke( ( Action ) ( () =>
            {
                BusyBar.IsBusy = true;
            } ) );
        }
        #endregion

        #region WRITE LOG
        public string GetTempPath ()
        {
            string path = System.Environment.GetEnvironmentVariable( "TEMP" );
            if ( !path.EndsWith( "\\" ) ) path += "\\";

            return path;
        }

        public void LogM ( string msg )
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                GetTempPath() + "My Log File.txt" );
            //MessageBox.Show( GetTempPath() );
            try
            {
                //sw.WriteLine(connectedmsg);
                string logLine = msg;
                sw.WriteLine( logLine );
            }
            finally
            {
                sw.Close();
            }
        } 
        #endregion

        #region GET DOMAIN NAME
        string GetDomainDN ( string domain )
        {
            DirectoryContext context = new DirectoryContext( DirectoryContextType.Domain , domain );
            Domain d = Domain.GetDomain( context );
            DirectoryEntry de = d.GetDirectoryEntry();
            return de.Properties[ "DistinguishedName" ].Value.ToString();
        }
        #endregion

        #region DOMAIN MANAGER
        public static class DomainManager
        {
            static DomainManager ()
            {
                Domain domain = null;
                DomainController domainController = null;
                try
                {
                    domain = Domain.GetCurrentDomain();
                    DomainName = domain.Name;
                    domainController = domain.PdcRoleOwner;
                    DomainControllerName = domainController.Name.Split( '.' )[ 0 ];
                    ComputerName = Environment.MachineName;
                }
                finally
                {
                    if ( domain != null )
                        domain.Dispose();
                    if ( domainController != null )
                        domainController.Dispose();
                }
            }

            public static string DomainControllerName { get; private set; }

            public static string ComputerName { get; private set; }

            public static string DomainName { get; private set; }

            public static string DomainPath
            {
                get
                {
                    bool bFirst = true;
                    StringBuilder sbReturn = new StringBuilder( 200 );
                    string[] strlstDc = DomainName.Split( '.' );
                    foreach ( string strDc in strlstDc )
                    {
                        if ( bFirst )
                        {
                            sbReturn.Append( "DC=" );
                            bFirst = false;
                        }
                        else
                            sbReturn.Append( ",DC=" );

                        sbReturn.Append( strDc );
                    }
                    return sbReturn.ToString();
                }
            }

            public static string RootPath
            {
                get
                {
                    return string.Format( "LDAP://{0}/{1}" , DomainName , DomainPath );
                }
            }
        }
        #endregion

    }
 }

