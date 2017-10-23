using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.ComponentModel;

namespace ALLBOTREMOTE
{
	[DesignTimeVisible(true)]
	partial class UIPadButton : UIButton
	{
		public int Count=0;
		public UIPadButton (IntPtr handle) : base (handle)
		{
			Initialize();
		}

		public UIPadButton()
		{
			Initialize();
		}

		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		private void Initialize()
		{
			Font = UIFont.FromName ("robotastic",49f);
			SetTitleColor (UIColor.Black, UIControlState.Normal);
			SetBackgroundImage (UIImage.FromFile ("Images/PADKNOP.png"), UIControlState.Normal);
			TranslatesAutoresizingMaskIntoConstraints = false;
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

		public override void Draw (CoreGraphics.CGRect rect)
		{
			//this.Transform = initialTransform;
			this.Transform = CGAffineTransform.MakeRotation (Radians (Rotation));
			base.Draw (rect);
		}

		private float Radians(float degrees)
		{
			return (float)(degrees * Math.PI) / 180.0f;
		}
	}
}
