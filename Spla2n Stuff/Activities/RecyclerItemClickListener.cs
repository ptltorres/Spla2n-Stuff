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

using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;

namespace Spla2n_Stuff.Activities {
    public class RecyclerItemClickListener : RecyclerView.SimpleOnItemTouchListener {
       
        public interface IOnRecyclerClickListener {
            void OnItemClick(View view, int position);
            void OnItemLongClick(View view, int position);
        }

        private class GestureListener : GestureDetector.SimpleOnGestureListener {
            private IOnRecyclerClickListener mListener;
            private RecyclerView mRecyclerView;

            public GestureListener(IOnRecyclerClickListener listener, RecyclerView recyclerView) {
                mListener = listener;
                mRecyclerView = recyclerView;
            }

            public override bool OnSingleTapUp(MotionEvent e) {
                View childView = mRecyclerView.FindChildViewUnder(e.GetX(), e.GetY());
                if (childView != null && mListener != null) {
                    mListener.OnItemClick(childView, mRecyclerView.GetChildAdapterPosition(childView));
                }

                return true;
            }

            public override void OnLongPress(MotionEvent e) {
                View childView = mRecyclerView.FindChildViewUnder(e.GetX(), e.GetY());
                if (childView != null && mListener != null) {
                    mListener.OnItemClick(childView, mRecyclerView.GetChildAdapterPosition(childView));
                }
            }
        }

        private static readonly string TAG = "RecyclerItemClickListen";

        private IOnRecyclerClickListener mListener;
        private GestureDetectorCompat mGestureDetector;
  
        public RecyclerItemClickListener(Context context, RecyclerView recyclerView, IOnRecyclerClickListener listener) {
            mListener = listener;

            mGestureDetector = new GestureDetectorCompat(context, new GestureListener(mListener, recyclerView));
        }

        public override bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e) {
            if (mGestureDetector != null)
                return mGestureDetector.OnTouchEvent(e);
            else
                return false;
        }
    }
}