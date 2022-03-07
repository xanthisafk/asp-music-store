using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ImageResizer.Resizing;
using System.IO;

namespace Music_Store.Add
{
    public partial class Artist : System.Web.UI.Page
    {
        protected SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");

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
            Title = "Add new artist - Music Store Admin";

            if (!Is_Admin())
            {
                Response.Redirect("~/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string q1 = "SELECT Id, Name FROM Artists;";
                
                try
                {
                    SqlCommand cmd = new SqlCommand(q1, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    Artist_dl.Items.Add(new ListItem("Add a new artist", "0"));
                    while (dr.Read())
                    {

                        Artist_dl.Items.Add(new ListItem((string)dr["Name"], dr["Id"].ToString()));
                    }
                    addnew_btn.Visible = true;
                    dr.Close();
                    id_lbl.Text = Artist_dl.Items.Count.ToString();
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
        }

        protected void Fetch_Click(object sender, EventArgs e)
        {
            string Id = Artist_dl.SelectedValue;
            if (Id == "0")
            {
                id_lbl.Text = Artist_dl.Items.Count.ToString();
                name_tb.Text = "";
                Image1.ImageUrl = "~/Content/Images/noimg.jpg";
                update_btn.Visible = false;
                addnew_btn.Visible = true;
                return;
            }
            try
            {
                con.Open();
                string q = $"SELECT Id, Name, Photo FROM Artists WHERE Id={Id}";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id_lbl.Text = dr["Id"].ToString();
                    name_tb.Text = (string)dr["Name"];
                    Image1.ImageUrl = "~/Assets/artist/" + (string)dr["Photo"];
                }
                dr.Close();
                addnew_btn.Visible = false;
                update_btn.Visible = true;
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

        protected string Save_File(string id)
        {
            string path = Server.MapPath("../Assets/artist/") + id + ".jpg";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileUpload1.SaveAs(path);
            ImageResizer.ImageBuilder.Current.Build(path, path, new ImageResizer.ResizeSettings("maxwidth=500&maxheight=500&mode=pad"));
            return "~/Assets/artist/"+id+".jpg";
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            string id = Artist_dl.SelectedValue;
            if (id == "0")
            {
                addnew_btn.Visible = true;
                update_btn.Visible = false;
                return;
            }

            string name = name_tb.Text;
            
            if (FileUpload1.HasFile)
            {
                string path = Save_File(id);
                Image1.ImageUrl = path;
            }

            string query = $"UPDATE Artists SET Name='{name}', Photo='{id+".jpg"}' WHERE Id = {id};";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    msg_lbl.Text = "Artist has been updated";
                    msg_lbl.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    msg_lbl.Text = "Update failed. Something unexpected went wrong.";
                    msg_lbl.ForeColor = System.Drawing.Color.Red;
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
            string id = Artist_dl.SelectedValue;
            if (id != "0")
            {
                addnew_btn.Visible = false;
                update_btn.Visible = true;
                return;
            }

            id = id_lbl.Text;

            string name = name_tb.Text;

            if (name == "")
            {
                name_tb.BorderColor = System.Drawing.Color.Red;
                return;
            }

            if (!FileUpload1.HasFile)
            {
                FileUpload1.BorderColor = System.Drawing.Color.Red;
                msg_lbl.Text = "Please upload an image";
                msg_lbl.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                Save_File(id);
            }

            string photo = id + ".jpg";

            string query = $"INSERT INTO Artists VALUES ({id}, '{name}', '{photo}');";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    query = $"SELECT Id, Name, Photo FROM Artists WHERE Id = {id}";
                    SqlCommand cmd2 = new SqlCommand(query, con);
                    SqlDataReader dr = cmd2.ExecuteReader();
                    if (dr.Read())
                    {
                        Artist_dl.Items.Add(new ListItem((string)dr["Name"], dr["Id"].ToString()));
                        name_tb.Text = dr["Name"].ToString();
                        id_lbl.Text = dr["Id"].ToString();
                        Image1.ImageUrl = "~/Assets/artist/"+(string)dr["Photo"];
                    }
                    Artist_dl.SelectedIndex = Artist_dl.Items.IndexOf(Artist_dl.Items.FindByText(name));
                    update_btn.Visible = false;
                    addnew_btn.Visible = true;
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
    }
}