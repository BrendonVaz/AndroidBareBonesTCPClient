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
    public class FragmentSpinner : Android.Support.V4.App.Fragment
    {

        public override void OnCreate(Bundle savedInstanceState) {base.OnCreate(savedInstanceState);}
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_spinner, container, false);
            return view;
        }
        
    }
}
