<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="EmployeeView.aspx.cs" Inherits="EmployeeView" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleContent" runat="server">
    Employee View
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>



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
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Time Off" ID="Tab2">
            <ContentTemplate>

                <asp:CheckBox ID="chkDays" runat="server" Text="One Day or More" />
                <asp:CheckBox ID="chkHalfDay" runat="server" Text="Half Day" />
                <br />
                <asp:Label ID="lblCheckNote" runat="server" Text="Please check an option" Visible="false"></asp:Label>
                <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceDays" runat="server" TargetControlID="chkDays" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="meceHalfDay" runat="server" TargetControlID="chkHalfDay" Key="Period"></ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                <asp:Button ID="btnTypeofTime" runat="server" Text="Submit" OnClick="btnTypeofTime_Click" />
                <br />
                <br />

                <%--Date Selection--%>
                <%--Full Day--%>
                <asp:Panel ID="pnlFull" runat="server" Visible="false">
                    <%--Start Date--%>
                    <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                     &nbsp&nbsp<asp:Button ID="btnFullSubmit" runat="server" Text="Submit" OnClick="btnFullSubmit_Click" />
                    <%--Calendar Extenders work by attaching them to a textbox using TargetControlID--%>
                    <ajaxToolkit:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" runat="server" PopupButtonID="CalBut"></ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" TargetControlID="txtStartDate" Mask="99/99/9999"></ajaxToolkit:MaskedEditExtender>
                    &nbsp&nbsp&nbsp
                    <%--End Date--%>
                    <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut2" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                    <ajaxToolkit:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" runat="server" PopupButtonID="CalBut2"></ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" MaskType="Date" TargetControlID="txtEndDate" Mask="99/99/9999"></ajaxToolkit:MaskedEditExtender>
                </asp:Panel>
                <%--End Full Day Panel--%>

                <%--Half Day--%>
                <asp:Panel ID="pnlHalf" runat="server" Visible="false">
                    <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                    <asp:TextBox ID="txtHalfDay" runat="server"></asp:TextBox><asp:ImageButton ID="CalBut3" runat="server" ImageUrl="Images/Calendar_schedule.png" />
                    &nbsp&nbsp<asp:Button ID="btnHalfSubmit" runat="server" Text="Submit Request" OnClick="btnHalfSubmit_Click" />
                    <%--Calendar Extenders work by attaching them to a textbox using TargetControlID--%>
                    <ajaxToolkit:CalendarExtender ID="ceHalfDay" TargetControlID="txtHalfDay" runat="server" PopupButtonID="CalBut3"></ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" MaskType="Date" TargetControlID="txtHalfDay" Mask="99/99/9999"></ajaxToolkit:MaskedEditExtender>
                </asp:Panel>
                <%--End Half Day Panel--%>


                <%--Type of Time Off--%>
                <br />
                <br />
                <asp:CheckBox ID="chkSick" runat="server" Text="Sick Time?" />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>



        <%--Tab Panel 3--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Status of Current Requests" ID="Tab3">
            <ContentTemplate>
                <p>
                    Items to be placed:
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

