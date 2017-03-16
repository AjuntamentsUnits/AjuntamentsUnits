using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace sfdsg
{
    class AgendaActivity : Activity
    {
        TextView txt;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int CA = 0;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AgendaActivity);

            txt = FindViewById<TextView>(Resource.Id.txt);

            CA = Intent.GetIntExtra("Codi_Ajuntament",0);
            xx(CA);
        }

        public void xx(int ca)
        {
            txt.Text = "" + ca;
        }
    }
}