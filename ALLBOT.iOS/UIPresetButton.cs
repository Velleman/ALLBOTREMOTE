using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.ComponentModel;

namespace ALLBOTREMOTE
{
	[DesignTimeVisible (true)]
	partial class UIPresetButton : UIButton
	{
		public UIPresetButton (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		private void Initialize ()
		{
			SetTitleColor (UIColor.Black, UIControlState.Normal);
			this.Focused = false;
			//UpdateBackground ();
		}

		private bool focused;

		[Export ("Focused"), Browsable (true)]
		 public bool Focused {
			get {
				return this.focused;
			}
			set {
				this.focused = value; 
				SetNeedsDisplay ();
			}
		}

		public override void Draw (CoreGraphics.CGRect rect)
		{

			if (TraitCollection.Contains (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Compact))) {
				Font = UIFont.FromName ("robotastic", 27f);
			} else {
				if (TraitCollection.Contains (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Regular)) && TraitCollection.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
					Font = UIFont.FromName ("robotastic", 30f);
				} else {
					Font = UIFont.FromName ("robotastic", 42f);
				}
			}
			if (Focused) {
				SetBackgroundImage(UIImage.FromFile ("Images/BOL-GREEN.png"),UIControlState.Normal);
			} else {
				SetBackgroundImage(UIImage.FromFile ("Images/BOL-WHITE.png"),UIControlState.Normal);
			}
			base.Draw (rect);
		}
	}
}
