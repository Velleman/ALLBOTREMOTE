using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using System;
using Android.Graphics;
using System.Timers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ALLBOT.Droid
{
    public enum DPadButtons
    {
        None,
        Left,
        Up,
        Right,
        Down,
        Middle
    };

    public class DPadButtonEventArgs : EventArgs
    {
        public DPadButtons Button
        {
            get;
            private set;
        }

        public DPadButtonEventArgs(DPadButtons button)
        {
            Button = button;
        }
    };

    class DPad : View
    {
        Context _context;
        System.Timers.Timer timer;
        MotionEvent currentEvent;
        Bitmap bmp, bmpRotated;
        Rect sourceRect;
        public DPad(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Button = DPadButtons.None;
            this.Centered = true;
            _context = context;
            SetBackgroundColor(Color.Transparent);
            Clickable = true;
            Focusable = true;
            Rotated = true;

            timer = new System.Timers.Timer(200);
            timer.Elapsed += timer_Elapsed;

        }

        protected override async void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            await LoadBitmap();
            Log.Debug("DPAD Height", DPadBounds.Height().ToString());
            CreateCroppedBitmap();
            Invalidate();
            base.OnLayout(changed, left, top, right, bottom);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            notifyButtonClick(Button);
        }

        public DPad(Context context)
            : base(context)
        {
            this.Button = DPadButtons.None;
            this.Centered = true;
            _context = context;
        }

        public DPad(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            this.Button = DPadButtons.None;
            this.Centered = true;
        }

        public delegate void DPadButtonEventHandler(object sender, DPadButtonEventArgs e);

        public Boolean Centered
        {
            get;
            set;
        }

        public DPadButtons Button
        {
            get;
            private set;
        }

        protected Rect DPadBounds
        {
            get
            {
                int dimension = Math.Min(Width, Height);
                var left = Centered ? Width / 2 - dimension / 2 : 0;
                var top = Centered ? Height / 2 - dimension / 2 : 0;
                return new Rect(left,
                                top,
                                left + dimension,
                                top + dimension);
            }
        }

        protected Android.Graphics.Point DPadCenter
        {
            get
            {
                return new Point(
                    DPadBounds.Left + (DPadBounds.Width() / 2),
                    DPadBounds.Top + (DPadBounds.Height() / 2)
                    );
            }
        }

        public int MiddleButtonRadius
        {
            get
            {
                return (int)((DPadBounds.Width() * 0.35) / 2);
            }
        }
        private bool _Rotated;
        public bool Rotated
        {
            get
            {
                return _Rotated;
            }
            set
            {
                _Rotated = value;
                CreateCroppedBitmap();
                Invalidate();
            }
        }

        private async Task LoadBitmap()
        {
            DisplayMetrics metrics = _context.Resources.DisplayMetrics;
            BitmapFactory.Options optionsNormal = await ImageUtils.GetBitmapOptionsOfImageAsync(_context.Resources, Resource.Drawable.dpad_rotated);
            BitmapFactory.Options optionsRotated = await ImageUtils.GetBitmapOptionsOfImageAsync(_context.Resources, Resource.Drawable.dpad_rotated);
            var bounds = DPadBounds;
            var dimensions = bounds.Bottom - bounds.Top;
            if (dimensions > 0)
            {
                bmp = await ImageUtils.LoadScaledDownBitmapForDisplayAsync(Resources, Resource.Drawable.dpad, optionsNormal, dimensions, dimensions);
                bmpRotated = await ImageUtils.LoadScaledDownBitmapForDisplayAsync(Resources, Resource.Drawable.dpad_rotated, optionsRotated, dimensions, dimensions);
            }
        }

        protected double angleBetween(Android.Graphics.Point p1, Point p2)
        {
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) + 1.57079633;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public void notifyButtonClick(DPadButtons button)
        {
            Button = button;

            if (ButtonClick != null)
            {
                ButtonClick(this, new DPadButtonEventArgs(button));
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {
                Button = DPadButtons.None;
                timer.AutoReset = false;
                timer.Stop();
                CreateCroppedBitmap();
                Invalidate();
            }
            else if (e.Action == MotionEventActions.Down)
            {
                Stopwatch st = new Stopwatch();
                st.Start();
                currentEvent = e;
                timer.AutoReset = true;
                timer.Start();
                double distance = Math.Sqrt(
               Math.Pow(DPadCenter.X - e.GetX(), 2) + Math.Pow(DPadCenter.Y - e.GetY(), 2));

                if (distance > MiddleButtonRadius)
                {
                    if (distance < DPadBounds.Width() / 2)
                    {
                        int X = ((int)e.GetX() - DPadCenter.X), Y = -((int)e.GetY() - DPadCenter.Y);

                        double angle = 90 + RadianToDegree(angleBetween(new Android.Graphics.Point(X, Y), new Android.Graphics.Point(0, 0)));
                        angle = Rotated ? angle : angle + 45;

                        if ((angle >= 45) && (angle <= 135))
                        {
                            this.notifyButtonClick(DPadButtons.Up);
                        }
                        else if ((angle >= 135) && (angle <= 225))
                        {
                            this.notifyButtonClick(DPadButtons.Left);
                        }
                        else if ((angle >= 225) && (angle <= 315))
                        {
                            this.notifyButtonClick(DPadButtons.Down);
                        }
                        else
                        {
                            this.notifyButtonClick(DPadButtons.Right);
                        }
                    }
                }
                else
                {
                    this.notifyButtonClick(DPadButtons.Middle);
                }
                Console.WriteLine(st.ElapsedMilliseconds);
                CreateCroppedBitmap();
                Invalidate();
                st.Stop();
            }
            return base.OnTouchEvent(e);
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (bmp != null && bmpRotated !=null)
            {
                if(Rotated)
                {
                    canvas.DrawBitmap(bmp, sourceRect, DPadBounds, null);
                }
                else
                {
                    canvas.DrawBitmap(bmpRotated, sourceRect, DPadBounds, null);
                }
                //Rotated ? canvas.DrawBitmap(bmpRotated, sourceRect, DPadBounds, null); : canvas.DrawBitmap(bmp, sourceRect, DPadBounds, null);
            }
        }

        private void CreateCroppedBitmap()
        {
            if (bmp != null && bmpRotated != null)
            {
                var left = bmp.Height * (int)Button;
                sourceRect = new Rect(left, 0, left + bmp.Height, bmp.Height);
            }
        }

        public event DPadButtonEventHandler ButtonClick;
    }
}