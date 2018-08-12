using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;
using System.Threading;
using Spla2n_Stuff.News;
using Spla2n_Stuff.Helpers;
using Newtonsoft.Json;


namespace Spla2n_Stuff.Activities {
    public class NewsFragment : Android.Support.V4.App.Fragment, NewsHelper.INewsCallback, RecyclerItemClickListener.IOnRecyclerClickListener {

        private static readonly string TAG = "NewsFragment";
        private static readonly string OBJECT_TRANSFER = "OBJECT_TRANSFER";

        List<NewsArticle> mArticles;

        private RecyclerView mRecyclerView;
        private NewsArticleAdapter mAdapter;
        private RecyclerView.LayoutManager mLayoutManager;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View view = inflater.Inflate(Resource.Layout.News_Fragment_Layout, container, false);

            mArticles = new List<NewsArticle>();

            // Initializes the member variables of this fragment
            InitViews(view);

            LoadNewsArticles();

            return view;
        }

        void InitViews(View view) {
            // RecyclerView
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.news_recycler_view);
            mLayoutManager = new LinearLayoutManager(this.Context);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.AddOnItemTouchListener(new RecyclerItemClickListener(this.Activity, mRecyclerView, this)); 

            mAdapter = new NewsArticleAdapter(this.Context, mArticles);
            mRecyclerView.SetAdapter(mAdapter);
        }

        private void LoadNewsArticles() {
            Task.Run(async () => {
                await NewsHelper.GetNewsArticlesAsync(this);
            });
        }

        public void OnNewsLoaded(List<NewsArticle> newsArticles) {
            Activity.RunOnUiThread(() => {
                mArticles.Clear();
                mArticles.AddRange(newsArticles);
                mAdapter.NotifyDataSetChanged();
            });
        }

        public void OnItemClick(View view, int position) {
            Intent intent = new Intent(this.Context, typeof(NewsDetailActivity));
            intent.PutExtra(OBJECT_TRANSFER, JsonConvert.SerializeObject(mAdapter.GetArticle(position)));
            StartActivity(intent);
        }

        public void OnItemLongClick(View view, int position) {
            // No implementation as for now :-(
        }
    }
}