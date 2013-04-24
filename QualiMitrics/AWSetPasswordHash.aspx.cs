using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Data.SqlClient;

public partial class AWSetPasswordHash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        


        //ENTER EMPLOYEE BEID HERE
        String businessEntityID = "1";


        // Establish a connection to the database server
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
        con.Open();

        // create a command and associate it with the connection
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        // specify the query 
        command.CommandText = "SELECT Person.Person.BusinessEntityID, Person.Person.FirstName, Person.Person.LastName, Person.Person.PersonType, " + 
				                " Person.Password.PasswordHash, Person.Password.PasswordSalt " + 
                                " FROM  Person.Password INNER JOIN Person.Person ON Person.Password.BusinessEntityID = Person.Person.BusinessEntityID " + 
                                " Where Person.Person.BusinessEntityID = "  + businessEntityID +
                                " ORDER BY Person.Person.PersonType, Person.Person.LastName, Person.Person.FirstName ";

        // creating a data reader that contains the result set of the query
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {

            // read the next row of the result set
            reader.Read();

            // get data from the columns of that row
            String fName = reader["FirstName"].ToString();
            String lName = reader["LastName"].ToString();

            String pwHash = reader["PasswordHash"].ToString();
            String pwSalt = reader["PasswordSalt"].ToString();

            // close the reader
            reader.Close();

            // display results of query...
            Response.Write("<br>Name: " + fName + " " + lName);
            Response.Write("<br>Hash: " + pwHash);
            Response.Write("<br>Salt: " + pwSalt);


            // now make a new password hash
            String password = fName + pwSalt;   // ...for this example, just using the first name as the password

            // using the SimpleHash class (which is stored in App_Code)
            // calling the ComputeHash method to create a hashed string, with SHA1 hash algorithm
            string passwordHashNew =
                   SimpleHash.ComputeHash(password, "SHA1", null);
            Response.Write("<br><br>New: " + passwordHashNew);

            // write the new hash to the database
            command.CommandText = "update Person.Password set PasswordHash = '" + passwordHashNew + 
                "' where BusinessEntityID = " + businessEntityID;
            command.ExecuteNonQuery();

        }

    }
}