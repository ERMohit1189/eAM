<%@ Page Title="Manual Discount | eAM&#174;" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="ManualDiscount.aspx.cs" Inherits="ManualDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
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
            vertical-align:top !important;
        }

            .box .col-sm-6 {
                padding: 5px;
        }
            input[disabled], textarea[disabled] {
    cursor: default;
    background: #e6e6e6 !important;
}
       @media print {
        .no-print, .no-print *
        {
            display: none !important;
        }
      }
       .btn {
            background-color:#fff;
            border: 1px solid #000;
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
                                            OnTextChanged="txtSearch_TextChanged" onblur="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                        <asp:HiddenField ID="hfStudentId" runat="server"  />
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
                                          <span id="btnPrint" runat="server" visible="false" class="btn-print-box" onclick="printDiv();"><a><i class="icon-printer"></i></a></span>
                                        <div id="Div1" runat="server" style="color: #FF0000"></div>
                                        <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                    </div>
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
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
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
                                                        <%--<a href="#" target="_blank" class="more-btn">more...</a>--%>
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
                                    <div class="col-sm-4  mgbt-xs-15" id="divMod">
                                        <label class="control-label">Discount Name</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDiscountHead" runat="server" TabIndex="1" CssClass="form-control-blue validateTextMode">
                                                
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    
                                </div>
                                
                                <div class="col-sm-12" runat="server" id="divTutionFee" visible="false">
                                    <fieldset style="padding: 5px !important;">
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
                                                                <th class="text-right" style="width: 9%">Paid</th>
                                                                <th class="text-right" style="width: 9%">Balance</th>
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
                                                                <asp:Label runat="server" ID="lblIcon" title="History" onclick="openClode(this)" CssClass="chks_h btn menu-icon vd_bd-grey-n vd_black-n iconhistory" Style="top: 0 !important; padding: 1px 6px"><i class="togalIcon fa fa-sort-amount-desc"></i></asp:Label>
                                                                <asp:CheckBox runat="server" ID="chkInstallment" CssClass="vd_check check-success hide" Style="height: 17px;" onchange="MainHeadCheck(this);" />
                                                                <asp:Label runat="server" ID="lblInstallmentName" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentAmount" CssClass="calfee vl" style="cursor:pointer !important" onmouseover="showfee(this)" onmouseout="hidefee(this)" Text='<%# Eval("headAmount") %>'></asp:Label>&nbsp;<asp:Label runat="server" style="cursor:pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" ID="HeadAmon" class="fa fa-info-circle"></asp:Label>
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
                                                                <asp:Label runat="server" ID="lblInstallmentDiscount" CssClass="calDisc vl" style="cursor:pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>&nbsp;<asp:Label runat="server" ID="HeadDiscount" style="cursor:pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" class="fa fa-info-circle"></asp:Label><br />
                                                                <asp:TextBox runat="server" ID="txtInstallmentdiscount" class="form-control-blue" placeholder="0.00" Style="text-align: right !important; height: 20px;" onblur="MainHeadDiscount(this);"></asp:TextBox>
                                                                <div class="box hide row">
                                                                        <asp:Repeater ID="RepeaterHeadDiscount" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                        <asp:Label runat="server" ID="lblDiscNameh"
                                                                                            Text='<%# Eval("DiscountName") %>'>
                                                                                        </asp:Label></div>
                                                                                        <div class="text-right col-sm-6">
                                                                                        :&nbsp;&nbsp;<asp:Label runat="server" ID="lblDiscAmth" Text='<%# Bind("DiscountAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                            <asp:Label runat="server" ID="lblPaidDiscAmth"  CssClass="hide" Text='<%# Bind("PaidDiscAmount") %>' Style="text-align: right;"></asp:Label>
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
                                                            </td>
                                                            <td class="text-right lasttd" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblhideInstallmentDue" Text='<%# Eval("DueAmount") %>'></asp:Label>
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
                                                                <table class="table no-bm no-head-border table-bordered pro-table table-header-group" border="1" style="border-collapse: collapse; margin-top: 2px !important;">
                                                                    <asp:Repeater ID="rptFee" runat="server">
                                                                        <HeaderTemplate>
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="text-align: center !important; vertical-align: middle !important; width: 4%; padding: 2px 0px !important;" scope="col">#</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 21%;" scope="col">Fee Head</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Amount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Discount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Total</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Paid</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 9%;" scope="col">Balance</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 30%;" scope="col" class="hidden-print">Narration</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr runat="server" id="feeheadrow" class="childRow">
                                                                                <td style="text-align: center !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <span id="lblFeeheadSrNo"><%# Container.ItemIndex+1 %></span>
                                                                                    <asp:CheckBox runat="server" ID="chkInstallmentFee" CssClass="vd_check check-success chks_c hide" Style="height: 17px;" onchange="ChildCheck(this);" />
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
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscount" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>&nbsp;<asp:Label runat="server" ID="ChildDiscount" class="fa fa-info-circle" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscountPaid" class="hide" Text='<%# Eval("PaidDiscount") %>'></asp:Label>
                                                                                    <asp:LinkButton runat="server" ID="btnResetDiscount" Visible="false" ToolTip="Reset Manual & Onpage Discount" OnClientClick="if ( !confirm('Are you sure you want to reset manual & onpage discounts ? ')) return false;" OnClick="btnResetDiscount_Click">&nbsp;<i class="fa fa-close text-danger"></i></asp:LinkButton>
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadDiscount" class="form-control-blue" placeholder="0.00" onblur="ChildDiscount(this);"  style="cursor:pointer !important text-align: right !important; height: 20px;"></asp:TextBox>
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
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadBalance" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important; line-height: 1;" colspan="2" class="hidden-print">
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadNarration" class="form-control-blue" TextMode="MultiLine" Style="height: 29px; color: #000;"></asp:TextBox>

                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    
                                                </asp:Repeater>
                                            </table>

                                        </div>
                                    </fieldset>

                                    <div class="col-sm-12 hidden-print">
                                        <asp:HiddenField runat="server" ID="hdnIspromot" />
                                        <div><i class="fa fa-credit-card"></i>&nbsp;Discount Details</div>
                                        <div class="col-sm-3" style="border: 1px solid #ccc" id="divFeeDetails">
                                            <div style="padding: 5px !important;">
                                                <div style="width: 100%; font-size: 12px !important; margin-top: 6px; padding: 0 0;">
                                                    
                                                    <div class="col-sm-12  text-left" style="background: #fafafa; font-weight: 700; color: #dc4448; padding: 5px;">
                                                        <label>Total Discount </label>
                                                        <span style="float: right;
    color: #000;
    width: 149px;"><input type="checkbox" onchange="chkEnableTextBox(this)" /><asp:TextBox ID="txtTotalPaid" runat="server" CssClass="validateTextMode txtmain" Text="0.00" Style="color: #000;width: 128px; text-align:right;" onblur="TotalPayable(this)" disabled="disabled"></asp:TextBox></span>
                                                    </div>
                                                    
                                                    <div class="col-sm-12  mgbt-xs-15 text-center" id="divSubmit">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" OnClientClick="ValidateTextBox('.validateTextMode');ValidateDropdown('.validatedrpMode');return validationReturn();RemoveHideFromPayment(1);" Style="margin-top: 7px;"><i class="fa fa-floppy-o"></i> Submit</asp:LinkButton>
            <div id="msgs" runat="server" style="color: #FF0000"></div>
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
                    </div>
                    
                </div>
            </div>
            <script>
                function chkEnableTextBox(tis) {
                    if ($(tis).prop('checked')) {
                        $('.txtmain').removeAttr('disabled');
                    }
                    else {
                        $('.txtmain').attr('disabled', 'disabled');
                    }
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
                        var due1 = $(this).find('td:eq(6) span:eq(0)').html();
                        var Pending = $(this).find('td:eq(0) span:eq(2)').html();
                        var balance1 = parseFloat(due1 == "" ? "0" : due1);
                        if (balance1 == 0 && Pending != "Pending") {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreen');
                            }
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
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
                        var due = $(this).find('td:eq(6) span:eq(0)').html();
                        var Pending = $(this).find('td:eq(1) span:eq(3)').html();
                        var balance = parseFloat(due == "" ? "0" : due);
                        if (balance == 0 && Pending != "Pending") {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreenchild');
                            }
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
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
                            var val = $("#instalBtn_" + (i + 1)).find('td:eq(3) input[type=text]').val();
                            totalamt += parseFloat(val == "" ? "0" : val);
                        }
                    }
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").val(totalamt.toFixed(2));
                }
                function Naretion(tis) {
                    var childRowid = "instalDeails_" + $(tis).closest('tr').attr('id').split("_")[1];
                    var len = $("#" + childRowid).find('td table tbody tr').length;
                    var cnt = 0;
                    for (var i = 0; i < len; i++) {
                        var ischecked = $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0) input[type=checkbox]').prop("checked");
                        if (ischecked) {
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').val($(tis).val());
                            cnt++;
                        }
                        else {
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').val('');
                        }
                        
                    }
                    if (cnt==0) {
                        $(tis).val('');
                    }
                }

                function MainHeadDiscount(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0) {
                        $(tis).val('');
                    }
                    var headRowid = $(tis).closest('tr').attr('id');
                    
                    var isvalid = 0;
                    var lblInstallmentAmt_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                    var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                    var txtDiscount_h = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                    isvalid = (lblInstallmentAmt_h - (lblpaid_h))
                    if (isvalid < txtDiscount_h) {
                        $(tis).val('');
                        txtDiscount_h = 0;
                    }
                    var remain = (lblInstallmentAmt_h - (lblDiscount_h)).toFixed(2)
                    if (remain < txtDiscount_h) {
                        txtDiscount_h = remain;
                        $(tis).val(remain);
                    }
                    if (txtDiscount_h > 0) {
                        $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                    }
                    var total_h = 0; var payable_h = 0; var due_h = 0;
                    
                   
                    total_h = (lblInstallmentAmt_h - (txtDiscount_h)).toFixed(2);
                    
                    $("#" + headRowid).find('td:eq(4) span:eq(0)').html(total_h);
                    var balance = 0;
                    balance = (total_h - lblpaid_h).toFixed(2);
                    $("#" + headRowid).find('td:eq(6) span:eq(0)').html(balance);

                    var childRowid = "instalDeails_" + $(tis).closest('tr').attr('id').split("_")[1];
                    var len = $("#" + childRowid).find('td table tbody tr').length;

                    for (var i = 0; i < len; i++) {
                        var lblHeadAmt = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                        var lblDiscount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                        var lblpaid = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                        var bals = (lblHeadAmt - (lblpaid + lblDiscount)).toFixed(2);
                        if (bals > 0) {
                            if (txtDiscount_h >= bals) {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(bals);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html("0.00")
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00")
                                txtDiscount_h = txtDiscount_h - bals;
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                            }
                            else if (txtDiscount_h < bals && txtDiscount_h > 0) {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(txtDiscount_h);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((bals - txtDiscount_h).toFixed(2))
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((bals - txtDiscount_h).toFixed(2))
                                txtDiscount_h = 0;
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                            }
                            else {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html(bals)
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html(bals)
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
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
                    var lblpaid = parseFloat($("#" + childRowid).find('td:eq(5) span:eq(0)').html());
                    var total = (lblHeadAmt - (lblpaid)).toFixed(2);
                    var txtDiscount = parseFloat($(tis).val() == '' ? "0" : $(tis).val());
                    if (txtDiscount > total) {
                        $(tis).val('');
                        txtDiscount=0;
                    }
                    
                    var lbltotal = (lblHeadAmt - (txtDiscount)).toFixed(2);
                    $("#" + childRowid).find('td:eq(4) span:eq(0)').html(lbltotal);
                    
                    var lbltotaldue = (lbltotal - lblpaid).toFixed(2);
                    $("#" + childRowid).find('td:eq(6) span:eq(0)').html(lbltotaldue);
                    if (txtDiscount > 0) {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked", false);
                    }
                    
                    var trid = $(tis).closest('table').closest('tr').attr('id');
                    var len = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr').length;
                    var fee = 0; var lblDiscount = 0; var txtDiscount = 0; var lbltotal = 0; var lblPaid = 0; var txtPaid = 0; var lblDue = 0;
                    for (var i = 0; i < len; i++) {
                        var dsc = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val();
                        txtDiscount += parseFloat(dsc == "" ? "0" : dsc);
                    }
                    var HeadtrId = "instalBtn_" + $(tis).closest('table').closest('tr').attr('id').split('_')[1];
                    if (txtDiscount > 0) {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                        $("#" + HeadtrId).find('td:eq(3) input[type=text]').val(txtDiscount.toFixed(2));
                    }
                    else {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                        $("#" + HeadtrId).find('td:eq(3)  input[type=text]').val('');
                    }
                    
                    var fee = parseFloat($("#" + HeadtrId).find('td:eq(2) span:eq(0)').html());
                    var disc = parseFloat($("#" + HeadtrId).find('td:eq(3) span:eq(0)').html());
                    var pad = parseFloat($("#" + HeadtrId).find('td:eq(5) span:eq(0)').html());
                    var totalc = (fee - (disc + txtDiscount)).toFixed(2);
                    $("#" + HeadtrId).find('td:eq(4) span:eq(0)').html(totalc);
                    var balc = (totalc - pad).toFixed(2)
                    $("#" + HeadtrId).find('td:eq(6) span:eq(0)').html(balc);
                    totalsAmt();
                }

                function TotalPayable(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0) {
                        $(tis).val('');
                    }
                    var maivval1 = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                    var len0 = $("#mainTable tbody").find(".mainRow").length;

                    for (var m = 0; m < len0; m++) {
                        var headRowid = $("#mainTable tbody").find(".mainRow:eq(" + m + ")").attr('id');
                        //$("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                        var payable_h = 0; var due_h = 0; var thisval = 0;
                        var lblfee_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                        var lbldiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                        var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                        var actualDue = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(0)').html());
                        payable_h = (lblfee_h - (lbldiscount_h + lblpaid_h)).toFixed(2);
                        if (payable_h > 0) {
                            var txttotal_h = 0;
                            if (maivval1 >= payable_h) {
                                $("#" + headRowid).find('td:eq(3) input[type=text]').val(payable_h);
                                $("#" + headRowid).find('td:eq(4) span:eq(0)').html((lblfee_h - (lbldiscount_h + payable_h)).toFixed(2));
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((lblfee_h - (lbldiscount_h + lblpaid_h + payable_h)).toFixed(2));
                                maivval1 = parseFloat(maivval1) - parseFloat(payable_h);
                                txttotal_h = payable_h;
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            }
                            else if (maivval1 < payable_h && maivval1 > 0) {
                                $("#" + headRowid).find('td:eq(3) input[type=text]').val(maivval1);
                                $("#" + headRowid).find('td:eq(4) span:eq(0)').html((lblfee_h - (lbldiscount_h + maivval1)).toFixed(2));
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((lblfee_h - (lbldiscount_h + lblpaid_h + maivval1)).toFixed(2));
                                txttotal_h = parseFloat(maivval1);
                                maivval1 = 0;
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            }
                            else {
                                $("#" + headRowid).find('td:eq(3) input[type=text]').val('');
                                $("#" + headRowid).find('td:eq(4) span:eq(0)').html((lblfee_h - (lbldiscount_h)).toFixed(2));
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((lblfee_h - (lbldiscount_h + lblpaid_h)).toFixed(2));
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);
                                txttotal_h = 0;
                                maivval1 = 0;
                            }
                            var childRowid = "instalDeails_" + headRowid.split("_")[1];

                            var len = $("#" + childRowid).find('td:eq(0)').find('table tbody tr').length;
                            var finetype = 0; var totalDuef = 0;
                            if (txttotal_h >= 0) {
                                for (var j = 0; j < len; j++) {
                                    var ApplyFine = $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(0) span:eq(2)').html();
                                    if (ApplyFine == "ApplyFine") {
                                        var lblfee_f = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(2) span:eq(0)').html());
                                        var lbldisc_f = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(3) span:eq(0)').html());
                                        var lblPayablefh_f = (lblfee_f - lbldisc_f).toFixed(2);
                                        $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html(lblPayablefh);
                                        var lblPaidfh_f = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(5) span:eq(0)').html());
                                        var duh = (lblPayablefh_f - lblPaidfh_f).toFixed(2);
                                        if (duh > 0) {
                                            finetype = finetype + 1;
                                            totalDuef = parseFloat(totalDuef) + parseFloat(duh);
                                        }
                                    }

                                }
                                if (totalDuef > 0 && finetype > 0) {
                                    for (var j = (len - finetype) ; j < len; j++) {
                                        var lblPayablefh = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(2) span:eq(0)').html());
                                        var lbldecfh = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(3) span:eq(0)').html());
                                        var lblTotalfh = (lblPayablefh - lbldecfh).toFixed(2);
                                        var lblPaidfh = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(5) span:eq(0)').html());
                                        var due2 = (lblTotalfh - lblPaidfh).toFixed(2);
                                        $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                        $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(3) input[type=text]').val(due2);
                                        $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html((lblTotalfh - due2).toFixed(2));
                                        $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(6) span:eq(0)').html("0.00");
                                        txttotal_h = parseFloat(txttotal_h) - parseFloat(due2);
                                        if (txttotal_h <= 0) {
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(3) input[type=text]').val("");
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html(due2);
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(6) span:eq(0)').html("0.00");
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + j + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
                                            txttotal_h = 0;
                                        }
                                    }
                                    len = (len - finetype);
                                }

                                for (var i = 0; i < len; i++) {
                                    var lblPayable = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                    var lbldisc = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                    var lblTotalc = (lblPayable - lbldisc).toFixed(2);
                                    var lblPaid = parseFloat($("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                    var totalDue = (lblTotalc - lblPaid).toFixed(2);
                                    if (totalDue > 0) {
                                        if (txttotal_h >= totalDue) {
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(totalDue);
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((lblTotalc - totalDue).toFixed(2));
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                            txttotal_h = txttotal_h - totalDue;
                                        }
                                        else if (txttotal_h < totalDue && txttotal_h > 0) {
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val(txttotal_h);
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((lblTotalc - txttotal_h).toFixed(2));
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalDue - txttotal_h).toFixed(2));
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                            txttotal_h = txttotal_h - totalDue;
                                            if (txttotal_h < 0) {
                                                txttotal_h = 0;
                                            }
                                        }
                                        else if (txttotal_h <= 0) {
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html(totalDue);
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html(totalDue);
                                            $("#" + childRowid).find('td:eq(0)').find('table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
                                            txttotal_h = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



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
                
        
        
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
