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
public class sqlMethods
{
    public sqlMethods()
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

    public static int getHours(String sdate, String edate)
    {
        //If for whatever reason the sql statement doesn't go through, it'll return -1 so that the class accessing this method 
        //knows that something is up (can't have negative hours and all that jazz)
        int hours = -1;

        //select dbo.WorkTime(@sdate, @edate) / 60 AS [Hours] 

        try
        {
            //Make the query with the parameter @username, which will an email
            String query =
                "SELECT dbo.WorkTime(@sdate, @edate) / 60 AS [Hours]";

            // Establish a connection to the database server
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            sqlCon.Open();

            // create a command and associate it with the connection
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = query;
            sqlComm.Connection = sqlCon;

            //Need to convert the parameter to the datatype in the database.  In this case, NVARCHAR (50)
            sqlComm.Parameters.Add("@sdate", System.Data.SqlDbType.DateTime).Value = sdate;
            sqlComm.Parameters.Add("@edate", System.Data.SqlDbType.DateTime).Value = edate;
            SqlDataReader reader = sqlComm.ExecuteReader();

            //reader reading read things readily
            if (reader.HasRows)
            {

                // read the next row of the result set
                reader.Read();

                // get data from the columns of that row

                String shours = reader["Hours"].ToString();

                // close the reader
                reader.Close();
                //parsing into an int for return
                hours = int.Parse(shours);
                return hours;

            } // END IF STATEMENT
                //same as above, return -1 if anything goes wrong so we don't crash
            else
            {
                return hours;
            }
        }
        catch (Exception e)
        {
            return hours;
        }
        

    } //END getHours METHOD

    public static void updateHours(int newHours, int BEID, int flag)
    {

        //create update statement
            string update = "UPDATE HumanResources.Employee " +
                            "SET @type = @hours " +
                            "WHERE BusinessEntityID = @BEID";

            //Establishing sql connection and running update
            try
            {
                // Establish a connection to the database server
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
                sqlCon.Open();

                // create a command and associate it with the connection
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = update;
                sqlComm.Connection = sqlCon;


                //Add parameters
                sqlComm.Parameters.Add("@hours", System.Data.SqlDbType.Int).Value = newHours;
                sqlComm.Parameters.Add("@BEID", System.Data.SqlDbType.Int).Value = BEID;
                //flag for identifying 
                if (flag == 1)
                {
                    sqlComm.Parameters.Add("@type", SqlDbType.NVarChar).Value = "SickLeaveHours";
                }
                else 
                {
                    sqlComm.Parameters.Add("@type", SqlDbType.NVarChar).Value = "VacationHours";
                }

                //Execute Insert statement
                sqlComm.ExecuteNonQuery();
                //Dispose
                sqlComm.Dispose();
                //Close connection
                sqlCon.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

    }
}