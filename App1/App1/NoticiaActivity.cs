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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int Codi_Ajuntament = 0;

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

            XmlNodeList xmlnodelstTrack = xmlDoc.GetElementsByTagName("Ajuntament");
            foreach (XmlNode NodeObj in xmlnodelstTrack)
            {
                // NodeObj.ChildNodes[0].InnerText
                // NodeObj.ChildNodes[1].InnerText
                // NodeObj.ChildNodes[2].InnerText
                // NodeObj.ChildNodes[3].InnerText
                // NodeObj.ChildNodes[4].InnerText
                //Configurem layout titol i desc
                var linearLayoutParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.FillParent,
                                                          ViewGroup.LayoutParams.WrapContent);
                linearLayoutParams.Gravity = GravityFlags.CenterHorizontal;
                linearLayoutParams.SetMargins(5, 0, 0, 0);
                // <-

                LinearLayout mainLayout = FindViewById<LinearLayout>(Resource.Id.mainlayout);
                Android.Support.V7.Widget.CardView card = new Android.Support.V7.Widget.CardView(this);
                card.LayoutParameters = linearLayoutParams;
                LinearLayout itemLayout = new LinearLayout(this);

                //Textview titol
                TextView titol = new TextView(this);
                titol.SetText("Hola", TextView.BufferType.Normal);
                //Afegim els parametres de altura i amplada (layout)
                titol.LayoutParameters = linearLayoutParams;
                titol.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Rgb(153, 153, 153)));
                titol.SetTextAppearance(this, Resource.Style.TextAppearance_AppCompat_Medium);
                itemLayout.AddView(titol);
                mainLayout.AddView(itemLayout);

                //TextView Descripcio
                TextView desc = new TextView(this);
                desc.SetText("Descripciooooo", TextView.BufferType.Normal);
                //Afegim els parametres de altura i amplada (layout)
                desc.LayoutParameters = linearLayoutParams;
                desc.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Rgb(170, 170, 170)));
                desc.SetTextAppearance(this, Resource.Style.TextAppearance_AppCompat_Medium);
                itemLayout.AddView(desc);
                mainLayout.AddView(itemLayout);

            }

        }
    }
}