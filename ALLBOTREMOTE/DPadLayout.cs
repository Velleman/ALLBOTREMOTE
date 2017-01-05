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
using Android.Util;

namespace ALLBOT
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