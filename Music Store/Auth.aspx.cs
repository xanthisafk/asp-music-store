using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Music_Store
{
    public partial class Auth1 : System.Web.UI.Page
    {
        protected SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "User authorization - Music Store";
            if (Request.Cookies.AllKeys.Contains("user"))
            {
                Response.Redirect("~/Profile.aspx");
            }
        }

        protected void reset_btn_Click(object sender, EventArgs e)
        {
            username_tb.Text = "";
            password_tb.Text = "";
            username_tb.Focus();
        }

        protected bool Custom_Validate()
        {
            List<string> msg = new List<string>();
            string msg2 = "";
            if (username_tb.Text == "")
            {
                msg.Add("Username can not be empty.");
            }
            if (password_tb.Text == "")
            {
                msg.Add("Password can not be empty.");
            }

            foreach (string s in msg)
            {
                msg2 += s + " ";
            }
            login_msg.Text = msg2;

            if (msg.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void login_btn_Click(object sender, EventArgs e)
        {
            if (!Custom_Validate())
            {
                return;
            }

            string query = $"SELECT Name FROM Users WHERE Username = '{username_tb.Text}' AND Password = '{password_tb.Text}';";

            SqlCommand cmd = new SqlCommand(query, con);
            
            try
            {
                con.Open();
                string name = (string)cmd.ExecuteScalar();
                if (name == null)
                {
                    login_msg.Text = "Invalid username or password";
                    return;
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("user");
                    cookie["name"] = name;
                    cookie["username"] = username_tb.Text;
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/Home.aspx");
                }
            }
            catch (SqlException ex)
            {
                login_msg.Text = ex.Message+" "+ex.Number;
            }
            finally
            {
                con.Close();
            }
        }

        protected void reset_btn2_Click(object sender, EventArgs e)
        {
            username_rtb.Text = "";
            password_rtb.Text = "";
            password2_rtb.Text = "";
            name_rtb.Text = "";
            email_rtb.Text = "";
            username_rtb.Focus();
        }

        protected bool Custom_Validate2()
        {
            List<string> msgs = new List<string>();
            string msg = "";

            if (username_rtb.Text == "")
            {
                msgs.Add("Username can not be empty");
            }

            if (username_rtb.Text.Length < 3)
            {
                msgs.Add("Username must be atleast 4 characters long");
            }

            if (password_rtb.Text == "")
            {
                msgs.Add("Password can not be empty");
            }

            if (password2_rtb.Text != password_rtb.Text)
            {
                msgs.Add("Passwords do not match");
            }

            if (name_rtb.Text == "")
            {
                msgs.Add("Name can not be empty");
            }

            if (email_rtb.Text == "")
            {
                msgs.Add("Email can not be empty");
            }

            string pattern = @"[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,3}";
            if (!Regex.Match(email_rtb.Text, pattern).Success)
            {
                msgs.Add("Invalid email");
            }

            foreach(string s in msgs)
            {
                msg += s + " | ";
            }

            register_msg.Text = msg;

            if (msgs.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void register_btn_Click(object sender, EventArgs e)
        {
            if(!Custom_Validate2())
            {
                return;
            }

            string username = username_rtb.Text;
            string password = password_rtb.Text;
            string email = email_rtb.Text;
            string name = name_rtb.Text;

            string query = $"INSERT INTO Users VALUES ('{username}','{password}','{name}','{email}')";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    HttpCookie cookie = new HttpCookie("user");
                    cookie["name"] = name;
                    cookie["username"] = username;
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/Home.aspx");
                }
                else
                {
                    register_msg.Text = "Some error occured";
                }
            }
            catch (SqlException ex)
            {
                register_msg.Text = ex.Message + " " + ex.Number;
            }
            finally
            {
                con.Close();
            }
        }
    }
}