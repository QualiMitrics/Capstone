using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;

public partial class EmployeeView : System.Web.UI.Page
{

    //FOR HOURS, USE select dbo.WorkTime(@sdate, @edate) / 60 AS [Hours] 

    protected void Page_Load(object sender, EventArgs e)
    {
        //Checking the session to see if it's null
        if (Session["BEID"] == null)
        {   //if it is null, the user is redirected to the login page
            Response.Redirect("Login.aspx");
        }
        else
        {
            //Making an int session variable for sqldatasource to use for parameter
            Session["BEIDINT"] = Convert.ToInt32(Session["BEID"]);
        }

        

        //Range validator settings
        //This makes sure that you can't set a date before today or after 2 years from today

        String today = DateTime.Now.Date.ToString("yyyy/MM/dd");
        String twoyears = DateTime.Now.Date.AddYears(2).ToString("yyyy/MM/dd");
        

        rvHalfDay.MinimumValue = today;
        rvHalfDay.MaximumValue = twoyears;

        rvStartDate.MinimumValue = today;
        rvStartDate.MaximumValue = twoyears;
        //End date has to be at least one day later than today
        rvEndDate.MinimumValue = DateTime.Now.Date.AddDays(1).ToString("yyyy/MM/dd");
        rvEndDate.MaximumValue = twoyears;

        //End range validator settings

        
    }

    protected void btnTypeofTime_Click(object sender, EventArgs e)
    {
        //This button will make sure that the user can only submit for one type of time off at a time.
        if (chkDays.Checked == true)
        {
            pnlHalf.Visible = false;
            pnlFull.Visible = true;
            
        }
        else if (chkHalfDay.Checked == true)
        {
            pnlFull.Visible = false;
            pnlHalf.Visible = true;
        }
        else
        {
            lblCheckNote.Visible = true;
        }



    }
    protected void btnHalfSubmit_Click(object sender, EventArgs e)
    {
        String startDate = txtHalfDay.Text;
        String endDate = txtHalfDay.Text;
        //startDate = endDate = txtHalfDay.Text;
        String sickDay = "0";
        int BEID = Convert.ToInt32(Session["BEID"]);

        if (chkSick.Checked == true)
        {
            sickDay = "1";
        }


        string insert = "INSERT INTO HumanResources.TimeOff " +
                       "VALUES (@TID, @BEID, @sdate, @edate, @sickday, 'p', null)";

        //Establishing sql connection and running insert

        // Establish a connection to the database server
        SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
        sqlCon.Open();

        // create a command and associate it with the connection
        SqlCommand sqlComm = new SqlCommand();
        sqlComm.CommandText = insert;
        sqlComm.Connection = sqlCon;

        //Add parameters
        sqlComm.Parameters.Add("@TID", System.Data.SqlDbType.Int).Value = getTransID.getID();
        sqlComm.Parameters.Add("@BEID", System.Data.SqlDbType.Int).Value = BEID;
        sqlComm.Parameters.Add("@sdate", System.Data.SqlDbType.Date).Value = startDate;
        sqlComm.Parameters.Add("@edate", System.Data.SqlDbType.Date).Value = endDate;
        sqlComm.Parameters.Add("@sickday", System.Data.SqlDbType.Char).Value = sickDay;

        //Execute Insert statement
        sqlComm.ExecuteNonQuery();
        //Dispose
        sqlComm.Dispose();
        //Close connection
        sqlCon.Close();

        Response.Write("<script>alert('Congratulations, your request has been submitted.  You may view it and any other pending requests in the Pending Requests tab.');</script>");
        ClearControl(pnlSelections);

    } // END HALF DAY METHOD




    protected void btnFullSubmit_Click(object sender, EventArgs e)
    {

        //Initializing variables, also making sure that sickday is set to 1 if they have the checkbox selected
        //Also converting session variable to int
        String startDate = txtStartDate.Text;
        String endDate = txtEndDate.Text;
        String sickDay = "0";
        int BEID = Convert.ToInt32(Session["BEID"]);

        if (chkSick.Checked == true)
        {
            sickDay = "1";
        }

        //create insert statement
        string insert = "INSERT INTO HumanResources.TimeOff " +
                       "VALUES (@TID, @BEID, @sdate, @edate, @sickday, 'p', null)";

        //Establishing sql connection and running insert
        try
        {
            // Establish a connection to the database server
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            sqlCon.Open();

            // create a command and associate it with the connection
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = insert;
            sqlComm.Connection = sqlCon;


            //Add parameters
            sqlComm.Parameters.Add("@TID", System.Data.SqlDbType.Int).Value = getTransID.getID();
            sqlComm.Parameters.Add("@BEID", System.Data.SqlDbType.Int).Value = BEID;
            sqlComm.Parameters.Add("@sdate", System.Data.SqlDbType.Date).Value = startDate;
            sqlComm.Parameters.Add("@edate", System.Data.SqlDbType.Date).Value = endDate;
            sqlComm.Parameters.Add("@sickday", System.Data.SqlDbType.Char).Value = sickDay;

            //Execute Insert statement
            sqlComm.ExecuteNonQuery();
            //Dispose
            sqlComm.Dispose();
            //Close connection
            sqlCon.Close();

            Response.Write("<script>alert('Congratulations, your request has been submitted.  You may view it and any other pending requests in the Pending Requests tab.');</script>");
            ClearControl(pnlSelections);
        }
        catch (Exception er)
        {
            Response.Write(er.ToString());
        }

    } //END FULL DAY METHOD
    
    //This method clear controls, is triggered after submission
    private void ClearControl( Control control )
    {
    var textbox = control as TextBox;
    if (textbox != null)
    {
        textbox.Text = string.Empty;
    }

    chkDays.Checked = false;
    chkHalfDay.Checked = false;
    chkSick.Checked = false;


    foreach( Control childControl in control.Controls )
    {
        ClearControl( childControl );
    }
}


}