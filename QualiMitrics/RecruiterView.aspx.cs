using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

using System.Xml; //needed for XML processing *//
using System.Data;
using System.Data.SqlClient;



public partial class RecruiterView : System.Web.UI.Page
{
    String s = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    protected void loadResume(object sender, EventArgs e)
    {

        //Instantiate and load an XmlDocument with a URL or file

        XmlDocument xmlDoc = new XmlDocument();
        SqlConnection cnn = null;
        SqlCommand cmd = null;

        try
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            cnn.Open();

            string selectQry = "SELECT Resume FROM HumanResources.JobCandidate WHERE (JobCandidateID = @JobCandidateID)";

            cmd = new SqlCommand();
            cmd.CommandText = selectQry;
            cmd.Connection = cnn;

            cmd.Parameters.Add("@JobCandidateID", System.Data.SqlDbType.Int).Value = DropDownList1.SelectedIndex;



            // XmlReader reader = cmd.ExecuteXmlReader();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                s = (String)reader["Resume"];
                xmlDoc.LoadXml(s);

            }
            //   xmlDoc.Load(reader);
        }


        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        

        Output.Text = Beautify(xmlDoc);
    }
    public static string FormatXMLString(string sUnformattedXML)
    {
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(sUnformattedXML);
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        XmlTextWriter xtw = null;
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        try
        {
            xtw = new XmlTextWriter(sw);
            xtw.Formatting = Formatting.Indented;
            xd.WriteTo(xtw);
        }
        finally
        {
            if (xtw != null)
                xtw.Close();
        }
        return sb.ToString();
    }

    static public string Beautify(XmlDocument doc)
    {
        StringBuilder sb = new StringBuilder();
        XmlWriterSettings settings = new XmlWriterSettings();
        
        settings.Indent = true;
        settings.IndentChars = "  ";
        settings.NewLineChars = "\r\n";
        settings.NewLineHandling = NewLineHandling.Replace;
        using (XmlWriter writer = XmlWriter.Create(sb, settings))
        {
            
            doc.Save(writer);
        }
        return sb.ToString();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Establishing sql connection and running update
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
                con.Open();
                string delete = "DELETE FROM HumanResources.JobCandidate WHERE JobCandidateID=@JCID";
                // create a command and associate it with the connection
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = delete;
                cmd.Connection = con;
            

                //Add parameters
                cmd.Parameters.Add("@JCID", System.Data.SqlDbType.Int).Value = DropDownList1.SelectedIndex;

               
                cmd.ExecuteNonQuery();


                cmd.Dispose();
                //Close connection
                con.Close();
                Response.Write("<script>alert('Job candidate removed.');</script>");
            }
            catch (Exception ex)
            {
                
            }



        
    }
}