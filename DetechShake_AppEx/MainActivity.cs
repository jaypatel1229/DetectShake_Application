using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace DetechShake_AppEx
{
    [Activity(Label = "DeteShakeDemo",MainLauncher =true)]
    public class MainActivity : Activity
    {
        private Button start;
        private Button stop;
        private TextView textV;
        private EditText editText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            UIReference();
            UIClickEvent();
        }

        private void UIClickEvent()
        {
            start.Click += Start_Click;
            stop.Click += Stop_Click;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                Accelerometer.Stop();
            }
        }



        private void Start_Click(object sender, EventArgs e)
        {
            if (!Accelerometer.IsMonitoring)
            {
                Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                Accelerometer.Start(SensorSpeed.UI);

            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            textV.Text = $"Reading X: {e.Reading.Acceleration.X}, Reading Y: {e.Reading.Acceleration.Y}, Reading Z: {e.Reading.Acceleration.Z}";
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {

            editText.Text = $"last shake happen at {DateTime.UtcNow:t}";


        }


        private void UIReference()
        {
            start = FindViewById<Button>(Resource.Id.buttonStartDS);
            stop = FindViewById<Button>(Resource.Id.buttonStopDS);
            textV = FindViewById<TextView>(Resource.Id.textViewDS);
            editText = FindViewById<EditText>(Resource.Id.editTextDS);

        }
    }
}