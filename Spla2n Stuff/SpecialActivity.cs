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
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;
using Android.Graphics;
using Android.Content.PM;

namespace Spla2n_Stuff
{
    [Activity(Label = "Specials", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SpecialActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<Special> abilities = DatabaseHelper.GetSpecials();
            CustomAdapter<Special> adapter = new CustomAdapter<Special>(this, abilities, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }
    }
}