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

namespace Spla2n_Stuff
{
    [Activity(Label = "Abilities")]
    public class AbilityActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ability);

            // Setting fonts
            Typeface tf = Typeface.CreateFromAsset(Assets, "HelveticaNeue.ttf");

            List<Ability> abilities = DatabaseHelper.GetAbilities();

            CustomAdapter<Ability> adapter = new CustomAdapter<Ability>(this, abilities, tf);
            ListView list = FindViewById<ListView>(Resource.Id.abilityListView);
            list.Adapter = adapter;
        }
    }
}