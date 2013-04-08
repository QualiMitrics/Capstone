using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            "SELECT        Person.Password.PasswordHash, Person.Password.PasswordSalt " +
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

            // close the reader
            reader.Close();



            //-----------------------
            //VERIFICATION
            //-----------------------

            if (SimpleHash.VerifyHash(password, "MD5", pwHash))
            {
                e.Authenticated = true;
                Session["SessionsAreCool"] = null;
            }
            else
            {
                e.Authenticated = false;
                Session["SessionsAreCool"] = 1;
            } //END IF STATEMENT


        } //END IF STATEMENT
    }
}