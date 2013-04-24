<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RecruiterView.aspx.cs" Inherits="RecruiterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
    Recruitment Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--This is the top level for the tab containers--%>
    <ajaxToolkit:TabContainer ID="tcOne" runat="server">
        <%--Each tab is created by a tab panel--%>
        <%--Tab Panel 1--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Home" ID="Tab1">
            <%--Each tab panel is populated with Content Template--%>
            <ContentTemplate>
                <p>
                    Home view for recruiters. 
                </p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <%--Tab Panel 2--%>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Department Statistics" ID="Tab2">
            <ContentTemplate>


                <div>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Select the requested Job Candidate from the dropdown below to review their resume"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Job Candidate ID"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server"
                        DataSourceID="sdsJobCanID"
                        DataValueField="JobCandidateID"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="loadResume">
                    </asp:DropDownList>
                    <br />
                    
        <asp:SqlDataSource ID="sdsJobCanID"
            ConnectionString="<%$ ConnectionStrings: AdventureWorks %>"
            SelectCommand="SELECT JobCandidateID FROM HumanResources.JobCandidate WHERE (BusinessEntityID IS NULL)"
            DeleteCommand="DELETE FROM HumanResources.JobCandidate WHERE JobCandidateID=@JCID"
            runat="server">
            <DeleteParameters>
                <asp:ControlParameter
                    Name="JCID"
                    Type="Int32"
                    ControlID="DropDownList1" />
            </DeleteParameters>

        </asp:SqlDataSource>

                    <br />
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Output" runat="server" Text=" "></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete Candidate" CommandName="Delete" OnClick="btnDelete_Click"  />

                </div>


            </ContentTemplate>
        </ajaxToolkit:TabPanel>


        <%--End Panels--%>
    </ajaxToolkit:TabContainer>

</asp:Content>

