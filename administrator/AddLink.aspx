<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLink.aspx.cs" Inherits="admin_AddMenuPages" MasterPageFile="~/Administrator/administrato_root-manager.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
// ReSharper disable once UnusedParameter
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
               // document.getElementById("errorMsg").style.display = "inline"; // Show error message
                return false;
            }
            //document.getElementById("errorMsg").style.display = "none"; // Hide error message
            return true;
        }

        function validateInput(input) {
            input.value = input.value.replace(/[^0-9]/g, ''); // Remove non-numeric characters
        }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                
                Sys.Application.add_load(scrollbar);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Menu Type &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                                                 <asp:ListItem Value="-1"><--Select--></asp:ListItem>
                                                <asp:ListItem Value="0">Parent</asp:ListItem>
                                                 <asp:ListItem Value="1">Child</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="MenuParentdiv" visible="false">
                                        <label class="control-label">Menu Parent &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                 <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="Div1" visible="false">
                                        <label class="control-label">Sub Menu Parent &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                 <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>

                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Menu Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtclassname" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Menu Url</label>
                                        <div class="">
                                            <asp:TextBox ID="txtclassCode" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                        </div>
                                    </div>
                                    
                                    

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                            
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Text" HeaderText="Menu Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                               
                                                <asp:TemplateField HeaderText="Menu Parent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ParentMenuName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Menu Url" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Url") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("MenuID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("MenuID") %>' Visible="false"></asp:Label>
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
                                    <asp:HiddenField ID="MenuIDMainEdit" runat="server" Value="0" />
                                    <td>Menu Parent <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="drpPanelCourse" runat="server" CssClass="form-control-blue validatedrpnew" AutoPostBack="true" OnSelectedIndexChanged="drpPanelCourse_SelectedIndexChanged">
                                                 <asp:ListItem Value="-1"><--Select--></asp:ListItem>
                                                <asp:ListItem Value="1">Parent</asp:ListItem>
                                                 <asp:ListItem Value="2">Child</asp:ListItem>
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="MenuParentEditdiv" runat="server" visible="false">
                                    <td>Menu Parent <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                      <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrpnew" 
                                          OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true">
                                                 <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                            </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                                 <tr id="Tr1" runat="server" visible="false">
                                    <td>Sub Menu Parent <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                      <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control-blue">
                                                 <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                              

                                <tr>
                                    <td>Menu Name <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClassNamePanel" runat="server" CssClass="form-control-blue validatetxtNew" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Menu Url 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClassCodePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                            
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnHiddenShow" runat="server" Style="display:none;" />
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClientClick="ValidateTextBox('.validatetxtNew');ValidateDropdown('.validatedrpnew');return validationReturn();"  OnClick="Button3_Click"  Text="Update"
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


                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" 
    TargetControlID="btnHiddenShow"
    PopupControlID="Panel1"
    CancelControlID="Button4"
    BackgroundCssClass="popup_bg" 
    BehaviorID="Panel1_ModalPopupExtender_Close">
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
