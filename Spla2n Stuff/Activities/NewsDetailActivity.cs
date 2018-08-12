using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Spla2n_Stuff.News;
using Newtonsoft.Json;
using Square.Picasso;

namespace Spla2n_Stuff.Activities {
    [Activity(Label = "NewsDetailActivity", ParentActivity = typeof(MainActivity), ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Custom")]
    public class NewsDetailActivity : AppCompatActivity {

        private static readonly string OBJECT_TRANSFER = "OBJECT_TRANSFER";

        private NewsArticle mArticle;

        private ImageView mArticleImage;
        private TextView mArticleTitle;
        private TextView mArticleContent;
        private TextView mArticleMetadata;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.news_detail_card_layout);

            string jsonString = Intent.GetStringExtra(OBJECT_TRANSFER);
            mArticle = JsonConvert.DeserializeObject<NewsArticle>(jsonString);

            SetUpToolbar();
            InitViews();
        }

        private void SetUpToolbar() {
            // Set the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.entryDetailToolbar);

            if (toolbar != null) {
                TextView toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
                toolbarTitle.Text = "";
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowTitleEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        private void InitViews() {
            mArticleImage = FindViewById<ImageView>(Resource.Id.entryImage);
            Picasso.With(this).Load(mArticle.ImageURI).Into(mArticleImage);

            mArticleTitle = FindViewById<TextView>(Resource.Id.entryTitle);
            mArticleTitle.Text = mArticle.Title;

            mArticleContent = FindViewById<TextView>(Resource.Id.entryContent);
            mArticleContent.Text = mArticle.Content;

            mArticleMetadata = FindViewById<TextView>(Resource.Id.entryCreationDate);
            mArticleMetadata.Text = mArticle.MetaData(); 
        }
    }
}