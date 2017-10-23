using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;

namespace ALLBOT.Droid
{
    class PresetButton : View
    {
        bool _Selected;
        public PresetButton(Context context) : base(context)
        {

        }

        public PresetButton(Context context, IAttributeSet attrs) : base(context,attrs)
        {
            TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.PresetButton);

            _Selected = a.GetBoolean(Resource.Styleable.PresetButton_Selected, false);

            a.Recycle();
        }

        public PresetButton(Context context, IAttributeSet attrs, int defStyle) : base(context,attrs,defStyle)
        {
            TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.PresetButton, defStyle, 0);

            _Selected = a.GetBoolean(Resource.Styleable.PresetButton_Selected,false);            

            a.Recycle();
        }


        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
            if (_Selected)
            {
                canvas.DrawARGB(255, 255, 0, 0);
            }
            else
            {
                canvas.DrawARGB(255, 0, 255, 0);
            }
        }
    }
}