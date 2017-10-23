using System;

using UIKit;
using System.Drawing;
using CoreGraphics;
using System.Collections.Generic;
using System.Timers;
using ALLBOT;

namespace ALLBOTREMOTE
{
	public static class Preset
	{
		public const int Preset1 = 0;
		public const int Preset2 = 1;
		public const int Preset3 = 2;
		public const int Preset4 = 3;
		public const int Preset5 = 4;
		public const int Preset6 = 5;
		public const int CustomPreset = 6;
	}

	public partial class ViewController : UIViewController
	{
		Robot robot;
		List<IDPadCommand> presets = new List<IDPadCommand> ();
		int selectedPreset;

		public ViewController (IntPtr handle) : base (handle)
		{
			robot = new Robot (new SoundPlayer());
			presets.Add (new Preset1Command (robot));
			presets.Add (new Preset2Command (robot));
			presets.Add (new Preset3Command (robot));
			presets.Add (new Preset4Command (robot));
			presets.Add (new Preset5Command (robot));
			presets.Add (new Preset6Command (robot));
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			UIImageView ImgBackground;

			var screenWidth = UIScreen.MainScreen.Bounds.Width;
			if (screenWidth == 568) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG-568h@2x.png"));
			} else if (screenWidth == 667) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG-667@2x.png"));
			} else if (screenWidth == 736) {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG.png"));
			} else {
				ImgBackground = new UIImageView (UIImage.FromFile ("Images/BG.png"));
			}
			ImgBackground.Frame = this.View.Frame;
			this.View.AddSubview (ImgBackground);
			this.View.SendSubviewToBack (ImgBackground);

			btnPreset1.AccessibilityIdentifier = "1";
			btnPreset2.AccessibilityIdentifier = "2";
			btnPreset3.AccessibilityIdentifier = "3";
			btnPreset4.AccessibilityIdentifier = "4";
			btnPreset5.AccessibilityIdentifier = "5";
			btnPreset6.AccessibilityIdentifier = "6";

			SetSelected (btnPreset1);
			CalculateSpeed ();

			this.View.TranslatesAutoresizingMaskIntoConstraints = false;
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				if (screenWidth >= 568) {
					if (screenWidth >= 667) {
						if (screenWidth >= 736) {
							SetConstraintConstant (dpadToSuperTop, 100);
							SetConstraintConstant (dpadToSuperLeading, 35);
							SetConstraintConstant (dpadToSuperBottom, 36);
							SetConstraintConstant (dpadToPreset1, 47);
							SetConstraintConstant (preset1ToPreset2, 27);
							SetConstraintConstant (sldSpeedToSuperTrailing, 40);
							SetConstraintConstant (widthPreset1, 70);
							SetConstraintConstant (heightPreset1, 70);
							SetConstraintConstant (heightSldSpeed, 120);
						} else {
							SetConstraintConstant (dpadToSuperTop, 110);
							SetConstraintConstant (dpadToSuperLeading, 30);
							SetConstraintConstant (dpadToSuperBottom, 34);
							SetConstraintConstant (dpadToPreset1, 59);
							SetConstraintConstant (preset1ToPreset2, 20);
							SetConstraintConstant (sldSpeedToSuperTrailing, 34);
							SetConstraintConstant (heightSldSpeed, 90);
						}
					} else {
						SetConstraintConstant (dpadToSuperTop, 72);
						SetConstraintConstant (dpadToSuperLeading, 10);
						SetConstraintConstant (dpadToSuperBottom, 16);
						SetConstraintConstant (dpadToPreset1, 38);
						SetConstraintConstant (preset1ToPreset2, 20);
						SetConstraintConstant (sldSpeedToSuperTrailing, 60);
						SetConstraintConstant (heightSldSpeed, 90);
					}
				} else {
					SetConstraintConstant (dpadToPreset1, 5);
					SetConstraintConstant (dpadToSuperLeading, 5);
					SetConstraintConstant (preset1ToPreset2, 5);
					SetConstraintConstant (dpadToSuperTop, 72);
					SetConstraintConstant (dpadToSuperBottom, 16);
					SetConstraintConstant (sldSpeedToSuperTrailing, 60);
					SetConstraintConstant (heightSldSpeed, 90);
				}
			} else if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				SetConstraintConstant (dpadToSuperTop, 292);
				SetConstraintConstant (dpadToSuperLeading, 53);
				SetConstraintConstant (dpadToSuperBottom, 117);
				SetConstraintConstant (dpadToPreset1, 89);
				SetConstraintConstant (preset1ToPreset2, 35);
				SetConstraintConstant (sldSpeedToSuperTrailing, 40);
				SetConstraintConstant (widthPreset1, 92);
				SetConstraintConstant (heightPreset1, 92);
				SetConstraintConstant (heightSldSpeed, 180);
			}

			Dpad.ButtonClick += (object sender, DPadButtonEventArgs e) => {
				switch (e.Button) {
				case DPadButtons.Up:
					presets [selectedPreset].UpAction ();
					break;
				case DPadButtons.Down:
					presets [selectedPreset].DownAction ();
					break;
				case DPadButtons.Left:
					presets [selectedPreset].LeftAction ();
					break;
				case DPadButtons.Right:
					presets [selectedPreset].RightAction ();
					break;
				case DPadButtons.Middle:
					presets [selectedPreset].MiddleAction ();
					break;
				default:
					break;
				}
			};
		}

		public override bool ShouldPerformSegue (string segueIdentifier, Foundation.NSObject sender)
		{
			return base.ShouldPerformSegue (segueIdentifier, sender);
		}

		void SetConstraintConstant (NSLayoutConstraint constraint, int value)
		{
			constraint.Constant = value;
		}

		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);
			if (toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight) {
			}
		}

		private float Radians (float degrees)
		{
			return (float)(degrees * Math.PI) / 180.0f;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void sldSpeed_ValueChanged (Slider sender)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				if (TraitCollection.DisplayScale == 3) {
					if (sender.Value < 12) {
						sender.Value = 12;
					}
					if (sender.Value > 243) {
						sender.Value = 243;
					}
				} else {
					if (sender.Value < 12) {
						sender.Value = 12;
					}
					if (sender.Value > 243) {
						sender.Value = 243;
					}
				}
			} else {
				if (sender.Value < 10) {
					sender.Value = 10;
				}
				if (sender.Value > 245) {
					sender.Value = 245;
				}
			}
			CalculateSpeed ();
		}

		private void CalculateSpeed ()
		{
			// position will be between 0 and 100
			var minp = sldSpeed.MinValue;
			var maxp = sldSpeed.MaxValue;

			// The result should be between 100 an 10000000
			var minv = Math.Log (10);
			var maxv = Math.Log (80);

			// calculate adjustment factor
			var scale = (maxv - minv) / (maxp - minp);

			int logValue = (int)Math.Exp (minv + scale * ((maxp - sldSpeed.Value) - minp));

			robot.MoveSpeed = logValue;
			robot.Speed = (int)sldSpeed.Value;
		}

		partial void btnPreset_Down (UIPresetButton sender)
		{
			SetSelected (sender);
		}

		void SetSelected (object selectedButton)
		{
			List<UIView> l = new List<UIView> (this.View.Subviews);
			foreach (UIView view in l) {
				if (view is UIPresetButton) {
					if (view.Equals (selectedButton)) {
						selectedPreset = int.Parse (view.AccessibilityIdentifier) - 1;
						((UIPresetButton)view).Focused = true;
					} else {
						((UIPresetButton)view).Focused = false;
					}
				}
			}
			Dpad.Rotated = presets [selectedPreset].DPadRotated;
			Dpad.SetNeedsDisplay ();
		}
	}
}

