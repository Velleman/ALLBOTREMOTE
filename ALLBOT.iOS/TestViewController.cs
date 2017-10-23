using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ALLBOTREMOTE
{
	partial class TestViewController : UIViewController
	{
		public TestViewController (IntPtr handle) : base (handle)
		{
			UIImageView ImgBackground;

			var screenWidth = this.View.Layer.Bounds.Width;
			if (screenWidth == 568) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG-568h@2x.png"));
			} else if (screenWidth == 667) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG-667@2x.png"));
			} else if (screenWidth == 736) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG.png"));
			}else {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG.png"));
			}
			ImgBackground.Frame = this.View.Frame;
			this.View.AddSubview (ImgBackground);
			this.View.SendSubviewToBack (ImgBackground);
		}
	}
}
