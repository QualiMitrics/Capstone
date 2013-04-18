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
        if (Session["BEID"] == null)
        {   //if it is null, the user is redirected to the login page
            Response.Redirect("Login.aspx");
        }
        
    }

    protected void btnTypeofTime_Click(object sender, EventArgs e)
    {
        //This button will make sure that the user can only submit for one type of time off at a time.
        if (chkDays.Checked == true)
        {
            pnlFull.Visible = true;
            
        }
        else if (chkHalfDay.Checked == true)
        {
            pnlHalf.Visible = true;
        }
        else
        {
            lblCheckNote.Visible = true;
        }



    }
}