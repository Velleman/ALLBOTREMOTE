using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Util;

namespace ALLBOT.Droid
{
    class DPadLayout : LinearLayout
    {
        public DPadLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            
        }

        public DPadLayout(Context context) : base(context)
        {

        }

        public DPadLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }
    }
}