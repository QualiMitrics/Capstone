<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="EmployeeView.aspx.cs" Inherits="EmployeeView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleContent" runat="server">
    Employee View
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--Data Source for Details View--%>
    <asp:SqlDataSource
        ID="sdsStatus"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
        SelectCommand="SELECT        HumanResources.TimeOff.StartDate, HumanResources.TimeOff.EndDate, Person.Person.FirstName + Person.Person.LastName AS [Name], 
                       CASE WHEN HumanResources.TimeOff.Sick_Vacation = '0' THEN 'No' ELSE 'Yes' END AS [Sick Time], 
					   CASE WHEN HumanResources.TimeOff.Approval = 'p' THEN 'Pending' WHEN HumanResources.TimeOff.Approval = 'a' THEN 'Approved' ELSE 'Denied' END AS [Approval Status], 
					   CASE WHEN HumanResources.TimeOff.Comments is null THEN 'None' END AS [Comments]
                       FROM            HumanResources.TimeOff INNER JOIN
                       HumanResources.Employee AS Employee_1 ON HumanResources.TimeOff.BusinessEntityID = Employee_1.BusinessEntityID INNER JOIN
                       Person.Person ON Employee_1.BusinessEntityID = Person.Person.BusinessEntityID
                       WHERE		(HumanResources.TimeOff.BusinessEntityID = @BEID)">
        <SelectParameters>
            <%--This is where you define parameters based on session variables--%>
            <asp:SessionParameter Name="BEID" SessionField="BEIDINT" Type="Int32" />
        </SelectParameters>

    </asp:SqlDataSource>
    <%--End SQL Data Source--%>


    <%--Update Panel is so that any time something causes postback (the conditional part) it causes the tabs to refresh and not the page--%>
    <%----%>
    <%--This is the top level for the tab containers--%>
    <ajaxToolkit:TabContainer ID="tcOne" runat="server">
        <%--Each tab is created by a tab panel--%>
        <%--Tab Panel 1--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Home" ID="Tab1">
            <%--Each tab panel is populated with Content Template--%>
            <ContentTemplate>
                <p>
                    Hello <%Response.Write(Session["name"]);%>, this is where you will request time off 
                in the form of either spans of days or specific portions of days.  You can 
                also check the status of your request(s) in the third tab.
                </p>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Time Off" ID="Tab2">
            <ContentTemplate>
                <%--This panel is here so that the controls can be cleared after a good submit--%>
                <asp:Panel ID="pnlSelections" runat="server">
                    <%--Begin Selection Controls--%>
                    <%--Checkboxes--%>
                    <asp:CheckBox ID="chkDays" runat="server" Text="One Day or More" />
                    <asp:CheckBox ID="chkHalfDay" runat="server" Text="Half Day" />
                    <br />
                    <%--Checkbox Extenders - These make it so that only one checkbox can be selected at a time--%>
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceDays" runat="server" TargetControlID="chkDays" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceHalfDay" runat="server" TargetControlID="chkHalfDay" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                    <%--Submit Button and Error Label--%>
                    <asp:Button ID="btnTypeofTime" runat="server" Text="Submit" OnClick="btnTypeofTime_Click" />
                    <asp:Label ID="lblCheckNote" runat="server" Text="Please check an option" Visible="false"></asp:Label>
                    <%--Label becomes visible if no checkbox selected--%>
                    <br />
                    <br />

                    <%--Date Selection--%>

                    <%--Full Day--%>
                    <asp:Panel ID="pnlFull" runat="server" Visible="false">
                        <%--Start Date--%>
                        <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                        <%--Calendar Extenders work by attaching them to a textbox using TargetControlID--%>
                        <ajaxToolkit:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" runat="server" PopupButtonID="CalBut" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>


                        &nbsp&nbsp&nbsp
                        <%--End Date--%>
                        <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut2" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                        <ajaxToolkit:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" runat="server" PopupButtonID="CalBut2" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>

                        &nbsp&nbsp<asp:Button ID="btnFullSubmit" runat="server" Text="Submit" OnClick="btnFullSubmit_Click" />
                        <%--Range and Comparison Validators--%>
                        <asp:RangeValidator ID="rvStartDate" runat="server" ErrorMessage="Please enter a valid date, yyyy/MM/dd format" ControlToValidate="txtStartDate"></asp:RangeValidator>
                        <asp:RangeValidator ID="rvEndDate" runat="server" ErrorMessage="Please enter a valid date, yyyy/MM/dd" ControlToValidate="txtEndDate"></asp:RangeValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Start date must be before end date" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThan"></asp:CompareValidator>
                    </asp:Panel>
                    <%--End Full Day Panel--%>

                    <%--Half Day--%>
                    <asp:Panel ID="pnlHalf" runat="server" Visible="false">
                        <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txtHalfDay" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut3" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                        &nbsp&nbsp<asp:Button ID="btnHalfSubmit" runat="server" Text="Submit Request" OnClick="btnHalfSubmit_Click" />
                        <%--Calendar Extenders work by attaching them to a textbox using TargetControlID--%>
                        <ajaxToolkit:CalendarExtender ID="ceHalfDay" TargetControlID="txtHalfDay" runat="server" PopupButtonID="CalBut3" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>

                        <%--Range Validator--%>
                        <asp:RangeValidator ID="rvHalfDay" runat="server" ControlToValidate="txtHalfDay" ErrorMessage="Please enter a valid date"></asp:RangeValidator>
                    </asp:Panel>
                    <%--End Half Day Panel--%>
                </asp:Panel>

                <%--Type of Time Off--%>
                <br />
                <br />
                <asp:CheckBox ID="chkSick" runat="server" Text="Sick Time?" />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>



        <%--Tab Panel 3--%>

        <ajaxToolkit:TabPanel runat="server" HeaderText="Status of Current Requests" ID="Tab3">
            <%--Update panels are literally hitler http://forums.asp.net/t/1553854.aspx --%>
            
                <ContentTemplate>



                    <asp:DetailsView
                        ID="DetailsView1"
                        runat="server"
                        DataSourceID="sdsStatus"
                        Height="330px"
                        Width="500px"
                        AllowPaging="true">
                    </asp:DetailsView>


                </ContentTemplate>
          
        </ajaxToolkit:TabPanel>

        <%--End Panels--%>
    </ajaxToolkit:TabContainer>

</asp:Content>

