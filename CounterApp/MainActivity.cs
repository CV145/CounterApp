using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Android;
using System;
using Android.Content;
using Android.Preferences;
using Android.Util;

namespace CounterApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button plusBtn;
        Button minusBtn;
        Button resetBtn;
        TextView countView;
        int count = 0;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                if (count < 0)
                {
                    count = 0;
                }
                countView.Text = count.ToString();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            plusBtn = FindViewById<Button>(Resource.Id.plusBtn);
            minusBtn = FindViewById<Button>(Resource.Id.minusBtn);
            resetBtn = FindViewById<Button>(Resource.Id.resetBtn);
            countView = FindViewById<TextView>(Resource.Id.countView);

            plusBtn.Click += PlusOnClick;
            minusBtn.Click += MinusOnClick;
            resetBtn.Click += ResetOnClick;

            

            if (savedInstanceState != null)
            {
                int restoredCount = savedInstanceState.GetInt("count");
                count = restoredCount;
                countView.Text = restoredCount.ToString();
            }
            else
            {
                countView.Text = "0";
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Saves information using bundles 
        /// </summary>
        /// <param name="outState"></param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("count", count);
            Log.Debug(GetType().FullName, "Saved instance state to bundle. Count: " + count + " was saved.");

            base.OnSaveInstanceState(outState);
        }


        void PlusOnClick(object sender, EventArgs e)
        {
            Count++;
        }

        void MinusOnClick(object sender, EventArgs e)
        {
            Count--;
        }

        void ResetOnClick(object sender, EventArgs e)
        {
            Count = 0;
        }
    }
}