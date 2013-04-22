using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// This will determine the max transaction ID
/// </summary>
public class getTransID
{
	public getTransID()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int getID()
    {


        try
        {
            //-----------------------
            //SQL THINGS
            //-----------------------


            //Make the query with the parameter @username, which will an email
            String query =
                "select MAX(HumanResources.TimeOff.TransactionID) AS [TID] from HumanResources.TimeOff";

            // Establish a connection to the database server
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            sqlCon.Open();

            // create a command and associate it with the connection
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = query;
            sqlComm.Connection = sqlCon;

            //Need to convert the parameter to the datatype in the database.  In this case, NVARCHAR (50)
            SqlDataReader reader = sqlComm.ExecuteReader();



            int transID = 0;
            //-----------------------
            //READER
            //-----------------------
            if (reader.HasRows)
            {

                // read the next row of the result set
                reader.Read();

                // get data from the columns of that row

                String maxID = reader["TID"].ToString();

                // close the reader
                reader.Close();

                // transID has to be the max plus one for the next available ID
                transID = int.Parse(maxID);
                transID = transID + 1;
                //return the valid ID
                return transID;

            } // END IF STATEMENT
            else
            {
                return transID;
            }
            
        } //END TRY

        catch (Exception e)
        {
            //Return 0 which means that something went wrong
            return 0;
        }

        //once again, this means that something went wrong
        



       
        //if the reader didn't have rows or got an error 
        
    } //END getID() METHOD
}