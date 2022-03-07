using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Music_Store
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.AllKeys.Contains("user"))
            {
                HttpCookie cookie = Request.Cookies["user"];
                AuthBTN.Text = "Welcome " + cookie["name"];
                AuthBTN.PostBackUrl = "~/Profile.aspx";
            }

            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;
        }
    }
}