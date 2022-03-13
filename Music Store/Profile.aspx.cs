using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Music_Store
{
    public partial class Profile : System.Web.UI.Page
    {
        protected SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");

        protected void Page_Load(object sender, EventArgs e)
        {

            Title = "Profile - Music Store";

            if (!Request.Cookies.AllKeys.Contains("user"))
            {
                Response.Redirect("~/Home.aspx");
            }

            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["user"];
                string username = cookie["username"];
                string name = cookie["name"];

                string query = $"SELECT Email, IsAdmin, Money FROM Users WHERE Username='{username}'";

                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string email = (string)dr["Email"];
                        bool v = (bool)dr["IsAdmin"];
                        name_tb.Text = name;
                        email_tb.Text = email;
                        username_tb.Text = username;
                        song_btn.Visible = v;
                        artist_btn.Visible = v;
                        money_current.Text = dr["Money"].ToString();
                        money_tb.Text = "100";
                    }  
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                Purchased_Hydrate();
            }
        }

        protected void Purchased_Hydrate()
        {
            string q = $"SELECT Owned FROM Users WHERE Username = '{username_tb.Text}'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            string ow = (string)cmd.ExecuteScalar();

            if (ow.Length == 0)
            {
                return;
            }

            ow = ow.Remove(ow.Length - 1);
            q = $"SELECT Id, Title, Artist, Art FROM Songs WHERE Id IN ({ow})";

            cmd.CommandText = q;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
            con.Close();
        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["user"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Response.Redirect("~/Home.aspx");
        }

        protected void Update_Cookies(string Name)
        {
            HttpCookie c = Request.Cookies["user"];
            c["name"] = Name;
            Response.Cookies.Add(c);
        }

        protected void Refresh_TextBox(string Username)
        {
            string query = $"SELECT Name, Email FROM Users WHERE Name = '{Username}'";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    name_tb.Text = dr[0].ToString();
                    email_tb.Text = dr[1].ToString();
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            string username = username_tb.Text;

            SqlCommand tcmd = new SqlCommand($"SELECT Password FROM Users WHERE Username='{username}'", con);
            try
            {
                con.Open();
                string password = (string)tcmd.ExecuteScalar();
                if (password != password_tb.Text)
                {
                    msg_lbl.Text = "Your old password is not correct";
                    return;
                }
            }
            catch(SqlException ex)
            {
                msg_lbl.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
            string pwd = "";
            if (passwordn_tb.Text != ""  || passwordn2_tb.Text  != "")
            { 
                if (passwordn2_tb.Text != passwordn_tb.Text)
                {
                    msg_lbl.Text = "New passwords do not match";
                    return;
                }
                else
                {
                    pwd =  passwordn_tb.Text;
                }
            }
            else
            {
                pwd = password_tb.Text;
            }

            string name = name_tb.Text;
            string email = email_tb.Text;


            string q = $"UPDATE Users SET Password = '{pwd}', Email = '{email}', Name='{name}' WHERE Username='{username}'";
            SqlCommand cmd = new SqlCommand(q, con);

            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    msg_lbl.Text = "Something went wrong";
                }
                else
                {
                    msg_lbl.Text = "Profile updated.";
                    msg_lbl.ForeColor = System.Drawing.Color.Green;
                    Update_Cookies(name);
                    Refresh_TextBox(username);
                }
            }
            catch (SqlException ex)
            {
                msg_lbl.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }

        }

        protected void Add_Money(object sender, EventArgs e)
        {
            int current = Convert.ToInt32(money_current.Text);
            int toadd = Convert.ToInt32(money_tb.Text);
            string q = $"UPDATE Users SET Money = {current + toadd} WHERE Username='{username_tb.Text}'";
            try
            {
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    Response.Write("Something wrong happened");
                }
                else
                {
                    money_current.Text = Convert.ToString(current + toadd);
                }
            }
            catch (SqlException ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}