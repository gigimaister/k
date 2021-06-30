using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace hashtil 
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriversMain : MainActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.drivers_main);
            // Create your application here
        }

        private void HandleScanResult(ZXing.Result result)
        {

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {

                msg = result.ToString();
                Intent intent = new Intent(this, typeof(AfterScanDrivers));
                intent.PutExtra("scanresult", msg);
                intent.PutExtra("User", Username);

                this.StartActivity(intent);

            }

            else
                NothingToScan();


        }


    }
}