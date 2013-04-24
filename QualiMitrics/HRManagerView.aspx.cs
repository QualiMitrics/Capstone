using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRManagerView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["BEID"] == null)
        {   //if it is null, the user is redirected to the login page
            Response.Redirect("Login.aspx");
        }
        else
        {
            //Making an int session variable for sqldatasource to use for parameter
            Session["BEIDINT"] = Convert.ToInt32(Session["BEID"]);
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
    }
}