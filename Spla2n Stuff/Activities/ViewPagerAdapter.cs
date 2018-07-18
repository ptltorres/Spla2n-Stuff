using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;

namespace Spla2n_Stuff.Activities {
    public class ViewPagerAdapter : FragmentPagerAdapter {
        private  List<V4Fragment> mFragmentList = new List<V4Fragment>();
        private List<string> mFragmentTitleList = new List<string>();

        public ViewPagerAdapter(V4FragmentManager manager) : base(manager) {

        }

        public override int Count => mFragmentList.Count;

        public override V4Fragment GetItem(int position) {
            return mFragmentList[position];
        }

        public void AddFragment(V4Fragment fragment, string title) {
            mFragmentList.Add(fragment);
            mFragmentTitleList.Add(title);
        }

        public override ICharSequence GetPageTitleFormatted(int position) {
            return new Java.Lang.String(mFragmentTitleList[position]);
        }
    }
}