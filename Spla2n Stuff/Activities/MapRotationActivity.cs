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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Spla2n_Stuff.Activities;


namespace Spla2n_Stuff {
    [Activity(Label = "Upcoming Rotation", ParentActivity = typeof(MainActivity), ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class MapRotationActivity : BaseActivity {

        private const string mToolbarTitle = "Upcoming Rotation";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

            SetupToolbar(mToolbarTitle);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "Paintball.ttf");

            string mode = Intent.GetStringExtra("Mode");

            List<MapRotation> mapRotation = new List<MapRotation>();

            if (mode == RotationFragment.regular) {
                mapRotation.AddRange(RotationFragment.regularMapRotation);
            } else if (mode == MainActivity.ranked) {
                mapRotation.AddRange(RotationFragment.rankedMapRotation);
            } else if (mode == MainActivity.league) {
                mapRotation.AddRange(RotationFragment.leagueMapRotation);
            }

            MapRotationAdapter adapter = new MapRotationAdapter(this, mapRotation, tf, mode);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.SetBackgroundResource(Resource.Drawable.background_image);
            list.Adapter = adapter;
        }
    }
}