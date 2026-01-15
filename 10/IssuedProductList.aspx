<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="IssuedProductList.aspx.cs" Inherits="_10.IssuedProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        var validNumber = new RegExp(/^\d*\.?\d*$/);


        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)

                return false;
            return true;
        }

    </script>
    <script>
        function fillTextbox() {
            var txtCompanyName = document.getElementById('<%= txtStaff.ClientID %>').value;
            document.getElementById('<%= txtStaff.ClientID %>').value = txtCompanyName.toString().trim();
        }
    </script>
    <script type="text/javascript">
        function getEmployeeList() {
            $(function () {
                $("[id$=txtStaff]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployeeForCode") %>',
                            data: "{ 'empId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfEmployeeId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15" runat="server" id="divProType">
                                        <label class="control-label">Enter Emp. ID/ Username/ Name &nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtStaff" CssClass="form-control-blue" runat="server" onchange="javascript:return fillTextbox();" placeholder="Enter Emp. ID/ Username/ Name"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Product Type&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="Consumable">Consumable</asp:ListItem>
                                                <asp:ListItem Value="NonConsumable">Non-Consumable</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 btn-a-devices-3-p6  mgbt-xs-15" style="padding-top: 26px;">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="button form-control-blue" OnClick="lnkView_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12" runat="server" id="divExport" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Div1" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-12  no-padding" style="padding:10px 15px !important" runat="server" id="pnlcontrols">
                                    <div class="col-lg-12  no-padding" runat="server">

                                        <div class=" table-responsive  table-responsive2  no-padding">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="gdv1" runat="server">
                                                        <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                            <tr>
                                                                <td style="padding:0 15px !important;">
                                                                    <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                        <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                        <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                        <div class="col-sm-12" runat="server" id="divlistshow">
                                                                            <div class="table-responsive2 table-responsive">
                                                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                                                    <tbody>
                                                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                <tr>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">#<a href="javascript:" id="ss" style="float:right;"><i onclick="showHideAll(this)" class="fa fa-sort-amount-asc"></i></a></th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Emp. Name</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Product</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Qty.</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Return Qty.</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Date of Issue</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                                                                    <th class="vd_bg-blue-np vd_white-np">Username</th>
                                                                                                </tr>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                                        <asp:Label ID="lblSR" runat="server" Text='<%# Container.ItemIndex %>' CssClass="hide"></asp:Label>
                                                                                                        <a href="javascript:" id="ss" onclick="showHide(this)" style="float:right;" class="historyIcon"><i class="fa fa-sort-amount-asc"></i></a>
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Name") %>'></asp:Label>&nbsp;
                                                                                                        (<asp:Label ID="Label1" runat="server" Text='<%# Bind("EmpCode") %>'></asp:Label>)
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblItemIds" runat="server" Visible="false" Text='<%# Bind("itemid") %>'></asp:Label>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblReturnQty" runat="server" Text='<%# Bind("ReturnQtys") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("IssueDates") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("RecordedDates") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr runat="server" id="tr" class="hide history">
                                                                                                    <td colspan="8" style="padding:5px 10px 5px 10px !important;">

                                                                                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                                                                            <tbody>
                                                                                                                <asp:Repeater ID="RepeaterHistory" runat="server">
                                                                                                                    <HeaderTemplate>
                                                                                                                        <tr>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">#</th>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">Product</th>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">Qty.</th>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">Date of Return</th>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                                                                                            <th class="vd_bg-blue-np vd_white-np">Username</th>
                                                                                                                        </tr>
                                                                                                                    </HeaderTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qtys") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ReturnDates") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("RecordedDates") %>'></asp:Label>
                                                                                                                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>

                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>




                </div>
            </div>
            <script>
                function showHide(tis)
                {
                    var id = $(tis).closest('td').find('span:eq(1)').html();
                    var sts = $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_tr_' + id).hasClass('hide');
                    if (sts) {
                        $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_tr_' + id).removeClass('hide');
                        $(tis).html("<i class='fa fa-sort-amount-desc'></i>");
                    }
                    else {
                        $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_tr_' + id).addClass('hide');
                        $(tis).html("<i class='fa fa-sort-amount-asc'></i>");
                    }
                }
                function showHideAll(tis) {
                    var sts = $(tis).hasClass('fa-sort-amount-asc');
                    if (sts) {
                        $('.history').removeClass('hide');
                        $(tis).removeClass("fa-sort-amount-asc");
                        $(tis).addClass("fa-sort-amount-desc");
                        $('.historyIcon').html("<i class='fa fa-sort-amount-desc'></i>");
                    }
                    else {
                        $('.history').addClass('hide');
                        $(tis).addClass("fa-sort-amount-asc");
                        $(tis).removeClass("fa-sort-amount-desc");
                        $('.historyIcon').html("<i class='fa fa-sort-amount-asc'></i>");
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

