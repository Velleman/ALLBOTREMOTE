using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Graphics;

namespace ALLBOT.Droid
{
    class RoboTextView : TextView
    {

        public RoboTextView(Context ctx,IAttributeSet attrs) : base(ctx,attrs)
        {
            this.SetTypeface(Typeface.CreateFromAsset(ctx.Assets,"robotastic.ttf"),TypefaceStyle.Normal);
        }
           
    }
}