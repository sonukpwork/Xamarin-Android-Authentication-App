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
using MySql.Data.MySqlClient;
using System.Data;

namespace LoginSystem
{
    public static class Constants
    {
        public static string insertUser = "INSERT INTO user_account(FullName,Email,Password) VALUES(@user,@email,@pass)";

        public static string selectOnlyEmail = "SELECT Email FROM user_account WHERE Email = @email"; 

        public static string selectUserAccount = "SELECT * FROM user_account WHERE Email = @email AND password = @password";

        public static bool emailExists(string email, MySqlConnection conn)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(selectOnlyEmail, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Connection = conn;
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                return true;
            }
            else
            {
                conn.Close();
            }
            return false;
         }
        public static string checkLoginData(string email, string password)
        {
            MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(selectUserAccount, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Connection = conn;

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        return "Succesfully Login";
                    }
                    else
                    {
                        //txtError.Text = "Email or Password is incorrect";
                        return "Email or Password is incorrect";
                    }
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }

            return null;
        }

        public static string insertNewAccount(string fullName,string email, string password)
        {
            MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    if (emailExists(email, conn) == true)
                    {
                        return "Email Already Exists !!!!  Try again";
                    } else
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(Constants.insertUser, conn);
                        cmd.Parameters.AddWithValue("@user", fullName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.ExecuteNonQuery();
                        return "Data Succesfully Inserted";
                    }
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return "Error";

        }
    }
}