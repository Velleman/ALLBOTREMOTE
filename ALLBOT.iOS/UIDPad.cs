using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using CoreGraphics;
using System.Timers;

namespace ALLBOTREMOTE
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

	partial class UIDPad : UIButton
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

		public DPadButtons Button
		{
			get;
			private set;
		}

		public bool Rotated{ get; set;}

		public delegate void DPadButtonEventHandler(object sender, DPadButtonEventArgs e);
		Timer timer;
		CGPoint currentPoint;
		public UIDPad (IntPtr handle) : base (handle)
		{
			Initialize();
		}

		public UIDPad()
		{
			Initialize();
		}

		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		private void Initialize()
		{
			TouchUpInside += HandleTouchUpInside;
			this.Button = DPadButtons.None;
			this.Centered = true;
			this.ContentMode = UIViewContentMode.ScaleAspectFit;
			timer = new Timer (200);
			timer.Elapsed += (object sender, ElapsedEventArgs e) => {
				InvokeOnMainThread(()=>{if(currentPoint != null)
					{
						HandleTouchDown(currentPoint);
					}});

			};
		}

		public Rectangle HudBounds
		{
			get
			{
				int dimension = (int)Math.Min (this.Frame.Width, this.Frame.Width);
				return new Rectangle(0, 0, dimension, dimension);
			}
		}

		public Boolean Centered
		{
			get;
			set;
		}

		protected Rectangle DPadBounds
		{
			get
			{
				int dimension = (int)Math.Min (Frame.Width, Frame.Height);
				return new Rectangle(
					Centered ? (int)(Frame.Width / 2 - dimension / 2): 0,
					Centered ? (int)(Frame.Height / 2 - dimension / 2) : 0,
					dimension, dimension);
			}
		}



		protected Point DPadCenter
		{
			get
			{
				return new Point(
					DPadBounds.Left + (DPadBounds.Width / 2),
					DPadBounds.Top + (DPadBounds.Height / 2)
				);
			}
		}
			
		public int MiddleButtonRadius
		{
			get
			{
				return (int)((DPadBounds.Width * 0.40) / 2);
			}
		}

		public void notifyButtonClick(DPadButtons button)
		{
			Button = button;

			if (ButtonClick != null)
			{
				ButtonClick(this, new DPadButtonEventArgs(button));
			}
		}
	
		public event DPadButtonEventHandler ButtonClick;

		private double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		private float Radians(float degrees)
		{
			return (float)(degrees * Math.PI) / 180.0f;
		}

		private void HandleTouchUpInside(object sender,EventArgs e)
		{
			Button = DPadButtons.None;
			timer.AutoReset = false;
			timer.Stop ();
			SetNeedsDisplay();
		}
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			UITouch touch = (UITouch)touches.AnyObject;
			currentPoint = touch.LocationInView (this);
			HandleTouchDown(currentPoint);
			timer.AutoReset = true;
			timer.Enabled = true;
			base.TouchesBegan (touches, evt);
		}

		protected double angleBetween(Point p1, Point p2)
		{
			return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) + 1.57079633;
		}

		private void HandleTouchDown(CGPoint e) 
		{
			double distance = Math.Sqrt(
				Math.Pow(DPadCenter.X - e.X, 2) + Math.Pow(DPadCenter.Y - e.Y, 2));

			if (distance > MiddleButtonRadius)
			{
				int X = (int)(e.X - DPadCenter.X), Y = (int)-(e.Y - DPadCenter.Y);

				double angle = 90 + RadianToDegree(angleBetween(new Point(X, Y), new Point(0, 0)));
				angle = Rotated ? angle : angle + 45;

				if ((angle >= 45) && (angle <= 135))
				{
					this.notifyButtonClick(DPadButtons.Up);
				}
				else if ((angle >= 135) && (angle <= 225))
				{
					this.notifyButtonClick(DPadButtons.Left);
				}
				else if ((angle >= 225) && (angle <=  315))
				{
					this.notifyButtonClick(DPadButtons.Down);
				}
				else
				{
					this.notifyButtonClick(DPadButtons.Right);
				}
			}
			else
				this.notifyButtonClick(DPadButtons.Middle);

			SetNeedsDisplay();
		}
			
		public override void Draw (CGRect rect)
		{
			UIImage image;
			if (Rotated) {
				image = UIImage.FromFile ("Images/DPAD.png");
			} else {
				image = UIImage.FromFile ("Images/DPAD_ROTATED.png");
			}
			var _imageHeight = (int)image.CGImage.Height;
			var distanceimage = (int)Button;
			var rectangle = new CGRect (_imageHeight * distanceimage, 0, _imageHeight, _imageHeight);
			CGImage cr = image.CGImage.WithImageInRect (rectangle);
			SetBackgroundImage (new UIImage (cr), UIControlState.Normal);
			SetBackgroundImage (new UIImage (cr), UIControlState.Highlighted);

			base.Draw (rect);
		}
	}
}
