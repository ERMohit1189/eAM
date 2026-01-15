<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Student_Information.aspx.cs" Inherits="_1.StudentInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=TxtEnter]").autocomplete({
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
    <script>
        function updown1(control) {
            if (control.className === "btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive") {
                control.className = "btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 active";
            }
            else {
                control.className = "btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive";
            }
        }

        function updown2() {

            var element = document.querySelectorAll(".actionhistory");

            for (var i = 0; i < element.length; i++) {
                var inputtab = element[i].getElementsByTagName('input');
                var spantab = element[i].getElementsByTagName('span');

                for (var j = 0; j < inputtab.length; j++) {
                    if (inputtab[j].value === -1) {
                        if (spantab.length > 0) {
                            spantab[0].className = "btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive";
                        }

                    }
                    else {
                        if (spantab.length > 0) {
                            spantab[0].className = "btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 active";
                        }
                    }
                }
            }
        }
        Sys.Application.add_load(updown2);

        function showfee(tis) {

            if ($(tis).closest('tr').find('td:eq(0)').find('span:eq(0)').html() == "Arrear") {
                $(tis).closest('td').find('.box').addClass('hide');
            }
            else {
                $(tis).closest('td').find('.box').removeClass('hide');
            }
        }
        function hidefee(tis) {
            $(tis).closest('td').find('.box').addClass('hide');
        }
        function showfine(tis) {
            if ($.trim($(tis).closest('td').find('div').html()) != "") {
                if (parseFloat($(tis).html()) > 0 && $(tis).closest('tr').find('td:eq(0)').find('span:eq(0)').html() != "Arrear") {
                    $(tis).closest('td').find('div').removeClass('hide');
                }
                else {
                    $(tis).closest('td').find('div').removeClass('hide');
                }
            }
        }
        function hidefine(tis) {
            $(tis).closest('td').find('div').addClass('hide');
        }
        function showdiscount(tis) {
            if ($.trim($(tis).closest('td').find('div').html()) != "") {
                if (parseFloat($(tis).html()) > 0 && $(tis).closest('tr').find('td:eq(0)').find('span:eq(0)').html() != "Arrear") {
                    $(tis).closest('td').find('div').removeClass('hide');
                }
                else {
                    $(tis).closest('td').find('div').addClass('hide');
                }
            }
        }
        function hidediscount(tis) {
            $(tis).closest('td').find('div').addClass('hide');
        }
    </script>
    <style>
        .chk input[type=checkbox] {
            vertical-align: middle !important;
        }

        .box {
            min-width: 100px;
            display: block;
            position: absolute;
            background: #DA4448;
            color: white;
            padding: 5px;
            /*border: 1px solid black;*/
            text-align: left;
            z-index: 99999;
            border-radius: 10px;
        }
        /*.box table>tbody>tr>td:last-child {
                text-align:right !important;
            }*/

        input.underlined {
            padding: 0px 0px 0px !important;
            margin-bottom: 1px !important;
        }

        .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 3px 5px !important;
        }

        .vd_body {
            float: left;
            width: 100%;
            overflow: auto !important;
        }
        .slide-u-d {
    padding: 5px 15px 5px 15px;
    border-top: 1px solid rgba(255, 255, 255, 0.2);
    border-bottom: 1px solid rgba(0, 0, 0, 0.15);
    position: relative;
    background-color: #C4FFC6 !important;
    color: #393a3e;
    margin: 0px;
    cursor: pointer;
    text-transform: uppercase;
    font-size: 14px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <script>
                    Sys.Application.add_load(getStudentsList);
                    
                    Sys.Application.add_load(prettyphoto);
                    Sys.Application.add_load(disablebtn);
                </script>

                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4   mgbt-xs-15">
                                        <div class="vd_input-wrapper">
                                            <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                            <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server"
                                                class="form-control-blue width-100 validatetxt" AutoPostBack="true" OnTextChanged="TxtEnter_OnTextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfStudentId" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 ">
                                        <div class="col-sm-6 col-xs-6 no-padding text-left mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="button form-control-blue" OnClick="LinkButton1_OnClick"> View</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6 col-xs-6 no-padding">
                                            <div id="msgbox" runat="server" style="left: 60px;"></div>
                                        </div>
                                    </div>
                                </div>

                                <div id="div1" runat="server" class="col-sm-12 " visible="False">
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
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
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
                                                <td class="tab-top tab-profile text-center ">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 56px;" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="2">
                                                    <div id="divmsgrecord" runat="server" style="left: 60px;"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                </div>

                                <div class="col-sm-12   vd_input-margin " runat="server" visible="False" id="div2">
                                    <div class="tabs">
                                        <ul class="nav nav-tabs nav-justified">
                                            <li class="active"><a href="#1" data-toggle="tab"><span class="menu-icon">
                                                <i class=" fa  fa-rupee"></i></span>&nbsp; Fee<span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                            <li runat="server" id="liTransportTab"><a href="#3" data-toggle="tab"><span class="menu-icon">
                                                <i class="fa fa-bus"></i></span>&nbsp; Transport <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li runat="server" id="liHostalTab"><a href="#4" data-toggle="tab"><span class="menu-icon">
                                                <i class="fa fa-building"></i></span>&nbsp; Hostel <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li><a href="#5" data-toggle="tab"><span class="menu-icon">
                                                <i class=" fa  fa-book"></i></span>&nbsp; Library <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li><a href="#2" data-toggle="tab">
                                                <span class="menu-icon"><i class="fa fa-pencil-square-o"></i></span>&nbsp; Attendance <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                            <li><a href="#6" data-toggle="tab"><span class="menu-icon">
                                                <i class="fa fa-star"></i></span>&nbsp; Review <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                        </ul>
                                        <div class="tab-content form-box-border">

                                            <div class="tab-pane active " id="1">
                                                <div class="row mgbt-xs-20">
                                                    <div class="col-sm-12 col-xs-12">
                                                        <ajaxToolkit:Accordion ID="Accordion2" runat="server" Width="100%" SuppressHeaderPostbacks="true">
                                                            <HeaderTemplate>
                                                                <h6 class="slide-u-d">
                                                                    <%--<span class="btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive"
                                                                                  title="History" 
                                                                                  data-placement="top" id="updown" onclick="updown1(this);">--%>
                                                                    <i class="fa fa-sort-amount-asc" style="padding: 0 10px 0 0"></i>
                                                                    <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("SessionName") %>'></asp:Label>
                                                                </h6>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div class="col-sm-12 no-padding">
                                                                    <br />
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
                                                                                            <asp:CheckBox runat="server" ID="chkInstallment" CssClass="vd_check check-success hide" Style="height: 17px;" onchange="MainHeadCheck(this);" />
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
                                                                                            <asp:TextBox runat="server" ID="txtInstallmentdiscount" class="form-control-blue hide" placeholder="0.00" Style="text-align: right !important; height: 20px;" onblur="MainHeadDiscount(this);"></asp:TextBox>
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
                                                                                            <asp:TextBox runat="server" ID="txtInstallmentPayable" class="form-control-blue hide" placeholder="0.00" Text='<%# Eval("DueAmount").ToString()=="0.00"?"":Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;" onblur="MainHeadPayable(this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="text-right lasttd" style="line-height: 1;">
                                                                                            <asp:Label runat="server" ID="lblInstallmentDue" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                            <asp:Label runat="server" ID="lblhideInstallmentDue" class="hide" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="text-right lasttd" style="line-height: 1;">
                                                                                            <asp:Label runat="server" ID="lblrecuring" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="line-height: 1;" class="hidden-print">
                                                                                            <asp:TextBox runat="server" ID="txtInstallmentNarration" class="form-control-blue hide" TextMode="MultiLine" Style="height: 29px; color: #000;" onblur="Naretion(this);"></asp:TextBox>
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
                                                                                                                <asp:CheckBox runat="server" ID="chkInstallmentFee" CssClass="vd_check check-success hide chks_c" Style="height: 17px;" onchange="ChildCheck(this);" />
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
                                                                                                                <asp:TextBox runat="server" ID="txtFeeHeadDiscount" class="form-control-blue hide" placeholder="0.00" onblur="ChildDiscount(this);" Style="cursor: pointer !important text-align: right !important; height: 20px;"></asp:TextBox>
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
                                                                                                                <asp:TextBox runat="server" ID="txtFeeHeadPayable" class="form-control-blue hide" placeholder="0.00" onblur="ChildPayable(this);" Text='<%# Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                                                <asp:Label runat="server" ID="lblFeeHeadBalanceShow" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                                                <asp:Label runat="server" ID="lblFeeHeadBalance" class="hide vl" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td style="text-align: left !important; vertical-align: middle !important; line-height: 1;" colspan="2" class="hidden-print">
                                                                                                                <asp:TextBox runat="server" ID="txtFeeHeadNarration" class="form-control-blue hide" TextMode="MultiLine" Style="height: 29px; color: #000;"></asp:TextBox>
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
                                                                                                                        NavigateUrl='<%# "../2/FeeReceiptAllDuplicate.aspx?IsIfo=1&&RecieptSrNo="+Eval("ReceiptNo").ToString().Replace("/","__")+"$"+Eval("SessionName").ToString()+"$"+Eval("BranchCode").ToString() %>' Target="_blank">
                                                                                                                    </asp:HyperLink>
                                                                                                                    <asp:HyperLink ID="HylnkReceiptNoT2" runat="server" Visible="false" CssClass="onprint" Style="color: #0019cc;" Text='<%# Bind("ReceiptNo") %>'
                                                                                                                        NavigateUrl='<%# "../2/FeeReceiptAllT2.aspx?RecieptSrNo="+Eval("ReceiptNo").ToString().Replace("/","__")+"$"+Eval("SessionName").ToString()+"$"+Eval("BranchCode").ToString() %>' Target="_blank">
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
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblInstallmentFee_total"></asp:Label></th>
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblDisvount_total"></asp:Label></th>
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblTotal_total"></asp:Label></th>
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblPaid_total"></asp:Label></th>
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblBalance_total"></asp:Label></th>
                                                                                <th class="text-right" style="width: 9%">
                                                                                    <asp:Label runat="server" ID="lblConsolidated_total"></asp:Label></th>
                                                                                <th class="text-left hidden-print" style="width: 24%"></th>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:Accordion>
                                                    </div>

                                                    
                                                </div>
                                            </div>






                                            <div class="tab-pane " id="3">
                                                <div class="row mgbt-xs-20" runat="server" id="TransportTab">
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="GridTransportAllot" runat="server"  AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Route Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="RouteName" Text='<%# Bind("locationName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="VehicleType" Text='<%# Bind("VehicleType") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vehicle No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="VehicleNo" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Driver">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="Driver" Text='<%# Bind("Driver") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Driver Contact No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="driverContact" Text='<%# Bind("driverContact") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="4">
                                                <div class="row mgbt-xs-20" runat="server" id="HostalTab">
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="GridHostal" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Bed No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblbedid" Text='<%# Bind("bedid") %>' CssClass="hide"></asp:Label>
                                                                        <asp:Label runat="server" ID="lblRoomAllotmentId" Text='<%# Bind("RoomAllotmentId") %>' CssClass="hide"></asp:Label>
                                                                        <asp:Label runat="server" ID="txtBedNo" Text='<%# Bind("allotedRoom") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Duration">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="txtFrom" Text='<%# Bind("DateFrom", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                                        -To-
                                                    <asp:Label runat="server" ID="txttO" Text='<%# Bind("DateTo", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                                        <br />
                                                                        <label style="color: #db0000;">
                                                                            (<asp:Label runat="server" ID="txtMonths" Text='<%# Bind("TotalMonths") %>'></asp:Label>
                                                                            Momths)</label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="115px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Charges">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="txtPrice" Text='<%# Bind("BedCharge") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="70px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="txtTotal" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Booking Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="txtBookingStatus" CssClass="label label-success" Visible="false"><i class="fa fa-check"></i> Occupied</asp:Label>
                                                                        <asp:Label runat="server" ID="txtBookingUnavailable" CssClass="label label-warning" Visible="false"><i class="fa fa-close"></i> Released</asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="5">
                                                <div class="row mgbt-xs-20">
                                                    <div class="col-sm-12 ">
                                                        <div class="table-responsive2 table-responsive">
                                                            <asp:GridView ID="grdDocList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Accession No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAccession" runat="server" Text='<%# Bind("accessionno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Title">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Issue Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIssue" runat="server" Text='<%# Bind("IssueDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Return Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReturn" runat="server" Text='<%# Bind("ReturnDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Return On">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReturnOn" runat="server" Text='<%# Bind("returnon") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Is Returned">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReturnOn" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="2">
                                                <div class="row mgbt-xs-20">
                                                    <div class="col-sm-6 col-xs-6">
                                                        <ajaxToolkit:Accordion ID="Accordion1" runat="server" Width="100%" SuppressHeaderPostbacks="true">
                                                            <HeaderTemplate>
                                                                <h6 class="slide-u-d">
                                                                    <%--<span class="btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive"
                                                                                  title="History"
                                                                                  data-placement="top" id="updown" onclick="updown1(this);">--%>
                                                                    <i class="fa fa-sort-amount-asc" style="padding: 0 10px 0 0"></i>
                                                                    <%--</span>--%>
                                                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MonthName") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YearName") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblMonthYear" runat="server" Text='<%# Eval("MonthYearName") %>'></asp:Label>
                                                                </h6>

                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                                    CssClass="table table-striped table-hover no-head-border table-bordered">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                                (<asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'></asp:Label>)
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                            <FooterTemplate>
                                                                                Total 
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Attendance">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAttendance" runat="server" Text='<%# Bind("Attendance") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                Present:&nbsp;<asp:Label ID="lblTotalPrs" runat="server" Text=""></asp:Label>, &nbsp;
                                                                                Absent:&nbsp;<asp:Label ID="lblTotalAb" runat="server" Text=""></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:Accordion>
                                                    </div>
                                                    <div class="col-sm-6 col-xs-6">
                                                        <div class="col-sm-12 col-md-12 col-lg-12  full-w-1280 no-padding sp-d-box1">
                                                            <div id="W3" runat="server"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="6">
                                                <div class="row mgbt-xs-20">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12  " id="divreport" runat="server" visible="False">
                                                                <div class="col-sm-12  ">
                                                                    <div id="w1" runat="server"></div>
                                                                </div>
                                                                <div class="col-sm-12 ">
                                                                    <div class=" table-responsive  table-responsive2">
                                                                        <asp:GridView ID="grdDownloadedPunch" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group" ShowFooter="true">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblsr" runat="server" Text="#"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsrnos" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Session">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMachineId" runat="server" Text='<%# Bind("SessionName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Review">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblIntime" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Username">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOuttime" runat="server" Text='<%# Bind("LoginUserName") %>'></asp:Label><br />
                                                                                        (<asp:Label ID="Label1" runat="server" Text='<%# Bind("CurDates") %>'></asp:Label>)
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <HeaderStyle CssClass="grid_heading_default" />
                                                                            <RowStyle CssClass="grid_details_default" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style>
        .unactive {
            -webkit-transform: rotate(180deg);
            -moz-transform: rotate(180deg);
            -o-transform: rotate(180deg);
            -ms-transform: rotate(180deg);
            transform: rotate(180deg);
            transition: all .3s ease 0s;
            transition: all .3s ease 0s;
        }

        .active {
            -webkit-transform: rotate(0deg);
            -moz-transform: rotate(0deg);
            -o-transform: rotate(0deg);
            -ms-transform: rotate(0deg);
            transform: rotate(0deg);
            transition: all .3s ease 0s;
            transition: all .3s ease 0s;
        }
    </style>
    <style>
        a.pp_next {
            display: none !important;
        }

        a.pp_previous {
            display: none !important;
        }

        div.light_square .pp_gallery a.pp_arrow_previous, div.light_square .pp_gallery a.pp_arrow_next {
            display: none !important;
        }

        .pp_gallery div {
            display: none !important;
        }
    </style>
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
            background-color: #fff;
            border: 1px solid #000;
        }

            .btns :hover {
                color: #FF0000;
                cursor: pointer;
            }
    </style>
    <script>
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
</asp:Content>

