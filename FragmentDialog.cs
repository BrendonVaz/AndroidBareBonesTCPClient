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
    public class FragmentDialog : Android.Support.V4.App.Fragment
    {
        public Button   btnDIA_Done;
        public TextView txtDIA_Prmt;
        public EditText txtDIA_Resp;

        public event EventHandler btnDIA_Done_Click;

        public override void OnCreate(Bundle savedInstanceState) { base.OnCreate(savedInstanceState); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_dialog, container, false);

            btnDIA_Done = view.FindViewById<Button>(Resource.Id.btnDIA_Done); btnDIA_Done.Click += btnDIA_Done_Handle;
            txtDIA_Prmt = view.FindViewById<TextView>(Resource.Id.txtDIA_Prmt); 
            txtDIA_Resp = view.FindViewById<EditText>(Resource.Id.txtDIA_Resp); 

            return view;
        }

        public void btnDIA_Done_Handle(object sender, System.EventArgs e) { btnDIA_Done_Click(sender, e); }

    }
}
