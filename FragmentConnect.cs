using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App3
{
    public class FragmentConnect : Android.Support.V4.App.Fragment
    {
        public Button   btnBTH_List;
        public Button   btnBTH_Open;
        public Button   btnBTH_Clos;
        public Button   btnBTH_Test;
        public TextView txtBTH_Conn;

        public event EventHandler btnBTH_Test_Click;
        public event EventHandler btnBTH_List_Click;
        public event EventHandler btnBTH_Clos_Click;
        public event EventHandler btnBTH_Open_Click;
        public event EventHandler txtBTH_Conn_Click;

        public override void OnCreate(Bundle savedInstanceState){base.OnCreate(savedInstanceState);}
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_connect, container, false);
            
            btnBTH_Open = view.FindViewById<Button>  (Resource.Id.btnBTH_Open); btnBTH_Open.Click       += btnBTH_Open_Handle;
            btnBTH_Test = view.FindViewById<Button>  (Resource.Id.btnBTH_Test); btnBTH_Test.Click       += btnBTH_Test_Handle;
            btnBTH_Clos = view.FindViewById<Button>  (Resource.Id.btnBTH_Clos); btnBTH_Clos.Click       += btnBTH_Clos_Handle;
            btnBTH_List = view.FindViewById<Button>  (Resource.Id.btnBTH_List); btnBTH_List.Click       += btnBTH_List_Handle;
            txtBTH_Conn = view.FindViewById<TextView>(Resource.Id.txtBTH_Conn); txtBTH_Conn.TextChanged += txtBTH_Conn_Handle;
            
            return view;
        }

        public void btnBTH_List_Handle(object sender, System.EventArgs e){btnBTH_List_Click(sender, e);}
        public void btnBTH_Open_Handle(object sender, System.EventArgs e){btnBTH_Open_Click(sender, e);}
        public void btnBTH_Clos_Handle(object sender, System.EventArgs e){btnBTH_Clos_Click(sender, e);}
        public void btnBTH_Test_Handle(object sender, System.EventArgs e){btnBTH_Test_Click(sender, e);}
        public void txtBTH_Conn_Handle(object sender, System.EventArgs e){txtBTH_Conn_Click(sender, e);}
    }
}