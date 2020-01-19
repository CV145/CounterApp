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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon ="@drawable/clock")]
    public class MainActivity : AppCompatActivity
    {
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor saveEditor;
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
                saveEditor.PutInt(Resource.String.savedCount.ToString(), count);
                saveEditor.Apply();
                Log.Debug(GetType().FullName, "Saved int using preferences. Count: " + count + " was saved.");
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

            //creates a new save file or accesses an existing one
            sharedPreferences = GetPreferences(FileCreationMode.Private);
            saveEditor = sharedPreferences.Edit();

            //restore stored count
            Count = sharedPreferences.GetInt(Resource.String.savedCount.ToString(), 0);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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