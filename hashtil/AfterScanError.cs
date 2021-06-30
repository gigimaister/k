﻿using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class AfterScanError : AppCompatActivity
    {
        private Button Back;
        Android.App.ProgressDialog progress;
        string User;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.after_scan_error);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);
            User = Intent.GetStringExtra("User") ?? "KOBI";

            Back = FindViewById<Button>(Resource.Id.btnbackmain);
            Back.Click += Back_Click;

            // Create your application here
        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void Back_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("User", User);
            this.StartActivity(intent);
            progress.Hide();
        }

        public override void OnBackPressed() { }

        protected override void OnResume()
        {
            base.OnResume();

        }
    }
}