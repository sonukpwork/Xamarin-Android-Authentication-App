using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System.Collections;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

namespace LoginSystem
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button mBtnSignUp;
        private Button mBtnLogin;
        private ProgressBar progressBar;
        private TextView txtError;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            txtError = FindViewById<TextView>(Resource.Id.txtError);
            mBtnLogin.Click += (object sender, EventArgs e) => {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogLogin loginDialog = new DialogLogin();
                loginDialog.Show(transaction, "Dialog Fragment");
                loginDialog.onLoginComplete += LoginDialog_onLoginComplete;
            };

            mBtnSignUp.Click += (object sender, EventArgs e) => {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogSignUp signUpDialog = new DialogSignUp();
                signUpDialog.Show(transaction, "Dialog Fragment");
                signUpDialog.onSignUpComplete += SignUpDialog_onSignUpComplete;
            };


        }

        private void LoginDialog_onLoginComplete(object sender, OnLoginEventArgs e)
        {
            string checkUser = Constants.checkLoginData(e.DialogEmail, e.DialogPassword);
            if(checkUser == "Succesfully Login")
            {
                Thread thread = new Thread(ActLikeARequest);
                thread.Start();
                progressBar.Visibility = ViewStates.Visible;
                txtError.Text = "";
            } else
            {
                txtError.Text = checkUser;
            }
        }

        private void SignUpDialog_onSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            string validationMsg = Validator.SignUpValidation(e.FirstName, e.Email, e.Password);
            if(validationMsg == "Clean")
            {
                string newAccount = Constants.insertNewAccount(e.FirstName, e.Email, e.Password);
                if (newAccount == "Data Succesfully Inserted")
                {
                    Thread thread = new Thread(ActLikeARequest);
                    thread.Start();
                    RunOnUiThread(() => { progressBar.Visibility = ViewStates.Visible; });
                    txtError.Text = "";
                }
                else
                {
                    txtError.Text = newAccount;
                }
            } else
            {
                txtError.Text = validationMsg;
            }
            

        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { progressBar.Visibility = ViewStates.Invisible; });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}