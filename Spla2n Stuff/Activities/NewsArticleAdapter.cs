using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Spla2n_Stuff.News;
using Square.Picasso;

namespace Spla2n_Stuff.Activities {
    public class NewsArticleAdapter : RecyclerView.Adapter {

        // ViewHolder class
        private class ViewHolder : RecyclerView.ViewHolder {
            public ImageView entryImage;
            public TextView entryTitle, entryContent;

            public ViewHolder(View view) : base(view) {
                entryImage = view.FindViewById<ImageView>(Resource.Id.entryImage);
                entryTitle = view.FindViewById<TextView>(Resource.Id.entryTitle);
                entryContent = view.FindViewById<TextView>(Resource.Id.entryContent);
            }
        }

        // List of articles
        private List<NewsArticle> mArticles;
        private Context mContext;

        public NewsArticleAdapter(Context context,List<NewsArticle> articles) {
            mContext = context;
            mArticles = articles;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.news_detail_layout, parent, false);

            ViewHolder vh = new ViewHolder(itemView);

            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            ViewHolder vh = holder as ViewHolder;
            Picasso.With(mContext).Load(mArticles[position].ImageURI).Into(vh.entryImage);
            vh.entryTitle.Text = mArticles[position].Title;
            vh.entryContent.Text = mArticles[position].Content;
        }

        public override int ItemCount => mArticles.Count;

        public NewsArticle GetArticle(int position) => mArticles[position];
    }
}