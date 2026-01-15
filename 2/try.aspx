<%@ Page Language="C#" AutoEventWireup="true" CodeFile="try.aspx.cs" Inherits="_2_try" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <asp:TextBox runat="server" ID="txtManualDiscount" 
                                                                        Text="0.00" Visible="false" 
                                                                        onkeyup="movetoNext(this)">
                                                                    </asp:TextBox>
        </div>
    </form>
</body>
</html>
