<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pdfToImage.aspx.cs" Inherits="pdfToImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="lit">
    </div>
        <iframe src="uploads/pdf/eAM_Manual.pdf"></iframe>
        <asp:Image runat="server" ID="img" />
    </form>
</body>
</html>
