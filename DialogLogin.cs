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
    public class OnLoginEventArgs : EventArgs
    {
        private string tbDialogEmail;
        private string tbDialogPassword;

        public string DialogEmail
        {
            get { return tbDialogEmail; }
            set { tbDialogEmail = value; }
        }

        public string DialogPassword
        {
            get { return tbDialogPassword; }
            set { tbDialogPassword = value; }
        }

        public OnLoginEventArgs(string email, string password) : base()
        {
            DialogEmail = email;
            DialogPassword = password;
        }
    }
    class DialogLogin : DialogFragment
    {
        private EditText tbDialogEmail;
        private EditText tbDialogPassword;
        private TextView txtError;
        private Button btnDialogLogin;

        public event EventHandler<OnLoginEventArgs> onLoginComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_login, container, false);

            tbDialogEmail = view.FindViewById<EditText>(Resource.Id.tb_DialogEmail);
            tbDialogPassword = view.FindViewById<EditText>(Resource.Id.tb_DialogPassword);
            btnDialogLogin = view.FindViewById<Button>(Resource.Id.btnDialogLogin);

            btnDialogLogin.Click += BtnDialogLogin_Click;

            return view;
        }

        private void BtnDialogLogin_Click(object sender, EventArgs e)
        {
            onLoginComplete.Invoke(this, new OnLoginEventArgs(tbDialogEmail.Text, tbDialogPassword.Text));
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