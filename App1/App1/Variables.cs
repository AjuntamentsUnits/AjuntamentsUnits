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

namespace App1
{
    class Variables
    {
        private int Codi_Ajuntament;
        private String Codi_Postal;

        public int Get_Codi_Ajuntament
        {
            get
            {
                return Codi_Ajuntament;
            }

            set
            {
                Codi_Ajuntament = value;
            }
        }

        public string Get_Codi_Postal
        {
            get
            {
                return Codi_Postal;
            }

            set
            {
                Codi_Postal = value;
            }
        }
    }
}