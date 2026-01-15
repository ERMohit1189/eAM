<%@ Page Title="Composite Fee | eAM&#174;" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="CompositFeeDeposit.aspx.cs" Inherits="CompositFeeDeposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="https://js.paystack.co/v1/inline.js"></script>
    <script src="CompositFeeDepositJS.js"></script>
    <script type="text/javascript">
        function getStudentsList() {
           $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
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
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <style>
        .pro-table1 > tbody > tr > th:nth-child(n) {
            background-color: #ddd;
            color: #000000;
        }

        .pro-table1 > tbody > tr > td:nth-child(n) {
            background-color: #eee;
            color: #000000;
        }

        input[type=radio], input[type=checkbox] {
            width: 17px;
            height: 17px;
            margin: 0px 0px;
            line-height: normal;
            vertical-align: text-bottom;
        }

        .rowYellow {
            background: #ff8c00 !important;
            color: #000 !important;
            height: 30px !important;
        }

        .rowGreen {
            background: #4e9057 !important;
            color: #eaeaea !important;
            height: 30px !important;
        }

        .rowGreenchild {
            background: #7fa284 !important;
            color: #eaeaea !important;
            height: 30px !important;
        }


        .box {
            max-width: 240px;
            display: block;
            position: absolute;
            background: #808080;
            color: white;
            /*padding: 5px;*/
            /*border: 1px solid black;*/
            text-align: left;
            z-index: 99999;
            border-radius: 10px;
            vertical-align: top !important;
        }

            .box .col-sm-6 {
                padding-left: 5px !important;
                padding-right: 5px !important;
                padding-bottom: 5px !important;
                padding-top: 5px !important;
            }

        input[readonly], textarea[readonly] {
            cursor: default;
            background: #e6e6e6 !important;
        }

        @media print {
            .no-print, .no-print * {
                display: none !important;
            }
        }
        .btns {
            background-color:#fff;
            border: 1px solid #000;
        }
        .btns :hover {
            color:#FF0000;
            cursor:pointer;
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                    Sys.Application.add_load(datetime);
                    
                    Sys.Application.add_load(prettyphoto);
                    Sys.Application.add_load(disablebtn);
                }
                catch (ex) {

                }
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12" id="divstd" runat="server">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body" id="printdiv">
                                <div class="col-sm-12  no-padding hidden-print" runat="server" id="studentdivnotshow">
                                    <div class="col-sm-4  mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <div class="col-sm-4 mgbt-xs-15" id="divEnter2" runat="server" visible="true">
                                        <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_TextChanged" onblur="onchangetxt();" onpaste="onchangeatcopyandpaste()" ValidationGroup="abc" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <script>
                                        function onchangetxt() {
                                            if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {
                                                document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                            }
                                        }

                                        function onchangeatcopyandpaste() {
                                            document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                        }
                                    </script>
                                    <div class="col-sm-8 text-right mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click"
                                            CssClass="button form-control-blue pull-left" OnClientClick="return ValidateTextBox('.validatetxt');"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        <span runat="server" id="spnPrint" class=" pull-right">
                                        <asp:LinkButton ID="reInitiate" runat="server" OnClick="reInitiate_Click" Visible="false"
                                            CssClass="button form-control-blue"><i class="fa fa-refresh"></i> Reinitiate Transaction</asp:LinkButton>
                                            <span id="btnPrint" runat="server"  visible="false" class="btn-print-box" onclick="printDiv();"><a>&nbsp;&nbsp;&nbsp;<i class="icon-printer"></i></a></span>
                                        </span>
                                        <div id="msgs" runat="server" style="color: #FF0000"></div>
                                        <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6  no-padding hidden-print">
                                    <div id="msgbox" runat="server" style="left: 60px;"></div>
                                </div>
                                <div id="divStudent" class="col-sm-12" runat="server" visible="false">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="grdStRecord" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblClassid" CssClass="hide" runat="server" Text='<%# Bind("classid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("CardId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDOA" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle />
                                                    </asp:GridView>

                                                </td>
                                                <td class="tab-top tab-profile text-center onprint">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                    </div>
                                                </td>

                                            </tr>
                                        </table>
                                        <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div id="divTools" class="col-sm-12 no-padding  onprint" runat="server" visible="false">
                                    <div class="col-sm-4  mgbt-xs-15" id="divdate">
                                        <label class="control-label">Date of Receipt</label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtDepositDate" CssClass="datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15" id="divMod">
                                        <label class="control-label">Mode</label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownMOD" runat="server" TabIndex="1" CssClass="form-control-blue" onchange="MODChenge();">
                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Online Transfer</asp:ListItem>
                                                <%--<asp:ListItem>Other</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  mgbt-xs-15 hide" id="divStatus">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Status"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server">
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15" id="div7" runat="server" visible="false">
                                        <label class="control-label">Yearly Paid</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYearlyPaid" runat="server" TabIndex="1" CssClass="form-control-blue">
                                                <asp:ListItem Value="A">Yes</asp:ListItem>
                                                <asp:ListItem Value="I">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding hide  hidden-print" id="divOtherTools">
                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:Label ID="lblChqDate" runat="server" class="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtChequeDate" CssClass="datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:Label ID="lblChqNo" runat="server" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtChequeNo" onblur="updateText();showBtn();" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:Label ID="lblBankName" CssClass="control-label" runat="server" Text="Bank Name"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtBank" runat="server" CssClass="form-control-blue" Text="NA"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                
                                <div class="col-sm-12" runat="server" id="divTutionFee" visible="false">
                                    <fieldset style="padding: 5px !important;">
                                        <legend style="width: 100%"><i class="fa fa-book"></i>&nbsp;Fee Details
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                                                <asp:CheckBox runat="server" ID="chkCompleteFee" Text="&nbsp;Complete Fee Payment" AutoPostBack="true" OnCheckedChanged="chkCompleteFee_CheckedChanged" Style="float: right; font-size: 13px; margin: 0; padding: 0; height: 16px; color: #ff8c00;" />
            </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </legend>
                                        <div class="table table-responsive">
                                            <table class="table no-bm no-head-border table-bordered pro-table1 table-header-group" id="mainTable">
                                                <asp:Repeater ID="rptFeeStructure" runat="server">
                                                    <HeaderTemplate>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 3%;">#</th>
                                                                <th class="text-left " style="width: 22%;">Installment</th>
                                                                <th class="text-right" style="width: 9%">Fee</th>
                                                                <th class="text-right" style="width: 9%">Discount</th>
                                                                <th class="text-right" style="width: 9%">Total</th>
                                                                <th class="text-right" style="width: 9%">Paid/ Payable</th>
                                                                <th class="text-right" style="width: 9%">Balance/ Due</th>
                                                                <th class="text-right" style="width: 9%">Consolidated</th>
                                                                <th class="text-left hidden-print" style="width: 24%">Narration</th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id='instalBtn_<%# Container.ItemIndex+1 %>' style="height: 30px !important; line-height: 1;" class="mainRow">
                                                            <td class="text-center" style="height: 30px !important; width: 36px; line-height: 1;">
                                                                <span id="lblInstallmentSrNo" class="vl"><%# Container.ItemIndex+1 %></span>
                                                                <asp:Label runat="server" ID="lblDueDate" Text='<%# Eval("DueDate") %>' CssClass="hide"></asp:Label>
                                                                <asp:Label runat="server" ID="lblStsMain" CssClass="hide"></asp:Label>
                                                            </td>
                                                            <td class="text-left" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblIcon" title="History" onclick="openClode(this)" CssClass="chks_h btns menu-icon vd_bd-grey-n vd_black-n iconhistory" Style="top: 0 !important; padding: 1px 6px"><i class="togalIcon fa fa-sort-amount-desc"></i></asp:Label>
                                                                <asp:CheckBox runat="server" ID="chkInstallment" CssClass="vd_check check-success" Style="height: 17px;" onchange="MainHeadCheck(this);" />
                                                                <asp:Label runat="server" ID="lblInstallmentName" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentAmount" CssClass="calfee vl" Style="cursor: pointer !important" onmouseover="showfee(this)" onmouseout="hidefee(this)" Text='<%# Eval("headAmount") %>'></asp:Label>&nbsp;<asp:Label runat="server" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" ID="HeadAmon" class="fa fa-info-circle"></asp:Label>
                                                                <div class="box hide row">
                                                                    <asp:Repeater ID="RepeaterHeadAmon" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                <asp:Label runat="server" ID="lblhNameh" Text='<%# Eval("Feehead") %>'> </asp:Label>
                                                                            </div>
                                                                            <div class="text-right col-sm-6">
                                                                                :&nbsp;&nbsp;<asp:Label runat="server" ID="lblhAmth" Text='<%# Bind("headAmount") %>' Style="text-align: right;"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                            <td class="text-right" style="cursor: pointer; line-height: 1; padding-top: 4px !important; padding-bottom: 4px !important;">
                                                                <asp:Label runat="server" ID="lblInstallmentDiscountPaid" CssClass="calDisc vl hide" Text='<%# Eval("PaidDiscount") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblInstallmentDiscount" CssClass="calDisc vl" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>&nbsp;<asp:Label runat="server" ID="HeadDiscount" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" class="fa fa-info-circle"></asp:Label><br />
                                                                <asp:TextBox runat="server" ID="txtInstallmentdiscount" class="form-control-blue" placeholder="0.00" Style="text-align: right !important; height: 20px;" onblur="MainHeadDiscount(this);"></asp:TextBox>
                                                                <div class="box hide row">
                                                                    <asp:Repeater ID="RepeaterHeadDiscount" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                <asp:Label runat="server" ID="lblDiscNameh"
                                                                                    Text='<%# Eval("DiscountName") %>'>
                                                                                </asp:Label>
                                                                            </div>
                                                                            <div class="text-right col-sm-6">
                                                                                :&nbsp;&nbsp;<asp:Label runat="server" ID="lblDiscAmth" Text='<%# Bind("DiscountAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblPaidDiscAmth" CssClass="hide" Text='<%# Bind("PaidDiscAmount") %>' Style="text-align: right;"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentTotal" class="vl" Text='<%# Eval("PaybleAmount") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lbInstallmentlPaid" class="vl" Text='<%# Eval("PaidAmount") %>'></asp:Label>
                                                                <asp:TextBox runat="server" ID="txtInstallmentPayable" class="form-control-blue" placeholder="0.00" Text='<%# Eval("DueAmount").ToString()=="0.00"?"":Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;" onblur="MainHeadPayable(this);"></asp:TextBox>
                                                            </td>
                                                            <td class="text-right lasttd" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentDue" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblhideInstallmentDue" class="hide" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right lasttd" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblrecuring" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                            </td>
                                                            <td style="line-height: 1;" class="hidden-print">
                                                                <asp:TextBox runat="server" ID="txtInstallmentNarration" class="form-control-blue" TextMode="MultiLine" Style="height: 29px; color: #000;" onblur="Naretion(this);"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide" id='instalDeails_<%# Container.ItemIndex+1 %>'>
                                                            <td colspan="9" style="padding: 10px !important; box-shadow: 0px 16px 233px #a0a0a0  inset;">
                                                                <table class="table no-bm no-head-border table-bordered pro-table table-header-group" id='headTable_<%# Container.ItemIndex+1 %>' border="1" style="border-collapse: collapse; margin-top: 2px !important;">
                                                                    <asp:Repeater ID="rptFee" runat="server">
                                                                        <HeaderTemplate>
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="text-align: center !important; vertical-align: middle !important; width: 4%; padding: 2px 0px !important;" scope="col">#</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 21%;" scope="col">Fee Head</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Amount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Discount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Total</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Paid/ Payable</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Balance/ Due</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 30%;" scope="col" class="hidden-print">Narration</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr runat="server" id="feeheadrow" class="childRow">
                                                                                <td style="text-align: center !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <span id="lblFeeheadSrNo"><%# Container.ItemIndex+1 %></span>
                                                                                    <asp:CheckBox runat="server" ID="chkInstallmentFee" CssClass="vd_check check-success chks_c" Style="height: 17px;" onchange="ChildCheck(this);" />
                                                                                    <asp:Label runat="server" ID="lblisFineFeeApply" CssClass="hide" Text='<%# Eval("isFineFee") %>'></asp:Label>

                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" CssClass="hide" ID="lblInstallmentId" Text='<%# Eval("MonthId") %>'></asp:Label>
                                                                                    <asp:Label runat="server" CssClass="hide" ID="lblFeeheadId" Text='<%# Eval("Feeheadid") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeehead" Text='<%# Eval("Feehead") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblStsChild" CssClass="hide"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeheadAmount" Text='<%# Eval("headAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscount" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>
                                                                                    &nbsp;<asp:Label runat="server" ID="ChildDiscount" class="fa fa-info-circle" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscountPaid" class="hide" Text='<%# Eval("PaidDiscount") %>'></asp:Label>
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadDiscount" class="form-control-blue" placeholder="0.00" onblur="ChildDiscount(this);" Style="cursor: pointer !important text-align: right !important; height: 20px;"></asp:TextBox>
                                                                                    <div class="box hide row">
                                                                                        <asp:Repeater ID="RepeaterChildDiscount" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                                    <asp:Label runat="server" ID="lblDiscNameC"
                                                                                                        Text='<%# Eval("DiscountName") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                                <div class="text-right col-sm-6">
                                                                                                    :&nbsp;&nbsp;<asp:Label runat="server" ID="lblDiscAmtC" Text='<%# Bind("DiscountAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                                    <asp:Label runat="server" ID="lblPaidDiscAmtC" CssClass="hide" Text='<%# Bind("PaidDiscAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </div>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadTotal" Text='<%# Eval("PaybleAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadPaid" Text='<%# Eval("PaidAmount") %>'></asp:Label><br>
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadPayable" class="form-control-blue" placeholder="0.00" onblur="ChildPayable(this);" Text='<%# Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;"></asp:TextBox>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadBalanceShow" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeeHeadBalance" class="hide vl" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important; line-height: 1;" colspan="2" class="hidden-print">
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadNarration" class="form-control-blue" TextMode="MultiLine" Style="height: 29px; color: #000;"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                   
                                                                </table>
                                                                <table class="table table-hover no-bm no-head-border table-bordered pro-table table-header-group" border="1" style="border-collapse: collapse; margin-top: 20px !important;">

                                                                    <asp:Repeater ID="rptHistory" runat="server">
                                                                        <HeaderTemplate>
                                                                            <thead>

                                                                                <tr>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 30px;" scope="col">#</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Date</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Receipt No.</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Installment</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Mode</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Status</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important;" scope="col">Paid Amount</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <span id="lblHstorySrNo"><%# Container.ItemIndex+1 %></span>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstoryDepositDate" Text='<%# Eval("DepositDate") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="Label2" Text='<%# Bind("ReceiptNo") %>' CssClass="onprint2 hide"></asp:Label>
                                                                                    <span class="hidden-print">
                                                                                        <asp:HyperLink ID="HylnkReceiptNo" runat="server" Visible="false" CssClass="onprint" Style="color: #0019cc;" Text='<%# Bind("ReceiptNo") %>'
                                                                                            NavigateUrl='<%# "FeeReceiptAllDuplicate.aspx?RecieptSrNo="+Eval("ReceiptNo").ToString().Replace("/","__")+"$"+Eval("SessionName").ToString()+"$"+Eval("BranchCode").ToString() %>' Target="_blank">
                                                                                        </asp:HyperLink>
                                                                                        <asp:HyperLink ID="HylnkReceiptNoT2" runat="server"  Visible="false" CssClass="onprint" Style="color: #0019cc;" Text='<%# Bind("ReceiptNo") %>'
                                                                                            NavigateUrl='<%# "FeeReceiptAllT2.aspx?RecieptSrNo="+Eval("ReceiptNo").ToString().Replace("/","__")+"$"+Eval("SessionName").ToString()+"$"+Eval("BranchCode").ToString() %>' Target="_blank">
                                                                                        </asp:HyperLink>
                                                                                    </span>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lbllHstoryInstallment" Text='<%# Eval("installmentName") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstoryMode" Text='<%# Eval("ModeOfPayment") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstorySatus" Text='<%# Eval("receiptStatus") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstorPaid" Text='<%# Eval("PaidAmount") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:Repeater>
                                                 <tr>
                                                                <th class="text-right " style="width: 25%;" colspan="2">Total</th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblInstallmentFee_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblDisvount_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblTotal_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblPaid_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblBalance_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 9%"><asp:Label runat="server" ID="lblConsolidated_total"></asp:Label></th>
                                                                <th class="text-left hidden-print" style="width: 24%"></th>
                                                            </tr>
                                            </table>
                                            <asp:Label runat="server" ID="hdnDiscountText" CssClass="hdnDiscountText hide"></asp:Label>
                                            <asp:Label runat="server" ID="hdnRemark" CssClass="hdnRemark hide"></asp:Label>
                                            <asp:Label runat="server" ID="hdnPaidText" CssClass="hdnPaidText hide"></asp:Label>
                                        </div>
                                    </fieldset>

                                    <div class="col-sm-12 hidden-print">
                                        <div><i class="fa fa-credit-card"></i>&nbsp;Payment Details</div>
                                        <asp:HiddenField runat="server" ID="hdnIspromot" />
                                        <div class="col-sm-3" style="border: 1px solid #ccc" id="divFeeDetails">
                                            <div style="padding: 5px !important;">
                                                <div style="width: 100%; font-size: 12px !important; margin-top: 6px; padding: 0 0;">
                                                    <div class="col-sm-12  text-left" style="background: #fafafa; font-weight: 700; color: #dc4448; padding: 5px;">
                                                        <label>Total Payable </label>
                                                        <asp:TextBox ID="txtTotalPaid" runat="server" CssClass="validateTextMode" Style="float: right; color: #000; width: 100px; text-align: right;" onblur="TotalPayable(this)"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnTotalPaid" />
                                                    </div>
                                                    <div class="col-sm-12  mgbt-xs-15 text-right" id="divSubmit" style="padding: 5px;">
                                                        
                                                        <input onclick="myFunction();" type="button" class="button form-control-blue dbclick" 
                                                            style="margin-top: 7px;width: 100px;" value="Submit"
                                                            />
                                                        <asp:Button ID="lnkSubmit" runat="server"
                                                            OnClientClick="ValidateTextBox('.validateTextMode'); 
                                                            ValidateDropdown('.validatedrpMode'); return validationReturn(); 
                                                            RemoveHideFromPayment(1);" CssClass="button form-control-blue" 
                                                            OnClick="lnkSubmit_Click" ondbclick="alertbox()" 
                                                            style="margin-top: 7px;width: 100px;display:none" Text="Submit" />
                                                    </div>
                                                </div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
            
            <script>
                function myFunction() {

                    $('.dbclick').attr('disabled', 'true');
                    const element = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_lnkSubmit");
                    element.click();
                }
                function printDiv() {
                    $(".iconhistory").each(function () {
                        openClode(this);
                    });
                    $(".onprint").addClass('hide');
                    $(".onprint2").removeClass('hide');
                    var headContent = document.getElementsByTagName('head')[0].innerHTML;
                    var divContents = document.getElementById("printdiv").innerHTML;
                    var printWindow = window.open('', '', 'height=700,width=1300, class="tbls"');
                    printWindow.document.write('<html><head><title"CompositFee"</title>' + headContent + '</head>');
                    printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
                    printWindow.document.close();
                    setTimeout(function () {
                        printWindow.print();
                        printWindow.close();
                        $(".iconhistory").each(function () {
                            openClode(this);
                        });
                        $(".onprint").removeClass('hide');
                        $(".onprint2").addClass('hide');
                    }, 1500);
                    return false;
                }
                function ChangeRowColor() {
                    $(".mainRow").each(function () {
                        var due1 = $(this).find('td:eq(6) span:eq(1)').html();
                        var Pending = $(this).find('td:eq(0) span:eq(2)').html();
                        var balance1 = parseFloat(due1 == "" ? "0" : due1);
                        if (balance1 == 0 && Pending != "Pending") {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreen');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(8) textarea').addClass('hide');
                        }
                        else if (balance1 == 0 || balance1 != 0 && Pending == "Pending") {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowYellow');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(8) textarea').addClass('hide');
                        }
                        else {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').removeClass('rowGreenChild');
                            }
                        }

                    });
                    $(".childRow").each(function () {
                        var due = $(this).find('td:eq(6) span:eq(1)').html();
                        var Pending = $(this).find('td:eq(1) span:eq(3)').html();
                        var balance = parseFloat(due == "" ? "0" : due);
                        if (balance == 0 && Pending != "Pending") {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreenchild');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(7) textarea').addClass('hide');
                        }
                        else if (balance == 0 || balance != 0 && Pending == "Pending") {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowYellow');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(7) textarea').addClass('hide');
                        }
                        else {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').removeClass('rowGreenchild');
                            }
                        }
                    });
                }
                function totalsAmt() {
                    var totalamt = 0;
                    var len = $(".chks_h").length;
                    for (var i = 0; i < len; i++) {
                        var sts = $("#instalBtn_" + (i + 1)).find('td:eq(1) input[type=checkbox]').prop("checked");
                        if (sts) {
                            var val = $("#instalBtn_" + (i + 1)).find('td:eq(5) input[type=text]').val();
                            totalamt += parseFloat(val == "" ? "0" : val);
                        }
                    }
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val(totalamt.toFixed(2));
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(totalamt.toFixed(2));
                }
                function Naretion(tis) {
                    var childRowid = "instalDeails_" + $(tis).closest('tr').attr('id').split("_")[1];
                    var len = $("#" + childRowid).find('td table tbody tr').length;
                    for (var i = 0; i < len; i++) {
                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').val($(tis).val());Session["srNo"].ToString()
                    }
                }
                function MainHeadCheck(tis) {

                    var headRowid = $(tis).closest('tr').attr('id');
                    var ischecked = $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked");
                    if (ischecked) {
                        $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                        $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                        $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
                    }
                    else {
                        $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                        $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
                    }
                    if ($(".hdnDiscountText").html() == "0") {
                        $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
                    }
                    if ($(".hdnPaidText").html() == "0") {
                        $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                    }

                    $("#" + headRowid).find('td:eq(3) input[type=text]').val('');
                    $("#" + headRowid).find('td:eq(5) input[type=text]').val('');

                    var lblInstallmentAmt_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                    var total_h = (lblInstallmentAmt_h - lblDiscount_h).toFixed(2);
                    $("#" + headRowid).find('td:eq(4) span:eq(0)').html(total_h);
                    var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                    var payable = (total_h - lblpaid_h).toFixed(2);

                    if (ischecked) {
                        $("#" + headRowid).find('td:eq(6) span:eq(0)').html("0.00");
                        $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable);
                    }
                    else {
                        $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable);
                        $("#" + headRowid).find('td:eq(5) input[type=text]').val("0.00");
                    }


                    var rowid = $(tis).closest('tr').attr('id');
                    var totalrec = 0; var idmached = 0;
                    $(".mainRow").each(function () {
                        var ischeckedThis = $(this).find('td:eq(1) input[type=checkbox]').prop("checked");
                        if (ischeckedThis) {
                        }
                        var childRowid = "instalDeails_" + $(this).attr('id').split("_")[1];
                        var len = $("#" + childRowid).find('td table tbody tr').length;
                        if (rowid == $(this).attr('id')) {
                            idmached = 1;
                        }
                        var ischecked2 = false;
                        if (idmached > 0) {
                            if (ischecked && rowid == $(this).attr('id')) {
                                $(this).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                                $(this).find('td:eq(3) input[type=text]').removeAttr('readonly');
                                $(this).find('td:eq(5) input[type=text]').removeAttr('readonly');
                                $(this).find('td:eq(8) textarea').removeAttr('readonly');
                                var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                                var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                                $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                                var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                                var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                                $(this).find('td:eq(5) input[type=text]').val((totals - paid).toFixed(2));
                                $(this).find('td:eq(6) span:eq(0)').html("0.00");
                                ischecked2 = true;
                            }
                            else {
                                $(this).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                                $(this).find('td:eq(3) input[type=text]').val('');
                                $(this).find('td:eq(5) input[type=text]').val('');
                                $(this).find('td:eq(8) textarea').val('');
                                $(this).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                $(this).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                $(this).find('td:eq(8) textarea').attr('readonly', 'readonly');
                                var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                                var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                                $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                                var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                                var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                                $(this).find('td:eq(5) input[type=text]').val('');
                                $(this).find('td:eq(6) span:eq(0)').html((totals - paid).toFixed(2));
                                ischecked2 = false;
                            }

                            if ($(".hdnDiscountText").html() == "0") {
                                $(this).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                $(this).find('td:eq(8) textarea').attr('readonly', 'readonly');
                            }
                            if ($(".hdnPaidText").html() == "0") {
                                $(this).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            }
                        }
                        else {

                            $(this).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                            $(this).find('td:eq(3) input[type=text]').removeAttr('readonly');
                            $(this).find('td:eq(3) input[type=text]').val('');
                            $(this).find('td:eq(5) input[type=text]').removeAttr('readonly');
                            $(this).find('td:eq(8) textarea').removeAttr('readonly');
                            var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                            var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                            $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                            var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                            var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                            $(this).find('td:eq(5) input[type=text]').val((totals - paid).toFixed(2));
                            $(this).find('td:eq(6) span:eq(0)').html("0.00");
                            ischecked2 = true;
                            if ($(".hdnDiscountText").html() == "0") {
                                $(this).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                $(this).find('td:eq(8) textarea').attr('readonly', 'readonly');
                            }
                            if ($(".hdnPaidText").html() == "0") {
                                $(this).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            }
                        }
                        if (idmached >= 0) {
                            for (var i = 0; i < len; i++) {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                var dues = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(1)').html());
                                var discount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                var paiddiscount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(2)').html());
                                var ApplyFine = $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0) span:eq(2)').html();
                                if (isNaN(discount)) {
                                    discount = 0;
                                }
                                if (isNaN(paiddiscount)) {
                                    paiddiscount = 0;
                                }
                                
                                if (dues == 0 && discount != paiddiscount) {
                                    if (ischecked2) {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", true);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').removeAttr('readonly');

                                        var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                        var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                        var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                        var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val((totalsh - paidh).toFixed(2));
                                        if (ApplyFine == "ApplyFine") {
                                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                        }
                                    }
                                    else {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').attr('readonly', 'readonly');

                                        var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                        var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                        var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                        var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalsh - paidh).toFixed(2));
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                    }
                                }
                                else if (dues > 0) {
                                    if (ischecked2) {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", true);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').removeAttr('readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').removeAttr('readonly');

                                        var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                        var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                        var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                        var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val((totalsh - paidh).toFixed(2));
                                        if (ApplyFine == "ApplyFine") {
                                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                        }
                                    }
                                    else {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').attr('readonly', 'readonly');
                                        if (ApplyFine == "ApplyFine") {
                                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                        }
                                        var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                        var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                        var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                        var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalsh - paidh).toFixed(2));
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                    }
                                }
                                else {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq() input[type=checkbox]').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').val('');
                                }
                                if (ApplyFine == "ApplyFine") {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                                }
                                if ($(".hdnDiscountText").html() == "0") {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').attr('readonly', 'readonly');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').attr('readonly', 'readonly');
                                }
                                if ($(".hdnPaidText").html() == "0") {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                }
                            }
                        }
                    });
                    //if (ischecked) {
                    //    $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                    //}
                    //else {
                    //    $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                    //}

                    totalsAmt();
                }
                function ChildCheck(tis) {

                    var childRowid = $(tis).closest('tr').attr('id');
                    $("#" + childRowid).find('td:eq(3) input[type=text]').val('');
                    $("#" + childRowid).find('td:eq(5) input[type=text]').val('');
                    var ischk = $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked");
                    if (ischk) {
                        $("#" + childRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                        $("#" + childRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                        $("#" + childRowid).find('td:eq(7) textarea').removeAttr('readonly');
                    }
                    else {
                        $("#" + childRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + childRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                        $("#" + childRowid).find('td:eq(7) textarea').attr('readonly', 'readonly');
                    }
                    if ($(".hdnDiscountText").html() == "0") {
                        $("#" + childRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + childRowid).find('td:eq(7) textarea').attr('readonly', 'readonly');
                    }
                    if ($(".hdnPaidText").html() == "0") {
                        $("#" + childRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                    }
                    var lblHeadAmt = parseFloat($("#" + childRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount = parseFloat($("#" + childRowid).find('td:eq(3) span:eq(0)').html());
                    var total = (lblHeadAmt - lblDiscount).toFixed(2);
                    $("#" + childRowid).find('td:eq(3) input[type=text]').val('');
                    if (parseFloat($(tis).val()) > total) {
                        $(tis).val('');
                    }
                    var txtDiscount = 0;
                    var lbltotal = (total - txtDiscount).toFixed(2);
                    $("#" + childRowid).find('td:eq(4) span:eq(0)').html(lbltotal);
                    var lblpaid = parseFloat($("#" + childRowid).find('td:eq(5) span:eq(0)').html());
                    var lbltotaldue = (lbltotal - lblpaid).toFixed(2);
                    if (ischk) {
                        $("#" + childRowid).find('td:eq(5) input[type=text]').val(lbltotaldue);
                        $("#" + childRowid).find('td:eq(6) span:eq(0)').html("0.00");
                    }
                    else {
                        $("#" + childRowid).find('td:eq(6) span:eq(0)').html(lbltotaldue);
                        $("#" + childRowid).find('td:eq(5) input[type=text]').val('');
                    }

                    var trid = $(tis).closest('table').closest('tr').attr('id');
                    var len = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr').length;
                    var discountPls = 0; var cnt = 0;
                    var fee = 0; var lblDiscount = 0; var txtDiscount = 0; var lbltotal = 0; var lblPaid = 0; var txtPaid = 0; var lblDue = 0;
                    for (var i = 0; i < len; i++) {
                        discountPls = discountPls + 0;
                        
                        var ApplyFine = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) span:eq(2)').html();
                        if (ApplyFine == "ApplyFine") {
                            //var lblHeadAmtf = parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(2) span:eq(0)').html());
                            //var lblDiscountf = parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) span:eq(0)').html());
                            //var totalf = (lblHeadAmtf - lblDiscountf).toFixed(2);
                            //$("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val('');
                            //var txtDiscountf = 0;
                            //var lbltotalf = (totalf - txtDiscountf).toFixed(2);
                            //$("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(4) span:eq(0)').html(lbltotalf);
                            //var lblpaidf = parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) span:eq(0)').html());
                            //var lbltotalduef = (lbltotalf - lblpaidf).toFixed(2);
                            //if (ischk) {
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val(lbltotalduef);
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html("0.00");
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", true);
                            //}
                            //else {
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html(lbltotalduef);
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val('');
                            //    $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                            //}
                        }
                        var ff = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked");
                        if (ff) {

                            cnt++;
                        }
                        fee += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(2) span:eq(0)').html());
                        lblDiscount += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) span:eq(0)').html());
                        var dsc = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val();
                        txtDiscount += parseFloat(dsc == "" ? "0" : dsc);
                        lbltotal += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(4) span:eq(0)').html());
                        lblPaid += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) span:eq(0)').html());
                        var payble = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val();
                        lblDue += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html());
                        txtPaid += parseFloat(payble == "" ? "0" : payble);
                    }
                    var HeadtrId = "instalBtn_" + $(tis).closest('table').closest('tr').attr('id').split('_')[1];
                    if (cnt > 0) {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                        $("#" + HeadtrId).find('td:eq(3) input[type=text]').removeAttr('readonly');
                        $("#" + HeadtrId).find('td:eq(5) input[type=text]').removeAttr('readonly');
                        $("#" + HeadtrId).find('td:eq(8) textarea').removeAttr('readonly');
                    }
                    else {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                        $("#" + HeadtrId).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + HeadtrId).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                        $("#" + HeadtrId).find('td:eq(8) textarea').attr('readonly', 'readonly');
                        for (var k = 0; k < len; k++) {
                            $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + k + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                        }
                        var tiss = $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]');
                        MainHeadCheck(tiss);
                    }
                    if ($(".hdnDiscountText").html() == "0") {
                        $("#" + HeadtrId).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                        $("#" + HeadtrId).find('td:eq(8) textarea').attr('readonly', 'readonly');
                    }
                    if ($(".hdnPaidText").html() == "0") {
                        $("#" + HeadtrId).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                    }
                    $("#" + HeadtrId).find('td:eq(3) input[type=text]').val(txtDiscount == 0 ? "" : txtDiscount.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(4) span:eq(0)').html(lbltotal.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(5) span:eq(0)').html(lblPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(5) input[type=text]').val(txtPaid == 0 ? "" : txtPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(6) span:eq(0)').html(lblDue.toFixed(2));
                    if (txtPaid==0) {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                    }
                    totalsAmt();
                }



                function MainHeadDiscount(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0) {
                        $(tis).val('');
                    }
                    var headRowid = $(tis).closest('tr').attr('id');
                    var ischecked = $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked");
                    if (!ischecked) {
                        $(tis).val('');
                    }

                    var lblInstallmentAmt_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                    var txtDiscount_h = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                    var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                    var lblcurrbalance_h = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(0)').html());
                    var lblbalance_h = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(1)').html());
                    if ((lblInstallmentAmt_h - (lblDiscount_h + lblpaid_h)) < txtDiscount_h || txtDiscount_h < 0) {
                        $(tis).val('');
                        txtDiscount_h = 0;
                    }
                    var total_h = 0; var payable_h = 0; var due_h = 0;
                    total_h = (lblInstallmentAmt_h - (lblDiscount_h + txtDiscount_h)).toFixed(2);
                    $("#" + headRowid).find('td:eq(4) span:eq(0)').html(total_h);
                    var payable_h = 0;

                    $("#" + headRowid).find('td:eq(4) span:eq(0)').html(total_h);
                    payable_h = (total_h - lblpaid_h).toFixed(2);
                    $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable_h);
                    $("#" + headRowid).find('td:eq(6) span:eq(0)').html("0.00");

                    var childRowid = "instalDeails_" + $(tis).closest('tr').attr('id').split("_")[1];
                    var len = $("#" + childRowid).find('td table tbody tr').length;

                    for (var i = 0; i < len; i++) {
                        var lblHeadAmt = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                        var lblDiscount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                        var total = (lblHeadAmt - lblDiscount).toFixed(2);
                        var lblbalance = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(1)').html());
                        var childChk = $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0) input[type=checkbox]').prop("checked");
                        if (lblbalance > 0 && childChk) {
                            if (txtDiscount_h >= total) {

                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(total);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html("0.00")
                                txtDiscount_h = txtDiscount_h - total;
                            }
                            else if (txtDiscount_h < total && txtDiscount_h > 0) {

                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(txtDiscount_h);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((total - txtDiscount_h).toFixed(2))
                                txtDiscount_h = 0;
                            }
                            else {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html(total)
                            }
                            var payable = 0;
                            var lbltotal = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                            var lblpaid = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());

                            payable = (lbltotal - lblpaid).toFixed(2);
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val(payable);
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                        }
                    }
                    if (txtDiscount_h > 0) {
                        $(tis).val('');
                        for (var i = 0; i < len; i++) {
                            var childChk = $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0) input[type=checkbox]').prop("checked");
                            var lblHeadAmt = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                            var lblDiscount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                            var total = (lblHeadAmt - lblDiscount).toFixed(2);
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html(total);
                            var lbltotal = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                            var lblpaid = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                            payable = (lbltotal - lblpaid).toFixed(2);
                            if (childChk) {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val(payable);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                            }
                            else {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val(payable);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                            }
                        }
                    }
                    totalsAmt();
                }


                function ChildDiscount(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0 || parseFloat($(tis).val()) < 0) {
                        $(tis).val('');
                    }


                    var childRowid = $(tis).closest('tr').attr('id');
                    var lblHeadAmt = parseFloat($("#" + childRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount = parseFloat($("#" + childRowid).find('td:eq(3) span:eq(0)').html());
                    var total = (lblHeadAmt - lblDiscount).toFixed(2);
                    if (parseFloat($(tis).val()) > total) {
                        $(tis).val('');
                    }
                    var txtDiscount = parseFloat($(tis).val() == '' ? "0" : $(tis).val());
                    
                    var lbltotal = (total - txtDiscount).toFixed(2);
                    $("#" + childRowid).find('td:eq(4) span:eq(0)').html(lbltotal);
                    var lblpaid = parseFloat($("#" + childRowid).find('td:eq(5) span:eq(0)').html());
                    var lbltotaldue = (lbltotal - lblpaid).toFixed(2);
                    $("#" + childRowid).find('td:eq(5) input[type=text]').val(lbltotaldue);
                    $("#" + childRowid).find('td:eq(6) span:eq(0)').html("0.00");
                    var tpd = $("#" + childRowid).find('td:eq(5) input[type=text]').val();
                    var txtpd = parseFloat(tpd == '' ? "0" : tpd);
                    if (txtDiscount > 0 || txtpd > 0) {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", false);
                    }

                    var trid = $(tis).closest('table').closest('tr').attr('id');
                    var len = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr').length;
                    var fee = 0; var lblDiscount = 0; var txtDiscount = 0; var lbltotal = 0; var lblPaid = 0; var txtPaid = 0; var lblDue = 0;
                    for (var i = 0; i < len; i++) {
                        fee += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(2) span:eq(0)').html());

                        var dsc = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val();
                        txtDiscount += parseFloat(dsc == "" ? "0" : dsc);
                        lbltotal += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(4) span:eq(0)').html());

                        var payble = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val();
                        txtPaid += parseFloat(payble == "" ? "0" : payble);
                        lblDue += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html());
                        lblPaid += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) span:eq(0)').html());
                        lblDiscount += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) span:eq(0)').html());
                    }
                    var HeadtrId = "instalBtn_" + $(tis).closest('table').closest('tr').attr('id').split('_')[1];
                    $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                    $("#" + HeadtrId).find('td:eq(2) span:eq(0)').html(fee.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(3) span:eq(1)').html(lblDiscount.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(3) input[type=text]').val(txtDiscount == 0 ? "" : txtDiscount.toFixed(2));
                    var totals = (fee - (lblDiscount + txtDiscount)).toFixed(2);
                    $("#" + HeadtrId).find('td:eq(4) span:eq(0)').html(totals);
                    $("#" + HeadtrId).find('td:eq(5) span:eq(0)').html(lblPaid.toFixed(2));
                    var payabls = (totals - lblPaid).toFixed(2)
                    $("#" + HeadtrId).find('td:eq(5) input[type=text]').val(txtPaid == 0 ? "" : txtPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(6) span:eq(0)').html((payabls - txtPaid).toFixed(2));
                    totalsAmt();
                }

                function MainHeadPayable(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0) {
                        $(tis).val('');
                    }

                    var txtMan = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                    var headRowid = $(tis).closest('tr').attr('id');
                    $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                    $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');

                    var payable_h = 0; var due_h = 0; var thisval = 0; var ischecked = false;
                    var lblTotal_h = parseFloat($("#" + headRowid).find('td:eq(4) span:eq(0)').html());
                    var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                    var actualDue = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(1)').html());
                    payable_h = (lblTotal_h - lblpaid_h).toFixed(2);
                    var txttotal_h = 0;
                    if (actualDue > 0) {
                        if (txtMan >= payable_h) {
                            $("#" + headRowid).find('td:eq(6) span:eq(0)').html('0.00');
                            $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable_h);
                            txtMan = txtMan - payable_h;
                            txttotal_h = payable_h;
                            ischecked = true;
                            $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                            $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                            $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
                        }
                        else if (txtMan < payable_h && txtMan > 0) {
                            $("#" + headRowid).find('td:eq(6) span:eq(0)').html((payable_h - txtMan).toFixed(2));
                            $("#" + headRowid).find('td:eq(5) input[type=text]').val(txtMan);
                            txttotal_h = txtMan;
                            txtMan = 0;
                            ischecked = true;
                            $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                            $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                            $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
                        }
                        else {
                            $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                            $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                            $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                            $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                            $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
                            txttotal_h = 0;
                            txtMan = 0;
                        }
                        if (txttotal_h >= 0) {
                            var childRowid = "instalDeails_" + $(tis).closest('tr').attr('id').split("_")[1];
                            var headTable_ids = "headTable_" + $(tis).closest('tr').attr('id').split("_")[1];
                            var len = $("#" + headTable_ids + " tbody tr").length;

                            //$("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(3) input[type=text]').val('');
                            //$("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').val('');
                            var finetype = 0; var totalDuef = 0;
                            for (var j = 0; j < len; j++) {
                                var ApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                                var lblPayablef = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                                var lblPaidf = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());

                                if (ApplyFine == "ApplyFine") {

                                    var duh = (lblPayablef - lblPaidf).toFixed(2);
                                    if (duh > 0) {
                                        finetype = finetype + 1;
                                        totalDuef = parseFloat(totalDuef) + parseFloat(duh);
                                    }
                                }

                            }
                            //if (txttotal_h < totalDuef) {
                            //    var boxValue = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val());
                            //    boxValue = boxValue - txttotal_h;
                            //    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val(boxValue.toFixed(2));
                            //    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(boxValue.toFixed(2));

                            //    $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                            //    $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                            //    $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                            //    $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                            //    $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            //    $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
                            //    txttotal_h = 0;
                            //    maivval = 0;
                            //    for (var i = 0; i < len; i++) {
                            //        var lblPayable = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(4) span:eq(0)').html());
                            //        var lblPaid = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) span:eq(0)').html());
                            //        var lblActualDue = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(1)').html());
                            //        if (lblActualDue > 0) {
                            //            var totalDue = (lblPayable - lblPaid).toFixed(2);
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').val('');
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(0)').html(totalDue);
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                            //            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(7) textarea').attr('readonly', 'readonly');
                            //        }
                            //    }
                            //}
                            //else {
                            if (totalDuef > 0 && txttotal_h >= totalDuef && finetype > 0) {
                                for (var j = 0 ; j < len; j++) {
                                    var isFineApply = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                                    if (isFineApply == "ApplyFine") {
                                        var lblPayablefh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                                        var lblPaidfh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());
                                        var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                                        $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                                        $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                                        $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                                        $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').val(tDuefh);
                                        $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(6) span:eq(0)').html("0.00")
                                        txttotal_h = txttotal_h - tDuefh;
                                        len = (len - finetype);
                                    }
                                }
                            }

                            for (var i = 0; i < len; i++) {

                                var lblPayable = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(4) span:eq(0)').html());
                                var lblPaid = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) span:eq(0)').html());
                                var lblActualDue = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(1)').html());
                                if (lblActualDue > 0) {
                                    var totalDue = (lblPayable - lblPaid).toFixed(2);
                                    var isApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0) span:eq(2)').html();
                                    if (isApplyFine != "ApplyFine") {

                                        if (txttotal_h >= totalDue) {
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').val(totalDue);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(0)').html("0.00");
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(7) textarea').removeAttr('readonly');
                                            txttotal_h = txttotal_h - totalDue;
                                        }
                                        else if (txttotal_h < totalDue && txttotal_h > 0) {
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').val(txttotal_h);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(0)').html((totalDue - txttotal_h).toFixed(2));
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(7) textarea').removeAttr('readonly');
                                            txttotal_h = txttotal_h - totalDue;
                                            if (txttotal_h < 0) {
                                                txttotal_h = 0;
                                            }
                                        }
                                        else if (txttotal_h == 0) {
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').val('');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(0)').html(totalDue);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) input[type=text]').removeAttr('readonly');
                                            $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(7) textarea').removeAttr('readonly');
                                            txttotal_h = txttotal_h - totalDue;
                                            if (txttotal_h < 0) {
                                                txttotal_h = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    totalsAmt();
                }
                function ChildPayable(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0 || parseFloat($(tis).val()) < 0) {
                        $(tis).val('');
                    }


                    var childRowid = $(tis).closest('tr').attr('id');
                    var lbltotal = parseFloat($("#" + childRowid).find('td:eq(4) span:eq(0)').html());
                    var lblpaid = parseFloat($("#" + childRowid).find('td:eq(5) span:eq(0)').html());
                    var totaldue = (lbltotal - lblpaid).toFixed(2);
                    if (parseFloat($(tis).val()) > totaldue) {
                        $(tis).val('');
                    }
                    var txtpaid = parseFloat($(tis).val() == '' ? "0" : $(tis).val());
                    var tdesc=$("#" + childRowid).find('td:eq(3) input[type=text]').val();
                    var txtdsc = parseFloat(tdesc == '' ? "0" : tdesc);
                    var lbltotaldue = (totaldue - txtpaid).toFixed(2);
                    if (txtpaid > 0 || txtdsc>0) {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", false);
                    }
                    $("#" + childRowid).find('td:eq(6) span:eq(0)').html(lbltotaldue);
                    var stsss = 0;
                    var trid = $(tis).closest('table').closest('tr').attr('id');
                    var len = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr').length;
                    var fee = 0; var lblDiscount = 0; var txtDiscount = 0; var lbltotal = 0; var lblPaid = 0; var txtPaid = 0; var lblDue = 0;
                    for (var i = 0; i < len; i++) {
                        fee += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(2) span:eq(0)').html());
                        var dsc = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val();
                        txtDiscount += parseFloat(dsc == "" ? "0" : dsc);
                        lbltotal += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(4) span:eq(0)').html());
                        var payble = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val();
                        txtPaid += parseFloat(payble == "" ? "0" : payble);
                        lblDue += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html());
                        lblPaid += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) span:eq(0)').html());
                        lblDiscount += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) span:eq(0)').html());
                        var chks = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked");
                        if (chks) {
                            stsss++;
                        }
                    }
                    var HeadtrId = "instalBtn_" + $(tis).closest('table').closest('tr').attr('id').split('_')[1];
                    if (stsss > 0) {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                        for (var i = 0; i < len; i++) {
                            $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                        }
                    }
                    
                    $("#" + HeadtrId).find('td:eq(2) span:eq(0)').html(fee.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(3) span:eq(1)').html(lblDiscount.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(3) input[type=text]').val(txtDiscount == 0 ? "" : txtDiscount.toFixed(2));
                    var totals = (fee - (lblDiscount + txtDiscount)).toFixed(2);
                    $("#" + HeadtrId).find('td:eq(4) span:eq(0)').html(totals);
                    $("#" + HeadtrId).find('td:eq(5) span:eq(0)').html(lblPaid.toFixed(2));
                    var payabls = (totals - lblPaid).toFixed(2)
                    $("#" + HeadtrId).find('td:eq(5) input[type=text]').val(txtPaid == 0 ? "" : txtPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(6) span:eq(0)').html((payabls - txtPaid).toFixed(2));
                    totalsAmt();
                }
                //function TotalPayable(tis) {
                //    if (isNaN(parseFloat($(tis).val()))) {
                //        $(tis).val('');
                //    }
                //    else if (parseFloat($(tis).val()) == 0) {
                //        $(tis).val('');
                //    }
                //    var maivval1 = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                //    var count = 0;
                //    $(".mainRow").each(function () {
                //        var headRowid = $(this).attr('id');
                //        //$("#" + headRowid).find('td:eq(3) input[type=text]').val('');
                //        $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                //        if ($('.hdnDiscountText').html() == "1") {
                //            $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                //        }
                //        if ($('.hdnPaidText').html() == "1") {
                //            $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                //        }
                //        $("#" + headRowid).find('td:eq(8) textarea').attr('readonly', 'readonly');
                //        $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                //        var payable_h = 0; var due_h = 0; var thisval = 0;
                //        var lblfee_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                //        var disc = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptFeeStructure_txtInstallmentdiscount_" + count).val();
                //        var lbldiscount_h = parseFloat((disc == '' || disc == undefined) ? 0 : disc);
                //        var lblTotal_h = (lblfee_h.toFixed(2) - lbldiscount_h.toFixed(2)).toFixed(2);
                //        var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                //        var actualDue = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(1)').html());
                //        payable_h = (lblTotal_h - lblpaid_h).toFixed(2);
                //        if (actualDue > 0) {
                //            var txttotal_h = 0;
                //            var boxValues = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val());
                //            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val(boxValues.toFixed(2));
                //            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(boxValues.toFixed(2));
                //            if (maivval1 >= payable_h) {
                //                $("#" + headRowid).find('td:eq(6) span:eq(0)').html('0.00');
                //                $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable_h);
                //                maivval1 = maivval1 - payable_h;
                //                txttotal_h = payable_h;
                //                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                //                if ($('.hdnDiscountText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                //                }
                //                if ($('.hdnPaidText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                //                }
                //                $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
                //            }
                //            else if (maivval1 < payable_h && maivval1 > 0) {
                //                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((payable_h - maivval1).toFixed(2));
                //                $("#" + headRowid).find('td:eq(5) input[type=text]').val(maivval1.toFixed(2));
                //                txttotal_h = maivval1;
                //                maivval1 = 0;
                //                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                //                if ($('.hdnDiscountText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(3) input[type=text]').removeAttr('readonly');
                //                }
                //                if ($('.hdnPaidText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(5) input[type=text]').removeAttr('readonly');
                //                }
                //                $("#" + headRowid).find('td:eq(8) textarea').removeAttr('readonly');
                //            }
                //            else {
                //                $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                //                $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                //                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);
                //                if ($('.hdnDiscountText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(3) input[type=text]').attr('readonly');
                //                }
                //                if ($('.hdnPaidText').html() == "1") {
                //                    $("#" + headRowid).find('td:eq(5) input[type=text]').attr('readonly');
                //                }
                //                txttotal_h = 0;
                //                maivval1 = 0;
                //            }
                //            var childRowid = "instalDeails_" + $(this).closest('tr').attr('id').split("_")[1];
                //            var headTable_ids = "headTable_" + $(this).closest('tr').attr('id').split("_")[1];
                //            var len = $("#" + headTable_ids + " tbody tr").length;
                //            if (txttotal_h > 0) {
                //                var finetype = 0; var totalDuef = 0;
                //                for (var j = 0; j < len; j++) {
                //                    var ApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                //                    var lblPayablef = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                //                    var lblPaidf = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());
                //                    if (ApplyFine == "ApplyFine") {

                //                        var duh = (lblPayablef - lblPaidf).toFixed(2);
                //                        if (duh > 0) {
                //                            finetype = finetype + 1;
                //                            totalDuef = parseFloat(totalDuef) + parseFloat(duh);
                //                        }
                //                    }
                //                }
                //                if (totalDuef > 0 && txttotal_h >= totalDuef && finetype > 0) {
                //                    for (var j = 0; j < len; j++) {
                //                        var isFineApply = $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0) span:eq(2)').html();
                //                        if (isFineApply == "ApplyFine") {
                //                            var lblPayablefh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(4) span:eq(0)').html());
                //                            var lblPaidfh = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) span:eq(0)').html());
                //                            var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                //                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                //                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(3) input[type=text]').removeAttr('readonly');
                //                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                //                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(5) input[type=text]').val(tDuefh);
                //                            $("#" + headTable_ids + " tbody tr:eq(" + j + ")").find('td:eq(6) span:eq(0)').html("0.00")
                //                            txttotal_h = txttotal_h - tDuefh;
                //                            len = (len - finetype);
                //                        }
                //                    }
                //                }

                //                for (var i = 0; i < len; i++) {
                //                    var lblActualDue = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(6) span:eq(1)').html());
                //                    if (lblActualDue > 0) {
                //                        var isApplyFine = $("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(0) span:eq(2)').html();
                //                        if (isApplyFine != "ApplyFine") {
                //                            var lblPayable = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(4) span:eq(0)').html());
                //                            var lblPaid = parseFloat($("#" + headTable_ids + " tbody tr:eq(" + i + ")").find('td:eq(5) span:eq(0)').html());
                //                            if (txttotal_h >= (lblPayable - lblPaid) && txttotal_h > 0) {
                //                                var totalDue1 = (lblPayable - lblPaid).toFixed(2);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val(totalDue1);
                //                                txttotal_h = txttotal_h - (lblPayable - lblPaid);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html("0.00");
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                //                                if ($('.hdnDiscountText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').removeAttr('readonly');
                //                                }
                //                                if ($('.hdnPaidText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').removeAttr('readonly');
                //                                }
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').removeAttr('readonly');
                //                            }
                //                            else if (txttotal_h < (lblPayable - lblPaid) && txttotal_h > 0) {
                //                                var totalDue2 = (lblPayable - lblPaid).toFixed(2);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val(txttotal_h);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html((totalDue2 - txttotal_h).toFixed(2));
                //                                txttotal_h = 0;
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', true);
                //                                if ($('.hdnDiscountText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').removeAttr('readonly');
                //                                }
                //                                if ($('.hdnPaidText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').removeAttr('readonly');
                //                                }
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').removeAttr('readonly');
                //                            }
                //                            else if (txttotal_h == 0) {
                //                                txttotal_h = 0;
                //                                var totalDue3 = (lblPayable - lblPaid).toFixed(2);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val('');
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html(totalDue3);
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                //                                if ($('.hdnDiscountText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                //                                }
                //                                if ($('.hdnPaidText').html() == "1") {
                //                                    $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                //                                }
                //                                $('#' + headTable_ids + ' tbody tr:eq(' + i + ')').find('td:eq(7) textarea').attr('readonly', 'readonly');
                //                            }
                //                        }
                //                    }
                //                }

                //            }
                //            else {
                //                for (var j = 0; j < len; j++) {
                //                    var lblfee = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(2) span:eq(0)').html());
                //                    var lbldisc = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(3) span:eq(0)').html());
                //                    var lblPayablefh = (lblfee - lbldisc).toFixed(2);
                //                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(4) span:eq(0)').html(lblPayablefh);
                //                    var lblPaidfh = parseFloat($('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) span:eq(0)').html());
                //                    var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                //                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(6) span:eq(0)').html(tDuefh);
                //                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) input[type=text]').val('');
                //                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(0)  input[type=checkbox]').prop('checked', false);
                //                    if ($('.hdnDiscountText').html() == "1") {
                //                        $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(3) input[type=text]').attr('readonly', 'readonly');
                //                    }
                //                    if ($('.hdnPaidText').html() == "1") {
                //                        $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(5) input[type=text]').attr('readonly', 'readonly');
                //                    }
                //                    $('#' + headTable_ids + ' tbody tr:eq(' + j + ')').find('td:eq(7) textarea').attr('readonly', 'readonly');
                //                }
                //            }
                //        }
                //        if (isNaN(parseFloat($(tis).val()))) {
                //            $(tis).val('0.00');
                //        }
                //        count += 1;
                //    });
                //}



                function showfee(tis) {
                    $(tis).closest('td').find('div:eq(0)').removeClass('hide');
                }
                function hidefee(tis) {
                    $(tis).closest('td').find('div:eq(0)').addClass('hide');
                }
                function showdiscount(tis) {
                    $(tis).closest('td').find('div:eq(0)').removeClass('hide');
                }
                function hidediscount(tis) {
                    $(tis).closest('td').find('div:eq(0)').addClass('hide');
                }
                function CheckDecimals(inputtxt) {
                    var exp = /^(\+|-)?(\d*\.?\d*)$/;
                    if (inputtxt.value.match(exp) && inputtxt != null) {
                        inputtxt.style.border = "1px solid #D5D5D5 !important";
                        return true;
                    }
                    else {
                        if (inputtxt.value != "") {
                            inputtxt.style.border = "1px solid Red !important";
                            inputtxt.value = "0.00";
                            inputtxt.focus();
                            return false;
                        }
                    }
                }
                function openClode(tis) {
                    if ($(tis).closest('tr').find("td:eq(1) i.togalIcon").hasClass("fa fa-sort-amount-desc")) {
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").removeClass("fa fa-sort-amount-desc")
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").addClass("fa fa-sort-amount-asc")
                    }
                    else {
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").removeClass("fa fa-sort-amount-asc")
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").addClass("fa fa-sort-amount-desc")
                    }
                    var tisId = $(tis).closest('tr').attr('id');
                    tisId = tisId.split('_')[1];
                    var bodyId = "instalDeails_" + tisId;
                    if ($("#" + bodyId).hasClass('hide')) {
                        $("#" + bodyId).removeClass('hide');
                    }
                    else {
                        $("#" + bodyId).addClass('hide');
                    }
                }
                function MODChenge() {
                    $("[id*=txtChequeNo]").val("");
                    $("[id*=txtBank]").val("NA");
                    if ($("[id*=DropDownMOD]").val() == "Online") {
                        $("#divOnlineDetails").removeClass("hide");
                        $("#divStatus").addClass("hide");
                        $("#divOtherTools").addClass("hide");
                    }
                    else {
                        $("#divOnlineDetails").addClass("hide");
                    }
                    if ($("[id*=DropDownMOD]").val() != "Online") {
                        if ($("[id*=DropDownMOD]").val() != "Cash") {
                            $("#divOtherTools").removeClass("hide");
                            if ($("[id*=DropDownMOD]").val() == "Cheque") {
                                $("#divStatus").removeClass("hide");
                                $("[id*=drpStatus]").val("Pending");

                            }
                            else if ($("[id*=DropDownMOD]").val() == "Other") {
                                $("#divStatus").removeClass("hide");
                                $("[id*=drpStatus]").val();
                            }
                            else {
                                $("[id*=drpStatus]").val("Paid");
                                $("#divStatus").addClass("hide");
                            }
                        }
                        else {
                            $("#divOtherTools").addClass("hide");
                        }
                    }
                    if ($("[id*=DropDownMOD]").val() == "Cash") {
                        $("#divOtherTools").addClass("hide");
                        $("#divStatus").addClass("hide");
                        $("[id*=txtChequeNo]").removeClass("validateTextMode");
                    }
                    if ($("[id*=DropDownMOD]").val() == "Cheque" || $("[id*=DropDownMOD]").val() == "DD") {
                        $("[id*=lblChqDate]").html("Date of Instrument");
                        $("[id*=lblChqNo]").html("Instrument No.");
                        $("[id*=txtChequeNo]").addClass("validateTextMode");
                    }
                    else if ($("[id*=DropDownMOD]").val() == "Online Transfer" || $("[id*=DropDownMOD]").val() == "Other") {
                        $("[id*=lblChqDate]").html("Date of Transaction");
                        $("[id*=lblChqNo]").html("Ref. No.");
                        $("[id*=txtChequeNo]").addClass("validateTextMode");
                    }
                    else if ($("[id*=DropDownMOD]").val() == "Card") {
                        $("[id*=lblChqDate]").html("Date of Transaction");
                        $("[id*=lblChqNo]").html("Card No.");
                        $("[id*=lblBankName]").html("Issuer");
                        $("[id*=txtChequeNo]").addClass("validateTextMode");
                    }
                    else {
                        $("[id*=lblChqDate]").html($("[id*=DropDownMOD]").val() + " Date");
                        $("[id*=lblChqNo]").html($("[id*=DropDownMOD]").val() + " No.");
                    }
                    if ($("[id*=DropDownMOD]").val() == "Other") {
                        $("[id*=lblBankName]").html("Reference Name");
                    }
                }
                function updateText() {
                    var chequeNo = $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtChequeNo').val();
                    $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtChequeNo').val(chequeNo.trim())
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
