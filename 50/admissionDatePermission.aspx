<%@ Page Title="Admission Portal Date | eAM&#174;" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="admissionDatePermission.aspx.cs"
    Inherits="admissionDatePermission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                <div class="col-sm-4   mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" CssClass="validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15" runat="server" id="divControls" visible="false">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control-blue validatetxt datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15" runat="server" id="divControls1" visible="false">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtto" runat="server" CssClass="form-control-blue validatetxt datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divControls3" visible="false">
                                        <label class="control-label">Online Registration</label>
                                        <div class="">
                                             <asp:RadioButtonList ID="rdoOnlineRegistration" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-sussess">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <span style="font-size:11px; color:red;">Enable online student registration from admission portal.</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divControls4" visible="false">
                                        <label class="control-label">Receipt No. compulsory</label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoReceiptNocompulsory" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-sussess">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <span style="font-size:11px; color:red;">Admission Receipt No. compulsory for student registration at Admin Portal.</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-8  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" visible="false" style="padding:24px;" runat="server" id="divControls2">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="fromdate" runat="server" Text='<%# Bind("fromdate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="todate" runat="server" Text='<%# Bind("todate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Online Registration Enable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OnlineRegistration" runat="server" Text='<%# Eval("OnlineRegistration").ToString()=="True"?"Yes":"No" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt No. compulsory">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ReceiptNocompulsory" runat="server" Text='<%# Eval("ReceiptNocompulsory").ToString()=="True"?"Yes":"No" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Class Master" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>



                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                               
                                <tr>
                                    <td>From <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfrom0" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>To <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtto0" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Online Registration <span class="vd_red"></span>
                                    </td>
                                    <td>
                                             <asp:RadioButtonList ID="rdoOnlineRegistration0" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-sussess">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <span style="font-size:11px; color:red;">Enable online student registration from admission portal.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Receipt No. compulsory<span class="vd_red"></span>
                                    </td>
                                    <td>
                                         <asp:RadioButtonList ID="rdoReceiptNocompulsory0" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-sussess">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:RadioButtonList>
                                        <span style="font-size:11px; color:red;">admission receipt no. compulsory on student registration admin portal.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y"  OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update"
                                             />
                                        &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
