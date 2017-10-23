// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ALLBOTREMOTE
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset1 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset2 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset3 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset4 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset5 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIPresetButton btnPreset6 { get; set; }

		[Outlet]
		ALLBOTREMOTE.UIDPad Dpad { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint dpadToPreset1 { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint dpadToSuper { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint dpadToSuperBottom { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint dpadToSuperLeading { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint dpadToSuperTop { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint heightPreset1 { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint heightSldSpeed { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint preset1ToPreset2 { get; set; }

		[Outlet]
		ALLBOTREMOTE.Slider sldSpeed { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint sldSpeedToSuperTrailing { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint widthPreset1 { get; set; }

		[Action ("btnPreset_Down:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnPreset_Down (UIPresetButton sender);

		[Action ("sldSpeed_ValueChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void sldSpeed_ValueChanged (Slider sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
