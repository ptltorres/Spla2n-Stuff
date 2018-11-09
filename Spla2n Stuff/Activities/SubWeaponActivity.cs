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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Spla2n_Stuff.Activities;

namespace Spla2n_Stuff {
    [Activity(Label = "Sub Weapons", ParentActivity = typeof(MainActivity), ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class SubWeaponActivity : BaseActivity {
        private const string mToolbarTitle = "Sub Weapons";
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListView_Activity_Layout);

            SetupToolbar(mToolbarTitle);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<SubWeapon> subs = DatabaseHelper.GetSubWeapons();

            CustomAdapter<SubWeapon> adapter = new CustomAdapter<SubWeapon>(this, subs, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }

    }
}