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
using Java.Lang;
using Spla2n_Stuff.GearTypes;
using Android.Graphics;
using Android.Content.Res;

namespace Spla2n_Stuff
{
    public class CustomAdapter<T> : BaseAdapter<T>
    {
        private Activity context;
        private List<T> items;
        private Typeface typeface;
        private View view;

        public CustomAdapter(Activity context, List<T> items, Typeface typeface) {
            this.context = context;
            this.items = items;
            this.typeface = typeface;
        }

        public override T this[int position] {
            get { return items[position]; }
        }

        public override int Count {
            get {
                return items.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position) {
            return null;
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            if (typeof(T) == typeof(Ability)) {
                SetUpAbilities(position, parent);
            } else if (typeof(T) == typeof(Special)) {
                SetUpSpecials(position, parent);
            } else if (typeof(T) == typeof(Brand)) {
                SetUpBrands(position, parent);
            } else if (typeof(T) == typeof(Weapon)) {

            }

            return view;
        }

        private void SetUpAbilities(int position, ViewGroup parent) {
            view = context.LayoutInflater.Inflate(Resource.Layout.Ability_List, parent, false);

            List<Ability> abilities = items.Cast<Ability>().ToList();

            ImageView image = view.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView title = view.FindViewById<TextView>(Resource.Id.abilityTitle);
            TextView description = view.FindViewById<TextView>(Resource.Id.abilityDescription);

            image.SetImageResource(abilities[position].ImageID);
            title.Text = abilities[position].Name;
            description.Text = abilities[position].Description;

            // Setting fonts
            title.SetTypeface(typeface, TypefaceStyle.Bold);
            description.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        private void SetUpSpecials(int position, ViewGroup parent) {
            view = context.LayoutInflater.Inflate(Resource.Layout.Ability_List, parent, false);

            List<Special> specials = items.Cast<Special>().ToList();

            ImageView image = view.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView title = view.FindViewById<TextView>(Resource.Id.abilityTitle);
            TextView description = view.FindViewById<TextView>(Resource.Id.abilityDescription);

            image.SetImageResource(specials[position].ImageID);
            title.Text = specials[position].Name;
            description.Text = specials[position].Description;

            // Setting fonts
            title.SetTypeface(typeface, TypefaceStyle.Bold);
            description.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        private void SetUpBrands(int position, ViewGroup parent) {
            view = context.LayoutInflater.Inflate(Resource.Layout.Brand_Layout, parent, false);

            List<Brand> brands = items.Cast<Brand>().ToList();

            ImageView image = view.FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView imageCommon = view.FindViewById<ImageView>(Resource.Id.imageCommon);
            ImageView imageUncommon = view.FindViewById<ImageView>(Resource.Id.imageUncommon);
            TextView brandTitle = view.FindViewById<TextView>(Resource.Id.brandTitle);
            TextView commonTitle = view.FindViewById<TextView>(Resource.Id.commonTitle);
            TextView commonDesc = view.FindViewById<TextView>(Resource.Id.commonDescription);
            TextView uncommonTitle = view.FindViewById<TextView>(Resource.Id.uncommonTitle);
            TextView uncommonDesc = view.FindViewById<TextView>(Resource.Id.uncommonDescription);

            if (brands[position].CommonAbility != null) {
                imageCommon.SetImageResource(brands[position].CommonAbility.ImageID);
                imageUncommon.SetImageResource(brands[position].UncommonAbility.ImageID);
                commonDesc.Text = brands[position].CommonAbility.Name;
                uncommonDesc.Text = brands[position].UncommonAbility.Name;
            }
            else {
                imageCommon.Visibility = ViewStates.Invisible;
                imageUncommon.Visibility = ViewStates.Invisible;
            }

            image.SetImageResource(brands[position].ImageID);   
            brandTitle.Text = brands[position].Name;

            brandTitle.SetTypeface(typeface, TypefaceStyle.Bold);
            commonTitle.SetTypeface(typeface, TypefaceStyle.Bold);
            uncommonTitle.SetTypeface(typeface, TypefaceStyle.Bold);
            commonDesc.SetTypeface(typeface, TypefaceStyle.Normal);
            uncommonDesc.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        public void SetUpWeapons(int position, ViewGroup parent) {
            view = context.LayoutInflater.Inflate(Resource.Layout.Weapon_Layout, parent, false);
        }
    }
}