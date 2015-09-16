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
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using WpfApplication1.Properties;
using Microsoft.WindowsAPICodePack.ApplicationServices;

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
        int i = 3 , j = 0 , i11=1, chk, j2, j3=1, j4=0, flg=0, k1=0;
        int unc = 0 , mapc = 0;
        string alDomains="";
        string domainnam = "";
        string unltsfield = "" , unltsvalue = "" , unltsfl = "";
        string[] s , s1;
        string ltsnum = "" , ltspath = "" , ltsvalue = "" , ltsfield = "" , lmapnum = "";
        string[] ltsnum1 , ltspath1 , ltsvalue1 , ltsfield1 , lmapnum1;
        string[] unsf , unsv , unsfl;
        int i1 = 0 , same=0, j1=0;
        bool tf = true, unv=true;
        string ftoken , fuser , fsecret , fl;
        Thread th,th2;
        int runn = 0;

        DirectoryEntry entry;
        DirectorySearcher search;
        PrincipalContext principalContext;
        string serverad = "localhost";

        string getlog;
        DispatcherTimer disp = new DispatcherTimer();
       
        // ... A List.
        List<string> data = new List<string>();
        List<string> data1 = new List<string>();
        List<string> oupathlist = new List<string>();

        List<string> unlistField = new List<String>();
        List<string> unlistFL = new List<String>();
        List<string> unlistValue = new List<String>();

        List<string> mplistfield = new List<String>();
        List<string> mplistValue = new List<String>();
        List<string> mplistLdap = new List<String>();

        List<string> maplfield = new List<string>();
        List<string> maplvalue = new List<string>();
        List<string> maplpath = new List<string>();
        List<Tuple<string , string>> mapnum = new List<Tuple<string , string>>();
        List<Tuple<string , string>> mapnum1 = new List<Tuple<string , string>>();

        List<string> usernameField = new List<string>();
        List<string> usernameFL = new List<string>();
        List<Tuple<string , string>> adfields = new List<Tuple<string ,string>>();
        List<string> adf = new List<string>();
        List<Tuple<string , string>> adfields2 = new List<Tuple<string , string>>();
        List<Tuple<int , int>> fieldsinmap = new List<Tuple<int , int>>();
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
                                'school': 'nyu',
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
                                        'school': 'Little Flowers',
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
            InitializeComponent();
            RegisterApplicationRecoveryAndRestart();
            Settings.Default[ "userslabel" ] = Settings.Default.Properties[ "userslabel" ].DefaultValue;
            tokentb.Text = /* "8023e6e1cpb6e3825202nh4ccfah5d45ftsh033"; */   Settings.Default["clienttoken"].ToString();
            usertb.Text = /* "api_Z26xY6L5vjnGbtV0dHD81d2seIfCWV";  */        Settings.Default[ "user" ].ToString();
            secrettb.Text = /* "itCpJXetUICkRfWE27Ru5Eq9DNZ1V2X9GUjpZyFo"; */ Settings.Default[ "secret" ].ToString();
            passtb.Text = Settings.Default[ "password" ].ToString();

                connectad();
            
                Settings.Default[ "exit" ] = "1";
                Settings.Default.Save();
                disp.Tick += new EventHandler( syapi );
                disp.Interval = new TimeSpan( 0 , 5 , 0 );
                data.Add( "FirstName" );
                data.Add( "LastName" );
                data.Add( "userToken" );
                data.Add( "YoG" );
                usernameFL.Add( "First" );
                usernameFL.Add( "Last" );
                adf.Add( "School" );
                adf.Add( "Grade" );
                adf.Add( "Record_Type" );
                adf.Add( "Year_Of_Graduation" );
                data1.AddRange( new string[] { "Business-Category = businessCategory" , "Country-Name = c" , "carLicense = carLicense" , "Text-Country = co" , "User-Comment = comment" , "Company = company" , "Country-Code = countryCode" , "Department = department" , "departmentNumber = departmentNumber" , "Description = description" , "Display-Name = displayName" , "Employee-ID = employeeID" , "Employee-Number = employeeNumber" , "Employee-Type = employeeType" , "Phone-Home-Primary = homePhone" , "Address-Home = homePostalAddress" , "Comment = info" , "Organization-Name = o" , "Phone-Fax-Other = otherFacsimileTelephoneNumber" , "Phone-Home-Other = otherHomePhone" , "Other-Mailbox = otherMailbox" , "Phone-Mobile-Other = otherMobile" , "Phone-Pager-Other = otherPager" , "Phone-Office-Other = otherTelephone" } );

                string pat = GetTempPath();
                System.IO.File.WriteAllText( pat + "\\My Log File.txt" , string.Empty );

                j1 = Int32.Parse( Settings.Default[ "userslabel" ].ToString() );
                userlbl.Content = j1;

                if ( Settings.Default[ "unsfield" ].ToString() != "" )
                {
                    unltsfield = Settings.Default[ "unsfield" ].ToString();
                }

                if ( Settings.Default[ "unsvalue" ].ToString() != "" )
                {
                    unltsvalue = Settings.Default[ "unsvalue" ].ToString();
                }
                if ( Settings.Default[ "unsfl" ].ToString() != "" )
                {
                    unltsfl = Settings.Default[ "unsfl" ].ToString();
                }

                if ( ( unltsfield != "" ) && ( unltsvalue != "" ) && ( unltsfl != "" ) )
                {
                    unametolist();
                    for ( int f = 2 ; f < ( unsf.Length ) ; f++ )
                    {
                        createField();
                    }
                    uninit();
                }


                if ( Settings.Default[ "mapsfield" ].ToString() != "" )
                {
                    ltsfield = Settings.Default[ "mapsfield" ].ToString();
                }

                if ( Settings.Default[ "mapspath" ].ToString() != "" )
                {
                    ltspath = Settings.Default[ "mapspath" ].ToString();
                }
                if ( Settings.Default[ "mapsvalue" ].ToString() != "" )
                {
                    ltsvalue = Settings.Default[ "mapsvalue" ].ToString();
                }
                if ( Settings.Default[ "mapsnum" ].ToString() != "" )
                {
                    lmapnum = Settings.Default[ "mapsnum" ].ToString();
                }
                if ( ( ltsfield != "" ) && ( ltsvalue != "" ) && ( ltspath != "" ) && ( lmapnum != "" ) )
                {
                    maptolist();
                    int map4 = 1;

                        for ( int f1 = 0 ; f1 < ( lmapnum1.Length ) ; f1++ )
                        {
                            createField1();
                            map4 = int.Parse( mapnum1.ElementAt( f1 ).Item2 );

                            for ( int f2 = 1 ; f2 < map4 ; f2++ )
                            {
                                createfield3();
                            }
                        }

                        mapsinit();
                 }

                stopsyncbt.IsEnabled = false;
                unsave.Visibility = Visibility.Hidden;

                if ( Settings.Default[ "exit" ].ToString() == "1" )
                {

                    synbt.RaiseEvent( new RoutedEventArgs( Button.ClickEvent ) );

                }
                Settings.Default[ "exit" ] = "1";
                Settings.Default.Save();
            
        } 
        #endregion

        #region  UNINIT AND MAPsINIT
        private void uninit ()
        {
            int unflag = 0;
            foreach ( Control x in g2.Children )
            {
                //string field = GetValue( x );

                if ( x is TextBox )
                {
                    ( ( TextBox ) x ).Text = unlistValue[ unflag ];
                    unflag++;
                }
                else if ( x is ComboBox )
                {
                    if ( ( ( ComboBox ) x ).Name.Contains( "cbbfl" ) )
                    {
                        ( ( ComboBox ) x ).SelectedValue = unlistFL[ unflag ];
                    }
                    else
                    {
                        ( ( ComboBox ) x ).SelectedValue = unlistField[ unflag ];
                    }
                }
            }
        }

        private void mapsinit ()
        {
            int jk = 0 , mpv = 0 , mpf = 0 , mpp = 0;
            foreach ( Control x in g3.Children )
            {
                //string field = GetValue( x );
                string nn = x.Name.ToString();
                if ( nn == null )
                {
                    continue;
                }
                else
                {


                    if ( ( x is TextBox ) && ( nn.Contains( "name" ) ) )
                    {

                        //rowName = Grid.GetRow( x );
                        ( ( TextBox ) x ).Text = "map" + ( jk + 1 );
                        jk++;
                        continue;
                    }

                    else if ( ( x is Label ) && ( nn == "mlabl2" ) )
                    {
                        //rowName = Grid.GetRow( x );
                        //first = 0;
                        ( ( Label ) x ).Content = "Mapping Name : ";
                        continue;
                    }

                    else
                    {


                        int rowC1 = Grid.GetRow( x );
                        //check for length of fields from user len<int(field)
                        //   var index1 = mapnum.FindIndex( a => a.Item1 == rowName.ToString() );

                        if ( x is TextBox )
                        {
                            if ( nn.Contains( "strng" ) )
                            {
                                ( ( TextBox ) x ).Text = maplvalue[ mpv ];
                                mpv++;
                            }
                            else
                            {
                                // MessageBox.Show( "Enter proper Format" );
                                continue;
                            }
                        }

                        else if ( x is ComboBox )
                        {
                            if ( nn.Contains( "api" ) )
                            {
                                ( ( ComboBox ) x ).SelectedValue = maplfield[ mpf ];
                                mpf++;
                            }
                            else if ( nn.Contains( "oupath" ) )
                            {
                                ( ( ComboBox ) x ).SelectedValue = maplpath[ mpp ];
                                mpp++;
                            }
                        }


                    }

                }
            }

        }
        #endregion

        #region HELLO
        private void hello(object o, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString());
        }
        #endregion

        #region VALIDATE CONTROLS
        public bool validate ()
        {
            if ( !mapvalidate() )
            {
                tf = false;
                MessageBox.Show( "Select Proper Mapping" );
            }

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
                            break;
                        }
                        else if ( !isnumb )
                        {
                            unv = false;
                            break;
                        }
                    }

                    if ( element is ComboBox )
                    {
                        if ( ( ( ComboBox ) element ).SelectedIndex > -1 )
                        {

                        }
                        else
                        {
                            unv = false;
                            break;
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
           //  comboBox1.SelectedIndex = 0;
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
            //comboBox1.SelectedIndex = 0;
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
            //comboBox1.SelectedIndex = 0;
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
            comboBox1.ItemsSource = oupathlist;

            // ... Make the first item selected.
           // comboBox1.SelectedIndex = 0;
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
            //comboBox1.SelectedIndex = 0;
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
                cb1.Margin = new Thickness( 22 , 15 , 0 , 0 );
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
                cbFL1.Margin = new Thickness( 32.8 , 15 , 0 , 0 );
                cbFL1.HorizontalAlignment = HorizontalAlignment.Left;
                cbFL1.VerticalAlignment = VerticalAlignment.Top;
                cbFL1.Loaded += ComboBoxFL_Loaded;
                cbFL1.SelectionChanged += new SelectionChangedEventHandler( ComboBoxFL_SelectionChanged );
                Grid.SetRow( cbFL1 , i );
                Grid.SetColumn( cbFL1 , 1 );
                g2.Children.Add( cbFL1 );
                cbFL1.Name = "cbbfl" + i;

                TextBox tb11 = new TextBox();
                tb11.Text = "Characters ( minimum : 1 , full-length : 0 )";
                tb11.Width = 305;
                tb11.Height = 25;
                tb11.Margin = new Thickness( 33.8 , 15 , 0 , 0 );
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
            removeunf();
        }

        private void removeunf()
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
                        {
                            elementsToRemove.Add( element );
                        }
                    }

                    foreach ( UIElement element in elementsToRemove )
                    {
                        g2.Children.Remove( element );
                    }
                    g2.RowDefinitions.RemoveAt( rowCount - 1 );

                    i--;
                }
                //MessageBox.Show( "row=" + ( g2.RowDefinitions.Count - 1 ).ToString() );
            }
            catch ( Exception ex ) { exc( ex ); }
        }

        #endregion

        #region GET VALUE
        public string GetValue ( Control x )
        {
            if ( x is TextBox ) return ( ( TextBox ) x ).Text;
            if ( x is ComboBox ) return ( ( ComboBox ) x ).SelectedValue.ToString();
            if ( x is Label ) return ( ( Label ) x ).Content.ToString();
            if ( x is Button ) return ( ( Button ) x ).Name.ToString();
            else return null;
        }
        #endregion

        #region SAVE USERNAME FORMAT BUTTON CLICK AND GET VALUES FROM CONTROLS

        private void unsave_Click ( object sender , RoutedEventArgs e )
        {
            saveuname();
            //string path123 = ldappath( "abcd" , 5 , 2015 , "student" );
            //MessageBox.Show(path123);
            validate();
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
                unlistField.Clear();
                unlistFL.Clear();
                unlistValue.Clear();

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
                                if ( int.Parse( field ) != 0 )
                                {
                                    unlistValue.Add(field);
                                    if ( fl == "First" )
                                    {
                                        usernameformat = usernameformat + un.Substring( 0 , Int32.Parse( field ) );
                                        int value = Int32.Parse( field );
                                        usernameValue.Add( value );
                                        jk++;
                                    }
                                    else if ( fl == "Last" )
                                    {
                                        usernameformat = usernameformat + un.Substring( usernameField[ jl - 1 ].Length - Int32.Parse( field ) , Int32.Parse( field ) );
                                        int value = Int32.Parse( field );
                                        usernameValue.Add( value );
                                        jk++;
                                    }
                                }
                                else
                                {
                                    usernameformat = usernameformat + un;
                                    usernameValue.Add(0);
                                    jk++;
                                }

                                unc++;
                            }

                            else
                            {
                                MessageBox.Show( "Enter proper Format" );
                                unc = 0;
                                break;
                            }
                        }
                        else if ( x is ComboBox )
                        {
                            if((field=="First") || (field=="Last"))
                            {
                                fl = field;
                                usernameFirstLast.Add(fl);
                                unlistFL.Add(fl);
                            }
                            else
                            {
                                un = field;
                                jl++;
                                usernameField.Add( field );
                                unlistField.Add(field);
                            }
                        }
                    }
                }
                
            }

            catch ( Exception ex ) { exc( ex ); }
        }
        #endregion

        #region USERNAME STRING TO LIST

        public void unametolist()
        {
            unsf = unltsfield.Split( '*' );
            unsv = unltsvalue.Split( '*' );
            unsfl = unltsfl.Split( '*' );

            unsf = unsf.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            unsv = unsv.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            unsfl = unsfl.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            
            for ( int i11 = 0 ; i11 < unsf.Length ; i11++ )
            {
                if ( unsf[ i11 ] != "" )
                {
                    unlistField.Add( unsf[ i11 ] );
                    unlistValue.Add( unsv[ i11 ] );
                    unlistFL.Add( unsfl[ i11 ] );
                }
            }

        }
        #endregion

        #region MAP TO LIST

        private void maptolist()
        {
            ltsfield1 = ltsfield.Split( '*' );
            ltsvalue1 = ltsvalue.Split( '*' );
            ltspath1 = ltspath.Split( '*' );
            lmapnum1 = lmapnum.Split( '*' );

            ltsfield1 = ltsfield1.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            ltsvalue1 = ltsvalue1.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            ltspath1 = ltspath1.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();
            lmapnum1 = lmapnum1.Where( s12 => !System.String.IsNullOrEmpty( s12 ) ).ToArray();

            for ( int i11 = 0 ; i11 < ltsfield1.Length ; i11++ )
            {
                if ( ltsfield1[ i11 ] != "" )
                {
                    maplfield.Add( ltsfield1[ i11 ] );
                    //MessageBox.Show(s[i]);
                }
            }
            for ( int i12 = 0 ; i12 < ltspath1.Length ; i12++ )
            {
                if ( ltspath1[ i12 ] != "" )
                {
                    maplpath.Add( ltspath1[ i12 ] );
                    //MessageBox.Show(s[i]);
                }
            }
            for ( int i13 = 0 ; i13 < ltsvalue1.Length ; i13++ )
            {
                if ( ltsvalue1[ i13 ] != "" )
                {
                    maplvalue.Add( ltsvalue1[ i13 ] );
                    //MessageBox.Show(s[i]);
                }
            }
            for ( int i14 = 0 ; i14 < lmapnum1.Length ; i14++ )
            {
                if ( lmapnum1[ i14 ] != "" )
                {
                    mapnum1.Add( new Tuple<string , string>( lmapnum1[ i14 ].Split( '(' , ',' )[ 1 ] , lmapnum1[ i14 ].Split( ',' , ')' )[ 1 ] ) );
                    //MessageBox.Show(s[i]);
                }
            }
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
                    {
                        ( ( TextBox ) x ).Clear();
                    }
                    else if ( x is ComboBox )
                    {
                        ( ( ComboBox ) x ).SelectedIndex = -1;
                    }
                }

                foreach ( Control x in g3.Children )
                {
                    if ( x is TextBox )
                    {
                        ( ( TextBox ) x ).Clear();
                    }
                    else if ( x is ComboBox )
                    {
                        ( ( ComboBox ) x ).SelectedIndex = -1;
                    }
                }

                resetall();
                MessageBox.Show( "Cleared !" );
            }
            catch ( Exception ex )
            {
                exc( ex );
            }
        }
        private void save_Click ( object sender , RoutedEventArgs e )
        {

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
                flg = 1;
                createField1();
            }
            catch ( Exception ex )
            {
                exc(ex);
            }
        }

        private void createField1 ()
        {
            scrl.Height = 150;
            RowDefinition newrow = new RowDefinition();
            newrow.Height = new GridLength( 1 , GridUnitType.Auto );
            g3.RowDefinitions.Insert( g3.RowDefinitions.Count , newrow );
            j3 = 1;             // number of child fields

            Label lb21 = new Label();
            lb21.Content = "Mapping Name : ";
            lb21.Margin = new Thickness( 20,47.4,30.8,3.2 );
            lb21.FontWeight = FontWeights.Bold;
            lb21.Foreground = new SolidColorBrush( Colors.White );
            Grid.SetRow( lb21 , j );
            Grid.SetColumn( lb21 , 0 );
            g3.Children.Add( lb21 );
            lb21.Name = "mlabl" + j;

            TextBox tb21 = new TextBox();
            //tb21.Text = "Characters ( min : 1 , max : full )";
            tb21.Width = 325;
            tb21.Margin = new Thickness( 19 , 55 , 0 , 0 );
            tb21.HorizontalAlignment = HorizontalAlignment.Left;
            tb21.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow( tb21 , j );
            Grid.SetColumn( tb21 , 1 );
            g3.Children.Add( tb21 );
            tb21.Name = "mname" + j;

            bool containsStr = mapnum.Any( c => c.Item1.Contains( j.ToString() ) );

            if ( !containsStr )
            {
                mapnum.Add( new Tuple<string , string>( j.ToString() , "1" ) );
            }

            j2 = j;             // row of mapping name where it starts
            j++;
            j4++;               // number of mappings

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
            cb1.Name = "mfapi" + j;

            TextBox tb22 = new TextBox();
            //tb21.Text = "Characters ( min : 1 , max : full )";
            tb22.Height = 25;
            tb22.Width = 136;
            tb22.Margin = new Thickness( 18 , 10 , 0 , 0 );
            tb22.HorizontalAlignment = HorizontalAlignment.Left;
            tb22.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow( tb22 , j );
            Grid.SetColumn( tb22 , 1 );
            g3.Children.Add( tb22 );
            tb22.Name = "mstrng" + j;

            ComboBox tb11 = new ComboBox();
            tb11.Width = 181;
            tb11.Margin = new Thickness( 144.4 , 10 , 6 , 0 );
            tb11.HorizontalAlignment = HorizontalAlignment.Right;
            tb11.VerticalAlignment = VerticalAlignment.Top;
            tb11.Loaded += ComboBox3_Loaded;
            tb11.SelectionChanged += new SelectionChangedEventHandler( ComboBox3_SelectionChanged );
            Grid.SetRow( tb11 , j );
            Grid.SetColumn( tb11 , 1 );
            g3.Children.Add( tb11 );
            tb11.Name = "oupath" + j;

            fieldsinmap.Add( new Tuple<int,int>(j2 , 1) );

            j++;                //  row to insert next

        }

        private void addfieldbtn3_Click ( object sender , RoutedEventArgs e )
        {
            createfield3();
        }

        private void createfield3()
        {
            if ( g3.Children.Count > 0 )
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
                cb2.Name = "mfapi" + j;

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
                tb23.Name = "mstrng" + j;
                j3++;                           // add number of children

                //fieldsinmap.RemoveAll(Tuple<int , int>( j2 ) );
                fieldsinmap.RemoveAll( x => x.Item1 == j2 );
                fieldsinmap.Add( new Tuple<int , int>( j2 , j3 ) );
                mapnum.RemoveAll( x => x.Item1 == j2.ToString() );
                mapnum.Add( new Tuple<string , string>( j2.ToString() , j3.ToString() ) );


                j++;
            }
            else
            {
                MessageBox.Show( "Add Mapping First" );
            }
        }

        private void removebtn3_Click ( object sender , RoutedEventArgs e )
        {
            removemap();
        } 

        private void removemap()
        {
            try
            {

                int rowCount = g3.RowDefinitions.Count;
                if ( rowCount > 0 && j2 >= 0 )
                {

                    List<UIElement> elementsToRemove = new List<UIElement>();

                    foreach ( UIElement element in g3.Children )
                    {

                        if ( Grid.GetRow( element ) >= j2 )

                            elementsToRemove.Add( element );

                    }
                    int[] remelm = new int[ 1000 ];
                    int pl = 0;
                    foreach ( UIElement element in elementsToRemove )
                    {
                        remelm[ pl ] = Grid.GetRow( element );
                        g3.Children.Remove( element );
                        pl++;
                    }
                    for ( int pm = 1 ; pm < remelm.Distinct().Count() ; pm++ )
                    {
                        g3.RowDefinitions.RemoveAt( rowCount - pm );
                    }

                    j = j2;                             // next row to insert after deletion

                    if ( j4 > 1 )
                    {
                        j2 = j2 - fieldsinmap[ j4 - 2 ].Item2 - 1;
                    }
                    else
                    {
                        flg = 0;
                        j2 = 1;
                    }

                    mapnum.RemoveAll( x => x.Item1 == j.ToString() );
                    fieldsinmap.RemoveAll( x => x.Item1 == j );
                    j4--;                           // number of mappings
                }

            }
            catch ( Exception ex )
            {
                //exc( ex );
                MessageBox.Show( ex.ToString() );
            }
        }

        #endregion

        #region MAPPING COMMMENTS
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

                //string password1 = "Swar123";
                //LDAP://localhost:398

                alDomains = DomainManager.DomainName;
                domainnam = GetDomainDN( alDomains );
                //domainnam = "DC=swardharia,DC=local";

                entry = new DirectoryEntry( "LDAP://localhost/" + domainnam );
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
                    oupathlist.Add( resEnt.GetDirectoryEntry().Path.ToString() );
                    ous.Add( resEnt.GetDirectoryEntry().Name.ToString());
                    LogM( resEnt.GetDirectoryEntry().Name.ToString() + "\n" );
                    LogM( resEnt.GetDirectoryEntry().Parent.Path.ToString() + "-------------------\n" );
                }
                LogM( "=========== End of Listing =============\n" );
                LogM( "" );

                //----------------------------------------------------------------------------------------------------------------------//

              //  LogM("connected"+entry.ToString());
                ous.Remove( "OU=defaultOU" );
                if(!ous.Contains("OU=TestOU"))
                {
                    createou();
                }
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
            this.Dispatcher.Invoke( ( Action ) ( () =>
            {
                ftoken = tokentb.Text;    // "8023e6e1cpb6e3825202nh4ccfah5d45ftsh033";
                fuser = usertb.Text;      //"api_Z26xY6L5vjnGbtV0dHD81d2seIfCWV";
                fsecret = secrettb.Text;  //"itCpJXetUICkRfWE27Ru5Eq9DNZ1V2X9GUjpZyFo";
            } ) );
            string tokenrequest = "https://api.simpler.im/" + ftoken + "/user/authorize?user=" + fuser + "&secret=" + fsecret;

            // string accessToken = "7e4c256ca2cf77675139ca3ea1ad19488ac81563c2f8aaf5d1aac3ed1a51";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            try
            {
                LogM("inside try html");
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

                if ( getlog != "" && googleSearchText != null )
                {
                    //string html = client.DownloadString( getlog );
                    //string s2 = html.Replace( "'" , " " );
                    //googleSearchText = s2.Replace( "\"" , "'" );

                    //************************************************************************************************//

                    LogM("\n"+html1+"\n");
                    // string googleSearchText;
                    JObject googleSearch = JObject.Parse( googleSearchText );
                    dynamic newgoogleSearch = googleSearch;
                    var values = newgoogleSearch.result[ 0 ].insert;

                    // get JSON result objects into a list
                    IList<JToken> results = googleSearch[ "result" ].Children().ToList();

                    // serialize JSON results into .NET objects
                    IList<SearchResult1> searchResults = new List<SearchResult1>();
                    k1 = 0;
                    foreach ( JToken result in results )
                    {
                        string fn1 = "" , ln1 = "" , type = "" , scl1 = "" , ph1 = "" , em1 = "";
                        int yog = 9999;
                        int grd1 = 0;

                        SearchResult1 searchResult = googleSearch[ "result" ][ k1 ][ "insert" ].ToObject<SearchResult1>();
                        uniqueId = ( string ) googleSearch[ "result" ][ k1 ][ "_id" ];

                        string m1 = searchResult.firstName;
                        string m2 = searchResult.lastName;
                        string m3 = searchResult.school;
                        string usrtoken = searchResult.userToken.ToString();

                        if ( ( m1 != null ) && ( m2 != null ) && ( m3 != null ) )
                        {
                            searchResults.Add( searchResult );


                            if ( searchResults[ i1 ].firstName != null )
                            {
                                fn1 = searchResults[ i1 ].firstName.ToString();
                            }
                            if ( searchResults[ i1 ].lastName != null )
                            {
                                ln1 = searchResults[ i1 ].lastName.ToString();
                            }
                            if ( searchResults[ i1 ].school != null )
                            {
                                scl1 = searchResults[ i1 ].school.ToString();
                            }
                            if ( searchResults[ i1 ].grade != null )
                            {
                                grd1 = Int32.Parse( searchResults[ i1 ].grade.ToString() );
                            }
                            if ( newgoogleSearch.result[ i1 ].insert.record.type != null )
                            {
                                type = newgoogleSearch.result[ i1 ].insert.record.type;
                            }


                            if ( newgoogleSearch.result[ i1 ].insert.yog != null )
                            {
                                yog = newgoogleSearch.result[ i1 ].insert.yog;
                            }


                            LogM( String.Format( "{0,-10} : {1}" , "id" , searchResults[ i1 ].lasid.ToString() ) );
                            LogM( String.Format( "{0,-10} : {1} " , "First Name" , fn1 ) );
                            LogM( String.Format( "{0,-10} : {1}" , "Last Name" , ln1 ) );
                            LogM( String.Format( "{0,-10} : {1}" , "School" , scl1 ) );
                            LogM( String.Format( "{0,-10} : {1}" , "Type" , type ) );

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
                                if ( !userexist( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken ) )
                                {
                                    addUser( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken , grd1 , type , yog );
                                    i1++;
                                    j1++;
                                    k1++;
                                    continue;
                                }
                                else
                                {
                                    // MessageBox.Show( "user exist" );
                                    addSameUser( fn1 , ln1 , ph1 , em1 , scl1 , usrtoken , grd1 , type , yog );
                                    i1++;
                                    j1++;
                                    k1++;
                                    continue;
                                }
                                //addUser( fn1 , ln1 , ph1 , em1 , scl1 );
                            }
                            catch ( Exception ex )
                            {
                                Application.Current.Dispatcher.Invoke( () => MessageBox.Show( "Problems adding user to Active - Directory : " + ex , "Exception Sample" , MessageBoxButton.OK , MessageBoxImage.Warning ) );
                            }

                        }

                        else
                        {
                            k1++;
                            LogM( "k : " + k1 );
                            continue;
                        }

                    }
                    googleSearchText = "";
                    LogM( "total : " + k1 );

                    Application.Current.Dispatcher.Invoke( () =>stopsyncbt.IsEnabled = true);
                    Application.Current.Dispatcher.Invoke( () => MessageBox.Show( "Total Records inserted =" + j1 ) );
                }
                else
                {
                    Application.Current.Dispatcher.Invoke( () => MessageBox.Show( "No data available right now !" ) );
                }
            }
            catch(Exception ex)
            {
                LogM( "after try html" );
                MessageBox.Show( "Error Requesting the API.. Please Check your Information! " );
                this.Dispatcher.Invoke( ( Action ) ( () =>
                {
                    synbt.Content = "Sync Users .. ";
                    stopsyncbt.IsEnabled = true;
                } ) );
                Thread.CurrentThread.Suspend();
                //MessageBox.Show(ex.ToString());
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
                if ( g3.Children.Count == 0 )
                {
                    MessageBox.Show( "add mapping first" );
                }
                else
                {
                    runn = 1;
                    //Settings.Default[ "exit" ] = "1";
                    Settings.Default[ "clienttoken" ] = tokentb.Text.ToString();
                    Settings.Default[ "user" ] = usertb.Text.ToString();
                    Settings.Default[ "secret" ] = secrettb.Text.ToString();
                    Settings.Default[ "password" ] = passtb.Text.ToString();
                    Settings.Default.Save();
                    reset.IsEnabled = false;
                    usertb.IsEnabled = false;
                    tokentb.IsEnabled = false;
                    secrettb.IsEnabled = false;
                    addmapbtn3.IsEnabled = false;
                    addfieldbtn3.IsEnabled = false;
                    removebtn3.IsEnabled = false;

                    synbt.Content = "Sync in Progress .. ";
                    foreach ( UIElement element in g2.Children )
                    {
                        element.IsEnabled = false;
                        if ( element is ComboBox )
                        {
                            ( ( ComboBox ) element ).IsHitTestVisible = false;
                            ( ( ComboBox ) element ).IsEditable = false;
                            ( ( ComboBox ) element ).Focusable = false;
                        }
                    }
                    foreach ( UIElement element in g3.Children )
                    {
                        element.IsEnabled = false;
                        if ( element is ComboBox )
                        {
                            ( ( ComboBox ) element ).IsHitTestVisible = false;
                            ( ( ComboBox ) element ).IsEditable = false;
                            ( ( ComboBox ) element ).Focusable = false;
                        }
                    }
                    unform = "";
                    saveuname();
                    bool v1 = validate();
                    bool v2 = unvalidate();

                    if ( v1 && v2 )
                    {
                        LogM( "before thread" );
                        try
                        {
                            th = new Thread( () => connectapi() );
                            th.Start();
                        }
                        catch ( Exception ex ) { exc( ex ); }
                    
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
                    else
                    {
                        stopsync();
                        stopsyncbt.IsEnabled = true;
                        runn = 0;
                    }
                    userlbl.Content = j1;
                    Settings.Default[ "userslabel" ] = j1.ToString();
                    Settings.Default.Save();

                    LogM( "after thread" );
                }
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

        #region SEARCH BUTTON
        /*              private void searchbtn_Click ( object sender , RoutedEventArgs e )
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
        }*/
        #endregion

        #region GET LDAP PATH
        public string ldappath(string School, int Grade, int Year_Of_Graduation, string Record_Type)
        {
            string ldapp = "", un1 =""; 
            int matchflag = 0, rowName=0,  jk=0, rowCurrent=0, first=1;
            string gra12 = Grade.ToString();
            string yog12 = Year_Of_Graduation.ToString();
            string apifield = "";
            List<Tuple<string , string>> match = new List<Tuple<string , string>>();
            string mappath = "";
            match.Add( new Tuple<string , string>( "School" , School ) );
            match.Add( new Tuple<string , string>( "Grade" , gra12 ) );
            match.Add( new Tuple<string , string>( "Year_Of_Graduation" , yog12 ) );
            match.Add( new Tuple<string , string>( "Record_Type" , Record_Type ) );

            foreach ( Control x in g3.Children )
            {
                string field = GetValue( x );

                if ( field == null )
                {
                    continue;
                }
                else
                {
                    string nn= x.Name.ToString();
                   // bool con = x.Name.ToString().Contains( "mame" );
                    if ( (x is TextBox)  && (nn.Contains("name")))
                    {
                        rowName = Grid.GetRow( x );
                            continue;
                    }

                    else if ( (x is Label) && (first== 1) && (nn=="mlabl0"))
                    {
                        rowName = Grid.GetRow( x );
                        first = 0;
                        continue;
                    }
                    else if ( ( x is Label ) && ( first == 1 ))
                    {
                        var index2 = fieldsinmap.FindIndex( a => a.Item1 == rowName );
                        if ( matchflag == fieldsinmap[ index2 ].Item2 )
                        {
                            return mappath;
                        }
                        matchflag = 0;
                        rowName = Grid.GetRow( x );
                        //first = 0;
                        continue;
                    }

                    else {
                        
                        //if(( x is Label))
                        //{
                        //    rowName = Grid.GetRow( x );
                        //}

                        rowCurrent = Grid.GetRow(x);
                        //check for length of fields from user len<int(field)
                        var index1 = fieldsinmap.FindIndex( a => a.Item1 == rowName );
                        if ( rowCurrent <= ( fieldsinmap[ index1 ].Item2 + fieldsinmap[ index1 ].Item1 ) )
                        {
                            if(x is TextBox)
                            {
                                    if ( nn.Contains( "strng" ) )
                                    {
                                        for(int in2=0; in2 < match.Count ; in2++)
                                        {

                                            if( apifield == match[in2].Item1)
                                            {
                                                if(match[in2].Item2 == field)
                                                {
                                                    matchflag++;
                                                }

                                                else
                                                {
                                                    matchflag = 0;
                                                }

                                             }
                                    
                                          }
                                       }
                                        else
                                        {
                                           // MessageBox.Show( "Enter proper Format" );
                                            continue;
                                        }
                                 }
                             
                                 else if ( x is ComboBox )
                                 {
                                    if ( nn.Contains( "api" ) )
                                    {
                                        apifield = field;
                                    
                                    }
                                    else if ( nn.Contains( "oupath" ) )
                                    {
                                        mappath = field;
                                    }
                                 }
                            }
                            else 
                            {
                              var index2 = fieldsinmap.FindIndex( a => a.Item1 == rowName );
                              if(matchflag == fieldsinmap[index2].Item2 )
                              {
                                  return mappath;
                              }
                              matchflag = 0;
                            }
                    }
                    
                }
            }
            //var index3 = fieldsinmap.FindIndex( a => a.Item1 == rowName ) - 1;
            if ( matchflag == fieldsinmap[ fieldsinmap.Count - 1 ].Item2 )
            {
                return mappath;
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
                        if ( usernameValue[ g11 ] != 0 )
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
                                    upname = upname + adfields2[ g12 ].Item2;
                                    //userLogonName = upn;
                                }

                                break;
                            }
                        }
                        else
                        {
                            upname = upname + adfields2[ g12 ].Item2;
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

            principalContext1 = new PrincipalContext( ContextType.Domain , serverad , domainnam );
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

            string gtldappath= Application.Current.Dispatcher.Invoke( () =>ldappath(scl1,grade,yog,type1));
            
            if(gtldappath!="")
            {
                gtldappath = gtldappath.Substring( gtldappath.IndexOf( "OU" ) );
            }


            if ( gtldappath == "" )
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=TestOU," + domainnam );
            }
            else
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , gtldappath );
            }
            
             //   principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=defaultOU,DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            
            //string[] words = sear.Split(' ');
            adfields.Add( new Tuple<string , string>( "FirstName" , firstn ) );
            adfields.Add( new Tuple<string , string>( "LastName" , lastn ) );
            adfields.Add( new Tuple<string , string>( "phoneNum" , phonen ) );
            adfields.Add( new Tuple<string , string>( "UserId" , "" ) );
            adfields.Add( new Tuple<string , string>( "Phone" , "" ) );
            adfields.Add( new Tuple<string , string>( "Email" , eid ) );
            adfields.Add( new Tuple<string , string>( "userToken" , token1 ) );
            adfields.Add( new Tuple<string , string>( "YoG", yog.ToString() ) );
            string FirstName = adfields[0].Item2;
            string LastName = adfields[ 1 ].Item2;
            string phoneNum = adfields[ 2 ].Item2;
            string UserId = adfields[ 3 ].Item2;
            string Phone = adfields[ 4 ].Item2;
            string YoG = adfields[ 7 ].Item2;
            string userLogonName = "";
            string emailAddress = adfields[5].Item2;//FirstName + LastName.Substring( 0 , 1 ) + "@swardharia.local";
            string upn="";
            for ( int g3 = 0 ; g3 < usernameField.Count ;g3++ )
            {
                for ( int g4 = 0 ; g4 < adfields.Count ; g4++ )
                {
                    if ( usernameField[ g3 ] == adfields[ g4 ].Item1 )
                    {
                        if ( usernameValue[ g3 ] != 0 )
                        {
                            if ( usernameFirstLast[ g3 ] == "First" )
                            {
                                if ( ( adfields[ g4 ].Item2.Length ) > ( usernameValue[ g3 ] ) )
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
                                    upn = upn + adfields[ g4 ].Item2.Substring( adfields[ g4 ].Item2.Length - usernameValue[ g3 ] , usernameValue[ g3 ] );
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
                        else
                        {
                            upn = upn + adfields[ g4 ].Item2;
                            userLogonName = upn;
                        }
                    }
                }
                
            }
            upn=upn+ "@"+alDomains;
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



            string pwdOfNewlyCreatedUser = Application.Current.Dispatcher.Invoke( () =>passtb.Text);
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
               Application.Current.Dispatcher.Invoke(() => MessageBox.Show( "Exception creating user object. " + ex.Message ));
                //   return false;
            }
        }
        #endregion

        #region ADD USERS AD
        public void addSameUser ( string firstn , string lastn , string phonen , string eid , string scl1 , string token1 , int grade, string type, int yog)
        {
            string gtldappath = Application.Current.Dispatcher.Invoke( () =>ldappath( scl1 , grade , yog , type ));
            if ( gtldappath != "" )
            {
                gtldappath = gtldappath.Substring( gtldappath.IndexOf( "OU" ) );
            }
            //if ( ous.Contains( "OU=" + scl1 ) )
            //{
            //    principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=" + scl1 + ",DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            //}
            //else
            //{
            //    principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=defaultOU,DC=swardharia,DC=local" , "SWARDHARIA\\Administrator" , "Swardharia4." );
            //}
            //string[] words = sear.Split(' ');

            if ( gtldappath == "" )
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , "OU=TestOU," + domainnam );
            }
            else
            {
                principalContext = new PrincipalContext( ContextType.Domain , serverad , gtldappath );
            }
            
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
            upn = upn + "@" +alDomains;
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



            string pwdOfNewlyCreatedUser = Application.Current.Dispatcher.Invoke( () =>passtb.Text);
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
            runn = 0;
            Settings.Default[ "exit" ] = "0";
            stopsync();
        }
        #endregion

        #region STOP SYNC METHOD
        void stopsync ()
        {
            disp.Stop();
            reset.IsEnabled = true;
            usertb.IsEnabled = true;
            tokentb.IsEnabled = true;
            // save.IsEnabled = true;
            secrettb.IsEnabled = true;
            addfieldbtn3.IsEnabled = true;
            addmapbtn3.IsEnabled = true;
            removebtn3.IsEnabled = true;
            synbt.Content = "Sync Users .. ";
            foreach ( UIElement element in g2.Children )
            {
                element.IsEnabled = true;
                if ( element is ComboBox )
                {
                    ( ( ComboBox ) element ).IsHitTestVisible = true;
                    ( ( ComboBox ) element ).IsEditable = true;
                    ( ( ComboBox ) element ).Focusable = true;
                }
            }
            foreach ( UIElement element in g3.Children )
            {
                element.IsEnabled = true;
                if ( element is ComboBox )
                {
                    ( ( ComboBox ) element ).IsHitTestVisible = true;
                    ( ( ComboBox ) element ).IsEditable = true;
                    ( ( ComboBox ) element ).Focusable = true;
                }
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

        #region CREATE DEFAULT OU
        void createou ()
        {
            DirectoryEntry objADAM;  // Binding object.
            DirectoryEntry objOU;    // Organizational unit.
            string strDescription;   // Description of OU.
            string strOU;            // Organiztional unit.
            string strPath;          // Binding path.

            // Construct the binding string.
            strPath = "LDAP://localhost/"+domainnam;

            // Get AD LDS object.
            try
            {
                objADAM = new DirectoryEntry( strPath );
                objADAM.RefreshCache();
            }
            catch ( Exception e )
            {
                LogM(e.ToString());
                return;
            }

            // Specify Organizational Unit.
            strOU = "OU=TestOU";
            strDescription = "Default Organizational Unit";

            // Create Organizational Unit.
            try
            {
                objOU = objADAM.Children.Add( strOU ,
                    "OrganizationalUnit" );
                objOU.Properties[ "description" ].Add( strDescription );
                objOU.CommitChanges();
            }
            catch ( Exception e )
            {
                return;
            }

            
            return;
        }
        #endregion

        #region WINDOW CLOSING
        private void Window_Closing ( object sender , CancelEventArgs e )
        {
            if ( runn == 1 )
            {
                MessageBox.Show( "Stop Syncing before Exit !!" );
                e.Cancel = true;
            }
            else
            {
                Settings.Default[ "exit" ] = "0";
                Settings.Default.Save();
                App.Current.Shutdown();
                //MessageBox.Show( Settings.Default[ "exit" ].ToString() );
            }
        }
        #endregion

        #region RESTART AND RECOVERY
        private void RegisterApplicationRecoveryAndRestart ()
        {

            // register for Application Restart
            RestartSettings restartSettings =
                new RestartSettings( string.Empty , RestartRestrictions.None );
            ApplicationRestartRecoveryManager.RegisterForApplicationRestart( restartSettings );

            // register for Application Recovery
            RecoverySettings recoverySettings =
                new RecoverySettings( new RecoveryData( PerformRecovery , null ) , 5000 );
            ApplicationRestartRecoveryManager.RegisterForApplicationRecovery( recoverySettings );
        }

        private void UnregisterApplicationRecoveryAndRestart ()
        {

            ApplicationRestartRecoveryManager.UnregisterApplicationRestart();
            ApplicationRestartRecoveryManager.UnregisterApplicationRecovery();
        }

        private int PerformRecovery ( object parameter )
        {
            try
            {
                ApplicationRestartRecoveryManager.ApplicationRecoveryInProgress();

                // Save your work here for recovery
                ApplicationRestartRecoveryManager.ApplicationRecoveryFinished( true );
            }
            catch
            {
                ApplicationRestartRecoveryManager.ApplicationRecoveryFinished( false );
            }

            return 0;
        }
        #endregion

        #region SAVE LAYOUT
        private void layoutbtn_Click ( object sender , RoutedEventArgs e )
        {
            try
            {
                if ( unvalidate() && validate() && mapvalidate() )
                {
                    Settings.Default[ "clienttoken" ] = tokentb.Text;
                    Settings.Default[ "user" ] = usertb.Text;
                    Settings.Default[ "secret" ] = secrettb.Text;
                    Settings.Default[ "password" ] = passtb.Text;
                    Settings.Default.Save();
                    saveunlayout();
                    savemaplayout();
                }
            }
            catch ( Exception ex )
            {
                exc( ex );
            }
        }
        #endregion

        #region SAVE UN LAYOUT

        private void saveunlayout ()
        {
            Settings.Default[ "unsfield" ] = Settings.Default.Properties[ "unsfield" ].DefaultValue;
            Settings.Default[ "unsfl" ] = Settings.Default.Properties[ "unsfl" ].DefaultValue;
            Settings.Default[ "unsvalue" ] = Settings.Default.Properties[ "unsvalue" ].DefaultValue;
            Settings.Default.Save();
            unlistField.Clear();
            unlistFL.Clear();
            unlistValue.Clear();
            unltsfield = "";
            unltsfl = "";
            unltsvalue = "";

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
                            if ( int.Parse( field ) != 0 )
                            {
                                unlistValue.Add( field );
                            }
                            else
                            {
                                continue;
                            }

                            // unc++;
                        }

                        else
                        {
                            MessageBox.Show( "Enter proper Format" );
                            //unc = 0;
                            break;
                        }
                    }
                    else if ( x is ComboBox )
                    {
                        if ( ( ( ComboBox ) x ).Name.Contains( "cbbfl" ) )
                        {
                            fl = field;
                            // usernameFirstLast.Add( fl );
                            unlistFL.Add( fl );
                        }
                        else
                        {
                            // un = field;
                            //jl++;
                            //  usernameField.Add( field );
                            unlistField.Add( field );
                        }
                    }
                }
            }

            foreach ( var score in unlistField )
            {
                unltsfield = unltsfield + score.ToString() + "*";
            }


            foreach ( var score in unlistFL )
            {
                unltsfl = unltsfl + score.ToString() + "*";
            }


            foreach ( var score in unlistValue )
            {
                unltsvalue = unltsvalue + score.ToString() + "*";
            }

            Settings.Default[ "unsfield" ] = unltsfield;
            Settings.Default[ "unsfl" ] = unltsfl;
            Settings.Default[ "unsvalue" ] = unltsvalue;
            Settings.Default.Save();
            MessageBox.Show( "username saved" );
        }

        #endregion

        #region SAVE MAP LAYOUT

        private void savemaplayout ()
        {
            Settings.Default[ "mapsfield" ] = Settings.Default.Properties[ "mapsfield" ].DefaultValue;
            Settings.Default[ "mapsvalue" ] = Settings.Default.Properties[ "mapsvalue" ].DefaultValue;
            Settings.Default[ "mapspath" ] = Settings.Default.Properties[ "mapspath" ].DefaultValue;
            Settings.Default[ "mapsnum" ] = Settings.Default.Properties[ "mapsnum" ].DefaultValue;
            Settings.Default.Save();
            maplfield.Clear();
            maplpath.Clear();
            maplvalue.Clear();
            ltsfield = "";
            ltspath = "";
            ltsvalue = "";
            lmapnum = "";

            foreach ( Control x in g3.Children )
            {
                string field = GetValue( x );

                if ( field == null )
                {
                    continue;
                }
                else
                {
                    string nn = x.Name.ToString();

                    if ( ( x is Label ) )
                    {
                        continue;
                    }

                    else
                    {
                        if ( x is TextBox )
                        {
                            if ( nn.Contains( "strng" ) )
                            {
                                maplvalue.Add( field );
                            }
                            else
                            {
                                continue;
                            }
                        }

                        else if ( x is ComboBox )
                        {
                            if ( nn.Contains( "api" ) )
                            {
                                maplfield.Add( field );
                            }
                            else if ( nn.Contains( "oupath" ) )
                            {
                                maplpath.Add( field );
                            }
                        }

                    }

                }
            }

            foreach ( var score in maplfield )
            {
                ltsfield = ltsfield + score.ToString() + "*";
            }


            foreach ( var score in maplpath )
            {
                ltspath = ltspath + score.ToString() + "*";
            }


            foreach ( var score in maplvalue )
            {
                ltsvalue = ltsvalue + score.ToString() + "*";
            }


            foreach ( var score in mapnum )
            {
                lmapnum = lmapnum + score.ToString() + "*";
            }
            Settings.Default[ "mapsfield" ] = ltsfield;
            Settings.Default[ "mapsvalue" ] = ltsvalue;
            Settings.Default[ "mapspath" ] = ltspath;
            Settings.Default[ "mapsnum" ] = lmapnum;
            Settings.Default.Save();
            MessageBox.Show( "map saved" );

        }

        #endregion

        #region MAP VALIDATE

        private bool mapvalidate ()
        {
            int err = 0;
            foreach ( Control x in g3.Children )
            {
                if ( x is ComboBox )
                {
                    if ( ( ( ComboBox ) x ).SelectedIndex > -1 )
                    {
                        continue;
                    }
                    else
                    {
                        err = 1;
                        break;
                    }
                }
                if ( ( x is Label ) )
                {
                    continue;
                }
                else if ( x is TextBox )
                {
                    if ( string.IsNullOrWhiteSpace( ( ( TextBox ) x ).Text ) )
                    {
                        err = 1;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if ( err == 1 )
            {
                return false;
                MessageBox.Show( "Select Proper Mapping" );
            }
            else
            {
                if ( g3.Children.Count > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                    MessageBox.Show( "Select Proper Mapping" );
                }
            }
        }

        #endregion

        #region RESET ALL

        private void resetall ()
        {
            Settings.Default[ "mapsfield" ] = Settings.Default.Properties[ "mapsfield" ].DefaultValue;
            Settings.Default[ "mapsvalue" ] = Settings.Default.Properties[ "mapsvalue" ].DefaultValue;
            Settings.Default[ "mapspath" ] = Settings.Default.Properties[ "mapspath" ].DefaultValue;
            Settings.Default[ "mapsnum" ] = Settings.Default.Properties[ "mapsnum" ].DefaultValue;
            Settings.Default[ "unsfield" ] = Settings.Default.Properties[ "unsfield" ].DefaultValue;
            Settings.Default[ "unsvalue" ] = Settings.Default.Properties[ "unsvalue" ].DefaultValue;
            Settings.Default[ "unsfl" ] = Settings.Default.Properties[ "unsfl" ].DefaultValue;
            Settings.Default.Save();
            maplfield.Clear();
            maplpath.Clear();
            maplvalue.Clear();
            ltsfield = "";
            ltspath = "";
            ltsvalue = "";
            unlistField.Clear();
            unlistFL.Clear();
            unlistValue.Clear();
            unltsfield = "";
            unltsfl = "";
            unltsvalue = "";

        }

        #endregion
    }
}
