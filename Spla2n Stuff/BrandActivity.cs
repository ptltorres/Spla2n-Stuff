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
using Android.Graphics;
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;
using Android.Content.PM;

namespace Spla2n_Stuff
{
    [Activity(Label = "Brands", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class BrandActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);
            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<Brand> brands = DatabaseHelper.GetBrands();

            CustomAdapter<Brand> adapter = new CustomAdapter<Brand>(this, brands, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }
    }
}