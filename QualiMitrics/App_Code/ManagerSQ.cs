using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class exists solely to hold stored procedure logic in case we need to use it 
/// </summary>
public class ManagerSQ
{
	public ManagerSQ()
	{
        //Using stored procedure from this page: http://msdn.microsoft.com/en-us/library/ms124456(v=sql.100).aspx
        //Using tutorial on C# stored procedures from this page: http://msdn.microsoft.com/en-us/library/yy6y35y8(d=printer,v=vs.100).aspx
        //try
        //{
        //    //Converting business entity ID to int
        //    //Opening sql connection
        //    int BEID = Convert.ToInt32(businessEI);
        //    SqlConnection conn = new SqlConnection();
        //    SqlDataReader reader = null;


        //    // create a connection object
        //    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;

        //    //Creating new command and setting it as a stored procedure
        //    SqlCommand command = new SqlCommand();
        //    command.Connection = conn;
        //    command.CommandText = "uspGetManagerEmployees";
        //    command.CommandType = CommandType.StoredProcedure;

        //    //adding the parameter and setting properties
        //    SqlParameter param = new SqlParameter();
        //    param.ParameterName = "@BusinessEntityID";
        //    param.SqlDbType = SqlDbType.Int;
        //    param.Direction = ParameterDirection.Input;
        //    param.Value = BEID;

        //    //Add the parameter to the param collection
        //    command.Parameters.Add(param);

        //    //Open connection and execute reader
        //    conn.Open();
        //    reader = command.ExecuteReader();



        //    if (reader.HasRows)
        //    {


        //        reader.Read();
        //        // read the next row of the result set

        //        String recursion = reader["RecursionLevel"].ToString();

        //        List<int> recList = (from IDataRecord r in reader
        //                             select (int)r["RecursionLevel"]
        //            ).ToList();

        //        if (recList.Contains(1))
        //        {
        //            reader.Close();
        //            return true;
        //        }
        //        else
        //        {
        //            reader.Close();
        //            return false;
        //        }



        //    } //End if statement

        //    else
        //    {
        //        return false;
        //    }


        //} //End Try 

        //catch (Exception e)
        //{
        //    return false;
        //} //End catch
	}
}