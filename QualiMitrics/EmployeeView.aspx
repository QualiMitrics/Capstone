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
                This is Tab 1
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Time Off" ID="Tab2">
            <ContentTemplate>
                <b><asp:Label ID="Label1" runat="server" Text="This is a calendar extender (DatePicker) with a warning validator that pops out"></asp:Label></b>
                <br />
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
                This is Tab 3
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--End Panels--%>
    </ajaxToolkit:TabContainer>

</asp:Content>

