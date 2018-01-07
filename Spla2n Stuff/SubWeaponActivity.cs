using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;

namespace Spla2n_Stuff {
    [Activity(Label = "Sub Weapons")]
    public class SubWeaponActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<SubWeapon> subs = DatabaseHelper.GetSubWeapons();

            CustomAdapter<SubWeapon> adapter = new CustomAdapter<SubWeapon>(this, subs, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }
    }
}