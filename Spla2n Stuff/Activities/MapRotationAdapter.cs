using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Spla2n_Stuff.Maps;
using Spla2n_Stuff.Helpers;
using Android.Support.V7.Widget;
using System.Globalization;
using Android.Graphics;

namespace Spla2n_Stuff {

    public class MapRotationAdapter : BaseAdapter {

        private Activity context;
        private List<MapRotation> items;
        private Typeface typeface;
        private View view;

        private string mode;

        public MapRotationAdapter(Activity context, List<MapRotation> items, Typeface typeface,string mode) {
            this.context = context;
            this.items = items;
            this.typeface = typeface;
            this.mode = mode;
        }

        public override int Count {
            get {
                return items.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position) {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            view = context.LayoutInflater.Inflate(Resource.Layout.MapRotation_List, parent, false);

            CardView card = view.FindViewById<CardView>(Resource.Id.rotatioCardView);
            TextView rotationTime = view.FindViewById<TextView>(Resource.Id.timeText);
            TextView modeTitle = view.FindViewById<TextView>(Resource.Id.modeTitle);
            TextView modeDesc = view.FindViewById<TextView>(Resource.Id.modeDesc);
            TextView map1Name = view.FindViewById<TextView>(Resource.Id.map1Name);
            ImageView map1Image = view.FindViewById<ImageView>(Resource.Id.map1Image);
            TextView map2Name = view.FindViewById<TextView>(Resource.Id.map2Name);
            ImageView map2Image = view.FindViewById<ImageView>(Resource.Id.map2Image);

            Color col = new Color();
            string modeType = "";

            if (mode == MainActivity.regular) {
                col = new Color(100, 221, 23);
                modeType = "Regular Battle";
            } else if (mode == MainActivity.ranked) {
                col = new Color(239, 108, 0);
                modeType = "Ranked Battle";
            } else if (mode == MainActivity.league) {
                col = new Color(236, 64, 122);
                modeType = "League Battle";
            }

            card.SetCardBackgroundColor(col);
            

            rotationTime.Text = items[position].Time;
            rotationTime.SetTypeface(typeface, TypefaceStyle.Normal);
            modeTitle.Text = modeType;
            modeTitle.SetTypeface(typeface, TypefaceStyle.Bold);
            modeDesc.Text = items[position].GameMode;
            modeDesc.SetTypeface(typeface, TypefaceStyle.Normal);
            map1Name.Text = items[position].Maps[0].Name;
            map1Name.SetTypeface(typeface, TypefaceStyle.Normal);
            map1Image.SetImageResource(ImageHelper.GetImageId(map1Name.Text));
            map2Name.Text = items[position].Maps[1].Name;
            map2Name.SetTypeface(typeface, TypefaceStyle.Normal);
            map2Image.SetImageResource(ImageHelper.GetImageId(map2Name.Text));

            return view;
        }
    }
}