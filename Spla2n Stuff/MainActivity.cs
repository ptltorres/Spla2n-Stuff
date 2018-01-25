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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Spla2n_Stuff
{
    [Activity(Label = "Spla2n Stuff", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity {
        private static string TAG = "MainActivity";

        //DatabaseHelper dbHelper;
        private Typeface tf;


        // Map rotation views
        private TextView regularTitle;
        private TextView rankedTitle;
        private TextView leagueTitle;
        private TextView regularDesc;
        private TextView regularMap1Name;
        private TextView regularMap2Name;
        private ImageView regularMap1Image;
        private ImageView regularMap2Image;
        private TextView rankedDesc;
        private TextView rankedMap1Name;
        private TextView rankedMap2Name;
        private ImageView rankedMap1Image;
        private ImageView rankedMap2Image;
        private TextView leagueDesc;
        private TextView leagueMap1Name;
        private TextView leagueMap2Name;
        private ImageView leagueMap1Image;
        private ImageView leagueMap2Image;

        private List<MapRotation> mapRotation;

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;


            var id = "ca-app-pub-1486333068830863~7686704947";
            Android.Gms.Ads.MobileAds.Initialize(this, id);
            var adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);


            CopyDatabase("Spla2n.db");

            tf = Typeface.CreateFromAsset(Assets, "Paintball.ttf");

            
            SetUpMapViews();

            Task.Run(async () => {
                mapRotation = await MapRotationHelper.GetMapRotationAsync();

                RunOnUiThread(() => {
                    BindRegularMode();
                    BindRankedMode();
                    BindLeagueMode();
                });      
            });

        }

        protected override void OnResume() {
            base.OnResume();

            Task.Run(async () => {
                mapRotation = await MapRotationHelper.GetMapRotationAsync();

                RunOnUiThread(() => {
                    BindRegularMode();
                    BindRankedMode();
                    BindLeagueMode();
                });
            });
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
            Intent intent;
            Intent activity;

            switch (menuItem.ItemId) {
                case Resource.Id.nav_abilities:
                    intent = new Intent(this, typeof(AbilityActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_brands:
                    intent = new Intent(this, typeof(BrandActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_headgear:
                    activity = new Intent(this, typeof(GearActivity));
                    activity.PutExtra("Type", "Headgear");
                    StartActivity(activity);
                    break;
                case Resource.Id.nav_clothes:
                    activity = new Intent(this, typeof(GearActivity));
                    activity.PutExtra("Type", "Clothe");
                    StartActivity(activity);
                    break;
                case Resource.Id.nav_shoes:
                    activity = new Intent(this, typeof(GearActivity));
                    activity.PutExtra("Type", "Shoe");
                    StartActivity(activity);
                    break;
                case Resource.Id.nav_specials:
                    intent = new Intent(this, typeof(SpecialActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_weapons:
                    intent = new Intent(this, typeof(WeaponActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_subweapons:
                    intent = new Intent(this, typeof(SubWeaponActivity));
                    StartActivity(intent);
                    break;
            }
        }

        private void BindRegularMode() {
            regularDesc.Text = mapRotation[0].GameMode;
            regularMap1Name.Text = mapRotation[0].Maps[0].Name;
            regularMap2Name.Text = mapRotation[0].Maps[1].Name;

            try {
                regularMap1Image.SetImageResource(ImageHelper.GetImageId(regularMap1Name.Text));
            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }

            try {
                regularMap2Image.SetImageResource(ImageHelper.GetImageId(regularMap2Name.Text));
            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }
        }

        private void BindRankedMode() {
            rankedDesc.Text = mapRotation[1].GameMode;
            rankedMap1Name.Text = mapRotation[1].Maps[0].Name;
            rankedMap2Name.Text = mapRotation[1].Maps[1].Name;

            try {
                rankedMap1Image.SetImageResource(ImageHelper.GetImageId(rankedMap1Name.Text));

            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }

            try {
                rankedMap2Image.SetImageResource(ImageHelper.GetImageId(rankedMap2Name.Text));

            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }
        }

        private void BindLeagueMode() {
            leagueDesc.Text = mapRotation[2].GameMode;
            leagueMap1Name.Text = mapRotation[2].Maps[0].Name;
            leagueMap2Name.Text = mapRotation[2].Maps[1].Name;

            try {
                leagueMap1Image.SetImageResource(ImageHelper.GetImageId(leagueMap1Name.Text));

            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }

            try {
                leagueMap2Image.SetImageResource(ImageHelper.GetImageId(leagueMap2Name.Text));

            } catch (Exception e) {
                Android.Util.Log.Error(TAG, e.StackTrace);
            }
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

        private void SetUpMapViews() {
            regularTitle = FindViewById<TextView>(Resource.Id.regularModeTitle);
            regularTitle.SetTypeface(tf, TypefaceStyle.Bold);
            rankedTitle = FindViewById<TextView>(Resource.Id.rankedModeTitle);
            rankedTitle.SetTypeface(tf, TypefaceStyle.Bold);
            leagueTitle = FindViewById<TextView>(Resource.Id.leagueModeTitle);
            leagueTitle.SetTypeface(tf, TypefaceStyle.Bold);

            regularDesc = FindViewById<TextView>(Resource.Id.regularModeDesc);
            regularDesc.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap1Name = FindViewById<TextView>(Resource.Id.regular1Name);
            regularMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap2Name = FindViewById<TextView>(Resource.Id.regular2Name);
            regularMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap1Image = FindViewById<ImageView>(Resource.Id.regular1Image);
            regularMap2Image = FindViewById<ImageView>(Resource.Id.regular2Image);

            rankedDesc = FindViewById<TextView>(Resource.Id.rankedModeDesc);
            rankedDesc.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap1Name = FindViewById<TextView>(Resource.Id.ranked1Name);
            rankedMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap2Name = FindViewById<TextView>(Resource.Id.ranked2Name);
            rankedMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap1Image = FindViewById<ImageView>(Resource.Id.ranked1Image);
            rankedMap2Image = FindViewById<ImageView>(Resource.Id.ranked2Image);

            leagueDesc = FindViewById<TextView>(Resource.Id.leagueModeDesc);
            leagueDesc.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap1Name = FindViewById<TextView>(Resource.Id.league1Name);
            leagueMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap2Name = FindViewById<TextView>(Resource.Id.league2Name);
            leagueMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap1Image = FindViewById<ImageView>(Resource.Id.league1Image);
            leagueMap2Image = FindViewById<ImageView>(Resource.Id.league2Image);
        }
    }
}

