#if DEBUG
//#define USE_AMPLITUDE
#endif
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.Collections.Generic;
using Android.Graphics;
using Java.Lang;
using Android.Util;
using Android.Text;
using Android.Text.Style;
using Android.Content.PM;
using Android.Media;
using Android.Content;
using System;

namespace ALLBOT.Droid
{
    [Activity(Label = "ALLBOT", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.SensorLandscape)]
    public class MainActivity : Activity
    {
        Robot robot;
        private List<IDPadCommand> presets;
        int selectedPreset;
        LinearLayout layout;
        ViewTreeObserver vto;
        DPad dpad;
        private AudioManager audio;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Insights.Initialize("5ed80d55ef77d594ccc7f39729badf583164c9f1", this);

            robot = new Robot(new SoundPlayer());
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
#if  DEBUG
            RoboTextView txt = FindViewById<RoboTextView>(Resource.Id.textscreensize);
            txt.Text = layout.ToString();
            txt.Text += " ";
            string density;
            var d = Resources.DisplayMetrics.DensityDpi;
            switch (d)
            {
                case DisplayMetricsDensity.Default:
                    density = "mdpi";
                    break;
                case DisplayMetricsDensity.High:
                    density = "hdpi";
                    break;
                case DisplayMetricsDensity.Xhigh:
                    density = "xhdpi";
                    break;
                case DisplayMetricsDensity.Xxhigh:
                    density = "xxhdpi";
                    break;
                case DisplayMetricsDensity.Xxxhigh:
                    density = "xxxhpi";
                    break;
                default:
                    density = "unknown";
                    break;
            }

            txt.Text += density;
#endif
            TextView TV = FindViewById<RoboTextView>(Resource.Id.txtTitle);
            SpannableString wordtoSpan = new SpannableString(TV.Text);
            wordtoSpan.SetSpan(new ForegroundColorSpan(Color.Argb(255, 48, 218, 30)), 3, 6, SpanTypes.ExclusiveExclusive);
            TV.TextFormatted = wordtoSpan;

            dpad = FindViewById<DPad>(Resource.Id.dpad);
            dpad.ButtonClick += dpad_ButtonClick;

            PresetsLayout presetsLayout = FindViewById<PresetsLayout>(Resource.Id.Presets);
            presetsLayout.PresetSelected += presets_PresetSelected;

            SeekBar sb = FindViewById<SeekBar>(Resource.Id.seekBar1);
            sb.ProgressChanged += Sb_ProgressChanged;
#if USE_AMPLITUDE
            sb.Max = 250000;
#endif
            audio = (AudioManager)GetSystemService(Context.AudioService);

            RoboTextView version = FindViewById<RoboTextView>(Resource.Id.textVersionNumber);
            version.Text = PackageManager.GetPackageInfo(PackageName, 0).VersionName;

        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            switch (keyCode)
            {
                case Keycode.VolumeUp:
                    audio.AdjustStreamVolume(Android.Media.Stream.Music,
                            Android.Media.Adjust.Raise, Android.Media.VolumeNotificationFlags.ShowUi);
                    return true;
                case Keycode.VolumeDown:
                    audio.AdjustStreamVolume(Android.Media.Stream.Music,
                            Android.Media.Adjust.Lower, Android.Media.VolumeNotificationFlags.ShowUi);
                    return true;
                default:
                    return false;
            }
        }

        private void Sb_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
#if USE_AMPLITUDE
            robot.SetAmplitude((uint)(e.Progress + 40000));
            Log.Debug("Amplitude",(e.Progress + 40000).ToString());
#else

            var progress = 250 - e.Progress;

            if (progress > 245)
            {
                progress = 245;
            }
            if (progress < 10)
            {
                progress = 10;
            }
            robot.Speed = e.Progress;

            var minp = 10;
            var maxp = 250;
            var minv = System.Math.Log(10);
            var maxv = System.Math.Log(80);

            var scale = (maxv - minv) / (maxp - minp);

            int logValue = (int)System.Math.Exp(minv + scale * ((maxp - progress) - minp));
            logValue = 80 - logValue;
            Log.Debug("SeekBarChanged", progress.ToString() + "=>" + logValue.ToString());
            robot.MoveSpeed = logValue;
            #endif
        }

        void presets_PresetSelected(object sender, PresetsEventArgs e)
        {
            Log.Debug("PresetTouched", "Touched Preset " + e.Preset);
            selectedPreset = e.Preset - 1;
            dpad.Rotated = presets[selectedPreset].DPadRotated;
        }

        void dpad_ButtonClick(object sender, DPadButtonEventArgs e)
        {
            Thread t1 = new Thread(() =>
            {
                var button = e.Button;
                switch (button)
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
            });
            t1.Start();

        }
    }
}

