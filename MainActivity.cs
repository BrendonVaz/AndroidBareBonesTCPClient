using Android.App;
using Android.Bluetooth;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using Android.Net.Wifi;
using Android.Content;
using System.Collections.Generic;
using Android.Text.Format;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;


namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {

        #region---variables app-----------------------

        public FragmentConnect      frag_Page1;                             //Page 1: Connection information
        public FragmentContinue     frag_Page3;                             //Page 2: Command Page 1
        public FragmentConfigure    frag_Page2;                             //Page 3: Command Page 2
        public FragmentSpinner      frag_spinner;                           //Page 4: wifi selector
        public FragmentDialog       frag_dialog;                            //Page 5: server info
        public BottomNavigationView navigation;                             //Page 6: Main

        #endregion

        //EMPTY
        
                    #region---variables connect-------------------
                    #endregion  

                    #region---variables configure-----------------
                    #endregion

                    #region---variables continue------------------
                    #endregion

                    #region---variables spinner-------------------
                    #endregion

                    #region---variables bluetooth-----------------
                    #endregion

    

        #region---variables tcpsockets---------------- 

        int    serv_port = 0;
        string serv_ipad = "";
        string wifi_pass = "";
        string wifi_ssid = "";

        int data_select = 0;

        #endregion

        #region---build functions---------------------

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //default view
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //instatiate fragment views
            frag_Page1 = new FragmentConnect();
            frag_Page3 = new FragmentContinue();
            frag_Page2 = new FragmentConfigure();
            frag_spinner = new FragmentSpinner();
            frag_dialog  = new FragmentDialog();

            //add fragment views to view container
            SupportFragmentManager.BeginTransaction().Add(Resource.Id.frag_container, frag_Page1, "_fragment_connect_").Commit();
            SupportFragmentManager.BeginTransaction().Add(Resource.Id.frag_container, frag_Page3, "_fragment_continue_").Commit();
            SupportFragmentManager.BeginTransaction().Add(Resource.Id.frag_container, frag_Page2, "_fragment_configure_").Commit();
            SupportFragmentManager.BeginTransaction().Add(Resource.Id.frag_container, frag_spinner, "_frag_spinner_").Commit();
            SupportFragmentManager.BeginTransaction().Add(Resource.Id.frag_container, frag_dialog, "_frag_dialog_").Commit();

            //display only one fragment view
            SupportFragmentManager.BeginTransaction().Hide(frag_dialog).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
            SupportFragmentManager.BeginTransaction().Show(frag_Page1).Commit();

            //set up navigation bar
            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            //set up event handlers for spinner fragment
            //frag_spinner.conveyorselect_Click += connect_selected_network;
            
            //set up event handlers for Page 1 fragment
            frag_Page1.btnBTH_Open_Click += btnBTH_Open_Click;
            frag_Page1.btnBTH_Test_Click += btnBTH_Test_Click;
            frag_Page1.btnBTH_Clos_Click += btnBTH_Clos_Click;
            frag_Page1.btnBTH_List_Click += btnBTH_List_Click;
            frag_Page1.txtBTH_Conn_Click += txtBTH_Conn_Click;

            //set up event handlers for Page 3 fragment
            frag_Page3.btnCON_Cmd5_Click += btnCON_Cmd5_Click;
            frag_Page3.btnCON_Cmd6_Click += btnCON_Cmd6_Click;
            frag_Page3.btnCON_Cmd7_Click += btnCON_Cmd7_Click;
            frag_Page3.btnCON_Cmd8_Click += btnCON_Cmd8_Click;
            frag_Page3.txtCON_Msgs_Click += txtCON_Msgs_Click;

            //set up event handlers for Page 2 fragment
            frag_Page2.btnCMD_Cmd1_Click += btnCMD_Cmd1_Click;
            frag_Page2.btnCMD_Cmd2_Click += btnCMD_Cmd2_Click;
            frag_Page2.btnCMD_Cmd3_Click += btnCMD_Cmd3_Click;
            frag_Page2.btnCMD_Cmd4_Click += btnCMD_Cmd4_Click;
            frag_Page2.txtCMD_Comm_Click += txtCMD_Comm_Click;

            frag_dialog.btnDIA_Done_Click += btnDIA_Done_Click;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion

        #region---navigation handle-------------------

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:

                    navigation.SetBackgroundColor(Android.Graphics.Color.Bisque);
                    navigation.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    navigation.SetBackgroundColor(Android.Graphics.Color.Transparent);

                    SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
                    SupportFragmentManager.BeginTransaction().Show(frag_Page1).Commit();

                    return true;

                case Resource.Id.navigation_dashboard:

                    SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
                    SupportFragmentManager.BeginTransaction().Show(frag_Page2).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page1).Commit();

                    return true;

                case Resource.Id.navigation_notifications:

                    SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
                    SupportFragmentManager.BeginTransaction().Show(frag_Page3).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page1).Commit();

                    return true;
            }

            return false;
        }

        #endregion

        #region---button clicks Page 1---------------

        private void btnBTH_List_Click(object sender, object obj)
        {
            frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nListing Available WiFi Networks. Please Wait...";
            Thread thread1 = new Thread(() => get_wifi_available()); thread1.Start();
        }

        private void btnBTH_Clos_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => connect_selected_network(sender, obj)); thread1.Start();
        }

        private void btnBTH_Open_Click(object sender, object obj)
        {
            if (serv_ipad == "")  { frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = "Please Reconnect to Wifi Network";  return; }

            frag_dialog.View.FindViewById<TextView>(Resource.Id.txtDIA_Prmt).Text = "Enter Listening Server IP Address:Port Number:";
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text = ""; data_select = 2;
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Hint = serv_ipad + ".XXX:XXXX"; 

            SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page1).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
            SupportFragmentManager.BeginTransaction().Show(frag_dialog).Commit();

        }

        private void btnBTH_Test_Click(object sender, object obj)
        {
            if (serv_ipad != "") { frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = "Already Connected to Wifi Network"; return; }

            frag_dialog.View.FindViewById<TextView>(Resource.Id.txtDIA_Prmt).Text = "Enter WIFI Network Pass Phrase:";
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text = ""; data_select = 1;
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Hint = "Pass_Phrase"; 

            SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page1).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
            SupportFragmentManager.BeginTransaction().Show(frag_dialog).Commit();

        }

        private void btnDIA_Done_Click(object sender, object obj)
        {

            if (data_select == 1) 
            { 
                wifi_pass = frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text;  
            }
            else if (data_select == 2)
            {
                serv_ipad = frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text.Split(':')[0];
                serv_port = int.Parse(frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text.Split(':')[1]);  
            }

            data_select = 0;
            frag_dialog.View.FindViewById<TextView>(Resource.Id.txtDIA_Prmt).Text = "";
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Text = ""; 
            frag_dialog.View.FindViewById<EditText>(Resource.Id.txtDIA_Resp).Hint = "";

            SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
            SupportFragmentManager.BeginTransaction().Show(frag_Page1).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
            SupportFragmentManager.BeginTransaction().Hide(frag_dialog).Commit();
        }
 
        private void txtBTH_Conn_Click(object sender, object obj)
        {
            string mytext = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text;
            if (mytext.Split('\n').Length > 7)
            {
                int idx = mytext.IndexOf('\n'); string newtxt = mytext.Substring(idx + 1, mytext.Length - idx - 1);
                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = newtxt;
            }
        }

        #endregion

        #region---button clicks Page 2-------------
       
        private void btnCMD_Cmd1_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command1\n\r", "configure")); thread1.Start();
        }//complete
        
        private void btnCMD_Cmd2_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command2\n\r", "configure")); thread1.Start();
        }//complete
        
        private void btnCMD_Cmd3_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command3\n\r", "configure")); thread1.Start();
        }//complete
        
        private void btnCMD_Cmd4_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command4\n\r", "configure")); thread1.Start();
        }//complete
        
        private void txtCMD_Comm_Click(object sender, object obj)
        {
            string mytext = frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text;
            if (mytext.Split('\n').Length > 7)
            {
                int idx = mytext.IndexOf('\n'); string newtxt = mytext.Substring(idx + 1, mytext.Length - idx - 1);
                frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text = newtxt;
            }

        }//complete

        #endregion

        #region---button clicks Page 3--------------
        
        private void btnCON_Cmd5_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command5\n\r", "continue")); thread1.Start();
        }

        private void btnCON_Cmd6_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command6\n\r", "continue")); thread1.Start();
        }

        private void btnCON_Cmd7_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command7\n\r", "continue")); thread1.Start();
        }

        private void btnCON_Cmd8_Click(object sender, object obj)
        {
            Thread thread1 = new Thread(() => send_command_update_page("Command8\n\r", "continue")); thread1.Start();
        }

        private void txtCON_Msgs_Click(object sender, object obj)
        {
            string mytext = frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text;
            if (mytext.Split('\n').Length > 7)
            {
                int idx = mytext.IndexOf('\n'); string newtxt = mytext.Substring(idx + 1, mytext.Length - idx - 1);
                frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text = newtxt;
            }
        }

        #endregion

        #region---button click handler---------------
        
        private void send_command_update_page(string msg2send, string page)
        {
            if (false)
            {
                if (page == "connect") frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "Not Connected to Conveyor\n";                       //get response and output to page
                if (page == "configure") frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text = frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text + "Not Connected to Conveyor\n";                       //get response and output to page
                if (page == "continue") frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text = frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text + "Not Connected to Conveyor\n";                       //get response and output to page
            }//change to wifimanager is connected? yes/no
            else
            {
                if (true)
                {
                    if (page == "connect") frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "Sending Command: " + msg2send.TrimEnd() + "\n";   //get response and output to page
                    if (page == "configure") frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text = frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text + "Sending Command: " + msg2send.TrimEnd() + "\n";   //get response and output to page
                    if (page == "continue") frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text = frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text + "Sending Command: " + msg2send.TrimEnd() + "\n";   //get response and output to page
                }
                if (true)//change to is response? yes/no
                {
                    string recv = socketsendrecv(msg2send).Trim();
                    if (page == "connect") frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + recv + "\n";             //get response and output to page
                    if (page == "configure") frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text = frag_Page2.View.FindViewById<TextView>(Resource.Id.txtCMD_Comm).Text + recv + "\n";             //get response and output to page
                    if (page == "continue") frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text = frag_Page3.View.FindViewById<TextView>(Resource.Id.txtCON_Msgs).Text + recv + "\n";             //get response and output to page
                }
            }
        }

        #endregion

        #region---tcp socket handlers-----------------
        
        private void get_wifi_available()
        {
            WifiManager wifi = (WifiManager)GetSystemService(Context.WifiService);

            if (wifi.WifiState == Android.Net.WifiState.Enabled)
            {
                string[] arrayList = new string[100]; int i = 1; arrayList[0] = "Select Network";
                wifi.StartScan();
                Thread.Sleep(5000);
                foreach (ScanResult sr in wifi.ScanResults) { arrayList[i] = sr.Ssid.ToString(); i++;}
                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nNetworks Found: " + (i-1).ToString();

                arrayList = arrayList.Where(c => c != null).ToArray();

                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, arrayList);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                var spinner = frag_spinner.View.FindViewById<Spinner>(Resource.Id.conveyorselect);
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(save_wifi_ssid);
                spinner.SetSelection(0);
                if (i>1)
                {
                    frag_spinner.View.FindViewById<Spinner>(Resource.Id.conveyorselect).Adapter = adapter;
                    frag_spinner.View.FindViewById<Spinner>(Resource.Id.conveyorselect).Visibility = ViewStates.Visible;
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
                    SupportFragmentManager.BeginTransaction().Hide(frag_Page1).Commit();
                    SupportFragmentManager.BeginTransaction().Show(frag_spinner).Commit();
                }
                else
                {
                    frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\n" + "No Conveyor WiFi Networks Found";
                    frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\n" + "Enable WiFi & Location Permission.";
                    SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
                }
            }
            else
            {
                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\n" + "Enable WiFi & Location Permission.";
                SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
            }
        }

        private void save_wifi_ssid(object sender, object obj)
        {
            var spinner = frag_spinner.View.FindViewById<Spinner>(Resource.Id.conveyorselect);

            if (spinner.SelectedItemPosition != 0)
            {
                wifi_ssid = spinner.SelectedItem.ToString();
                wifi_pass = spinner.SelectedItem.ToString(); 

                SupportFragmentManager.BeginTransaction().Show(frag_Page1).Commit();
                SupportFragmentManager.BeginTransaction().Hide(frag_Page2).Commit();
                SupportFragmentManager.BeginTransaction().Hide(frag_Page3).Commit();
                SupportFragmentManager.BeginTransaction().Hide(frag_spinner).Commit();
                SupportFragmentManager.BeginTransaction().Hide(frag_dialog).Commit();

                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nSelected Network: " + wifi_ssid;

            }


        }
        
        private void connect_selected_network(object sender, object obj) 
        {

            frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nConnecting to Selected Network";

            WifiConfiguration wifiConfig = new WifiConfiguration();
            WifiManager wifiManager = GetSystemService(WifiService).JavaCast<WifiManager>();

            string networkSSID = wifi_ssid;
            string networkPass = wifi_pass;

            wifiConfig.Ssid         = string.Format("\"{0}\"", networkSSID);
            wifiConfig.PreSharedKey = string.Format("\"{0}\"", networkPass);

            int netId = wifiManager.AddNetwork(wifiConfig);

            wifiManager.Disconnect();
            bool b = wifiManager.EnableNetwork(netId, true);
            wifiManager.Reconnect(); Thread.Sleep(1000);

            string curr_ssid = wifiManager.ConnectionInfo.SSID.ToString();
            string curr_ipad = Formatter.FormatIpAddress(wifiManager.ConnectionInfo.IpAddress).ToString();

            serv_ipad = curr_ipad.Substring(0, curr_ipad.LastIndexOf("."));

            frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nConnected to:\t" + curr_ssid + "\nIP Address:\t" + curr_ipad;

            if (b && curr_ssid == "\"" + networkSSID + "\"")   
            { 
                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nSuccessfully Connected to: " + networkSSID;
            }
            else 
            {
                frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text = frag_Page1.View.FindViewById<TextView>(Resource.Id.txtBTH_Conn).Text + "\nFailed to Connect to: " + networkSSID;
            }   
            
        }
        
        private string socketsendrecv(string send)
        {           
            int     port = serv_port;                       // The port number for the remote device.  
            string  ipad = serv_ipad;                       // The IP address  for the remote device.
            
            byte[] bytes = new byte[2048];                  // Receive Buffer
            string recv  = String.Empty;                    // Receive String

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(ipad);                                                                                                                        // Establish the remote endpoint for the socket.
                IPAddress ipAddress = ipHostInfo.AddressList[0];                                                                                                                        // get ip address
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);                                                                                                                  // get end point
                System.Net.Sockets.Socket client = new System.Net.Sockets.Socket(ipAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);   // Create a TCP/IP socket.  
                client.Connect(remoteEP);
                int bytesSent = client.Send(Encoding.ASCII.GetBytes(send));                                                                                                             // Send test data to the remote device.
                int bytesRecv = client.Receive(bytes);                                                                                                                                  // Receive Confirmation Bytes
                recv = Encoding.ASCII.GetString(bytes, 0, bytesRecv);                                                                                                                   // Parse Confirmation to String
                bytesRecv = client.Receive(bytes);                                                                                                                                      // Receive Response String
                recv += Encoding.ASCII.GetString(bytes, 0, bytesRecv);                                                                                                                  // Parse Response to String
                client.Shutdown(SocketShutdown.Both); client.Close();                                                                                                                   // Close Connection   
            }
            catch (Exception e) { recv = e.ToString(); }                                                                                                                                //If error Return Error
            return recv;
        }

        #endregion

    }
}
