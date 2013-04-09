<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="EmployeeView.aspx.cs" Inherits="EmployeeView" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleContent" runat="server">
  Employee View
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--This is the top level for the tab containers--%>
    <ajaxToolkit:TabContainer ID="tcOne" runat="server">
        <%--Each tab is created by a tab panel--%>
        <%--Tab Panel 1--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Home" ID="Tab1">
            <%--Each tab panel is populated with Content Template--%> 
            <ContentTemplate>
                <p>Hello [Employee Name Placeholder], this is where you will request time off 
                in the form of either spans of days or specific portions of days.  You can 
                also check the status of your request(s) in the third tab.</p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Time Off" ID="Tab2">
            <ContentTemplate>
            <%--Checkboxes for Length Determination
                When one of these is checked, different controls should become visible
                IE when Half day is selected, only one datepicker comes up, and it should have a time control as well
                Or if we just select a day to do half off of, only a datepicker--%>
                <asp:CheckBox ID="chkDays" runat="server" Text="One Day or More" />
                <asp:CheckBox ID="chkHalfDay" runat="server" Text="Half Day" />
                <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceDays" runat="server" TargetControlID="chkDays" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceHalfDay" runat="server" TargetControlID="chkHalfDay" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                <br /><br />
                <%--Date Selection--%>
                <b><asp:Label ID="Label1" runat="server" Text="This is a calendar extender (DatePicker)"></asp:Label></b>
                <br />
                <%--Start Date--%>
                <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <%--Calendar Extenders work by attaching them to a textbox using TargetControlID--%>
                <ajaxToolkit:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                &nbsp&nbsp&nbsp
                <%--End Date--%>
                <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                <%--Type of Time Off--%>
                <br /><br />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>



        <%--Tab Panel 3--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Status of Current Requests" ID="Tab3">
            <ContentTemplate>
                <p>Items to be placed:
                <ol>
                <li>List of pending requests</li>
                <li>Formview with details, including bolded and color coded status (PENDING, APPROVED, DENIED)</li>
                </ol>
                </p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--End Panels--%>
    </ajaxToolkit:TabContainer>

</asp:Content>

