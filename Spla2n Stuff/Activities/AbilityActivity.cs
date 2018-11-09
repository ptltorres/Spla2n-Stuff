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

namespace Spla2n_Stuff
{
    [Activity(Label = "Abilities",ParentActivity = typeof(MainActivity),ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class AbilityActivity : BaseActivity {

        private const string mToolbarTitle = "Abilities";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
          
            // Create your application here
            SetContentView(Resource.Layout.ListView_Activity_Layout);

            SetupToolbar(mToolbarTitle);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<Ability> abilities = DatabaseHelper.GetAbilities();

            CustomAdapter<Ability> adapter = new CustomAdapter<Ability>(this, abilities, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }

    }

}