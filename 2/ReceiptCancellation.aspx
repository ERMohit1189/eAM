<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ReceiptCancellation.aspx.cs"
    Inherits="_2.AdminReceiptCancellation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtStudentEnter]").autocomplete({
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
        .borders {
            border: 1px solid red !important;
        }

        .borders2 {
            border: 1px solid #D5D5D5;
        }

        .blink {
            animation-duration: 1200ms;
            animation-name: blink;
            animation-iteration-count: infinite;
            animation-direction: alternate;
            -webkit-animation: blink 1200ms infinite; /* Safari and Chrome */
        }

        @keyframes blink {
            from {
                color: yellow;
            }

            to {
                color: red;
            }
        }

        @-webkit-keyframes blink {
            from {
                color: yellow;
            }

            to {
                color: red;
            }
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Module&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpTables" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="DrpTables_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Composit Fee</asp:ListItem>
                                                <asp:ListItem Value="2">Other Fee</asp:ListItem>
                                                <asp:ListItem Value="3">TC Fee</asp:ListItem>
                                                <asp:ListItem Value="4">CC Fee</asp:ListItem>
                                                <asp:ListItem Value="5">Admission Form Fee</asp:ListItem>
                                                <asp:ListItem Value="6">Additional Fee</asp:ListItem>
                                                <asp:ListItem Value="7">Product Fee</asp:ListItem>
                                                <asp:ListItem Value="8">Library Fine</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                       
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divSr">
                                         <label class="control-label">Enter Name/ S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtStudentEnter" placeholder="Enter Name/ S.R. No." OnTextChanged="txtStudentEnter_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divReceipt" visible="false">
                                         <label class="control-label">Enter Receipt No.&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtReceipt" placeholder="Enter Receipt No."  runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="padding-top:26px;">
                                        <asp:LinkButton ID="lnkShow" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkShow_Click"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px"></div>
                                    </div>
                                    <div class="col-sm-12   mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-danger blink">Note:- It will delete only on page discount of a particular Receipt No. and receipt will be cancelled.</span>
                            </div>

                                </div>

                                <div class="col-sm-12 " id="divExport" runat="server">
                                    <div class="table-responsive2 table-responsive" id="abc" runat="server">
                                        <table style="width: 100%;" runat="server" id="grdshow" visible="False" class="mgbt-xs-15">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("combineClassName") %>'></asp:Label>
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
                                                    </asp:GridView>
                                                </td>
                                                 <td class="tab-top tab-profile text-center onprint" runat="server" id="divimg" visible="false">
                                                     <div class="gallery-item fee-pic-box">
                                                         <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                             <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                         </asp:HyperLink>
                                                         <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                     </div>
                                                 </td>
                                                <%--<td class="tab-top tab-profile text-center " runat="server" id="divimg" visible="false">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                            </asp:HyperLink>
                                                        </div>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>--%>
                                            </tr>
                                        </table>
                                        <div class="col-sm-12  no-padding mgtp-15">
                                            <asp:Label runat="server" ID="lblHeading1" Style="font-weight: bold;" Visible="false">Fee Deposit</asp:Label>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sn" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="LabelSrNo" runat="server" CssClass="hide" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deposit Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("DepositDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label29" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mode">
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label38" runat="server" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labelmop" runat="server" Text='<%# Bind("mop") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paid Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpaid" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="fTotal" runat="server" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                          <asp:TextBox ID="txtboxremark" runat="server" Visible="false" MaxLength="250"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("ReceiptNo") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                title="Cancel"  class="btn menu-icon vd_bd-red vd_red" Visible="false">
                                                            <i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td>
                                                        <h4>Are you sure you want to cancel this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
                                                        <asp:Label ID="lblSrNovalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" CausesValidation="false" OnClick="btnDelete_Click">Yes</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Label ID="cancelBal" runat="server" Style="display: none"></asp:Label>
                                        <asp:Label ID="Mode" runat="server" Style="display: none"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                                            CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="Label2">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>
            <%--<div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td style="text-align: center;">
                                <h4>Are you sure you want to cancel this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSrNovalue" runat="server" Visible="False"></asp:Label>
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" Text="" Visible="false" />
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button8_Click" Text="No" />
                                &nbsp;  &nbsp;
                                                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" CssClass="button-y" OnClick="ButtonCancel_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button3" runat="server" Style="display: none" />
                </asp:Panel>
                 <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button1" DynamicServicePath=""
                        Enabled="True" PopupControlID="Panel1" TargetControlID="Button8" BackgroundCssClass="popup_bg">
                 </ajaxToolkit:ModalPopupExtender>
            </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>
