using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;
using System.Collections.Generic;
using Spla2n_Stuff.Maps;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using System;
using Android.Views;
using Android.Content.PM;
using Android.Gms.Ads;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using V4ViewPager = Android.Support.V4.View.ViewPager;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V4FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Spla2n_Stuff.Activities;

namespace Spla2n_Stuff
{
    [Activity(Label = "Spla2n Stuff", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity {
        //private static string TAG = "MainActivity";
     
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        public const string regular = "regular";
        public const string ranked = "gachi";
        public const string league = "league";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            SetupToolbar();
            SetupTabs();
          

            // Prepare Database
            CopyDatabase("Spla2n.db");
          
        }

        protected override void OnResume() {
            base.OnResume();
        }

        private void SetupToolbar() {
            // Set the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.mainActivityToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;
        }

        private void SetupTabs() {
            V4ViewPager viewPager = FindViewById<V4ViewPager>(Resource.Id.pager);
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);

            // Add Fragments to adapter one by one
            adapter.AddFragment(new RotationFragment(), "Map Rotation");
            adapter.AddFragment(new NewsFragment(), "News");

            viewPager.Adapter = adapter;

            TabLayout tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void HomeNavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e) {
            var menuItem = e.MenuItem;
            menuItem.SetChecked(!menuItem.IsChecked);
            Intent activity = null;

            if (menuItem.ItemId == Resource.Id.nav_abilities) {
                activity = new Intent(this, typeof(AbilityActivity));
            } else if (menuItem.ItemId == Resource.Id.nav_brands) {
                activity = new Intent(this, typeof(BrandActivity));
            } else if (menuItem.ItemId == Resource.Id.nav_headgear) {
                activity = new Intent(this, typeof(GearActivity));
                activity.PutExtra("Type", "Headgear");
            } else if (menuItem.ItemId == Resource.Id.nav_clothes) {
                activity = new Intent(this, typeof(GearActivity));
                activity.PutExtra("Type", "Clothe");
            } else if (menuItem.ItemId == Resource.Id.nav_shoes) {
                activity = new Intent(this, typeof(GearActivity));
                activity.PutExtra("Type", "Shoe");
            } else if (menuItem.ItemId == Resource.Id.nav_specials) {
                activity = new Intent(this, typeof(SpecialActivity));
            } else if (menuItem.ItemId == Resource.Id.nav_weapons) {
                activity = new Intent(this, typeof(WeaponActivity));
            } else if (menuItem.ItemId == Resource.Id.nav_subweapons) {
                activity = new Intent(this, typeof(SubWeaponActivity));
            }

            if (activity != null )
                StartActivity(activity);
        }

        
        private void CopyDatabase(string dataBaseName) {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dataBaseName);

            if (!System.IO.File.Exists(dbPath)) {
                var dbAssetStream = Assets.Open(dataBaseName);
                var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0) {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
        }

  
    }
}

