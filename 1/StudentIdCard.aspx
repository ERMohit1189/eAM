<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentIdCard.aspx.cs" Inherits="_1.StudentIdCard" %>

<%@ Register Src="~/admin/usercontrol/portidcard.ascx" TagPrefix="uc1" TagName="portidcard" %>
<%@ Register Src="~/admin/usercontrol/idcard.ascx" TagPrefix="uc" TagName="idcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <link rel="prefetch" href="../css/IDAutomationHC39M.ttf">
    <div id="loader" runat="server"></div>
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
        #Label1 {
            margin-left: -33px !important;
        }

        .port-logo-size {
            width: 60px !important;
            height: 60px !important;
        }

        .portrait {
            width: 200px !important;
            height: 400px !important;
        }

        .landscape {
            width: 400px !important;
            height: 200px !important;
        }

        .imgs {
            max-width: none !important;
        }

        .idtext {
            font-size: 11px !important;
            text-transform: uppercase !important;
        }
        .border-1 {border: 1px solid;}
    </style>

    <asp:UpdatePanel ID="fre" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  no-padding " style="border-bottom:1px solid #ccc;" runat="server" id="divtp">
                                 <div class="col-sm-4 ">
                                    <asp:RadioButtonList ID="reportType" runat="server" class="form-control-blue vd_radio radio-success" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="reportType_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">Card View Print</asp:ListItem>
                                        <asp:ListItem Value="1">Normal View Print</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue"
                                                        OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropBranch" runat="server" CssClass="form-control-blue validatedrp"
                                                        OnSelectedIndexChanged="DropBranch_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                         <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                        </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">S.R. No.&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropSrno" runat="server"
                                                        CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Layout&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlLayout" runat="server"
                                                        CssClass="form-control-blue" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Portrait" Selected="True">Portrait</asp:ListItem>
                                                        <asp:ListItem Value="Landscape">Landscape</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtFrom2">
                                    <label class="control-label">From (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel82" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYear2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonth2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDate2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtTo2" >
                                    <label class="control-label">To (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel92" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYearTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearTo2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonthTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthTo2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDateTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDateTo2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-6 half-width-50 mgbt-xs-15" runat="server" id="DisplayOrder" >
                                    <label class="control-label">Display Order</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value="Name" Selected="True">Alphabetical</asp:ListItem>
                                            <asp:ListItem Value="Id">Sequential</asp:ListItem>
                                            <asp:ListItem Value="InstituteRollNo">Roll No. Wise</asp:ListItem>
                                            <asp:ListItem Value="doa">Date of Admission</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-sm-1  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1"  OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" Style="margin-top: 25px;">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 58px"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <div style="float: right; font-size: 19px;">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" Visible="false">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>


                                                <script>
                                                    
                                                </script>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>

                                <div class="col-sm-12 " id="Panel1" runat="server">
                                    <div class="table-responsive2 table-responsive" id="gdv" runat="server">
                                        <div class="portraits" id="abc" runat="server" cellpadding="0">
                                            <style>
                                                #Label1 {
                                                    margin-left: -33px !important;
                                                }

                                                .imgs {
                                                    max-width: none !important;
                                                }

                                                .idtext {
                                                    font-size: 9px !important;
                                                    text-transform: uppercase !important;
                                                }

                                                style attribute {
                                                    font-size: 9px !important;
                                                    padding-left: 5px !important;
                                                }

                                                .tablePortRat > tbody > tr > td {
                                                    padding: 1px !important;
                                                    line-height: 1.6 !important;
                                                    vertical-align: middle !important;
                                                    font-size: 10px !important;
                                                    text-align:left !important;
                                                }
                                                .tablePortRat > tbody > tr > th {
                                                    padding: 1px !important;
                                                    line-height: 1.6 !important;
                                                    vertical-align: top !important;
                                                    font-size: 10px !important;
                                                    text-align:left !important;
                                                }
                                                .port-logo-size {
                                                    width: 100% !important;
                                                    height: initial !important;
                                                }
                                            </style>
                                            <asp:Repeater runat="server" ID="rptICard">
                                                <ItemTemplate>
                                                    <div class="border-1" style="line-height: 16px !important; border-radius: 5px; margin: 0px; margin-right:0px; margin-bottom:20px; width: 204px !important; height: 324px !important; page-break-after:always;">
                                                        <div class="col-md-12" style="margin-top: 5px; padding: 0 5px;">
                                                            <uc1:portidcard runat="server" ID="l" />
                                                        </div>
                                                        <div class="col-md-12 col-sm-12  idtext" style="margin:0px; padding: 5px;">
                                                            <div class="col-md-12 col-sm-12  no-mgpd text-center">
                                                             <div class="text-left" style="width:100%; float:left; text-align:center;position:relative;">
                                                                <h3 style="font-size: 11px; text-align: center; font-weight: bold; margin-top:0px; margin-bottom:4px; position:absolute;position: absolute; transform: rotate(90deg); margin-top: 28px; margin-left: 23px;">
                                                                    <asp:Label ID="lblsessions" runat="server"></asp:Label>
                                                                </h3>
                                                                <asp:Image ID="Image1" CssClass="mgbt-lg-5 imgcenter" runat="server" Height="70px" Width="70px" Style="border: 1px solid; display: inline-block !important; margin-top:0px; border-radius: 5px;" />
                                                                <asp:Image ID="Image2" CssClass="mgbt-lg-5 imgcenter" runat="server" ImageUrl='<%# Eval("PhotoPaths")  %>' Width="70px" Style="margin-top: -30px; inline-block !important" />
                                                                 <span style="padding-top: 28px;padding-left: 5px;font-size: 12px;font-weight: 600; position:absolute;right:24px;top:0;">  <asp:Label ID="lblBloodGroupValue" runat="server" Text='<%# Eval("BloodGroup") %>'></asp:Label></span>
                                                            </div>
                                                                <div class="text-center" style="width:59%; float:right;display:none;">
                                                                     <asp:Image runat="server" ID="imgs" CssClass="customfont imgs" Style="width: 90px;    margin-top: 30px;" />
                                                                </div>
                                                            </div>
                                                           <div class="col-md-12 col-sm-12  no-mgpd idtext" style="font-size: 11px; padding-left:3px !important;">
                                                                <div class="col-sm-4 no-mgpd hide">
                                                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="S.R. No."></asp:Label><span style="float: right;">:&nbsp;</span>
                                                                </div>
                                                                <div class="col-sm-8 no-mgpd hide">
                                                                    <asp:Label ID="Label16" runat="server" CssClass="barcode" Text='<%# Eval("SrNo")  %>'></asp:Label>
                                                                </div>
                                                                <table class="tablePortRat" style="width:100%;">
                                                                    <tr>
                                                                        <th style="width:86px;white-space:nowrap;"><asp:Label ID="Label17" runat="server" Text="Name" Font-Bold="True"></asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                        <td><asp:Label ID="Label20" runat="server" Text='<%# Eval("Name")  %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                       <th style="white-space:nowrap;display:flex; align-items:center;justify-content: space-between;"><asp:Label ID="Label18" runat="server" Font-Bold="True" Text="FATHER's NAME"> </asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                        <td><asp:Label ID="Label19" runat="server" Text='<%# Eval("FatherName")  %>'></asp:Label></td>
                                                                    </tr>
                                                                     <tr>
                                                                       <th style="white-space:nowrap;display:flex; align-items:center;justify-content: space-between;"><asp:Label ID="Label2" runat="server" Font-Bold="True" Text="MOTHER's NAME"> </asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                        <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("MotherName")  %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th><asp:Label ID="Label15s" runat="server" Text="Class" Font-Bold="True"></asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                        <td><asp:Label ID="Label23" runat="server" Text='<%# Eval("CombineClassName")  %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="white-space:nowrap;"><asp:Label ID="Label24" runat="server" Font-Bold="True">Phone No.</asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                        <td><asp:Label ID="Label25" runat="server" Text='<%# Eval("FatherContactNo")  %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <span style="font-weight:600;"><asp:Label ID="Label1" runat="server" Text="Address" Font-Bold="True"></asp:Label>&nbsp;</span>
                                                                            <asp:Label ID="Label1s" runat="server" Text='<%# Eval("Laddress")  %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="landscape" id="Div1" runat="server" cellpadding="0">
                                            <style>
                                                #Label1 {
                                                    margin-left: -33px !important;
                                                }
                                                .imgs {
                                                    max-width: none !important;
                                                }

                                                .idtext {
                                                    font-size: 9px !important;
                                                    text-transform: uppercase !important;
                                                }

                                                style attribute {
                                                    font-size: 9px !important;
                                                    padding-left: 5px !important;
                                                }

                                                .tablePortRat > tbody > tr > td {
                                                    padding: 1px !important;
                                                    line-height: 1.5 !important;
                                                    vertical-align: middle !important;
                                                    font-size: 9px !important;
                                                    text-align:left !important;
                                                }
                                                .tablePortRat > tbody > tr > th {
                                                    padding: 1px !important;
                                                    line-height: 1.5 !important;
                                                    vertical-align: top !important;
                                                    font-size: 9px !important;
                                                    text-align:left !important;
                                                }
                                                .port-logo-size {
                                                    width: 100% !important;
                                                    height: initial !important;
                                                }
                                            </style>
                                            <asp:Repeater runat="server" ID="rptICardLandscape">
                                                <ItemTemplate>
                                                    <div class="border-1" style="height:212px !important; width: 326px !important; float:left; padding: 3px; margin-bottom:50px; margin-right:30px; border-radius: 5px;">
                                                        <div class="col-md-12" style="margin-top: 3px; padding: 0 2px;">
                                                            <uc:idcard runat="server" ID="idcard" />
                                                        </div>
                                                         <div class="col-md-12 col-sm-12  idtext" style="margin: 0; padding:2px 5px;">
                                                                        <div class="no-mgpd text-left" style="width: 22%; float:left;    position: relative; vertical-align: top; text-align: center; padding: 0px !important; padding-top: 0px !important;">
                                                                           <h3 style="font-size: 11px; text-align: center; margin-bottom: 3px; font-weight: bold; margin-top: 0px !important; padding-top: 0px !important;">
                                                                            <asp:Label ID="Label1ss" runat="server">
                                                                                <asp:Label ID="lblsession" runat="server"></asp:Label></asp:Label>
                                                                        </h3>
                                                                        <asp:Image ID="Image1" CssClass="mgbt-lg-5 imgcenter" runat="server" Width="80px"  Style="height:80px; border: 1px solid; margin-top: 2px; border-radius: 5px;" />
                                                                        <asp:Image ID="Image2" CssClass="mgbt-lg-5 imgcenter" runat="server" Width="80px" Style="height:80px;position:absolute;" />
                                                                        <p style="margin-top:0px;font-size: 12px;font-weight: 600;text-align:center;position: relative;display: none;justify-content: center;width: 100%;">  <asp:Label ID="lblBloodGroupValue1" runat="server" Text='<%# Eval("BloodGroup") %>'></asp:Label></p>
                                                                        <div class="text-center" style="position:relative;padding-top: 13px;">
                                                                            <span style="position:absolute;top:-6px;left: 20px;max-width: 40px;"><img src="../img/priya sign.jpg" /></span>
                                                                            <p>Principal</p>
                                                                        </div>
                                                                    </div>
                                                                    <div class="no-mgpd idtext" style="line-height: 14px !important; float:right; width: 78%; padding-left:6px !important; vertical-align: top;">
                                                                        <div class="col-md-3 col-sm-3 col-xs-3 no-mgpd hide">
                                                                            <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="S.R. No."></asp:Label><span style="float: right;">:&nbsp;</span>
                                                                        </div>
                                                                        <div class="col-md-9 col-sm-9 col-xs-9 no-mgpd hide">
                                                                            <asp:Label ID="Label16" runat="server" CssClass="barcode" Text='<%# Eval("SrNo")  %>'></asp:Label>
                                                                        </div>
                                                                        <table class="tablePortRat" style="width:100%;">
                                                                         <tr>
                                                                             <th style="width:86px;white-space:nowrap;">Sr No.</th>
                                                                             <td>231243243</td>
                                                                         </tr>
                                                                        <tr>
                                                                            <th style="width:86px;"><asp:Label ID="Label17" runat="server" Text="Name" Font-Bold="True"></asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                            <td style="white-space:nowrap;"><asp:Label ID="Label20" runat="server" Text='<%# Eval("Name")  %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                           <th style="white-space:nowrap; display:flex; align-items:center;justify-content: space-between;"><asp:Label ID="Label18" runat="server" Font-Bold="True" Text="FATHER's NAME"> </asp:Label><span style="float: right;">:&nbsp;</span></th>
 
                                                                            <td style="white-space:nowrap;"><asp:Label ID="Label19" runat="server" Text='<%# Eval("FatherName")  %>'></asp:Label></td>
                                                                        </tr>
                                                                           
                                                                         <tr>
                                                                           <th style="white-space:nowrap; display:flex; align-items:center;justify-content: space-between;"><asp:Label ID="Label2" runat="server" Font-Bold="True" Text="MOTHER's NAME"> </asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                            <td style="white-space:nowrap;"><asp:Label ID="Label3" runat="server" Text='<%# Eval("MotherName")  %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><asp:Label ID="Label15s" runat="server" Text="Class" Font-Bold="True"></asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                            <td><asp:Label ID="Label23" runat="server" Text='<%# Eval("CombineClassName")  %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th style="white-space:nowrap;"><asp:Label ID="Label24" runat="server" Font-Bold="True">Phone No.</asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                            <td><asp:Label ID="Label25" runat="server" Text='<%# Eval("FatherContactNo")  %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><asp:Label ID="Label1" runat="server" Text="Address" Font-Bold="True"></asp:Label><span style="float: right;">:&nbsp;</span></th>
                                                                            <td><asp:Label ID="Label1s" runat="server" Text='<%# Eval("Laddress")  %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr style="display:none;">
                                                                            <td colspan="2" class="no-mgpd text-left">
                                                                                <asp:Image runat="server" ID="imgs" CssClass="customfont imgs" Width="85px" />
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                      
                                                                    </div>
                                                         </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
            <script src="../js/angularjs/jquery.qrcode.min.js"></script>
            <script>
                $(document).ready(function () {
                    $("#btn").click(function () {
                        $('#output').qrcode($("#txtCode").val());
                    });
                });
                jQuery(function () {
                    jQuery('#output').qrcode("pns");
                });
            </script>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
