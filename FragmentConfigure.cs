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
    public class FragmentConfigure : Android.Support.V4.App.Fragment
    {
        public Button   btnCMD_Cmd1;
        public Button   btnCMD_Cmd2;
        public Button   btnCMD_Cmd3;
        public Button   btnCMD_Cmd4;
        public TextView txtCMD_Comm;

        public event EventHandler btnCMD_Cmd1_Click;
        public event EventHandler btnCMD_Cmd2_Click;
        public event EventHandler btnCMD_Cmd3_Click;
        public event EventHandler btnCMD_Cmd4_Click;
        public event EventHandler txtCMD_Comm_Click;
        public override void OnCreate(Bundle savedInstanceState){base.OnCreate(savedInstanceState);}
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_configure, container, false);

            btnCMD_Cmd1 = view.FindViewById<Button>(Resource.Id.btnCMD_Cmd1);   btnCMD_Cmd1.Click       += btnCMD_Cmd1_Handle;
            btnCMD_Cmd2 = view.FindViewById<Button>(Resource.Id.btnCMD_Cmd2);   btnCMD_Cmd2.Click       += btnCMD_Cmd2_Handle;
            btnCMD_Cmd3 = view.FindViewById<Button>(Resource.Id.btnCMD_Cmd3);   btnCMD_Cmd3.Click       += btnCMD_Cmd3_Handle;
            btnCMD_Cmd4 = view.FindViewById<Button>(Resource.Id.btnCMD_Cmd4);   btnCMD_Cmd4.Click       += btnCMD_Cmd4_Handle;
            txtCMD_Comm = view.FindViewById<TextView>(Resource.Id.txtCMD_Comm); txtCMD_Comm.TextChanged += txtCMD_Comm_Handle;

            return view;
        }
        public void btnCMD_Cmd1_Handle(object sender, System.EventArgs e){btnCMD_Cmd1_Click(sender, e);}
        public void btnCMD_Cmd2_Handle(object sender, System.EventArgs e){btnCMD_Cmd2_Click(sender, e);}
        public void btnCMD_Cmd3_Handle(object sender, System.EventArgs e){btnCMD_Cmd3_Click(sender, e);}
        public void btnCMD_Cmd4_Handle(object sender, System.EventArgs e){btnCMD_Cmd4_Click(sender, e);}
        public void txtCMD_Comm_Handle(object sender, System.EventArgs e){txtCMD_Comm_Click(sender, e);}
    }
}