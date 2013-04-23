using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

public partial class RecruiterView : System.Web.UI.Page
{
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

                String s = (String)reader["Resume"];

                xmlDoc.LoadXml(s);

            }
            //   xmlDoc.Load(reader);
        }


        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }


        recurseElements(xmlDoc.FirstChild, 0);
    }

    public void recurseElements(XmlNode node, int level)
    {



        if (node.NodeType == XmlNodeType.Element)
        {
            Output.Text += (node.Name);

            foreach (XmlAttribute attr in node.Attributes)
            {
                Output.Text += (attr.Name + "=" + attr.Value + " ");
            }

        }
        if (node.NodeType == XmlNodeType.Text)
            Output.Text += (node.Value);

        Output.Text += "<br>";

        //recurse through the child elements

        foreach (XmlNode child in node.ChildNodes)
        {

            recurseElements(child, level + 1);
        }

    }
}