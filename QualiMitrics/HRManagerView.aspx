<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="HRManagerView.aspx.cs" Inherits="HRManagerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
    Human Resources Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">







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


                <%--Datasources need to be in the same content template--%>
                <%--Data Source for Details View--%>
                <asp:SqlDataSource
                    ID="sdsLB"
                    runat="server"
                    ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
                    SelectCommand="SELECT TransactionID FROM HumanResources.TimeOff WHERE (Approval = 'p')"></asp:SqlDataSource>
                <%--End SQL Data Source--%>


                <asp:SqlDataSource
                    ID="sdsForm"
                    runat="server"
                    ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
                    SelectCommand="SELECT        HumanResources.TimeOff.TransactionID, HumanResources.TimeOff.StartDate, HumanResources.TimeOff.EndDate, Person.Person.FirstName + ' ' + Person.Person.LastName AS [Name], 
                       CASE WHEN HumanResources.TimeOff.Sick_Vacation = '0' THEN 'No' ELSE 'Yes' END AS [Sick Time], 
					   CASE WHEN HumanResources.TimeOff.Approval = 'p' THEN 'Pending' WHEN HumanResources.TimeOff.Approval = 'a' THEN 'Approved' ELSE 'Denied' END AS [Approval Status], 
					   CASE WHEN HumanResources.TimeOff.Comments is null THEN 'None' ELSE END AS [Comments]
                       FROM            HumanResources.TimeOff INNER JOIN
                       HumanResources.Employee AS Employee_1 ON HumanResources.TimeOff.BusinessEntityID = Employee_1.BusinessEntityID INNER JOIN
                       Person.Person ON Employee_1.BusinessEntityID = Person.Person.BusinessEntityID
                       WHERE		(HumanResources.TimeOff.TransactionID = @TransactionID)"
                    UpdateCommand="UPDATE HumanResources.TimeOff SET Approval = @Approval, Comments = @Comments WHERE TransactionID = @TransactionID">

                    <SelectParameters>
                        <asp:ControlParameter
                            Name="TransactionID"
                            Type="Int32"
                            ControlID="lbTrans" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:ControlParameter
                            Name="Approval"
                            Type="String"
                            ControlID="fvPerson$txtApproval" />
                        <asp:ControlParameter
                            Name="Comments"
                            Type="String"
                            ControlID="fvPerson$txtComments" />
                        <asp:ControlParameter
                            Name="TransactionID"
                            Type="String"
                            ControlID="fvPerson$lblTID" />
                    </UpdateParameters>


                </asp:SqlDataSource>
                <%--End SQL Data Source--%>


                <%--Transaction ID Listbox - This will list all the transactions that are still "Pending"--%>
                <asp:ListBox
                    ID="lbTrans"
                    runat="server"
                    DataSourceID="sdsLB"
                    DataTextField="TransactionID"
                    DataValueField="TransactionID"
                    AutoPostBack="True"
                    Width="90px"></asp:ListBox>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                <%--The formview will show the actual request with all the details--%>
                <%--Anything that shouldn't be edited will be a label--%>
                <asp:FormView
                    ID="fvPerson"
                    DataKeyNames="TransactionID"
                    DataSourceID="sdsForm"
                    AllowPaging="true"
                    DefaultMode="Edit"
                    runat="server">

                    <EditItemTemplate>

                        <%--Need to change textboxes to labels and finish adding things to the formview--%>

                        <%--TransactionID--%>
                        <asp:Label ID="Label8" runat="server" Text="Transaction ID: "></asp:Label>
                        <asp:Label ID="lblTID" runat="server" Text='<%#Eval("TransactionID") %>'></asp:Label>
                        <br />
                        <%--Name--%>
                        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        <br />
                        <%--Start Date--%>
                        <asp:Label ID="Label2" runat="server" Text="Start Date: "></asp:Label>
                        <asp:Label ID="lblSD" runat="server" Text='<%#Eval("StartDate") %>'></asp:Label>
                        <br />
                        <%--End Date--%>
                        <asp:Label ID="Label3" runat="server" Text="Start Date: "></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("EndDate") %>'></asp:Label>
                        <br />
                        <%--Sick Time--%>
                        <asp:Label ID="Label5" runat="server" Text="Sick Time: "></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Sick Time") %>'></asp:Label>
                        <br />
                        <%--Approval Status--%>
                        <asp:Label ID="Label7" runat="server" Text="Approval Status: "></asp:Label>
                        <asp:TextBox ID="txtApproval" Text='<%#Eval("Approval Status") %>' runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegExp1" runat="server"
                            ErrorMessage="Please enter either 'a' for Approved or 'd' for Denied."
                            ControlToValidate="txtApproval"
                            ValidationExpression="^[a|d]" />
                        <br />
                        <%--Comments--%>
                        <asp:Label ID="Label9" runat="server" Text="Comments: "></asp:Label>
                        <asp:TextBox ID="txtComments" Text='<%#Eval("Comments") %>' runat="server"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Comment is required.  Character limit is 250."
                            ControlToValidate="txtComments"
                            ValidationExpression="/^[a-zA-Z0-9*]{0,250}$/" 
                            />--%>
                        <br />
                        <%--Update Button--%>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Request" CommandName="Update" />
                    </EditItemTemplate>





                </asp:FormView>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--End Panels--%>
    </ajaxToolkit:TabContainer>
    <%--</asp:UpdatePanel>--%>
</asp:Content>

