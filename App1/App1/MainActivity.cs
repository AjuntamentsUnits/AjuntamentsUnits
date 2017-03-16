using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Runtime;
using System;
using Android.Util;
using Xamarin;
using System.Xml;
using System.Net;
using Android.Content;
using Android.Net;
using System.IO;
using System.Threading.Tasks;
using Org.Json;
using Xamarin.Forms.Platform.Android;

namespace App1
{
    [Activity(Label = "AjuntamentsUnits", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {

        string tag = "MainActivity";
        Boolean loctrobat = false;
        LocationManager locMngr;
        string codi_postal = "";
        int codi_Ajuntament;
        ImageButton agenda;
        ImageButton noticia;
        Variables v = new Variables();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FormsMaps.Init(this, bundle);

           // codi = FindViewById<TextView>(Resource.Id.codi_postal);
           // ImageButton agenda = FindViewById<ImageButton>(Resource.Id.btnAgenda);
            noticia = FindViewById<ImageButton>(Resource.Id.btnNoticies);
            agenda = FindViewById<ImageButton>(Resource.Id.btnAgenda);


            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            bool isOnline = networkInfo.IsConnected;

            if (isOnline)
            {
                //buscar codi postal per obrir app
                geocodificacio();
            }
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
                       
            

            agenda.Click += delegate {
                 Intent i = new Intent(this, typeof(AgendaActivity));
                 i.PutExtra("Codi_Ajuntament", codi_Ajuntament);
                 StartActivity(i);
            };
            noticia.Click += delegate {
                Intent myIntent = new Intent(this, typeof(NoticiaActivity));
                myIntent.PutExtra("Codi_Ajuntament", codi_Ajuntament);
                StartActivity(myIntent);
            };

        }

        public void agafarCodiAjuntament(String codi_postal)
        {
            codi_postal = "08503";
            String url = "http://www.ajuntamentsunits.cat/rss/WSAjuntament.php?Codi_PAj=" + codi_postal + "";

            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.UserAgent = "AjuntamentsUnits";
            XmlDocument xmlDoc = new XmlDocument();

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                xmlDoc.Load(resp.GetResponseStream());
            }

            XmlNodeList xmlnodelstTrack = xmlDoc.GetElementsByTagName("Ajuntament");
            foreach (XmlNode NodeObj in xmlnodelstTrack)
            {
                codi_Ajuntament = Convert.ToInt32(NodeObj.ChildNodes[0].FirstChild.Value);
            }
            v.Get_Codi_Ajuntament = codi_Ajuntament;
           
        }

        public void geocodificacio()
        {
            //comença a buscar la geoloalitzacio
                        
            locMngr = GetSystemService(LocationService) as LocationManager;

            if(locMngr.AllProviders.Contains(LocationManager.NetworkProvider) && locMngr.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                locMngr.RequestLocationUpdates(LocationManager.NetworkProvider, 2, 1, this);
            }
            else
            {
                Toast.MakeText(this, "No s'ha pogut trobar cap proveïdor de xarxa. Reinicia l'aplicació!", ToastLength.Long).Show();
            }

        }


        public async void OnLocationChanged(Location location)
        {

            if (!loctrobat)
            {
                // al canviar localitzacio, buscara quina és.
                string latitude = "";
                string longitude = "";
                Log.Debug(tag, "Location changed");
                latitude = location.Latitude.ToString();
                longitude = location.Longitude.ToString();


                System.Collections.Generic.List<Address> addresses;
                Geocoder geocoder = new Geocoder(this);


                addresses = new System.Collections.Generic.List<Address>(geocoder.GetFromLocation(location.Latitude, location.Longitude, 1)); // Here 1 represent max location result to returned, by documents it recommended 1 to 5
                string s = addresses[0].GetAddressLine(1).ToString();

                codi_postal = s.Split()[0];

                v.Get_Codi_Postal = codi_postal;

                //codi_Ajuntament = agafarCodiAjuntament(codi_postal);
                agafarCodiAjuntament(codi_postal);
                loctrobat = true;
            }
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}

