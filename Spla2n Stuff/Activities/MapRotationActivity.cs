using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spla2n_Stuff.Maps;


namespace Spla2n_Stuff {
    [Activity(Label = "Upcoming Rotation", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class MapRotationActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "Paintball.ttf");

            string mode = Intent.GetStringExtra("Mode");

            List<MapRotation> mapRotation = null;

            if (mode == MainActivity.regular) {
                mapRotation = MainActivity.regularMapRotation;
            } else if (mode == MainActivity.ranked) {
                mapRotation = MainActivity.rankedMapRotation;
            } else if (mode == MainActivity.league) {
                mapRotation = MainActivity.leagueMapRotation;
            }

            MapRotationAdapter adapter = new MapRotationAdapter(this, mapRotation, tf, mode);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.SetBackgroundResource(Resource.Drawable.background_image);
            list.Adapter = adapter;
        }
    }
}