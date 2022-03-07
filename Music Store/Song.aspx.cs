using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Music_Store
{
    public partial class Song : System.Web.UI.Page
    {
        protected SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            songid.Value = id;
            string query = $"SELECT Songs.Title, Songs.Artist, Songs.Cost, Songs.Art, Songs.Link, Artists.Photo FROM Songs JOIN Artists ON Songs.ArtistId = Artists.Id WHERE Songs.Id = {id}";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    title.Text = (string)dr["Title"];
                    artist.Text = (string)dr["Artist"];
                    price.Text = "₹" + dr["Cost"].ToString();
                    string a = (string)dr["Art"];
                    string b = (string)dr["Photo"];
                    art.ImageUrl = $"~/Assets/art/{a}";
                    artist_photo.ImageUrl = $"~/Assets/artist/{b}";
                    Title = title.Text + " - Music Store";
                    actualprice.Value = dr["Cost"].ToString();
                    url_hf.Value = dr["Link"].ToString();
                }
            }
            catch (SqlException)
            {
                //Response.Redirect("~/Home.aspx");
                throw;
            }
            finally
            {
                con.Close();
            }

            if (Request.Cookies.AllKeys.Contains("user"))
            {
                HttpCookie cookie = Request.Cookies["user"];
                string username = cookie["username"];
                string q2 = $"SELECT Money, Owned FROM Users WHERE Username = '{username}'";
                con.Open();
                SqlCommand cmd2 = new SqlCommand(q2, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                string ow = "";
                while (dr.Read())
                {
                    money_lbl.Text = "Wallet: ₹" + dr["Money"].ToString();
                    ow = (string)dr["Owned"];
                }
                dr.Close();
                string[] owned = ow.Split(',');
                foreach (string o in owned)
                {
                    if (o == Request.QueryString["id"])
                    {
                        purchase.Text = "Already owned";
                        purchase.Enabled = false;
                    }
                }
                con.Close();
            }
        }

        protected void Send_Alert(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}')", true);
        }

        protected void purchase_Click(object sender, EventArgs e)
        {
            if (!Request.Cookies.AllKeys.Contains("user"))
            {
                Send_Alert("You must be logged in to purchase");
                return;
            }

            HttpCookie cookie = Request.Cookies["user"];
            string username = cookie["username"];

            int cost = Convert.ToInt32(actualprice.Value);

            SqlCommand cmd = new SqlCommand($"SELECT Money FROM Users WHERE Username='{username}'", con);
            try
            {
                con.Open();
                int money = (int)cmd.ExecuteScalar();
                if (money < cost)
                {
                    Send_Alert("You do not have enough money to purchase this.");
                    return;
                }
                string id = songid.Value;
                money = money - cost;
                cmd.CommandText = $"SELECT Owned FROM Users WHERE Username='{username}'";
                string owned = (string)cmd.ExecuteScalar();
                owned += id + ",";
                cmd.CommandText = $"UPDATE Users SET Owned = '{owned}', Money = {money} WHERE Username='{username}'";
                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    Send_Alert("Something went wrong");
                }
                else
                {
                    Send_Alert($"You successfully purchased: {artist.Text} - {title.Text}");
                    money_lbl.Text = "₹" + money.ToString();
                    purchase.Text = "Already owned";
                    purchase.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                Send_Alert(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}