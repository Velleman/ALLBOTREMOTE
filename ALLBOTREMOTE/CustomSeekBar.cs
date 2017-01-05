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
using System.Threading.Tasks;

namespace ALLBOT
{
	class CustomSeekBar : SeekBar
    {
        Context _context;
		Bitmap background;
		public CustomSeekBar(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
			_context = context;
        }

		public CustomSeekBar(Context context)
            : base(context)
        {
            _context = context;
        }

		public CustomSeekBar(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

		private Rect BackgroundBounds {
			get{
				var HeightBackground = Math.Round(Height / 2.4f);
				var left = 0;
				var right = Width;
				var top = (int)((Height - HeightBackground)/2);
				var bottom = (int)(top + HeightBackground);
				return new Rect (left, top, right, bottom);
			}
		}

		protected override async void OnLayout (bool changed, int left, int top, int right, int bottom)
		{
			background = await LoadBitmap ();	
			Invalidate ();
			base.OnLayout (changed, left, top, right, bottom);
		}

		private void CalculateBounds()
		{

		}

		private async Task<Bitmap> LoadBitmap()
		{
			DisplayMetrics metrics = _context.Resources.DisplayMetrics;
			int id = Resource.Drawable.sliderbackground;
			BitmapFactory.Options options = await ImageUtils.GetBitmapOptionsOfImageAsync(_context.Resources, id);
			return await ImageUtils.LoadScaledDownBitmapForDisplayAsync(Resources, id, options, BackgroundBounds.Width(), BackgroundBounds.Height());
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			switch (e.Action) {
			case MotionEventActions.Down:
			case MotionEventActions.Move:
			case MotionEventActions.Up:
				Progress = (Max - (int) (Max * e.GetY() / Height));
				OnSizeChanged(Width, Height, 0, 0);
				break;

			case MotionEventActions.Cancel:
				break;
			}
			return true;
		}
        
        protected override void OnDraw(Android.Graphics.Canvas canvas)
		{
			canvas.Rotate (-90);
			canvas.Translate (-Height, 0);
			if (background != null) {
				canvas.DrawBitmap (background, null, BackgroundBounds, null);
			}

            base.OnDraw(canvas);
        }
    }
}