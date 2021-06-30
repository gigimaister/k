﻿using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace hashtil
{

    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class Login_screen : AppCompatActivity
    {

        private Button Send;
        private TextView Txt;
        private EditText User;
        private EditText Password;
        private CheckBox mCbxRemMe;
        private ProgressBar progressBar;
        string usr, pwd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("User", string.Empty);
            string password = pref.GetString("Password", string.Empty);




            User = FindViewById<EditText>(Resource.Id.username);
            Password = FindViewById<EditText>(Resource.Id.password);
            Send = FindViewById<Button>(Resource.Id.send);
            Txt = FindViewById<TextView>(Resource.Id.systemlog);
            mCbxRemMe = FindViewById<CheckBox>(Resource.Id.cbxRememberMe);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Invisible;
            usr = User.Text;
            pwd = Password.Text;
            Send.Click += LoginSendClick;


        }

        private void LoginSendClick(object sender, EventArgs e)
        {
            if (mCbxRemMe.Checked)
            {
                ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("User", User.Text.Trim());
                edit.PutString("Password", Password.Text.Trim());
                edit.Apply();
            }

            try
            {
                progressBar.Visibility = ViewStates.Visible;
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hashtildb.pe.hu/login.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("username", User.Text.Trim());
                parameters.Add("password", Password.Text.Trim());

                client.UploadValuesCompleted += Client_UploadValuesCompleted;
                client.UploadValuesAsync(uri, parameters);

            }

            catch (SystemException)
            {
                Txt.Text = "משהו השתבש...פנה למנהל";

            }

        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        public override void OnBackPressed() { }

        protected override void OnResume()
        {
            base.OnResume();

        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {


                    string id = "";
                    id = Encoding.UTF8.GetString(e.Result);
                    int newId = 0;

                    int.TryParse(id, out newId);

                    switch (id)
                    {

                        case "1":

                            Txt.Text = "מתחבר...";
                            Intent intent = new Intent(this, typeof(MainActivity));
                            intent.PutExtra("User", User.Text.Trim());
                            intent.PutExtra("UserLevel", "1");
                            this.StartActivity(intent);

                            break;


                        case "2":

                            Txt.Text = "מתחבר...";
                            Intent mainmngr = new Intent(this, typeof(MainManager));
                            mainmngr.PutExtra("User", User.Text.Trim());
                            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                            ISharedPreferencesEditor edit = pref.Edit();
                            edit.PutString("UserLevel", "2");
                            edit.Apply();
                            //mainmngr.PutExtra("UserLevel","2");

                            this.StartActivity(mainmngr);

                            break;

                        case "3":

                            Txt.Text = "מתחבר...";
                            Intent drivers_scan = new Intent(this, typeof(DriversMain));
                            drivers_scan.PutExtra("User", User.Text.Trim());
                            ISharedPreferences pref_drivers = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                            ISharedPreferencesEditor edit_drivers = pref_drivers.Edit();
                            edit_drivers.PutString("UserLevel", "3");
                            edit_drivers.Apply();
                            //mainmngr.PutExtra("UserLevel","2");

                            this.StartActivity(drivers_scan);

                            break;
                        default:

                            Txt.Text = "שם משתמש ו/או סיסמה לא נכונים!";
                            progressBar.Visibility = ViewStates.Invisible;

                            break;
                    }

                }

                catch (System.Reflection.TargetInvocationException )
                {
                    Txt.Text = "בעית תקשורת...נא לנסות שוב";
                    progressBar.Visibility = ViewStates.Invisible;

                }
            });
        }
    }

}
