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
using Spla2n_Stuff.Helpers;
using Spla2n_Stuff.GearTypes;
using Android.Graphics;
using Android.Content.PM;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Spla2n_Stuff.Activities;


namespace Spla2n_Stuff {
    [Activity(Label = "Gear", ParentActivity = typeof(MainActivity), ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class GearActivity : BaseActivity {
        private const string mToolbarTitle = "Gear";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListView_Activity_Layout);

            SetupToolbar(mToolbarTitle);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<Gear> gear = null;

            string type = Intent.GetStringExtra("Type");

            if (type == typeof(Headgear).Name) {
                gear = DatabaseHelper.GetGear<Headgear>();
            } else if (type == typeof(Clothe).Name) {
                gear = DatabaseHelper.GetGear<Clothe>();
            } else if (type == typeof(Shoe).Name) {
                gear = DatabaseHelper.GetGear<Shoe>();
            }

            CustomAdapter<Gear> adapter = new CustomAdapter<Gear>(this, gear, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }
    }
}