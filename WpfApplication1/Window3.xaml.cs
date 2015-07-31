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
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Threading;
using System.Threading;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using WpfApplication1.Properties;

using vm = WpfApplication1;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {

        #region VARIABLES
        string n1 ,c1 , c2 , token , user , secret , usernameformat, unform, uniqueId ;
        int i = 3 , j = 2 , i11=1, chk, j2, j3=0;
        int i1 = 0 , same=0, j1=0;
        bool tf = true, unv=true;
        string ftoken , fuser , fsecret , fl;
        Thread th;

        DirectoryEntry entry;
        DirectorySearcher search;
        PrincipalContext principalContext;
        string serverad = "192.168.1.215";

        string getlog;
        DispatcherTimer disp = new DispatcherTimer();
       
        // ... A List.
        List<string> data = new List<string>();
        List<string> data1 = new List<string>();
        List<string> oupath = new List<string>();

        List<string> usernameField = new List<string>();
        List<string> usernameFL = new List<string>();
        List<Tuple<string , string>> adfields = new List<Tuple<string ,string>>();
        List<string> adf = new List<string>();
        List<Tuple<string , string>> adfields2 = new List<Tuple<string , string>>();
        List<Tuple<int,int>> fieldsinmap = new List<Tuple<int,int>>();
        List<int> usernameValue = new List<int>();
        List<string> usernameFirstLast = new List<string>();
        List<string> ous = new List<string>();

        string googleSearchText = @"{
                    'status': 200,
                    'result': [
                        {
                            'action': {
                                'userToken': '92cefd81131351911f88',
                                'type': 'insert'
                            },
                            'insert': {
                                'lasid': 75195,
                                'firstName': 'Owen',
                                'lastName': 'Montgomery',
                                'grade': 4,
                                'school': 'abcd',
                                'yog': 2015,
                                'homePhone': '(509) 315-9929',
                                'guardian': {
                                    'pg1': {
                                        'cellPhone': '(253) 312-7849',
                                        'email': 'debramontgomery0899@gmail.com'
                                    }
                                },
                                'attendance': 'Meadow  Ridge Elementary School',
                                'schoolCode': 'Montgomery',
                                'transportation': {
                                    'snowRoute': 'Montgomery'
                                },
                                'comm': 'Montgomery',
                                'record': {
                                    'type': 'student'
                                },
                                'userToken': '92cefd81131351911f88'
                            },
                            'update': null,
                            'timestamp': 1426009262,
                            '_id': '54ff2caeee60cbe16f8b46b1'
                        },
                        {
                                    'action': {
                                        'userToken': '92c57508b2dcc020bc5e',
                                        'type': 'insert'
                                    },
                                    'insert': {
                                        'lasid': 75170,
                                        'firstName': 'Alyssa',
                                        'lastName': 'Rodriguez',
                                        'grade': 7,
                                        'yog': 2015,
                                        'school': 'nusers',
                                        'homePhone': '(509) 999-0838',
                                        'guardian': {
                                            'pg1': {
                                                'email': 'CDROD@MSN.com'
                                            }
                                        },
                                        'attendance': 'Northwood Middle School',
                                        'schoolCode': 'Rodriguez',
                                        'transportation': {
                                            'snowRoute': 'Rodriguez'
                                        },
                                        'comm': 'Rodriguez',
                                        'record': {
                                            'type': 'student'
                                        },
                                        'userToken': '92c57508b2dcc020bc5e'
                                    },
                                    'update': null,
                                    'timestamp': 1426009328,
                                    '_id': '54ff2cf0ee60cb02718b463b'
                          },
                         
        
		                
	                ]
                } ";
            

        #endregion

        #region INITIALIZE WINDOW
        public Window3 ()
        {
            disp.Tick += new EventHandler( syapi );
            disp.Interval = new TimeSpan( 0 , 0 , 10 );
            data.Add( "FirstName" );
            data.Add( "LastName" );
            data.Add( "userToken" );
            usernameFL.Add( "First" );
            usernameFL.Add( "Last" );
            adf.Add("School");
            adf.Add( "Grade" );
            adf.Add( "Record_Type" );
            adf.Add("Year_Of_Graduation");
            data1.AddRange( new string[] { "Business-Category = businessCategory","Country-Name = c","carLicense = carLicense","Text-Country = co","User-Comment = comment","Company = company","Country-Code = countryCode","Department = department","departmentNumber = departmentNumber","Description = description","Display-Name = displayName","Employee-ID = employeeID","Employee-Number = employeeNumber","Employee-Type = employeeType","Phone-Home-Primary = homePhone","Address-Home = homePostalAddress","Comment = info","Organization-Name = o","Phone-Fax-Other = otherFacsimileTelephoneNumber","Phone-Home-Other = otherHomePhone","Other-Mailbox = otherMailbox","Phone-Mobile-Other = otherMobile","Phone-Pager-Other = otherPager","Phone-Office-Other = otherTelephone"} );

            string pat = GetTempPath();
            System.IO.File.WriteAllText( pat + "\\My Log File.txt" , string.Empty );

            InitializeComponent();

            j1 = Int32.Parse(Settings.Default[ "userslabel" ].ToString());
            userlbl.Content = j1;

            stopsyncbt.IsEnabled = false;

            tokentb.Text = Settings.Default["clienttoken"].ToString();
            usertb.Text = Settings.Default[ "user" ].ToString();
            secrettb.Text = Settings.Default[ "secret" ].ToString();
            connectad();
        } 
        #endregion

        private void hello(object o, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString());
        }

        #region VALIDATE CONTROLS
        public bool validate ()
        {
            if ( string.IsNullOrWhiteSpace( this.tokentb.Text ) )
            {
                tf = false;
                MessageBox.Show( "Enter Client Token" );
            }
            if ( string.IsNullOrWhiteSpace( this.usertb.Text ) )
            {
                tf = false;
                MessageBox.Show( "Enter User" );
            }
            if ( string.IsNullOrWhiteSpace( this.secrettb.Text ) )
            {
                tf = false;
                MessageBox.Show( "Enter Secret" );
            }
            if ( string.IsNullOrWhiteSpace( usernameformat ) )
            {
                tf = false;
                MessageBox.Show( "Select Username Format" );
            }
            else
            {
                tf = true;
            }
            return tf;
        } 
        #endregion

        #region USERNAME VALIDATE
        public bool unvalidate ()
        {
            int n11;
            unv = true;
            try
            {
                foreach ( UIElement element in g2.Children )
                {

                    if ( element is TextBox )
                    {
                        bool isnumb = int.TryParse( ( ( TextBox ) element ).Text , out n11 );
                        if ( string.IsNullOrWhiteSpace( ( ( TextBox ) element ).Text ) )
                        {
                            unv = false;
                        }
                        else if ( !isnumb )
                        {
                            unv = false;
                        }
                    }

                }

                return unv;
            }
            catch ( Exception ex ) { exc( ex ); return false; }
        }
        #endregion

        #region COMBO-BOX
        private void ComboBox_Loaded ( object sender , RoutedEventArgs e )
        {


            // ... Get the ComboBox reference.
            var comboBox1 = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox1.ItemsSource = data;

            // ... Make the first item selected.
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged ( object sender , SelectionChangedEventArgs e )
        {
            // ... Get the ComboBox.
            var comboBox1 = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            n1 = comboBox1.SelectedItem as string;
        }

        private void ComboBox1_Loaded ( object sender , RoutedEventArgs e )
        {


            // ... Get the ComboBox reference.
            var comboBox1 = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox1.ItemsSource = data1;

            // ... Make the first item selected.
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectionChanged ( object sender , SelectionChangedEventArgs e )
        {
            // ... Get the ComboBox.
            var comboBox1 = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            n1 = comboBox1.SelectedItem as string;
        }

        private void ComboBox2_Loaded ( object sender , RoutedEventArgs e )
        {


            // ... Get the ComboBox reference.
            var comboBox1 = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox1.ItemsSource = adf;

            // ... Make the first item selected.
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox2_SelectionChanged ( object sender , SelectionChangedEventArgs e )
        {
            // ... Get the ComboBox.
            var comboBox1 = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            n1 = comboBox1.SelectedItem as string;
        }

        private void ComboBox3_Loaded ( object sender , RoutedEventArgs e )
        {


            // ... Get the ComboBox reference.
            var comboBox1 = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox1.ItemsSource = oupath;

            // ... Make the first item selected.
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox3_SelectionChanged ( object sender , SelectionChangedEventArgs e )
        {
            // ... Get the ComboBox.
            var comboBox1 = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            n1 = comboBox1.SelectedItem as string;
        }

        private void ComboBoxFL_Loaded ( object sender , RoutedEventArgs e )
        {


            // ... Get the ComboBox reference.
            var comboBox1 = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox1.ItemsSource = usernameFL;

            // ... Make the first item selected.
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBoxFL_SelectionChanged ( object sender , SelectionChangedEventArgs e )
        {
            // ... Get the ComboBox.
            var comboBox1 = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            n1 = comboBox1.SelectedItem as string;
        } 

        #endregion

        #region USER-NAME FORMAT ADD-REMOVE FIELD
        private void createField ()
        {
            try
            {
                RowDefinition newrow = new RowDefinition();
                newrow.Height = new GridLength( 1 , GridUnitType.Auto );
                g2.RowDefinitions.Insert( g2.RowDefinitions.Count , newrow );

                ComboBox cb1 = new ComboBox();
                cb1.Width = 71;
                cb1.Margin = new Thickness( 22 , 10 , 0 , 0 );
                cb1.HorizontalAlignment = HorizontalAlignment.Left;
                cb1.VerticalAlignment = VerticalAlignment.Top;
                cb1.Loaded += ComboBox_Loaded;
                cb1.SelectionChanged += new SelectionChangedEventHandler( ComboBox_SelectionChanged );
                Grid.SetRow( cb1 , i );
                Grid.SetColumn( cb1 , 0 );
                g2.Children.Add( cb1 );
                cb1.Name = "cbb" + i;

                ComboBox cbFL1 = new ComboBox();
                cbFL1.Width = 103;
                cbFL1.Margin = new Thickness( 32.8 , 10.2 , 0 , 0 );
                cbFL1.HorizontalAlignment = HorizontalAlignment.Left;
                cbFL1.VerticalAlignment = VerticalAlignment.Top;
                cbFL1.Loaded += ComboBoxFL_Loaded;
                cbFL1.SelectionChanged += new SelectionChangedEventHandler( ComboBoxFL_SelectionChanged );
                Grid.SetRow( cbFL1 , i );
                Grid.SetColumn( cbFL1 , 1 );
                g2.Children.Add( cbFL1 );
                cb1.Name = "cbbFL" + i;

                TextBox tb11 = new TextBox();
                tb11.Text = "Characters ( min : 1 , max : full )";
                tb11.Width = 305;
                tb11.Height = 27;
                tb11.Margin = new Thickness( 33.8 , 10.2 , 0 , 0 );
                tb11.HorizontalAlignment = HorizontalAlignment.Right;
                tb11.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow( tb11 , i );
                Grid.SetColumn( tb11 , 1 );
                g2.Children.Add( tb11 );
                tb11.Name = "tbb" + i;

                i++;
               // MessageBox.Show( "i=" + ( i - 1 ).ToString() );
            }
            catch ( Exception ex )
            { exc( ex ); }
        } 

        private void addbtn_Click ( object sender , RoutedEventArgs e )
        {
            try
            {
                createField();
            }
            catch(Exception ex)
            { exc( ex ); }
        }

        private void removebtn_Click (object sender,RoutedEventArgs e)
        {
            try
            {

                int rowCount = g2.RowDefinitions.Count;

                if ( rowCount > 3 )
                {

                    List<UIElement> elementsToRemove = new List<UIElement>();

                    foreach ( UIElement element in g2.Children )
                    {

                        if ( Grid.GetRow( element ) == rowCount - 1 )

                            elementsToRemove.Add( element );

                    }

                    foreach ( UIElement element in elementsToRemove )

                        g2.Children.Remove( element );

                    g2.RowDefinitions.RemoveAt( rowCount - 1 );

                }
                i--;
                //MessageBox.Show( "row=" + ( g2.RowDefinitions.Count - 1 ).ToString() );
            }
            catch ( Exception ex ) { exc( ex ); }
        }

        #endregion
        
        #region SAVE USERNAME FORMAT BUTTON CLICK AND GET VALUES FROM CONTROLS
        public string GetValue ( Control x )
        {
            if ( x is TextBox ) return ( ( TextBox ) x ).Text;
            if ( x is ComboBox ) return ( ( ComboBox ) x ).SelectedValue.ToString();
            else return null;
        }

        private void unsave_Click ( object sender , RoutedEventArgs e )
        {
            saveuname();
        }
        #endregion

        #region SAVE USERNAME
        public void saveuname()
        {
            try
            {
                string un = ""; int jk = 0, jl=0;
                usernameformat = "";
                usernameField.Clear();
                usernameValue.Clear();

                foreach ( Control x in g2.Children )
                {
                    string field = GetValue( x );

                    if ( field == null )
                    {
                        continue;
                    }
                    else
                    {
                        if ( x is TextBox )
                        {
                            //check for length of fields from user len<int(field)
                            if ( int.TryParse( field , out chk ) )
                            {
                                if ( fl == "First" )
                                {
                                    usernameformat = usernameformat + un.Substring( 0 , Int32.Parse( field ) );
                                    int value = Int32.Parse( field );
                                    usernameValue.Add( value );
                                    jk++;
                                }
                                else
                                {
                                    usernameformat = usernameformat + un.Substring( usernameField[jl-1].Length - Int32.Parse( field ) , Int32.Parse( field ) );
                                    int value = Int32.Parse( field );
                                    usernameValue.Add( value );
                                    jk++;
                                }
                            }

                            else
                            {
                                MessageBox.Show( "Enter proper Format" );
                                break;
                            }
                        }
                        else if ( x is ComboBox )
                        {
                            if((field=="First") || (field=="Last"))
                            {
                                fl = field;
                                usernameFirstLast.Add(fl);
                            }
                            else
                            {
                                un = field;
                                jl++;
                                usernameField.Add( field );
                            }
                        }
                    }
                }
                //MessageBox.Show( "Format Saved : " + usernameformat );
                // MessageBox.Show( "fields: "+usernameField.Count.ToString() );
            }



            catch ( Exception ex ) { exc( ex ); }
        }
        #endregion

        #region SAVE - RESET
        private void reset_Click ( object sender , RoutedEventArgs e )
        {
        
            try
            {
                tokentb.Clear();
                usertb.Clear();
                secrettb.Clear();
                Settings.Default[ "userslabel" ] = Settings.Default.Properties[ "userslabel" ].DefaultValue;
                Settings.Default[ "clienttoken" ] = Settings.Default.Properties[ "clienttoken" ].DefaultValue;
                Settings.Default[ "user" ] = Settings.Default.Properties[ "user" ].DefaultValue;
                Settings.Default["secret" ] = Settings.Default.Properties[ "secret" ].DefaultValue;
                userlbl.Content = Settings.Default[ "userslabel" ].ToString();
                tokentb.Text = Settings.Default[ "clienttoken" ].ToString();
                usertb.Text = Settings.Default[ "user" ].ToString();
                secrettb.Text = Settings.Default[ "secret" ].ToString();
                Settings.Default.Save();

                foreach ( Control x in g2.Children )
                {
                    if ( x is TextBox )
                        ( ( TextBox ) x ).Clear();
                }

                MessageBox.Show( "Cleared !" );
            }
            catch ( Exception ex )
            {
                exc( ex );
            }
        }

        //private void save_Click ( object sender , RoutedEventArgs e )
        //{
        //    bool v = validate();
        //    saveuname();
        //    if ( v )
        //    {
        //        token = tokentb.Text.ToString();
        //        user = usertb.Text.ToString();
        //        secret = secrettb.Text.ToString();
        //        c1 = tb31.Text.ToString();
        //        c2 = tb32.Text.ToString();
        //        string c3 = n1.Substring( 0 , Convert.ToInt16( c1 , 16 ) );
        //        string c4 = "";//.Substring( 0 , Convert.ToInt16( c2 , 16 ) );
        //        string msg = "token : " + token + "\n user : " + user + "\n secret : " + secret + "\n c1 : " + c3 + "\n c2 : " + c4;
        //        MessageBox.Show( msg );
        //    }
        //} 
        #endregion

        #region OU FIELDS MAPPING
        private void addmapbtn3_Click ( object sender , RoutedEventArgs e )
        {

            try
            {
                createField1();
            }
            catch ( Exception ex )
            {
                exc(ex);
            }
        }

        private void createField1 ()
        {

            RowDefinition newrow = new RowDefinition();
            newrow.Height = new GridLength( 1 , GridUnitType.Auto );
            g3.RowDefinitions.Insert( g3.RowDefinitions.Count , newrow );

            //Label lb21=new Label();
            //lb21.Content="Mapping Name: ";
            //Grid.SetRow(lb21,j);
            //Grid.SetColumn(lb21,0);
            //g3.Children.Add(lb21);

            TextBox tb21 = new TextBox();
            //tb21.Text = "Characters ( min : 1 , max : full )";
            tb21.Width = 166;
            tb21.Margin = new Thickness( 22 , 15 , 0 , 0 );
            tb21.HorizontalAlignment = HorizontalAlignment.Left;
            tb21.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow( tb21 , j );
            Grid.SetColumn( tb21 , 0 );
            g3.Children.Add( tb21 );
            tb21.Name = "tbfld" + j;
            j2 = j;
            j++;

            RowDefinition newrow1 = new RowDefinition();
            newrow1.Height = new GridLength( 1 , GridUnitType.Auto );
            g3.RowDefinitions.Insert( g3.RowDefinitions.Count , newrow1 );

            ComboBox cb1 = new ComboBox();
            cb1.Width = 166;
            cb1.Margin = new Thickness( 22 , 10 , 0 , 0 );
            cb1.HorizontalAlignment = HorizontalAlignment.Left;
            cb1.VerticalAlignment = VerticalAlignment.Top;
            cb1.Loaded += ComboBox2_Loaded;
            cb1.SelectionChanged += new SelectionChangedEventHandler( ComboBox2_SelectionChanged );
            Grid.SetRow( cb1 , j );
            Grid.SetColumn( cb1 , 0 );
            g3.Children.Add( cb1 );
            cb1.Name = "mcbbapi" + j;

            TextBox tb22 = new TextBox();
            //tb21.Text = "Characters ( min : 1 , max : full )";
            tb22.Height = 27;
            tb22.Width = 136;
            tb22.Margin = new Thickness( 18 , 10 , 0 , 0 );
            tb22.HorizontalAlignment = HorizontalAlignment.Left;
            tb22.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow( tb22 , j );
            Grid.SetColumn( tb22 , 1 );
            g3.Children.Add( tb22 );
            tb22.Name = "tbval" + j;

            ComboBox tb11 = new ComboBox();
            tb11.Width = 191;
            tb11.Margin = new Thickness( 144.4 , 10 , 0 , 0 );
            tb11.HorizontalAlignment = HorizontalAlignment.Right;
            tb11.VerticalAlignment = VerticalAlignment.Top;
            tb11.Loaded += ComboBox3_Loaded;
            tb11.SelectionChanged += new SelectionChangedEventHandler( ComboBox3_SelectionChanged );
            Grid.SetRow( tb11 , j );
            Grid.SetColumn( tb11 , 1 );
            g3.Children.Add( tb11 );
            tb11.Name = "mcbbad" + j;

            fieldsinmap.Add( new Tuple<int , int>( j2 , j3 + 1 ) );
            j3++;

            j++;

        }

        private void addfieldbtn3_Click ( object sender , RoutedEventArgs e )
        {
            RowDefinition newrow1 = new RowDefinition();
            newrow1.Height = new GridLength( 1 , GridUnitType.Auto );
            g3.RowDefinitions.Insert( g3.RowDefinitions.Count , newrow1 );

            ComboBox cb2 = new ComboBox();
            cb2.Width = 166;
            cb2.Margin = new Thickness( 22 , 10 , 0 , 0 );
            cb2.HorizontalAlignment = HorizontalAlignment.Left;
            cb2.VerticalAlignment = VerticalAlignment.Top;
            cb2.Loaded += ComboBox2_Loaded;
            cb2.SelectionChanged += new SelectionChangedEventHandler( ComboBox2_SelectionChanged );
            Grid.SetRow( cb2 , j );
            Grid.SetColumn( cb2 , 0 );
            g3.Children.Add( cb2 );
            cb2.Name = "mfield" + j;
            fieldsinmap.Add( new Tuple<int , int>( j2 , j3 + 1 ) );

            TextBox tb23 = new TextBox();
            //tb21.Text = "Characters ( min : 1 , max : full )";
            tb23.Height = 27;
            tb23.Width = 136;
            tb23.Margin = new Thickness( 18 , 10 , 0 , 0 );
            tb23.HorizontalAlignment = HorizontalAlignment.Left;
            tb23.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow( tb23 , j );
            Grid.SetColumn( tb23 , 1 );
            g3.Children.Add( tb23 );
            tb23.Name = "tbb22" + j;

            j++;
        }

        private void removebtn3_Click ( object sender , RoutedEventArgs e )
        {
            try
            {
                
                int rowCount = g3.RowDefinitions.Count;

                if ( rowCount > 2 && j2>1)
                {

                    List<UIElement> elementsToRemove = new List<UIElement>();

                    foreach ( UIElement element in g3.Children )
                    {

                        if ( Grid.GetRow( element ) >= j2 )

                            elementsToRemove.Add( element );

                    }

                    foreach ( UIElement element in elementsToRemove )

                        g3.Children.Remove( element );

                    g3.RowDefinitions.RemoveAt( rowCount - 1 );

                }
                j=j2;
                j2--;
            }
            catch ( Exception ex )
            {
                exc( ex );
            }
        } 
        #endregion

   /*     #region GET MAPPING

        public void getmap()
        {
            foreach ( Control x in g2.Children )
            {
                string field = GetValue( x );

                if ( field == null )
                {
                    continue;
                }
                else
                {
                    if ( x is TextBox )
                    {
                        //check for length of fields from user len<int(field)
                        if ( int.TryParse( field , out chk ) )
                        {
                            if ( fl == "First" )
                            {
                                usernameformat = usernameformat + un.Substring( 0 , Int32.Parse( field ) );
                                int value = Int32.Parse( field );
                                usernameValue.Add( value );
                                jk++;
                            }
                            else
                            {
                                usernameformat = usernameformat + un.Substring( usernameField[ jl - 1 ].Length - Int32.Parse( field ) , Int32.Parse( field ) );
                                int value = Int32.Parse( field );
                                usernameValue.Add( value );
                                jk++;
                            }
                        }

                        else
                        {
                            MessageBox.Show( "Enter proper Format" );
                            break;
                        }
                    }
                    else if ( x is ComboBox )
                    {
                        if ( ( field == "First" ) || ( field == "Last" ) )
                        {
                            fl = field;
                            usernameFirstLast.Add( fl );
                        }
                        else
                        {
                            un = field;
                            jl++;
                            usernameField.Add( field );
                        }
                    }
                }
            }
        }
        #endregion
        */

        #region MAPPING
        //private void addbtn1_Click ( object sender , RoutedEventArgs e )
        //{

        //    try
        //    {
        //        createField1();
        //    }
        //    catch ( Exception ex )
        //    {
        //        exc( ex );
        //    }
        //}

        //private void createField1 ()
        //{

        //    RowDefinition newrow = new RowDefinition();
        //    newrow.Height = new GridLength( 1 , GridUnitType.Auto );
        //    g3.RowDefinitions.Insert( g3.RowDefinitions.Count , newrow );

        //    ComboBox cb1 = new ComboBox();
        //    cb1.Width = 166;
        //    cb1.Margin = new Thickness( 22 , 10 , 0 , 0 );
        //    cb1.HorizontalAlignment = HorizontalAlignment.Left;
        //    cb1.VerticalAlignment = VerticalAlignment.Top;
        //    cb1.Loaded += ComboBox2_Loaded;
        //    cb1.SelectionChanged += new SelectionChangedEventHandler( ComboBox2_SelectionChanged );
        //    Grid.SetRow( cb1 , j );
        //    Grid.SetColumn( cb1 , 0 );
        //    g3.Children.Add( cb1 );
        //    cb1.Name = "mcbbapi" + j;

        //    ComboBox tb11 = new ComboBox();
        //    tb11.Width = 250;
        //    tb11.Margin = new Thickness( 33.8 , 10 , 0 , 0 );
        //    tb11.HorizontalAlignment = HorizontalAlignment.Left;
        //    tb11.VerticalAlignment = VerticalAlignment.Top;
        //    tb11.Loaded += ComboBox3_Loaded;
        //    tb11.SelectionChanged += new SelectionChangedEventHandler( ComboBox3_SelectionChanged );
        //    Grid.SetRow( tb11 , j );
        //    Grid.SetColumn( tb11 , 1 );
        //    g3.Children.Add( tb11 );
        //    tb11.Name = "mcbbad" + j;

        //    j++;

        //}

        //private void removebtn3_Click ( object sender , RoutedEventArgs e )
        //{
        //    try
        //    {
        //        int rowCount = g3.RowDefinitions.Count;

        //        if ( rowCount > 2 )
        //        {

        //            List<UIElement> elementsToRemove = new List<UIElement>();

        //            foreach ( UIElement element in g3.Children )
        //            {

        //                if ( Grid.GetRow( element ) == rowCount - 1 )

        //                    elementsToRemove.Add( element );

        //            }

        //            foreach ( UIElement element in elementsToRemove )

        //                g3.Children.Remove( element );

        //            g3.RowDefinitions.RemoveAt( rowCount - 1 );

        //        }
        //        j--;
        //    }
        //    catch ( Exception ex )
        //    {
        //        exc( ex );
        //    }
        //}
        #endregion

        #region EXCEPTION MESSAGE

        public void exc(Exception ex)
        {
            MessageBox.Show( "Exception just occurred: " + ex.Message , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning );
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

                string password1 = "Swardharia4.";
                //LDAP://localhost:398
                entry = new DirectoryEntry( "LDAP://" + serverad + "/DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , password1 , AuthenticationTypes.Secure );
                search = new DirectorySearcher( entry );
                //----------------------------------------------------------------------------------------------------------------------//
                // 
                //dynamic newgoogleSearch = JObject.Parse( googleSearchText );
                //var values = newgoogleSearch.result[ 0 ].insert;
                //var jar = values.ToObject<Dictionary<string , object>>();
                //int cnt = jar.Count;

                //foreach ( KeyValuePair<string , object> kvp in jar )
                //{
                //    //var exis=newgoogleSearch.result[0].insert.Property("Name");
                //    if ( !kvp.Value.ToString().Contains( '{' ) )
                //    {
                //        LogM( kvp.Key + " : " + kvp.Value );
                //        adf.Add( kvp.Key.ToString() );
                //    }
                //}

                // LIST USERS AND OU IN ACTIVE DIRECTORY
                //**********************************************************************************************************************//

                search.Filter = ( "(&(objectCategory=person)(objectClass=user)(!sAMAccountType=805306370))" );
                LogM( "Listing of Users in the Active Directory" );
                LogM( "============================================" );

                foreach ( SearchResult resEnt in search.FindAll() )
                {
                    LogM( resEnt.GetDirectoryEntry().Name.ToString() );
                }

                LogM( "=========== End of Listing =============\n \n" );
                LogM( "" );


                search.Filter = ( "(objectClass=organizationalUnit)" );
                LogM( "Listing of Organisational-Units in the Active Directory" );
                LogM( "============================================" );

                foreach ( SearchResult resEnt in search.FindAll() )
                {
                    oupath.Add( resEnt.GetDirectoryEntry().Path.ToString() );
                    ous.Add( resEnt.GetDirectoryEntry().Name.ToString());
                    LogM( resEnt.GetDirectoryEntry().Name.ToString() + "\n" );
                    LogM( resEnt.GetDirectoryEntry().Parent.Path.ToString() + "-------------------\n" );
                }
                LogM( "=========== End of Listing =============\n" );
                LogM( "" );

                //----------------------------------------------------------------------------------------------------------------------//

              //  LogM("connected"+entry.ToString());
                ous.Remove( "OU=defaultOU" );
            }

            catch ( Exception ex )
            {
                MessageBox.Show( "Problems connecting to Active - Directory : "+ex.Message , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning );
            }
        }
        #endregion

        #region CONNECT API
        public void connectapi()
        {
            

            //   accesstoken request format  : https://api.simpler.im/{clientToken}/{type}/{endpoint}?queryString
            var client1 = new WebClient();
            ftoken = tokentb.Text;    // "8023e6e1cpb6e3825202nh4ccfah5d45ftsh033";
            fuser = usertb.Text;      //"api_Z26xY6L5vjnGbtV0dHD81d2seIfCWV";
            fsecret = secrettb.Text;  //"itCpJXetUICkRfWE27Ru5Eq9DNZ1V2X9GUjpZyFo";
            string tokenrequest = "https://api.simpler.im/" + ftoken + "/user/authorize?user=" + fuser + "&secret=" + fsecret;

            // string accessToken = "7e4c256ca2cf77675139ca3ea1ad19488ac81563c2f8aaf5d1aac3ed1a51";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string html1 = client1.DownloadString( tokenrequest );

            JObject jobj = JObject.Parse( html1 );

            Acess ac = jobj.ToObject<Acess>();

            string accesstokenstr = ac.AccessToken;

            getlog = "https://api-dev.simpler.im/" + ftoken + "/demographic/log?method=new&app=1234&accessToken=" + accesstokenstr;


            //************************************************************************************************//

            //      HTTP put demographic logs
            //  https://api-dev.simpler.im/8023e6e1cpb6e3825202nh4ccfah5d45ftsh033/demographic/log?method=ack&app=1234&accessToken=7e4c256ca2cf77675139ca3ea1ad19488ac81563c2f8aaf5d1aac3ed1a51

            /*    using ( var client = new System.Net.WebClient() )
                {
                    client.UploadData( address , "PUT" , data );
                }
             */

            //************************************************************************************************//

            var client = new WebClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            if ( googleSearchText != "" )
            {
                //string html = client.DownloadString( getlog );
                //string s2 = html.Replace( "'" , " " );
                //string googleSearchText = s2.Replace( "\"" , "'" );

                //************************************************************************************************//


                // string googleSearchText;
                JObject googleSearch = JObject.Parse( googleSearchText );
                dynamic newgoogleSearch = JObject.Parse( googleSearchText );
                var values = newgoogleSearch.result[ 0 ].insert;

                // get JSON result objects into a list
                IList<JToken> results = googleSearch[ "result" ].Children().ToList();

                // serialize JSON results into .NET objects
                IList<SearchResult1> searchResults = new List<SearchResult1>();

                foreach ( JToken result in results )
                {
                    string fn1 = "" , ln1 = "" , type = "" , scl1 = "" , ph1 = "" , em1 = "";
                    int yog = 0;
                    int grd1 = 0 ;

                    SearchResult1 searchResult = googleSearch[ "result" ][ i1 ][ "insert" ].ToObject<SearchResult1>();
                    uniqueId = ( string ) googleSearch[ "result" ][ i1 ][ "_id" ];

                    string m1 = searchResult.firstName;
                    string m2 = searchResult.lastName;
                    string m3 = searchResult.school;
                    string usrtoken = searchResult.userToken.ToString();

                    if ( ( m1 != null ) && ( m2 != null ) && ( m3 != null ) )
                    {
                        searchResults.Add( searchResult );


                        fn1 = searchResults[ i1 ].firstName.ToString();
                        ln1 = searchResults[ i1 ].lastName.ToString();
                        scl1 = searchResults[ i1 ].school.ToString();
                        grd1 = Int32.Parse( searchResults[ i1 ].grade.ToString());
                        type = newgoogleSearch.result[ 0 ].insert.record.type;
                        yog = newgoogleSearch.result[ 0 ].insert.yog; 

                        LogM( String.Format( "{0,-10} : {1}" , "id" , searchResults[ i1 ].lasid.ToString() ) );
                        LogM( String.Format( "{0,-10} : {1} " , "First Name" , fn1 ) );
                        LogM( String.Format( "{0,-10} : {1}" , "Last Name" , ln1 ) );
                        LogM( String.Format( "{0,-10} : {1}" , "School" , scl1 ) );

                        if ( ( searchResult.homePhone != null ) )
                        {
                            ph1 = searchResults[ i1 ].homePhone.ToString();
                        }

                        if ( ( searchResult.guardian != null ) && ( searchResult.guardian.pg1.email != null ) )
                        {
                            em1 = searchResults[ i1 ].guardian.pg1.email.ToString();
                        }

                        if ( ( searchResult.guardian != null ) && ( searchResult.guardian.pg1.cellPhone != null ) )
                        {
                            LogM( String.Format( "{0,-10} : {1}" , "Cell Phone" , searchResults[ i1 ].guardian.pg1.cellPhone.ToString() ) );
                        }

                        LogM( String.Format( "{0,-10} : {1}" , "Phone" , ph1 ) );
                        LogM( String.Format( "{0,-10} : {1}" , "Email" , em1 ) );

                        try
                        {
                            if ( !userexist( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken) )
                            {
                                addUser( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken, grd1,type,yog);
                                i1++;
                                j1++;
                            }
                            else
                            {
                               // MessageBox.Show( "user exist" );
                                addSameUser( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken , grd1,type,yog);
                                i1++;
                                j1++;
                                continue;
                            }
                            //addUser( fn1 , ln1 , ph1 , em1 , scl1 );
                        }
                        catch ( Exception ex )
                        {
                            Application.Current.Dispatcher.Invoke(() =>MessageBox.Show( "Problems adding user to Active - Directory : " + ex , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning ));
                        }

                    }

                    else
                        i1++;
                    continue;
                }
                googleSearchText = "";
                Application.Current.Dispatcher.Invoke(() =>MessageBox.Show( "Total Records inserted =" + j1 ));
            }
            else 
            {
                Application.Current.Dispatcher.Invoke(() =>MessageBox.Show("No data available right now !"));
            }
        }
        #endregion

        #region JSON CLASSES
        //************************************************************************************************//

        public class SearchResult1
        {
            //  public string Title { get; set; }
            public int lasid { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string school { get; set; }
            public string homePhone { get; set; }
            public Guardian guardian { get; set; }
            public string userToken { get; set; }
            public string grade { get; set; }

            public class Pg1
            {
                public string email { get; set; }
                public string cellPhone { get; set; }

                public Pg1 ()
                {
                    this.email = null;
                    this.cellPhone = null;
                }
            }

            public class Guardian
            {
                public Pg1 pg1 { get; set; }

                public Guardian ()
                {
                    this.pg1 = null;
                }
            }



            public SearchResult1 ()
            {
                this.lasid = 0;
                this.firstName = null;
                this.lastName = null;
                this.school = null;
                this.homePhone = null;
                this.guardian = null;
                //this.guardian.pg1.email = null;
            }
        }

        //************************************************************************************************//

        public class Acess
        {
            public string status { get; set; }
            public string AccessToken { get; set; }
            public string expires { get; set; }

            public Acess ()
            {
                this.status = null;
                this.expires = null;
                this.AccessToken = null;
            }
        }

        //************************************************************************************************//
          

        #endregion

        #region SYNC BUTTON
        private void synbt_Click ( object sender , RoutedEventArgs e )
        {
            try
            {
                th = new Thread(() => syapi(this,e));
                th.Start();
                disp.Start();
            }
            catch ( Exception ex ) { exc(ex); }
        }
        #endregion

        #region EXTRA
        public void sample()
        {

        }
        #endregion

        #region SYNC FROM API
        private void syapi(object o,EventArgs e)
        {
            this.Dispatcher.Invoke( ( Action ) ( () =>
            {
                Settings.Default[ "clienttoken" ] = tokentb.Text.ToString();
                Settings.Default[ "user" ] = usertb.Text.ToString();
                Settings.Default[ "secret" ] = secrettb.Text.ToString();
                Settings.Default.Save();
                reset.IsEnabled = false;
                usertb.IsEnabled = false;
                tokentb.IsEnabled = false;
                secrettb.IsEnabled = false;
                synbt.Content = "Sync in Progress .. ";
                foreach ( UIElement element in g2.Children )
                {
                    element.IsEnabled = false;
                }
                foreach ( UIElement element in g3.Children )
                {
                    element.IsEnabled = false;
                }
                unform = "";
                saveuname();
                bool v1 = validate();
                bool v2 = unvalidate();

                if ( v1 && v2 )
                {
                    // connectad();
                    connectapi();
                    token = tokentb.Text.ToString();
                    user = usertb.Text.ToString();
                    secret = secrettb.Text.ToString();
                    c1 = tb31.Text.ToString();
                    c2 = tb32.Text.ToString();
                    string msg = "token : " + token + "\n user : " + user + "\n secret : " + secret + "\n username fields : " + ( i - 1 ) + "\n mapping fields : " + ( j - 2 ) + "\n";
                    for ( int p = 0 ; p < usernameField.Count ; p++ )
                    {
                        msg = msg + " c" + p + ": " + usernameField[ p ].Substring( 0 , usernameValue[ p ] ) + "\n";
                        unform = unform + usernameField[ p ].Substring( 0 , usernameValue[ p ] );
                    }
                    LogM( msg );
                }

                userlbl.Content = j1;
                Settings.Default[ "userslabel" ] = j1.ToString();
                Settings.Default.Save();

                stopsyncbt.IsEnabled = true;

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
            try
            {
                // string logLine = System.String.Format("{0:G}: {1}." , System.DateTime.Now , msg );
                string logLine = msg;
                sw.WriteLine( logLine );
            }
            finally
            {
                sw.Close();
            }
        }
        #endregion


        /*      #region SEARCH BUTTON
        private void searchbtn_Click ( object sender , RoutedEventArgs e )
        {
            
            string infoo = searchtb.Text;

            search.Filter = "(cn=" + infoo + ")";

            // create results objects from search object  

            // create an array of properties that we would like and  
            // add them to the search object  

            string[] requiredProperties = new string[] { "givenname" , "sn" , "cn" , "samaccountname" , "mail" , "telephone" , "distinguishedname" , "objectclass" , "whencreated" , "userprincipalname" };

            foreach ( String property in requiredProperties )
                search.PropertiesToLoad.Add( property );

            try
            {
            SearchResult result1 = search.FindOne();

            tblock.Visibility = Visibility.Visible;
            string sres="";
            if ( result1 != null )
            {
                foreach ( String property in requiredProperties )
                    foreach ( Object myCollection in result1.Properties[ property ] )
                    {
                        sres += ( String.Format( "{0,-20} : {1}\n" ,
                                          property , myCollection.ToString()) );
                        
                    }
            }
            tblock.Text = sres;}
            catch ( Exception ex )
            {
                MessageBox.Show( "Problems searching in Active - Directory : " + ex.Message , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning );
            }
        }
        #endregion
        */

        #region GET LDAP PATH
        public string ldappath(string School, int Grade, int Year_Of_Graduation, string Record_Type)
        {
            string ldapp = ""; int ic = -1,num=0;

            foreach ( Control x in g3.Children )
            {
                string field = GetValue( x );

                if ( field == null )
                {
                    continue;
                }
                else
                {
                    if ( x is TextBox )
                    {
                        if ( x.Name.ToString().Contains("tbfld") ) 
                        {
                            ic++;
                            num=fieldsinmap[ ic ].Item2;
                        }
                        //check for length of fields from user len<int(field)
                        if ( int.TryParse( field , out chk ) )
                        {
                            if ( fl == "First" )
                            {
                                usernameformat = usernameformat + un.Substring( 0 , Int32.Parse( field ) );
                                int value = Int32.Parse( field );
                                usernameValue.Add( value );
                                jk++;
                            }
                            else
                            {
                                usernameformat = usernameformat + un.Substring( usernameField[ jl - 1 ].Length - Int32.Parse( field ) , Int32.Parse( field ) );
                                int value = Int32.Parse( field );
                                usernameValue.Add( value );
                                jk++;
                            }
                        }

                        else
                        {
                            MessageBox.Show( "Enter proper Format" );
                            break;
                        }
                    }
                    else if ( x is ComboBox )
                    {
                            un = field;
                            jl++;
                            usernameField.Add( field );
                    }
                }
            }

            return ldapp;
        }
        #endregion

        #region SEARCH FOR USER
        public bool userexist ( string firstname1 , string lastname1 , string phoneno1 , string eid1 , string scll1 , string tkn1 )
        {
            string upname = "";
            string infoo = firstname1 + " " + lastname1;

            adfields2.Add( new Tuple<string , string>( "FirstName" , firstname1 ) );
            adfields2.Add( new Tuple<string , string>( "LastName" , lastname1 ) );
            adfields2.Add( new Tuple<string , string>( "phoneNum" , phoneno1 ) );
            adfields2.Add( new Tuple<string , string>( "UserId" , "" ) );
            adfields2.Add( new Tuple<string , string>( "Phone" , "" ) );
            adfields2.Add( new Tuple<string , string>( "Email" , eid1 ) );
            adfields2.Add( new Tuple<string , string>( "userToken" , tkn1 ) );

            for ( int g11 = 0 ; g11 < usernameField.Count ; g11++ )
            {
                for ( int g12 = 0 ; g12 < adfields2.Count ; g12++ )
                {
                    if ( usernameField[ g11 ] == adfields2[ g12 ].Item1 )
                    {
                        if ( usernameFirstLast[ g11 ] == "First" )
                        {
                            if ( ( adfields2[ g12 ].Item2.Length ) > ( usernameValue[ g11 ] ) )
                            {
                                upname = upname + adfields2[ g12 ].Item2.Substring( 0 , usernameValue[ g11 ] );
                            }
                            else
                            {
                                upname = upname + adfields2[ g12 ].Item2;
                            }
                            break;
                        }
                        else
                        {
                            if ( ( adfields2[ g12 ].Item2.Length ) > ( usernameValue[ g11 ] ) )
                            {
                                upname = upname + adfields2[ g12 ].Item2.Substring( adfields2[ g12 ].Item2.Length - usernameValue[ g11 ] , usernameValue[ g11 ] );
                                //userLogonName = upn;
                            }
                            else
                            {
                                upname = upname + adfields[ g12 ].Item2;
                                //userLogonName = upn;
                            }

                            break;
                        }
                    }
                }

            }
            adfields2.Clear();
            PrincipalContext principalContext1 = null;

            //if ( ous.Contains( "OU="+scll1 ) )
            //{
            //    principalContext1 = new PrincipalContext( ContextType.Domain , serverad , "OU=" + scll1 + ",DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            //}
            //else
            //{
            //    principalContext1 = new PrincipalContext( ContextType.Domain , serverad , "OU=defaultOU,DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            //} 

            principalContext1 = new PrincipalContext( ContextType.Domain , serverad , "DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            UserPrincipal usr12 = UserPrincipal.FindByIdentity( principalContext1 , IdentityType.SamAccountName , upname );
            
            try
            {

                if ( usr12 != null )
                {
                   // MessageBox.Show( "user exist : " + usr12.ToString() );
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( "Problems searching in Active - Directory : " + ex.Message , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning );
                return false;
            }


        }
        #endregion

        #region ADD USER TO AD
        public void addUser (string firstn,string lastn,string phonen,string eid,string scl1,string token1,int grade,string type1,int yog)
        {

            ldappath(scl1,grade,yog,type1);

            if ( ous.Contains("OU="+scl1) )
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU="+scl1+",DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            }
            else
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=defaultOU,DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            }
            //string[] words = sear.Split(' ');
            adfields.Add( new Tuple<string , string>( "FirstName" , firstn ) );
            adfields.Add( new Tuple<string , string>( "LastName" , lastn ) );
            adfields.Add( new Tuple<string , string>( "phoneNum" , phonen ) );
            adfields.Add( new Tuple<string , string>( "UserId" , "" ) );
            adfields.Add( new Tuple<string , string>( "Phone" , "" ) );
            adfields.Add( new Tuple<string , string>( "Email" , eid ) );
            adfields.Add( new Tuple<string , string>( "userToken" , token1 ) );
            string FirstName = adfields[0].Item2;
            string LastName = adfields[ 1 ].Item2;
            string phoneNum = adfields[ 2 ].Item2;
            string UserId = adfields[ 3 ].Item2;
            string Phone = adfields[ 4 ].Item2;
            string userLogonName = "";
            string emailAddress = adfields[5].Item2;//FirstName + LastName.Substring( 0 , 1 ) + "@swardharia.local";
            string upn="";
            for ( int g3 = 0 ; g3 < usernameField.Count ;g3++ )
            {
                for ( int g4 = 0 ; g4 < adfields.Count ; g4++ )
                {
                    if ( usernameField[ g3 ] == adfields[ g4 ].Item1 )
                    {
                        if(usernameFirstLast[g3]=="First")
                        {
                                if((adfields[g4].Item2.Length) > (usernameValue[g3]))
                                {
                                    upn = upn + adfields[ g4 ].Item2.Substring( 0 , usernameValue[ g3 ] );
                                    userLogonName = upn;
                                }
                                else
                                {
                                    upn = upn + adfields[ g4 ].Item2;
                                    userLogonName = upn;
                                }

                                break;
                        }
                        else
                        {
                            if ( ( adfields[ g4 ].Item2.Length ) > ( usernameValue[ g3 ] ) )
                            {
                                upn = upn + adfields[ g4 ].Item2.Substring( adfields[ g4 ].Item2.Length-usernameValue[g3] , usernameValue[ g3 ] );
                                userLogonName = upn;
                            }
                            else
                            {
                                upn = upn + adfields[ g4 ].Item2;
                                userLogonName = upn;
                            }

                            break;
                        }
                    }
                }
                
            }
            upn=upn+ "@swardharia.local";
           // Application.Current.Dispatcher.Invoke(() => MessageBox.Show(upn));
            UserPrincipal userPrincipal = new UserPrincipal( principalContext );

            if ( LastName != null && LastName.Length > 0 )
                userPrincipal.Surname = LastName;

            if ( FirstName != null && FirstName.Length > 0 )
                userPrincipal.GivenName = FirstName;

            if ( phoneNum != null && phoneNum.Length > 0 )
                userPrincipal.VoiceTelephoneNumber = phoneNum;


            if ( userLogonName != null && userLogonName.Length > 0 )
                userPrincipal.SamAccountName = userLogonName.Trim();

            if ( upn != null && upn.Length > 0 )
                userPrincipal.UserPrincipalName = upn;

            if ( emailAddress != null && emailAddress.Length > 0 )
                userPrincipal.EmailAddress = emailAddress;



            string pwdOfNewlyCreatedUser = "abcde@@"+j1;
            userPrincipal.SetPassword( pwdOfNewlyCreatedUser );

            userPrincipal.Enabled = true;
            userPrincipal.ExpirePasswordNow();
           
            try
            {
                userPrincipal.Save();
                //MessageBox.Show( "New User created\n" );
                adfields.Clear();
            }
            catch ( Exception ex )
            {
               Application.Current.Dispatcher.Invoke(() =>MessageBox.Show( "Exception creating user object. " + ex.Message ));
                //   return false;
            }
        }
        #endregion

        #region ADD USER WITH CONFLICTING CREDENTIALS TO AD
        public void addSameUser ( string firstn , string lastn , string phonen , string eid , string scl1 , string token1 , int grade, string type, int yog)
        {
            
            if ( ous.Contains( "OU=" + scl1 ) )
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=" + scl1 + ",DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            }
            else
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=defaultOU,DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            }
            //string[] words = sear.Split(' ');
            adfields.Add( new Tuple<string , string>( "FirstName" , firstn ) );
            adfields.Add( new Tuple<string , string>( "LastName" , lastn ) );
            adfields.Add( new Tuple<string , string>( "phoneNum" , phonen ) );
            adfields.Add( new Tuple<string , string>( "UserId" , "" ) );
            adfields.Add( new Tuple<string , string>( "Phone" , "" ) );
            adfields.Add( new Tuple<string , string>( "Email" , eid ) );
            adfields.Add( new Tuple<string , string>( "userToken" , token1 ) );
            string FirstName = adfields[ 0 ].Item2;
            string LastName = adfields[ 1 ].Item2;
            string phoneNum = adfields[ 2 ].Item2;
            string UserId = adfields[ 3 ].Item2;
            string Phone = adfields[ 4 ].Item2;
            string userLogonName = "";
            string emailAddress = adfields[ 5 ].Item2;//FirstName + LastName.Substring( 0 , 1 ) + "@swardharia.local";
            string upn = "";
            for ( int g3 = 0 ; g3 < usernameField.Count ; g3++ )
            {
                for ( int g4 = 0 ; g4 < adfields.Count ; g4++ )
                {
                    if ( usernameField[ g3 ] == adfields[ g4 ].Item1 )
                    {
                        if ( ( adfields[ g4 ].Item2.Length ) > ( usernameValue[ g3 ] ) )
                        {
                            upn = upn + adfields[ g4 ].Item2.Substring( 0 , usernameValue[ g3 ] );
                            
                        }
                        else
                        {
                            upn = upn + adfields[ g4 ].Item2;
                        }

                        break;
                    }
                }

            }
            upn=upn+(j1).ToString();
            //same++;
            userLogonName = upn;
            upn = upn + "@swardharia.local";
            // Application.Current.Dispatcher.Invoke(() => MessageBox.Show(upn));
            UserPrincipal userPrincipal = new UserPrincipal( principalContext );

            if ( LastName != null && LastName.Length > 0 )
                userPrincipal.Surname = LastName;

            if ( FirstName != null && FirstName.Length > 0 )
                userPrincipal.GivenName = FirstName;

            if ( phoneNum != null && phoneNum.Length > 0 )
                userPrincipal.VoiceTelephoneNumber = phoneNum;


            if ( userLogonName != null && userLogonName.Length > 0 )
                userPrincipal.SamAccountName = userLogonName.Trim();

            if ( upn != null && upn.Length > 0 )
                userPrincipal.UserPrincipalName = upn;

            if ( emailAddress != null && emailAddress.Length > 0 )
                userPrincipal.EmailAddress = emailAddress;



            string pwdOfNewlyCreatedUser = "abcde@@" + j1;
            userPrincipal.SetPassword( pwdOfNewlyCreatedUser );

            userPrincipal.Enabled = true;
            userPrincipal.ExpirePasswordNow();

            try
            {
                userPrincipal.Save();
                //MessageBox.Show( "New User created\n" );
                adfields.Clear();
            }
            catch ( Exception ex )
            {
                Application.Current.Dispatcher.Invoke( () => MessageBox.Show( "Exception creating user object. " + ex.Message ) );
                //   return false;
            }
        }
        #endregion

        #region STOP SYNCING BUTTON
        private void stopsyncbt_Click ( object sender , RoutedEventArgs e )
        {
            disp.Stop();
            reset.IsEnabled = true;
            usertb.IsEnabled = true;
            tokentb.IsEnabled = true;
           // save.IsEnabled = true;
            secrettb.IsEnabled = true;
            synbt.Content = "Sync Users .. ";
            foreach ( UIElement element in g2.Children )
            {
                element.IsEnabled = true;
            }
            foreach ( UIElement element in g3.Children )
            {
                element.IsEnabled = true;
            }
        }
        #endregion

       
    }
}
