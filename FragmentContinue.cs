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
    public class FragmentContinue : Android.Support.V4.App.Fragment
    {
        public Button   btnCON_Cmd5;
        public Button   btnCON_Cmd6;
        public Button   btnCON_Cmd7;
        public Button   btnCON_Cmd8;
        public TextView txtCON_Msgs;

        public event EventHandler btnCON_Cmd5_Click;
        public event EventHandler btnCON_Cmd6_Click;
        public event EventHandler btnCON_Cmd7_Click;
        public event EventHandler btnCON_Cmd8_Click;
        public event EventHandler txtCON_Msgs_Click;
        public override void OnCreate(Bundle savedInstanceState){base.OnCreate(savedInstanceState);}
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment_continue, container, false);

            btnCON_Cmd5 = view.FindViewById<Button>  (Resource.Id.btnCON_Cmd5); btnCON_Cmd5.Click       += btnCON_Cmd5_Handle;
            btnCON_Cmd6 = view.FindViewById<Button>  (Resource.Id.btnCON_Cmd6); btnCON_Cmd6.Click       += btnCON_Cmd6_Handle;
            btnCON_Cmd7 = view.FindViewById<Button>  (Resource.Id.btnCON_Cmd7); btnCON_Cmd7.Click       += btnCON_Cmd7_Handle;
            btnCON_Cmd8 = view.FindViewById<Button>  (Resource.Id.btnCON_Cmd8); btnCON_Cmd8.Click       += btnCON_Cmd8_Handle;
            txtCON_Msgs = view.FindViewById<TextView>(Resource.Id.txtCON_Msgs); txtCON_Msgs.TextChanged += txtCON_Msgs_Handle;
            
            return view;
        }
        public void btnCON_Cmd5_Handle(object sender, System.EventArgs e){btnCON_Cmd5_Click(sender, e);}
        public void btnCON_Cmd6_Handle(object sender, System.EventArgs e){btnCON_Cmd6_Click(sender, e);}
        public void btnCON_Cmd7_Handle(object sender, System.EventArgs e){btnCON_Cmd7_Click(sender, e);}
        public void btnCON_Cmd8_Handle(object sender, System.EventArgs e){btnCON_Cmd8_Click(sender, e);}
        public void txtCON_Msgs_Handle(object sender, System.EventArgs e){txtCON_Msgs_Click(sender, e);}
    }
}