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
    public class MainActivity : AppCompatActivity, View.IOnClickListener {
        private static string TAG = "MainActivity";

        //Font
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
        private Button upcomingRegularBtn;
        private TextView rankedDesc;
        private TextView rankedMap1Name;
        private TextView rankedMap2Name;
        private ImageView rankedMap1Image;
        private ImageView rankedMap2Image;
        private Button upcomingRankedBtn;
        private TextView leagueDesc;
        private TextView leagueMap1Name;
        private TextView leagueMap2Name;
        private ImageView leagueMap1Image;
        private ImageView leagueMap2Image;
        private Button upcomingLeagueBtn;

        
        // Lists for map rotations
        public static List<MapRotation> regularMapRotation;
        public static List<MapRotation> rankedMapRotation;
        public static List<MapRotation> leagueMapRotation;

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        public const string regular = "regular";
        public const string ranked = "gachi";
        public const string league = "league";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;

            // Set AdMob
            var id = KeysHelper.AdMob;
            Android.Gms.Ads.MobileAds.Initialize(this, id);
            var adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);

            // Prepare Database
            CopyDatabase("Spla2n.db");

            // Font to be used in the activity
            tf = Typeface.CreateFromAsset(Assets, "Paintball.ttf");

            // Setup the views
            SetViews();
            SetRotationImages();
        }

        protected override void OnResume() {
            base.OnResume();
            SetRotationImages();
        }

        public void OnClick(View v) {
            Intent activity = null;

            if (v == upcomingRegularBtn) {
                activity = new Intent(this, typeof(MapRotationActivity));
                activity.PutExtra("Mode", regular);
            } else if (v == upcomingRankedBtn) {
                activity = new Intent(this, typeof(MapRotationActivity));
                activity.PutExtra("Mode", ranked);
            } else if (v == upcomingLeagueBtn) {
                activity = new Intent(this, typeof(MapRotationActivity));
                activity.PutExtra("Mode", league);
            }

            if (activity != null)
                StartActivity(activity);
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

        private void SetRotationImages() {
            Task.Run(async () => {
                regularMapRotation = await MapRotationHelper.GetMapRotationAsync(regular);
                rankedMapRotation = await MapRotationHelper.GetMapRotationAsync(ranked);
                leagueMapRotation = await MapRotationHelper.GetMapRotationAsync(league);

                RunOnUiThread(() => {
                    BindRegularMode();
                    BindRankedMode();
                    BindLeagueMode();
                });
            });
        }

        private void BindRegularMode() {
            regularDesc.Text = regularMapRotation[0].GameMode;
            regularMap1Name.Text = regularMapRotation[0].Maps[0].Name;
            regularMap2Name.Text = regularMapRotation[0].Maps[1].Name;

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
            rankedDesc.Text = rankedMapRotation[0].GameMode;
            rankedMap1Name.Text = rankedMapRotation[0].Maps[0].Name;
            rankedMap2Name.Text = rankedMapRotation[0].Maps[1].Name;

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
            leagueDesc.Text = leagueMapRotation[0].GameMode;
            leagueMap1Name.Text = leagueMapRotation[0].Maps[0].Name;
            leagueMap2Name.Text = leagueMapRotation[0].Maps[1].Name;

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

        private void SetViews() {
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

            // Buttons setup
            upcomingRegularBtn = FindViewById<Button>(Resource.Id.upcomingStagesTurf);
            upcomingRegularBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingRegularBtn.SetOnClickListener(this);

            upcomingRankedBtn = FindViewById<Button>(Resource.Id.upcomingStagesRanked);
            upcomingRankedBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingRankedBtn.SetOnClickListener(this);

            upcomingLeagueBtn = FindViewById<Button>(Resource.Id.upcomingStagesLeague);
            upcomingLeagueBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingLeagueBtn.SetOnClickListener(this);
        }
    }
}

