using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.ComponentModel;
using CoreGraphics;

namespace ALLBOTREMOTE
{
	[DesignTimeVisible(true)]
	sealed partial class Slider : UISlider
	{
		
		public Slider (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		private void Initialize()
		{
			SetThumbImage(UIImage.FromFile ("Images/SLIDERBUTTON.png"),UIControlState.Normal);
			SetMinTrackImage (new UIImage(), UIControlState.Normal);
			SetMaxTrackImage (new UIImage(), UIControlState.Normal);
		}

		private int rotation;
		[Export("Rotation"), Browsable(true)]
		public int Rotation{ 
			get 
			{
				return rotation;
			}
			set
			{
				rotation = value; 
				SetNeedsDisplay ();
			} 
		}

		public override void Draw (CGRect rect)
		{
			this.Transform = CGAffineTransform.MakeRotation(Radians(rotation));
			var image = UIImage.FromFile ("Images/SLIDERBACKGROUND.png");
			using (CGContext g = UIGraphics.GetCurrentContext ()) {
				g.DrawImage (rect, image.CGImage);
			}
			base.Draw (rect);
		}

		private float Radians(float degrees)
		{
			return (float)(degrees * Math.PI) / 180.0f;
		}

	}
}
