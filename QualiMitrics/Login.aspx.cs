using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginAuth(object sender, AuthenticateEventArgs e)
    {

        //-----------------------
        //SQL THINGS
        //-----------------------

        //making strings for the login fields
        string email = loginQM.UserName;
        string password = loginQM.Password;
        bool managerSuite = false;

        if (CheckBox1.Checked == true) { managerSuite = true; }

        //Make the query with the parameter @username, which will an email
        String query =
            "SELECT        Person.Password.PasswordHash, Person.Password.PasswordSalt, Person.Password.BusinessEntityID, Person.Person.FirstName, Person.Person.LastName " +
            "FROM            Person.Person INNER JOIN " +
                         "Person.EmailAddress ON Person.Person.BusinessEntityID = Person.EmailAddress.BusinessEntityID INNER JOIN " +
                         "Person.Password ON Person.Person.BusinessEntityID = Person.Password.BusinessEntityID " +
            "WHERE        (Person.EmailAddress.EmailAddress = @email);";

        // Establish a connection to the database server
        SqlConnection sqlCon = new SqlConnection();
        sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
        sqlCon.Open();

        // create a command and associate it with the connection
        SqlCommand sqlComm = new SqlCommand();
        sqlComm.CommandText = query;
        sqlComm.Connection = sqlCon;

        //Need to convert the parameter to the datatype in the database.  In this case, NVARCHAR (50)
        sqlComm.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 50).Value = email;
        SqlDataReader reader = sqlComm.ExecuteReader();


        //-----------------------
        //READER
        //-----------------------
        if (reader.HasRows)
        {

            // read the next row of the result set
            reader.Read();

            // get data from the columns of that row

            String fName = reader["FirstName"].ToString();
            String lName = reader["LastName"].ToString();
            String pwHash = reader["PasswordHash"].ToString();
            String pwSalt = reader["PasswordSalt"].ToString();
            String BEID = reader["BusinessEntityID"].ToString();

            String fullName = fName + " " + lName;
            password = password + pwSalt;
            // close the reader
            reader.Close();



            //-----------------------
            //VERIFICATION
            //-----------------------

            if (SimpleHash.VerifyHash(password, "SHA1", pwHash))
            {
                //If the user is authenticated their BEID is stored in the session variable along with their full name (for the home page)
                e.Authenticated = true;
                Session["BEID"] = BEID;
                Session["name"] = fullName;

                //Manager check
                bool manager = isManager(BEID);
                if (managerSuite == true)
                {
                    //Redirecting to either manager or employee page
                    if (manager == true)
                    {
                        if (BEID == "235")
                        {
                            Response.Redirect("HRManagerView.aspx");
                        }
                        else
                        {
                            Response.Redirect("ManagerView.aspx");
                        }

                    } //End Manager if statement


                    else
                    {
                        if (BEID == "240" || BEID == "238")
                        {
                            Response.Redirect("RecruiterView.aspx");
                        }
                        else
                        {
                            Response.Redirect("EmployeeView.aspx");
                        }

                    } //End Employee if statement
                }
                else
                {
                    Response.Redirect("EmployeeView.aspx");
                }
            } //End if for verify, else is below

            else
            {
                //If they are not authed the session var is set to null
                e.Authenticated = false;
                Session["BEID"] = null;
                Session["name"] = null;
            } //END VERIFY HASH IF STATEMENT


        } //END READER HAS ROWS IF STATEMENT

    } //END AUTH METHOD


    //Method to check if the user is a manager
    protected bool isManager(string businessEI)
    {

        int BEID = Convert.ToInt32(businessEI);

        //This query is selecting all the employees that the BEID in question manages.  If the reader doesn't have rows, that means that the employee
        //in question doesn't manage anybody.
        String query =
            "SELECT        e1.LoginID, e1.OrganizationNode, e1.JobTitle, e2.LoginID, e2.jobtitle, e2.BusinessEntityID " +
            "FROM            HumanResources.Employee AS e1 INNER JOIN " +
            "             HumanResources.Employee AS e2 ON e2.[OrganizationNode].GetAncestor(1) = e1.[OrganizationNode] " +
            "where e1.businessentityid = @BEID;";

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

        //-----------------------
        //READER
        //-----------------------
        if (reader.HasRows)
        {
            return true;
        }
        else
        {
            return false;
        }

    } //End isManager Method


}