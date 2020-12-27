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
    public class Validator
    {
        public static string SignUpValidation(string FullName, string Email, string Password)
        {
            if(FullName.Length == 0)
            {
                return "Please Enter Full Name";
            } else if(FullName.Length < 3 || FullName.Length > 30)
            {
                return "Full Name must be between 3 and 30 characters";
            } else if(Email.Length == 0)
            {
                return "Please Enter Email";
            } else if(Email.Length < 5 || Email.Length > 30)
            {
                return "Email must be between 5 and 30 characters";
            } else if(Password.Length == 0)
            {
                return "Please Enter Password first";
            }
            else if(Password.Length < 6 || Password.Length > 30) {
                return "Password must be between 8 and 30 characters";
            } else
            {
                return "Clean";
            }
        }

        public static string LoginValidation(string Email,string Password)
        {

            return "Clean";
        }
    }
}