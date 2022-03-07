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
    public partial class Store : System.Web.UI.Page
    {

        protected SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False");

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Store - Music Store";
            if(!IsPostBack)
            {
                string q2 = "";
                string username = "";
                if (Request.Cookies.AllKeys.Contains("user"))
                {
                    HttpCookie cookie = Request.Cookies["user"];
                    username = cookie["username"];
                    q2 = $"SELECT Money FROM Users WHERE Username = '{username}'";
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand(q2, con);
                    int money = (int)cmd2.ExecuteScalar();
                    con.Close();
                    money_lbl.Text = $"Wallet: ₹{money}";
                }

                string q = "SELECT * FROM Songs ORDER BY OnSale DESC;";
                
                SqlCommand cmd = new SqlCommand(q, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataList1.DataSource = dr;
                    DataList1.DataBind();
                    dr.Close();
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

        protected void sort_btn_Click(object sender, EventArgs e)
        {
            string cate = cate_dl.SelectedValue;
            string type = type_dl.SelectedValue;

            string query = $"SELECT * FROM Songs ORDER BY {cate} {type}";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataList1.DataSource = dr;
                DataList1.DataBind();
            }
            catch (SqlException)
            {
                ;
            }
            finally
            {
                con.Close();
            }
        }
    }
}