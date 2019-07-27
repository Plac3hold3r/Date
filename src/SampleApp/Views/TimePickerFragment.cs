﻿using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Java.Util;
using MaterialTimePicker = Wdullaer.MaterialDateTimePicker.Time;

namespace SampleApp.Views
{
    public class TimePickerFragment : Fragment, MaterialTimePicker.TimePickerDialog.IOnTimeSetListener
    {
        private TextView timeTextView;
        private CheckBox mode24Hours;
        private CheckBox modeDarkTime;
        private CheckBox modeCustomAccentTime;
        private CheckBox vibrateTime;
        private CheckBox dismissTime;
        private CheckBox titleTime;
        private CheckBox enableSeconds;
        private CheckBox limitSelectableTimes;
        private CheckBox showVersion2;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.timepicker_layout, container, false);

            timeTextView = view.FindViewById<TextView>(Resource.Id.time_textview);
            Button timeButton = view.FindViewById<Button>(Resource.Id.time_button);
            mode24Hours = view.FindViewById<CheckBox>(Resource.Id.mode_24_hours);
            modeDarkTime = view.FindViewById<CheckBox>(Resource.Id.mode_dark_time);
            modeCustomAccentTime = view.FindViewById<CheckBox>(Resource.Id.mode_custom_accent_time);
            vibrateTime = view.FindViewById<CheckBox>(Resource.Id.vibrate_time);
            dismissTime = view.FindViewById<CheckBox>(Resource.Id.dismiss_time);
            titleTime = view.FindViewById<CheckBox>(Resource.Id.title_time);
            enableSeconds = view.FindViewById<CheckBox>(Resource.Id.enable_seconds);
            limitSelectableTimes = view.FindViewById<CheckBox>(Resource.Id.limit_times);
            showVersion2 = view.FindViewById<CheckBox>(Resource.Id.show_version_2);

            timeButton.Click += TimeButton_Click;

            return view;
        }

        private void TimeButton_Click(object sender, System.EventArgs e)
        {
            Calendar now = Calendar.Instance;
            var tpd = new MaterialTimePicker.TimePickerDialog();
            tpd.Initialize(
                    this,
                    now.Get(CalendarField.HourOfDay),
                    now.Get(CalendarField.Minute),
                    now.Get(CalendarField.Second),
                    mode24Hours.Checked
            );
            tpd.SetThemeDark(modeDarkTime.Checked);
            tpd.Vibrate(vibrateTime.Checked);
            tpd.DismissOnPause(dismissTime.Checked);
            tpd.EnableSeconds(enableSeconds.Checked);
            tpd.Version = showVersion2.Checked ? MaterialTimePicker.TimePickerDialog.AppVersion.Version2 : MaterialTimePicker.TimePickerDialog.AppVersion.Version1;
            if (modeCustomAccentTime.Checked)
            {
                tpd.AccentColor = Color.ParseColor("#9C27B0");
            }
            if (titleTime.Checked)
            {
                tpd.Title = "TimePicker Title";
            }
            if (limitSelectableTimes.Checked)
            {
                tpd.SetTimeInterval(3, 5, 10);
            }

            tpd.Show(FragmentManager, "Timepickerdialog");
        }

        public override void OnResume()
        {
            base.OnResume();
            var tpd = (MaterialTimePicker.TimePickerDialog)FragmentManager.FindFragmentByTag("Timepickerdialog");
            if (tpd != null)
            {
                tpd.OnTimeSetListener = this;
            }
        }

        public void OnTimeSet(MaterialTimePicker.TimePickerDialog view, int hourOfDay, int minute, int second)
        {
            string hourString = hourOfDay < 10 ? "0" + hourOfDay : "" + hourOfDay;
            string minuteString = minute < 10 ? "0" + minute : "" + minute;
            string secondString = second < 10 ? "0" + second : "" + second;
            timeTextView.Text = "You picked the following time: " + hourString + "h" + minuteString + "m" + secondString + "s";
        }
    }
}
