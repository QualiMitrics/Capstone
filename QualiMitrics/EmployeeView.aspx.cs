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

    }
    protected void chkDays_CheckedChanged(object sender, EventArgs e)
    {
        pnlHalf.Visible = false;
        pnlFull.Visible = true;
        
    }
    protected void chkHalfDay_CheckedChanged(object sender, EventArgs e)
    {
        pnlFull.Visible = false;
        pnlHalf.Visible = true;
        
    }
}