<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="ManagerView.aspx.cs" Inherits="ManagerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" Runat="Server">
    Manager View
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--This is the top level for the tab containers--%>
    <ajaxToolkit:TabContainer ID="tcOne" runat="server">
        <%--Each tab is created by a tab panel--%>
        <%--Tab Panel 1--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Home" ID="Tab1">
            <%--Each tab panel is populated with Content Template--%> 
            <ContentTemplate>
                <p>This will eventually be a summary of what the manager will find here and how to use 
                   the request process tools.  
                </p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Department Statistics" ID="Tab2">
            <ContentTemplate>

                <p>To be: Chart of departmental time off, chart showing concentration of days on which time off is taken.</p>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>



        <%--Tab Panel 3--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Request Verification" ID="Tab3">
            <ContentTemplate>
             <p>Items:
                 <ol>
                <li>List of pending requests</li>
                <li>Formview listing details of request</li>
                <li>Checkbox of whether to approve or disprove</li>
                <li>Textbox for comments (not required)</li>
                     </ol></p>


            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--End Panels--%>
    </ajaxToolkit:TabContainer>

</asp:Content>

