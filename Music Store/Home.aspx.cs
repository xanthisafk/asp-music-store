using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

namespace Music_Store
{
    public partial class Home : System.Web.UI.Page
    {
        protected string con_string = "Data Source=localhost;Initial Catalog=MusicShop;Integrated Security=True;Pooling=False";
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Welcome to Music Store";

            if (!IsPostBack)
            {
                string query = @"SELECT TOP 3 Id, Title, Artist, Cost, Art FROM Songs WHERE Popular = 1 ORDER BY newid();";
                SqlConnection con = new SqlConnection(con_string);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataList1.DataSource = dr;
                DataList1.DataBind();
                dr.Close();
                con.Close();
            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}