using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace sfdsg
{
    [Activity(Label = "sfdsg", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            ImageButton agenda = FindViewById<ImageButton>(Resource.Id.btnAgenda);

            int Codi_Ajuntament = 1;

            agenda.Click += delegate {
                Intent i = new Intent(this, typeof(AgendaActivity));
                i.PutExtra("Codi_Ajuntament", Codi_Ajuntament);
                StartActivity(i);
            };
        }
    }
}

