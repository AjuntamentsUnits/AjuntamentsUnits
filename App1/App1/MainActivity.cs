﻿using Android.App;
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

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {

        string tag = "MainActivity";
        LocationManager locMngr;
        string codi_postal = "";
        int codi_Ajuntament;
        TextView codi;
        //ImageButton agenda;
        //ImageButton noticia;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            String s = "";
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FormsMaps.Init(this, bundle);

            codi = FindViewById<TextView>(Resource.Id.codi_postal);

            //buscar codi postal per obrir app
            geocodificacio();
            codi_Ajuntament = agafarCodiAjuntament(codi_postal);
            //agenda.CallOnClick(Resource.Id.btnAgenda);

            ImageButton agenda = FindViewById<ImageButton>(Resource.Id.btnAgenda);
            ImageButton noticia = FindViewById<ImageButton>(Resource.Id.btnNoticies);

            agenda.Click += delegate {
                var activityAgenda = new Intent(this, typeof(AgendaActivity));
                activityAgenda.PutExtra("MyData", "Data from Agenda");
                StartActivity(activityAgenda);
            };
            noticia.Click += delegate {
                var activityNoticies = new Intent(this, typeof(NoticiaActivity));
                activityNoticies.PutExtra("MyData", "Data from Noticies");
                StartActivity(activityNoticies);
            };

        }

        public int agafarCodiAjuntament(String codi_postal)
        {
            int codi = 0;
            String xml = "";

            String FilePath = "www.ajutnamentsunits.cat/rss/WSAjuntament.php?Codi_PAj=" + codi_postal + "";

            using (var wc = new WebClient())
            {
                xml = wc.DownloadString(FilePath);
            }
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);



            return codi;
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
                Toast.MakeText(this, "The Network Provider does not exist or is not enabled!", ToastLength.Long).Show();
            }

        }
    

        public async void OnLocationChanged(Location location)
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
            codi.Text = codi_postal;

            OnStop();

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

