<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentDetail.aspx.cs" Inherits="admin_PaymentDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment Detail</title>
    <meta charset="utf-8" />

    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />

    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../img/ico/favicon.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../img/ico/favicon.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../img/ico/favicon.png">
    <link rel="apple-touch-icon-precomposed" href="../img/ico/favicon.png">
    <link rel="shortcut icon" href="../img/ico/favicon.png">


    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/theme-responsive.min.css" rel="stylesheet" />
    <link href="../css/theme.min.css" rel="stylesheet" />

</head>
<body>

    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 ">
                    <div class=" table-responsive  table-responsive2">
                        <asp:GridView ID="gvPaymentHistory" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="varPaymentDate" HeaderText="Date" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="PaymentNo" HeaderText="Ref No." HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Mode" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="BankName" HeaderText="Bank" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="BankBranchName" HeaderText="Bank Branch" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="AccountName" HeaderText="Account Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="DDChequeUTRNo" HeaderText="No." HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                <asp:BoundField DataField="varDDChequeUTRDate" HeaderText="Allotment Date" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/jquery.js") %>'></script>
    <!--[if lt IE 9]>
  <script type="text/javascript" src='<%= ResolveClientUrl("~/js/excanvas.js") %>'></script>      
<![endif]-->
    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/bootstrap.min.js") %>'></script>
</body>
</html>

