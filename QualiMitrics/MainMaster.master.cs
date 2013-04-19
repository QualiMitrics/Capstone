using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //Killing the session
        Session.Abandon();
        //Removing contents of all session variables
        Session.Contents.RemoveAll();
        //Form signout
        System.Web.Security.FormsAuthentication.SignOut();
        //Bringing user back to login page
        Response.Redirect("Login.aspx");
    }
}
