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
using System.Net;
using System.Xml;

namespace App1
{
    [Activity(Label = "NoticiaActivity")]
    public class NoticiaActivity : Activity
    {

        TextView textView1;
        int Codi_Ajuntament = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.noticies);

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            

            // Create your application here
            Codi_Ajuntament = Convert.ToInt32(Intent.GetStringExtra("Codi_Ajuntament"));
            agafarNoticies(Codi_Ajuntament);
        }


        public void agafarNoticies(int Codi_Ajuntament)
        {

            String url = "http://www.ajuntamentsunits.cat/rss/WSNoticia.php?Codi_Aj=" + Codi_Ajuntament + "";

            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.UserAgent = "AjuntamentsUnits";
            XmlDocument xmlDoc = new XmlDocument();

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                xmlDoc.Load(resp.GetResponseStream());
            }

            XmlNodeList xmlnodelstTrack = xmlDoc.GetElementsByTagName("Noticia");
            foreach (XmlNode NodeObj in xmlnodelstTrack)
            {
                  textView1.Text = NodeObj.ChildNodes[0].FirstChild.Value;
                //  textView1.Text = textView1.Text + NodeObj.ChildNodes[1].FirstChild.Value;
                //  textView1.Text = textView1.Text + NodeObj.ChildNodes[2].FirstChild.Value;
                //  textView1.Text = textView1.Text + NodeObj.ChildNodes[3].FirstChild.Value;
            }

        }
    }
}