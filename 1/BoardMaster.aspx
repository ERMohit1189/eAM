<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BoardMaster.aspx.cs" Inherits="_1.AdminBoardMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        document.onkeyup = Esc;
        function Esc() {
            var keyId = event.keycode;
            if (keyId === 27) {
                if (window.$find("Panel1_ModalPopupExtender")) {
                    window.$find("Panel1_ModalPopupExtender").hide();
                }
            }
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="maindiv" runat="server">
                                    <div class="col-sm-12 no-padding" runat="server" id="table1">
                                        <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                    <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" CssClass="validatedrp" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                     <%--   <div class="col-sm-2   mgbt-xs-15" id="divSession" runat="server">
                                            <label class="control-label">Session</label>
                                            <div class="">
                                                <asp:DropDownList runat="server" ID="DrpSessionName" CssClass="validatedrp"></asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Board/ University&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                            <label class="control-label">Remark</label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-9">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" ValidationGroup="a">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        </div>


                                    </div>
                                    <div class="col-sm-12  ">
                                        <div class="table-responsive2  table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" OnPreRender="GridView1_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board/ University">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text="Board/ University"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BoardName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Board/ University" 
                                                                OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
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
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Board/ University"></asp:Label><span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Remark </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"
                                    OnClick="LinkButton4_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;  &nbsp;
                                                     <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False"
                                                         OnClick="LinkButton5_Click" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>

                        </tr>
                    </table>
                    <asp:Button ID="Button9" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                        CancelControlID="LinkButton5"
                        PopupControlID="Panel1" TargetControlID="Button9" BackgroundCssClass="popup_bg" Drag="True" BehaviorID="Panel1_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>

                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" CausesValidation="False" />
                            </td>
                        </tr>

                    </table>
                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                        CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                        PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

