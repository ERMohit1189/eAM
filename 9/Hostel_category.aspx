<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Hostel_category.aspx.cs" Inherits="admin_Hostel_category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Can't leave blank!"
                                                SetFocusOnError="True" ValidationGroup="a" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark&nbsp;</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 85px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
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





            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        
                                    
                                    <tr>
                                        <td >Category Name </td>
                                        <td >
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox>
                                            <asp:Button ID="Button9" runat="server" Style="display: none" />
                                            <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                                CancelControlID="LinkButton5" PopupControlID="Panel1" TargetControlID="Button9">
                                            </asp:ModalPopupExtender>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td>Remark </td>
                                        <td >
                                            <asp:TextBox ID="TextBox4" runat="server" SkinID="txtmulti" CssClass="form-control-blue"  Rows="1"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td style="padding-left: 5px; text-align: left">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button-y" CausesValidation="False"
                                                OnClick="LinkButton4_Click">Update</asp:LinkButton>
                                            &nbsp;
                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-n" CausesValidation="False"
                                        OnClick="LinkButton5_Click">Cancel</asp:LinkButton>
                                            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="trgap1" colspan="2"></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td><h4>Are you sure you want to delete this?<asp:Label ID="lblvalue"
                                runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                                    CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                                    PopupControlID="Panel2" TargetControlID="Button7">
                                </asp:ModalPopupExtender></h4>
                            </td>
                        </tr>

                        <tr>
                            <td >
                                <asp:Button ID="Button8" CssClass="button-n" runat="server" CausesValidation="False"
                                    OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="btnDelete_Click" Text="Yes" />


                            </td>
                        </tr>

                    </table>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

