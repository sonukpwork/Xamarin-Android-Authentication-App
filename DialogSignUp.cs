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

namespace LoginSystem
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string tbFirstName;
        private string tbEmail;
        private string tbPassword;

        public string FirstName
        {
            get { return tbFirstName; }
            set { tbFirstName = value; }
        }

        public string Email
        {
            get { return tbEmail; }
            set { tbEmail = value; }
        }

        public string Password
        {
            get { return tbPassword; }
            set { tbPassword = value; }
        }

        public OnSignUpEventArgs(string firstName, string email, string password) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
        }
    }
    class DialogSignUp : DialogFragment
    {
        private EditText tbFullName;
        private EditText tbEmail;
        private EditText tbPassword;
        private Button btnSignUp;

        public event EventHandler<OnSignUpEventArgs> onSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            tbFullName = view.FindViewById<EditText>(Resource.Id.tb_FullName);
            tbEmail = view.FindViewById<EditText>(Resource.Id.tb_Email);
            tbPassword = view.FindViewById<EditText>(Resource.Id.tb_Password);
            btnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogSignUp);

            btnSignUp.Click += BtnSignUp_Click;

            return view;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            onSignUpComplete.Invoke(this, new OnSignUpEventArgs(tbFullName.Text, tbEmail.Text, tbPassword.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //set the title bar to visible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // set the animation
        }
    }
}