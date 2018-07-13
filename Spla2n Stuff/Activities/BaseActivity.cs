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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Spla2n_Stuff.Activities {

    [Activity(Label = "BaseActivity")]
    public class BaseActivity : AppCompatActivity {
        
        protected void SetupToolbar(string title) {
            // Set the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.ActivityToolbar);

            if (toolbar != null) {
                TextView toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
                toolbarTitle.Text = title;
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowTitleEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(true);
            }

        }
    }
}