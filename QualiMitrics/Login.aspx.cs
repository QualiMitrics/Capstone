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

        //NOTE: NOT CONFIGURED
        //THINGS THAT NEED TO HAPPEN IN THE QUERY/SESSION VARIABLE
        //Query needs to find out if they're a manager, and if so direct them to the ManagerView page, else direct them to EmployeeView page
        //We should define what a manager is before that actually
        //The session variable needs to then carry this data through
        //For instance, it should pull the department the manager is in 
        //so that the query for that page can display proper departmental statistics
        



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
                Session["SessionsAreCool"] = BEID;
            }
            else
            {
                //If they are not authed the session var is set to null
                e.Authenticated = false;
                Session["SessionsAreCool"] = null;
            } //END IF STATEMENT


        } //END IF STATEMENT
    } //END AUTH METHOD


    //Method to check if the user is a manager
    protected bool storedProc(string businessEI)
    {
        int BEID = Convert.ToInt32(businessEI);
        SqlConnection conn = new SqlConnection();
		SqlDataReader reader  = null;


		// create a connection object
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
        

		SqlCommand command = new SqlCommand;
        command.Connection = conn;
        command.CommandText = "uspGetManagerEmployees";
        command.CommandType = CommandType.StoredProcedure;

        //adding the parameter and setting properties
        SqlParameter param = new SqlParameter;
        param.ParameterName = "@ManagerIDint";
        param.SqlDbType = SqlDbType.Int;
        param.Direction = ParameterDirection.Input;
        param.Value = BEID;

        //Add the parameter to the param collection
        command.Parameters.Add(param);

        //Open connection and execute reader
        conn.Open();
        reader = command.ExecuteReader();

        if (reader.HasRows)
        {

            //TO DO: Find recursion level, how many managed (make array possibly?)

            // read the next row of the result set
            reader.Read();

            // get data from the columns of that row

            //String pwHash = reader["PasswordHash"].ToString();
            //String pwSalt = reader["PasswordSalt"].ToString();
            //String BEID = reader["BusinessEntityID"].ToString();

            // close the reader
            reader.Close();

        }
		
		






        return true;
    }

}