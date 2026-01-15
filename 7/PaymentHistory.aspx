<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentHistory.aspx.cs" Inherits="_7.AdminPaymentHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment History</title>
    <meta charset="utf-8" />

    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />

    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../img/ico/favicon.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../img/ico/favicon.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../img/ico/favicon.png" />
    <link rel="apple-touch-icon-precomposed" href="../img/ico/favicon.png" />
    <link rel="shortcut icon" href="../img/ico/favicon.png" />

    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/theme-responsive.min.css" rel="stylesheet" />
    <link href="../css/theme.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function openDetail(url, name) {
            var width = 750;
            var height = 350;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status=no,scrollbars=yes,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;

            myWindow = window.open(url, '_blank', windowFeatures);
            //return false;
        }
    </script>
    <script>
        function msgboxnew(divmsgboxid, msg, msgsymbol) {
            var divmsgbox = document.getElementById(divmsgboxid);
            divmsgbox.InnerHtml = "";
            var background = "";
            var icon = "";
            switch (msgsymbol) {
            case "S":
                background = "vd_bg-green";
                icon = "fa-check";
                break;
            case "U":
                background = "vd_bg-green";
                icon = "fa-check";

                break;
            case "A":
                background = "vd_bg-yellow";
                icon = "fa-exclamation-triangle";
                break;
            case "W":
                background = "vd_bg-red";
                icon = "fa-times";
                break;
            default:
                divmsgbox.InnerHtml = "";
                break;
            }

            enable(divmsgboxid, background, icon, msg);
        }

        function enable(divmsgboxid, background, icon, msg) {
            var hide = document.getElementById(divmsgboxid); hide.className = 'msgbox ' + background + ' animated  fadeInLeft'; hide.innerHTML = '<i class=fa' + icon + 'aria-hidden=true></i> ' + msg;
            function disable() {
                var hide = document.getElementById(divmsgboxid); if (hide.innerHTML !== '') {
                    hide.className = 'msgbox msgbox-bx-n-z-n ' + background + 'animated fadeInRight-dn';
                    setTimeout(clear, 5000);
                }
            } function clear() {
                var hide = document.getElementById(divmsgboxid);
                hide.className = ''; hide.innerHTML = '';
            } function jscript() { setTimeout(disable, 10000); }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

                <div class="container">
                    <div class="row">
                        <div class="col-sm-12 ">

                            <div id="msgbox" runat="server" style="left: 147px !important;"></div>

                            <div class=" table-responsive  table-responsive2">
                                <asp:GridView ID="gvPaymentHistory" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" OnRowDataBound="gvPaymentHistory_RowDataBound" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="varDate" HeaderText="Payment Date" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="PayType" HeaderText="PayType" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="Description" HeaderText="Particulars" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="PaymentMode" HeaderText="Mode" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                        <asp:BoundField DataField="varCR" HeaderText="Amount" HeaderStyle-CssClass="vd_bg-blue vd_white" />

                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetail" runat="server" Text='<%# Eval("H04ID") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" title="Payment Detail" OnClick="lbtnDetail_Click" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-search"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                        </asp:TemplateField>
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
