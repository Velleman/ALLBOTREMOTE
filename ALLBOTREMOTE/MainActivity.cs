using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Android.Media;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using Java.Lang;
using Android.Util;
using Android.Text;
using Android.Text.Style;

namespace ALLBOT
{
    [Activity(Label = "ALLBOT", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
        Robot robot;
        private List<IDPadCommand> presets; 
        int selectedPreset;
        LinearLayout layout;
        ViewTreeObserver vto;
        DPad dpad; 
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            robot = new Robot();
            robot.Speed = 125;
            robot.MoveSpeed = 20;
            presets = new List<IDPadCommand>();
            presets.Add(new Preset1Command(robot));
            presets.Add(new Preset2Command(robot));
            presets.Add(new Preset3Command(robot));
            presets.Add(new Preset4Command(robot));
            presets.Add(new Preset5Command(robot));
            presets.Add(new Preset6Command(robot));
            selectedPreset = 1;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            System.String packageName = ApplicationContext.PackageName;

            Configuration config = Resources.Configuration;
            var layout = config.ScreenLayout & ScreenLayout.SizeMask;

            RoboTextView txt = FindViewById<RoboTextView>(Resource.Id.textscreensize);
            txt.Text = layout.ToString();
            txt.Text += " ";
            string density;
            float d = Resources.DisplayMetrics.Density;
            if (d >= 4.0)
            {
                density = "xxxhdpi";
            }
            if (d >= 3.0)
            {
                density = "xxhdpi";
            }
            if (d >= 2.0)
            {
                density = "xhdpi";
            }
            if (d >= 1.5)
            {
                density = "hdpi";
            }
            if (d >= 1.0)
            {
                density = "mdpi";
            }
            else
            {
                density = "ldpi";
            }
            txt.Text += density;
            TextView TV = FindViewById<RoboTextView>(Resource.Id.txtTitle);
            SpannableString wordtoSpan = new SpannableString(TV.Text);
            wordtoSpan.SetSpan(new ForegroundColorSpan(Color.Argb(255,48,218,30)), 3, 6, SpanTypes.ExclusiveExclusive);
            TV.TextFormatted = wordtoSpan;

            dpad = FindViewById<DPad>(Resource.Id.dpad);
            dpad.ButtonClick += dpad_ButtonClick;

            PresetsLayout presetsLayout = FindViewById<PresetsLayout>(Resource.Id.Presets);
            presetsLayout.PresetSelected += presets_PresetSelected;
        }

        void presets_PresetSelected(object sender, PresetsEventArgs e)
        {
            Log.Debug("PresetTouched", "Touched Preset " + e.Preset);
            selectedPreset = e.Preset -1 ;
            dpad.Rotated = presets[selectedPreset].DPadRotated;
        }

        void dpad_ButtonClick(object sender, DPadButtonEventArgs e)
        {
            var button = e.Button;
            switch(button)
            {
                case DPadButtons.Up:
                    presets[selectedPreset].UpAction();
                break;
                case DPadButtons.Left:
                    presets[selectedPreset].LeftAction();
                break;
                case DPadButtons.Right:
                    presets[selectedPreset].RightAction();
                break;
                case DPadButtons.Down:
                    presets[selectedPreset].DownAction();
                break;
                case DPadButtons.Middle:
                    presets[selectedPreset].MiddleAction();
                break;
                default:
                    break;
            }
        }
    }
}

