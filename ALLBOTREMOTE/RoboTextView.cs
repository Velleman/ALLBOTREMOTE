using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ALLBOT
{
    class RoboTextView : TextView
    {

        public RoboTextView(Context ctx,IAttributeSet attrs) : base(ctx,attrs)
        {
            this.SetTypeface(Typeface.CreateFromAsset(ctx.Assets,"robotastic.ttf"),TypefaceStyle.Normal);
        }
           
    }
}