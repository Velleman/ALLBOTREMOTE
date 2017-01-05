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
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace ALLBOT
{
    public delegate void PresetSelectedEventHandler(object sender, PresetsEventArgs e);

    public class PresetsEventArgs : EventArgs
    {
        public int Preset
        {
            get;
            private set;
        }

        public PresetsEventArgs(int preset)
        {
            Preset = preset;
        }
    };

    class PresetsLayout : View
    {
        private int _presets;
        private int _columns;
        private int _columnWidth;
        private int _rowHeight;
        private int _padding;
        private Context ctx;
        Drawable green,white;
        Paint textPaint;
        public event PresetSelectedEventHandler PresetSelected;
        private int _selectedPreset;
        public PresetsLayout(Context context, IAttributeSet attrs) :base(context,attrs)
        {
            ctx = context;
            TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.PresetsLayout);

            _presets = a.GetInteger(Resource.Styleable.PresetsLayout_Presets,1);
            _columns = a.GetInt(Resource.Styleable.PresetsLayout_Columns, 1);
            a.Recycle();

            ViewTreeObserver vto = this.ViewTreeObserver;
            vto.GlobalLayout += (sender, args) =>
            {
                _columnWidth = this.Width / _columns;
                int _rows = (int)Math.Ceiling((double)_presets / (double)_columns);
                _rowHeight = this.Height / _rows;
                _rowHeight = Math.Min(_rowHeight, _columnWidth);
                _padding = this.Height - (_rowHeight * _rows);
                _padding /=2;
                Invalidate();
            };

            green = Resources.GetDrawable(Resource.Drawable.bol_green);
            white = Resources.GetDrawable(Resource.Drawable.bol_white);

            textPaint = new Paint();
            textPaint.TextSize = Resources.GetDimensionPixelSize(Resource.Dimension.PresetFontSize);
            textPaint.TextAlign = Paint.Align.Center;
            textPaint.SetTypeface(Typeface.CreateFromAsset(ctx.Assets, "robotastic.ttf"));
            _selectedPreset = 1;
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnPresetSelected(PresetsEventArgs e)
        {
            if (PresetSelected != null)
                PresetSelected(this, e);
        }


        public PresetsLayout(Context context) : base(context)
        {

        }

        public PresetsLayout(IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference,transfer)
        {

        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch(e.Action)
            {
                case MotionEventActions.Down:
                    HandleTouchDown(e);
                    break;
                case MotionEventActions.Up:
                    HandleTouchUp(e);
                    break;
                default:
                    break;
            }
            return base.OnTouchEvent(e);
        }

        private void HandleTouchDown(MotionEvent e)
        {
            var point = new PointF(e.GetX(),e.GetY());
            Log.Debug("PresetTouchPosition", "X=" + point.X + ", Y=" + point.Y);
            var preset = GetPresetFromPoint(point);
            if (preset <= _presets)
            {
                OnPresetSelected(new PresetsEventArgs(preset));
                _selectedPreset = preset;
            }
            Invalidate();
        }

        private void HandleTouchUp(MotionEvent e)
        {

        }

        private int GetPresetFromPoint(PointF point)
        {
            
            int column =(int)( point.X / _columnWidth);
            int row = (int)((point.Y - _padding)/ _rowHeight);
            
            return GetPresetFromColumnAndRow(column,row);
        }

        private int GetPresetFromColumnAndRow(int column,int row)
        {
            return (column + 1) + (row * 2);
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
            var _red = new Paint();
            _red.SetStyle(Paint.Style.Fill);
            _red.SetARGB(255, 255, 0, 0);
            var _green = new Paint();
            _green.SetStyle(Paint.Style.Fill);
            _green.SetARGB(255, 0, 255, 0);

            
            int presetsDrawn = 1;
            for (int c = 0; c < _columns;c++ )
            {   
                var row = ((float)_presets/(float)_columns);
                var rows = Math.Round(row,MidpointRounding.AwayFromZero);
                for(int r =0;r<rows;r++)
                {
                    var x = _columnWidth/2 + _columnWidth*c;
                    var y = _rowHeight / 2 + _rowHeight * r;
                    var radius = _rowHeight / 2;
                    var diameter = (_rowHeight/4)*3;
                    var xDrawable = (x) - (diameter/2);
                    var yDrawable = (y) - (diameter / 2);
                   
                    
                    radius /= 2;
                    if (presetsDrawn <= _presets)
                    {
                       //Draw Buttons
                        if (_selectedPreset == GetPresetFromColumnAndRow(c, r))
                        {
                            white.SetBounds(xDrawable, yDrawable + _padding, xDrawable + diameter, yDrawable + _padding + diameter);
                            white.Draw(canvas);
                        }
                        else
                        {
                            green.SetBounds(xDrawable, yDrawable + _padding, xDrawable + diameter, yDrawable + _padding + diameter);
                            green.Draw(canvas);
                        }
                        

                        //Draw Text
                        var text = GetPresetFromColumnAndRow(c,r).ToString();                       
                        var offset = ((textPaint.Descent() + textPaint.Ascent()) / 2);
                        canvas.DrawText(text, x, y - offset + _padding, textPaint);
                        presetsDrawn++;
                    }
                }
            }
        }
    }
}