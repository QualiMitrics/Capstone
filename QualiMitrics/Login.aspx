<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AdventureWorks Time Off / Management Application</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="loginQM" runat="server" OnAuthenticate="LoginAuth"></asp:Login>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Login to Management Suite?" />
    </div>
    </form>
</body>
</html>
