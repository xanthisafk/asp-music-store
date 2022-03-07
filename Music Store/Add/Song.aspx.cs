using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ImageResizer.Resizing;

namespace Music_Store.Add
{
    public partial class Song : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");

        protected bool Is_Admin()
        {
            HttpCookie cookie;
            if (!Request.Cookies.AllKeys.Contains("user"))
            {
                return false;
            }
            else
            {
                cookie = Request.Cookies["user"];
            }
            string username = cookie["username"];
            string q = $"SELECT IsAdmin FROM Users WHERE Username = '{username}';";

            SqlCommand cmd = new SqlCommand(q, con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                bool p = false;
                while (dr.Read())
                {
                    p = (bool)dr["IsAdmin"];
                }
                return p;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if(!Is_Admin())
            {
                Response.Redirect("~/Home.aspx");
                return;
            }

            Title = "Add new song - Music Store Admin";

            if (!IsPostBack)
            {
                string q1 = "SELECT Id, Name FROM Artists";
                string q2 = "SELECT Id, Title FROM Songs";
                
                try
                {
                    SqlCommand cmd = new SqlCommand(q1, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        artist_dl.Items.Add(new ListItem((string)dr["Name"], dr["Id"].ToString()));
                    }
                    
                    dr.Close();
                    cmd.CommandText = q2;
                    dr = cmd.ExecuteReader();
                    song_dl.Items.Add(new ListItem("Add a new song", "0"));
                    while (dr.Read())
                    {
                        song_dl.Items.Add(new ListItem((string)dr["Title"], dr["Id"].ToString()));
                    }
                    id_lbl.Text = song_dl.Items.Count.ToString();
                    dr.Close();
                    addenw_btn.Visible = true;
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

        protected void Clear_Fields()
        {
            id_lbl.Text = song_dl.Items.Count.ToString();
            title_tb.Text = "";
            artist_dl.SelectedIndex = 0;
            cost_tb.Text = "";
            popular_cb.Checked = false;
            sale_cb.Checked = false;
            addenw_btn.Visible = false;
            update_btn.Visible = false;
            link_tb.Text = "";
            art.ImageUrl = "~/Content/Images/noimg.jpg";
            title_tb.Focus();
        }

        protected string Save_File(string id)
        {
            string path = Server.MapPath("~/Assets/art/") + id + ".jpg";
            art_upload.SaveAs(path);
            ImageResizer.ImageBuilder.Current.Build(path, path, new ImageResizer.ResizeSettings("maxwidth=500&maxheight=500&mode=pad"));
            return path;
        }

        protected void Update_Fields(string id)
        {
            string q = $"SELECT * FROM Songs WHERE Id={id};";
            try
            {
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id_lbl.Text = reader["Id"].ToString();
                    title_tb.Text = (string)reader["Title"];
                    song_dl.Items.Remove(song_dl.Items.FindByValue(id));
                    song_dl.Items.Add(new ListItem((string)reader["Title"], reader["Id"].ToString()));
                    song_dl.SelectedIndex = song_dl.Items.IndexOf(song_dl.Items.FindByValue(id));
                    art.ImageUrl = "~/Assets/art/" + (string)reader["Art"];
                    cost_tb.Text = reader["Cost"].ToString();
                    popular_cb.Checked = (bool)reader["Popular"];
                    sale_cb.Checked = (bool)reader["OnSale"];
                    link_tb.Text = (string)reader["Link"];
                }
            }
            catch (SqlException ex)
            {
                msg_lbl.Text = ex.Message;
                msg_lbl.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                con.Close();
            }
        }

        protected void Fetch_Click(object sender, EventArgs e)
        {
            string id = song_dl.SelectedValue;
            if (id == "0")
            {
                Clear_Fields();
                addenw_btn.Visible = true;
                return;
            }

            string q = $"SELECT Id, Title, Art, ArtistId, Cost, Popular, OnSale, Link FROM Songs WHERE Id={id}";
            SqlCommand cmd = new SqlCommand(q, con);
            try
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        id_lbl.Text = dr["ID"].ToString();
                        title_tb.Text = (string)dr["Title"];
                        cost_tb.Text = dr["Cost"].ToString();
                        popular_cb.Checked = (bool)dr["Popular"];
                        sale_cb.Checked = (bool)dr["OnSale"];
                        artist_dl.SelectedIndex = artist_dl.Items.IndexOf(artist_dl.Items.FindByValue(dr["ArtistId"].ToString()));
                        art.ImageUrl = "~/Assets/art/" + (string)dr["Art"];
                        link_tb.Text = (string)dr["Link"];
                        addenw_btn.Visible = false;
                        update_btn.Visible = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                msg_lbl.Text = ex.Message;
                msg_lbl.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                con.Close();
            }
        }

        protected void Add_New(object sender, EventArgs e)
        {
            string id = song_dl.SelectedValue;
            if (id != "0")
            {
                addenw_btn.Visible = false;
                update_btn.Visible = true;
                return;
            }

            id = id_lbl.Text;
            if (art_upload.HasFile)
            {
                Save_File(id);
            }
            else
            {
                art_upload.BorderColor = System.Drawing.Color.Red;
                return;
            }

            
            string arti = id + ".jpg";
            string title = title_tb.Text;
            string artist = artist_dl.SelectedItem.Text;
            string artistid = artist_dl.SelectedValue;
            string cost = cost_tb.Text;
            int popular = (popular_cb.Checked) ? 1 : 0;
            int sale = (sale_cb.Checked)? 1 : 0;
            string link = link_tb.Text;

            string q = $"INSERT INTO Songs VALUES ({id}, '{title}', '{artist}', {artistid}, '{arti}', {cost}, {popular}, {sale}, '{link}');";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);
                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    Response.Write("Something went wrong.");
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

            Update_Fields(id);
            update_btn.Visible = true;
            addenw_btn.Visible = false;
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            string id = song_dl.SelectedValue;
            if (id == "0")
            {
                update_btn.Visible = false;
                addenw_btn.Visible = true;
            }
            id = id_lbl.Text;
            string path;
            if (art_upload.HasFile)
            {
                path = Save_File(id);
            }

            string arti = id + ".jpg";
            string title = title_tb.Text;
            string artist = artist_dl.SelectedItem.Text;
            string artistid = artist_dl.SelectedValue;
            string cost = cost_tb.Text;
            int popular = (popular_cb.Checked) ? 1 : 0;
            int sale = (sale_cb.Checked) ? 1 : 0;
            string link = link_tb.Text;

            string q = $"UPDATE Songs SET Title = '{title}', Artist = '{artist}', ArtistId = {artistid}, Art = '{arti}', Cost = {cost}, Popular = {popular}, OnSale = {sale}, Link = '{link}' WHERE Id = {id};";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);
                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    Response.Write("Something went wrong could not update song.");
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

            Update_Fields(id);
            update_btn.Visible = true;
            addenw_btn.Visible = false;
        }
    }
}