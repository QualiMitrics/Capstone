using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checking the session to see if it's null
        if (Session["QualSess"] == null)
        {   //if it is null, the user is redirected to the login page
            Response.Redirect("Login.aspx");
        }
        
    }
    protected void chkDays_CheckedChanged(object sender, EventArgs e)
    {
        

    }
    protected void chkHalfDay_CheckedChanged(object sender, EventArgs e)
    {
        

    }
}