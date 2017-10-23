using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.ComponentModel;

namespace ALLBOTREMOTE
{
	[DesignTimeVisible(true)]
	partial class UIBGView : UIView
	{
		public UIBGView (IntPtr handle) : base (handle)
		{
			Initialize ();	
		}

		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		private void Initialize()
		{
			BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("Images/BG"));
			SetNeedsDisplay ();
		}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			base.Draw (rect);
		}
	}
}
