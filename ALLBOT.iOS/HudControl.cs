using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace HudControls
{
    public partial class HudControl : UserControl
    {
        public enum HudState
        {
            None,
            Left,
            Up,
            Right,
            Down,
            Center
        }

        public HudControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ButtonState = HudState.None;
        }

        public HudState ButtonState
        {
            get;
            set;
        }

        public Rectangle HudBounds
        {
            get
            {
                int dimension = Math.Min(Width, Height);
                return new Rectangle(0, 0, dimension, dimension);
            }
        }

        public Point HudCenter
        {
            get
            {
                return new Point(
                    HudBounds.Left + (HudBounds.Width / 2),
                    HudBounds.Top + (HudBounds.Height / 2)
                    );
            }
        }

        public Rectangle CenterRect
        {
            get
            {
                return new Rectangle(
                    HudCenter.X - CenterRadius,
                    HudCenter.Y - CenterRadius,
                    CenterRadius * 2,
                    CenterRadius * 2);
            }
        }

        public int CenterRadius
        {
            get
            {
                return (int)((HudBounds.Width * 0.20) / 2);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.Refresh();
        }

        protected double angleBetween(Point p1, Point p2)
        {
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) + 1.57079633;
        }

        public event EventHandler UpButtonClick;
        public event EventHandler LeftButtonClick;
        public event EventHandler RightButtonClick;
        public event EventHandler DownButtonClick;
        public event EventHandler CenterButtonClick;

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            double distance = Math.Sqrt(
                Math.Pow(HudCenter.X - e.X, 2) + Math.Pow(HudCenter.Y - e.Y, 2));
            
            if (distance <= CenterRadius)
            {
                CenterButtonClick(this, new EventArgs());
                ButtonState = HudState.Center;
            }
            else
            {
                int X = (e.X - HudCenter.X);
                int Y = -(e.Y - HudCenter.Y);
                double angle = 90 + RadianToDegree(angleBetween(new Point(X, Y), new Point(0, 0)));

                if ((angle >= 45) && (angle <= 135))
                {
                    UpButtonClick(this, new EventArgs());
                    ButtonState = HudState.Up;
                }
                else if ((angle >= 135) && (angle <= 225))
                {
                    LeftButtonClick(this, new EventArgs());
                    ButtonState = HudState.Left;
                }
                else if ((angle >= 225) && (angle <= 315))
                {
                    DownButtonClick(this, new EventArgs());
                    ButtonState = HudState.Down;
                }
                else
                {
                    RightButtonClick(this, new EventArgs());
                    ButtonState = HudState.Right;
                }
            }

            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ButtonState = HudState.None;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            Bitmap bmp = new Bitmap("hud.png");
            ImageAttributes attr = new ImageAttributes();
            pe.Graphics.DrawImage(bmp, HudBounds, bmp.Height * (int)ButtonState, 0, bmp.Height, bmp.Height, GraphicsUnit.Pixel, attr);
        }
    }
}
