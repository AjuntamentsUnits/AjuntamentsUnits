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
    [Activity(Label = "AgendaActivity")]
    public class AgendaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int Codi_Ajuntament = 0;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            /*
             *  <android.support.v7.widget.CardView
        android:layout_width="fill_parent"
        android:layout_height="wrap_content"
        android:layout_gravity="center_horizontal"
        cardview:cardElevation="4dp"
        cardview:cardCornerRadius="5dp">
        <LinearLayout
            android:layout_width="fill_parent"
            android:layout_height="wrap_content"
            android:orientation="vertical"
            android:padding="8dp">
            <ImageView
                android:layout_width="fill_parent"
                android:layout_height="190dp"
                android:id="@+id/imageView"
                android:scaleType="centerCrop" />
            <TextView
                android:layout_width="fill_parent"
                android:layout_height="wrap_content"
                android:textAppearance="?android:attr/textAppearanceMedium"
                android:textColor="#999999"
                android:text="titol"
                android:id="@+id/textView"
                android:layout_gravity="center_horizontal"
                android:layout_marginLeft="5dp" />
            <TextView
                android:layout_width="fill_parent"
                android:layout_height="wrap_content"
                android:textAppearance="?android:attr/textAppearanceMedium"
                android:textColor="#AAAAAA"
                android:text="Descripció"
                android:id="@+id/textView"
                android:layout_gravity="center_horizontal"
                android:layout_marginLeft="5dp" />
        </LinearLayout>
    </android.support.v7.widget.CardView>*/
             
             

            LinearLayout mainLayout = FindViewById <LinearLayout> (Resource.Id.mainlayout);
            Android.Support.V7.Widget.CardView card = new Android.Support.V7.Widget.CardView(this);
            LinearLayout itemLayout = new LinearLayout(this);
            TextView titol = new TextView(this);
            titol.SetText("Hola", TextView.BufferType.Normal);
            itemLayout.AddView(titol);
            mainLayout.AddView(itemLayout);

            // Create your application here
            Codi_Ajuntament = Convert.ToInt32(Intent.GetStringExtra("Codi_Ajuntament"));
            agafarAgenda(Codi_Ajuntament);
        }

        public void agafarAgenda(int Codi_Ajuntament)
        {

            String url = "http://www.ajuntamentsunits.cat/rss/WSAgenda.php?Codi_Aj=" + Codi_Ajuntament + "";

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
            }

        }
    }
}
 