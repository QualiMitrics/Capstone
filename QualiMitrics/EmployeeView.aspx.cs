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
using Microsoft.Reporting.WebForms;

public partial class EmployeeView : System.Web.UI.Page
{







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
        
        String sickDay = "0";
        int BEID = Convert.ToInt32(Session["BEID"]);

        //This will confirm that they can in fact take the time off that they requested.  
        //If it's successful they'll go on to the rest of the submit method, and if not it will inform them that they're out of hours
        bool canTake = hasHours(startDate, endDate, Convert.ToInt32(sickDay), BEID);

        if (canTake == false)
        {
            Response.Write("<script>alert('You do not have sufficient vacation/sick leave left to take the requested time off.');</script>");
        }
        else
        {

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
            sqlComm.Parameters.Add("@TID", System.Data.SqlDbType.Int).Value = sqlMethods.getID();
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
            //Also try http://www.w3schools.com/jsref/met_loc_reload.asp if this doesn't work either
            //location.reload();
            Response.Redirect(Request.RawUrl);
            
        }

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
        //This will confirm that they can in fact take the time off that they requested.  
        //If it's successful they'll go on to the rest of the submit method, and if not it will inform them that they're out of hours
        bool canTake = hasHours(startDate, endDate, Convert.ToInt32(sickDay), BEID);

        if (canTake == false)
        {
            Response.Write("<script>alert('You do not have sufficient vacation/sick leave left to take the requested time off.');</script>");
        }
        else
        {

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
                sqlComm.Parameters.Add("@TID", System.Data.SqlDbType.Int).Value = sqlMethods.getID();
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
        }
    } //END FULL DAY METHOD


    //-------------------------------------------
    //Misc Methods
    //-------------------------------------------

    //This method checks the hours in a period of time selected by employee
    //and checks to make sure they have availble hours for that timespan
    //Will return true if the employee has sufficient hours 
    //Will return false if employee lacks sufficient hours
    private Boolean hasHours(String sdate, String edate, int sickChk, int BEID)
    {
        //Find out how many hours the employee is requesting
        int reqHours = sqlMethods.getHours(sdate, edate);

        if (chkHalfDay.Checked == true)
        {
            //This makes sure that if they picked a half day, that the hours requested off are 4 (no other definition of half day)
            reqHours = 4;
        }
        

        try
        {
            string query =
                "SELECT VacationHours AS [VHours], SickLeaveHours AS [SHours], BusinessEntityID AS [BEID] " +
                "FROM HumanResources.Employee " +
                "WHERE BusinessEntityID = @BEID";

            // Establish a connection to the database server
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            sqlCon.Open();

            // create a command and associate it with the connection
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = query;
            sqlComm.Connection = sqlCon;

            //Need to convert the parameter to the datatype in the database.  In this case, NVARCHAR (50)
            sqlComm.Parameters.Add("@BEID", System.Data.SqlDbType.Int).Value = BEID;
            SqlDataReader reader = sqlComm.ExecuteReader();

            if (reader.HasRows)
            {

                // read the next row of the result set
                reader.Read();

                // get data from the columns of that row

                String vacHours = reader["VHours"].ToString();
                String sickHours = reader["SHours"].ToString();

                //Make int version of the hour totals
                int sHours = int.Parse(sickHours);
                int vHours = int.Parse(vacHours);

                // close the reader
                reader.Close();

                //If they're using sick days, check sick hours etc
                //Yes this code is hideous I know
                if (sickChk == 1)
                {
                    if (reqHours > sHours)
                    {
                        return false;
                    }
                    else
                    {
                        //pass the new hours, the BEID and the sicktime flag to the update method
                        sqlMethods.updateHours((sHours-reqHours), BEID, sickChk);
                        return true;
                    }

                } //END SICK CHECK IF
                else
                {
                    if (reqHours > vHours)
                    {
                        return false;
                    }
                    else
                    {
                        //pass the new hours, the BEID and the sicktime flag to the update method
                        sqlMethods.updateHours((vHours-reqHours), BEID, sickChk);
                        return true;
                    }
                }
            } //END READER IF

            else
            {
                return false;
            }


        } //END TRY
        catch (Exception e)
        {
            Response.Write(e.ToString());
            return false;
        }

        
    } //END hasHours METHOD




    //This method clears controls and resets panel visibility
    private void ClearControl(Control control)
    {
        //Clearing all textboxes
        var textbox = control as TextBox;
        if (textbox != null)
        {
            textbox.Text = string.Empty;
        }
        //Making sure that all the checkboxes are unselected
        chkDays.Checked = false;
        chkHalfDay.Checked = false;
        chkSick.Checked = false;
        //Making all the panels invisible again
        pnlFull.Visible = false;
        pnlHalf.Visible = false;
        

        //woooo recursion is cool
        foreach (Control childControl in control.Controls)
        {
            ClearControl(childControl);
        }
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Write("<script>location.reload();</script>");
    }
    protected void ReportViewer1_Init(object sender, EventArgs e)
    {
        // Create the sales order number report parameter
        ReportParameter ID = new ReportParameter();
        ID.Name = "BE_ID";
        ID.Values.Add(Session["BEID"].ToString());

        // Set the report parameters for the report
        ReportViewer1.ServerReport.SetParameters(
            new ReportParameter[] { ID });
    }
}