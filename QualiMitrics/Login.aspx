<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login to QualiMitrics TOMS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="loginQM" runat="server" OnAuthenticate="LoginAuth"></asp:Login>
    </div>
    </form>
</body>
</html>
