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

        //Make the query with the parameter @username, which will an email
        String query =
            "SELECT        Person.Password.PasswordHash, Person.Password.PasswordSalt, Person.Password.BusinessEntityID " +
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

            String pwHash = reader["PasswordHash"].ToString();
            String pwSalt = reader["PasswordSalt"].ToString();
            String BEID = reader["BusinessEntityID"].ToString();

            // close the reader
            reader.Close();



            //-----------------------
            //VERIFICATION
            //-----------------------

            if (SimpleHash.VerifyHash(password, "MD5", pwHash))
            {
                //If the user is authenticated their BEID is stored in the session variable
                e.Authenticated = true;
                Session["QualSess"] = BEID;

                //Manager check
                bool manager = isManager(BEID);

                //Redirecting to either manager or employee page
                if (manager == true)
                {
                    Response.Redirect("ManagerView.aspx");
                }
                else
                {
                    Response.Redirect("EmployeeView.aspx");
                }
            }
            else
            {
                //If they are not authed the session var is set to null
                e.Authenticated = false;
                Session["QualSess"] = null;
            } //END IF STATEMENT


        } //END IF STATEMENT
    } //END AUTH METHOD


    //Method to check if the user is a manager
    protected bool isManager(string businessEI)
    {

        int BEID = Convert.ToInt32(businessEI);

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