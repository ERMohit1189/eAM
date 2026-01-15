<%@ Page Language="C#" AutoEventWireup="true" CodeFile="iciciGateway.aspx.cs" Inherits="iciciGateway" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Plain URL : <asp:Label runat="server" ID="Label1"></asp:Label><br /><br />
        Encrypted URL : <asp:Label runat="server" ID="txtString"></asp:Label>
        <asp:LinkButton runat="server" ID="dd">Click</asp:LinkButton>
    </div>
    </form>
</body>
</html>
