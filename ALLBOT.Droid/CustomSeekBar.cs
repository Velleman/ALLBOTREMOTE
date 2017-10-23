using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace ALLBOT.Droid
{
    class SeekBarRotator : ViewGroup
    {
        Context _context;

        public SeekBarRotator(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }

        public SeekBarRotator(Context context)
            : base(context)
        {
            _context = context;
        }

        public SeekBarRotator(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {

            View child = GetChildAt(0);
            if (child.Visibility != ViewStates.Gone)
            {
                // swap width and height for child
                MeasureChild(child, heightMeasureSpec, widthMeasureSpec);
                SetMeasuredDimension(child.MeasuredHeightAndState, child.MeasuredWidthAndState);
            }
            else
            {
                SetMeasuredDimension(ResolveSizeAndState(0, widthMeasureSpec, 0),
                        ResolveSizeAndState(0, heightMeasureSpec, 0));
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            View child = GetChildAt(0);
            if (child.Visibility != ViewStates.Gone)
            {
                // rotate the child 90 degrees counterclockwise around its upper-left
                child.PivotX = 0;
                child.PivotY = 0;
                child.Rotation = -90;
                // place the child below this view, so it rotates into view
                int mywidth = r - l;
                int myheight = b - t;
                int childwidth = myheight;
                int childheight = mywidth;
                child.Layout(0, myheight, childwidth, myheight + childheight);
            }
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {

            base.OnDraw(canvas);
        }
    }
}