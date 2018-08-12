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

namespace Spla2n_Stuff.Activities {
    public class RotationFragment : Android.Support.V4.App.Fragment, View.IOnClickListener {
        private readonly static string TAG = "RotationFragment";

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

        private bool mapsReady = false;


        // Lists for map rotations
        public static List<MapRotation> regularMapRotation;
        public static List<MapRotation> rankedMapRotation;
        public static List<MapRotation> leagueMapRotation;

        public const string regular = "regular";
        public const string ranked = "gachi";
        public const string league = "league";


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Rotation_Fragment_Layout, container, false);


            // Font to be used in the activity
            tf = Typeface.CreateFromAsset(this.Context.Assets, "Paintball.ttf");

            // Setup the views
            SetViews(view);
            SetRotationImages();

            // Set AdMob
            var id = KeysHelper.AdMob;
            Android.Gms.Ads.MobileAds.Initialize(Context, id);
            var adView = view.FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);

            return view;
        }

        public override void OnResume() {
            base.OnResume();
            SetRotationImages();
        }

        private void SetViews(View view) {
            regularTitle = view.FindViewById<TextView>(Resource.Id.regularModeTitle);
            regularTitle.SetTypeface(tf, TypefaceStyle.Bold);
            rankedTitle = view.FindViewById<TextView>(Resource.Id.rankedModeTitle);
            rankedTitle.SetTypeface(tf, TypefaceStyle.Bold);
            leagueTitle = view.FindViewById<TextView>(Resource.Id.leagueModeTitle);
            leagueTitle.SetTypeface(tf, TypefaceStyle.Bold);

            regularDesc = view.FindViewById<TextView>(Resource.Id.regularModeDesc);
            regularDesc.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap1Name = view.FindViewById<TextView>(Resource.Id.regular1Name);
            regularMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap2Name = view.FindViewById<TextView>(Resource.Id.regular2Name);
            regularMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            regularMap1Image = view.FindViewById<ImageView>(Resource.Id.regular1Image);
            regularMap2Image = view.FindViewById<ImageView>(Resource.Id.regular2Image);

            rankedDesc = view.FindViewById<TextView>(Resource.Id.rankedModeDesc);
            rankedDesc.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap1Name = view.FindViewById<TextView>(Resource.Id.ranked1Name);
            rankedMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap2Name = view.FindViewById<TextView>(Resource.Id.ranked2Name);
            rankedMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            rankedMap1Image = view.FindViewById<ImageView>(Resource.Id.ranked1Image);
            rankedMap2Image = view.FindViewById<ImageView>(Resource.Id.ranked2Image);

            leagueDesc = view.FindViewById<TextView>(Resource.Id.leagueModeDesc);
            leagueDesc.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap1Name = view.FindViewById<TextView>(Resource.Id.league1Name);
            leagueMap1Name.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap2Name = view.FindViewById<TextView>(Resource.Id.league2Name);
            leagueMap2Name.SetTypeface(tf, TypefaceStyle.Normal);
            leagueMap1Image = view.FindViewById<ImageView>(Resource.Id.league1Image);
            leagueMap2Image = view.FindViewById<ImageView>(Resource.Id.league2Image);

            // Buttons setup
            upcomingRegularBtn = view.FindViewById<Button>(Resource.Id.upcomingStagesTurf);
            upcomingRegularBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingRegularBtn.SetOnClickListener(this);

            upcomingRankedBtn = view.FindViewById<Button>(Resource.Id.upcomingStagesRanked);
            upcomingRankedBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingRankedBtn.SetOnClickListener(this);

            upcomingLeagueBtn = view.FindViewById<Button>(Resource.Id.upcomingStagesLeague);
            upcomingLeagueBtn.SetTypeface(tf, TypefaceStyle.Normal);
            upcomingLeagueBtn.SetOnClickListener(this);
        }

        public void OnClick(View v) {
            Intent activity = new Intent(Activity, typeof(MapRotationActivity));

            if (v == upcomingRegularBtn) {
                activity.PutExtra("Mode", regular);
            } else if (v == upcomingRankedBtn) {
                activity.PutExtra("Mode", ranked);
            } else if (v == upcomingLeagueBtn) {
                activity.PutExtra("Mode", league);
            }

            if (mapsReady)
                StartActivity(activity);
            else
                Toast.MakeText(Activity, "Please connect to a network and try again", ToastLength.Long).Show();
        }

        private void SetRotationImages() {
            Task.Run(async () => {
                regularMapRotation = await MapRotationHelper.GetMapRotationAsync(regular);
                rankedMapRotation = await MapRotationHelper.GetMapRotationAsync(ranked);
                leagueMapRotation = await MapRotationHelper.GetMapRotationAsync(league);

                mapsReady = true;

                if (Activity != null) {
                    Activity.RunOnUiThread(() => {
                        BindRegularMode();
                        BindRankedMode();
                        BindLeagueMode();
                    });
                }
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
    }
}