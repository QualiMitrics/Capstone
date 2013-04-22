<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="HRManagerView.aspx.cs" Inherits="HRManagerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
Human Resources Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--Data Source for Details View--%>
    <asp:SqlDataSource
        ID="sdsDetails"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
        SelectCommand="SELECT        Person.Person.BusinessEntityID AS [BEID], Person.Person.FirstName + Person.Person.LastName AS Name, HumanResources.TimeOff.Approval, 
                         CASE WHEN HumanResources.TimeOff.Comments is null THEN 'None' END AS [Comments]
                         FROM            Person.Person INNER JOIN
                         HumanResources.Employee AS Employee_1 ON Person.Person.BusinessEntityID = Employee_1.BusinessEntityID INNER JOIN
                         HumanResources.TimeOff ON Employee_1.BusinessEntityID = HumanResources.TimeOff.BusinessEntityID"></asp:SqlDataSource>
    <%--End SQL Data Source--%>


    <asp:SqlDataSource
        ID="sdsForm"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
        SelectCommand="SELECT        HumanResources.TimeOff.StartDate, HumanResources.TimeOff.EndDate, Person.Person.FirstName + Person.Person.LastName AS [Name], 
                       CASE WHEN HumanResources.TimeOff.Sick_Vacation = '0' THEN 'No' ELSE 'Yes' END AS [Sick Time], 
					   CASE WHEN HumanResources.TimeOff.Approval = '0' THEN 'Pending' ELSE 'Approved' END AS [Approval Status], 
					   CASE WHEN HumanResources.TimeOff.Comments is null THEN 'None' END AS [Comments]
                       FROM            HumanResources.TimeOff INNER JOIN
                       HumanResources.Employee AS Employee_1 ON HumanResources.TimeOff.BusinessEntityID = Employee_1.BusinessEntityID INNER JOIN
                       Person.Person ON Employee_1.BusinessEntityID = Person.Person.BusinessEntityID
                       WHERE		(HumanResources.TimeOff.BusinessEntityID = @BEID)">

        <SelectParameters>
            <asp:ControlParameter
                Name="BEID"
                Type="Int32"
                ControlID="dvEmp" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--End SQL Data Source--%>





    <%--<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">--%>
    <%--This is the top level for the tab containers--%>
    <ajaxToolkit:TabContainer ID="tcOne" runat="server">
        <%--Each tab is created by a tab panel--%>
        <%--Tab Panel 1--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Home" ID="Tab1">
            <%--Each tab panel is populated with Content Template--%>
            <ContentTemplate>
                <p>
                    HR Manager Homeview
                </p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>



        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Verification" ID="Tab2">
            <ContentTemplate>
                <asp:DetailsView
                    ID="dvEmp"
                    runat="server"
                    DataKeyNames="BEID, Name"
                    DataSourceID="sdsDetails"
                    Height="330px"
                    Width="500px"
                    AllowPaging="true">
                </asp:DetailsView>


                <asp:FormView
                    ID="fvPerson"
                    DataKeyNames="BusinessEntityID"
                    DataSourceID="sdsForm"
                    AllowPaging="true"
                    DefaultMode="Edit"
                    runat="server">

                    <ItemTemplate>

                        <%--Need to change textboxes to labels and finish adding things to the formview--%>

                        <%--First Name Label and Textbox - Beware of the single/double quotes rule--%>
                        <asp:Label ID="lblName" runat="server" Text="First Name"></asp:Label>
                        <asp:TextBox ID="txtFirst" Text='<%#Eval("Name") %>' runat="server"></asp:TextBox><br />
                        <%--Last Name Label and Textbox--%>
                        <asp:Label ID="lblSD" runat="server" Text="Last Name"></asp:Label>
                        <asp:TextBox ID="txtLast" Text='<%#Eval("StartDate") %>' runat="server"></asp:TextBox><br />
                        <%--Last Name Label and Textbox--%>
                        <asp:Label ID="lblEnd" runat="server" Text="Last Name"></asp:Label>
                        <asp:TextBox ID="TextBox1" Text='<%#Eval("EndDate") %>' runat="server"></asp:TextBox><br />
                        <%--Button--%>
                        

                    </ItemTemplate>





                </asp:FormView>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--End Panels--%>
    </ajaxToolkit:TabContainer>
    <%--</asp:UpdatePanel>--%>
</asp:Content>

