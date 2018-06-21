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
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;

namespace Spla2n_Stuff {
    [Activity(Label = "Gear", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class GearActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

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