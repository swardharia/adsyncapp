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
using System.Windows.Shapes;
using WpfApplication1.Properties;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1 ()
        {
            InitializeComponent();

            lbbbbb1.Content = Settings.Default["userslabel"].ToString();
        }

        private void btttt1_Click ( object sender , RoutedEventArgs e )
        {

           
            Settings.Default[ "userslabel" ] = tbbbb1.Text.ToString();
            Settings.Default.Save();
            lbbbbb1.Content = Settings.Default[ "userslabel" ].ToString();
        }

        private void resssss1_Click ( object sender , RoutedEventArgs e )
        {

            //Settings.Default.PropertyValues[ "userslabel" ].SerializedValue = Settings.Default.Properties["userslabel"].DefaultValue ;
           // Settings.Default.Reset();
            //Settings.Default.PropertyValues[ "userslabel" ].Deserialized = false;
           // Settings.Default.Save();
            Settings.Default[ "userslabel" ] = Settings.Default.Properties[ "userslabel" ].DefaultValue;
            lbbbbb1.Content = Settings.Default[ "userslabel" ].ToString();
            Settings.Default.Save();
        }
    }
}
