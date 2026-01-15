<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="vehicle_details.aspx.cs" Inherits="vehicle_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(scrollbar);
                
            </script>
           
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                             <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpVehicleType" runat="server" AutoPostBack="True" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                            </ContentTemplate>
                                                 </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Reg. No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtRegistrationNo" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnTextChanged="txtRegistrationNo_TextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                     <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Reg. Expiry&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                    <asp:TextBox ID="txtRegistrationExpiry" runat="server" CssClass="form-control-blue datepicker-normal" onchange="RegistrationExpiry(this)"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnRegistrationExpiry" />
                                             </div>
                                        
                                    </div>
                                            </ContentTemplate>
                                         </asp:UpdatePanel>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Reg. Expiry Notify&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                                <asp:TextBox ID="txtRegExpiryNotifyBefore" runat="server" CssClass="form-control-blue"  MaxLength="2" Text="15" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                             </div>

                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Body Type</label>
                                        <div class="">
                                            <asp:TextBox ID="txtBodyType" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Seat Capacity</label>
                                        <div class="">
                                            <asp:TextBox ID="txtSeatCapacity" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Model</label>
                                        <div class="">
                                            <asp:TextBox ID="txtModel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Made</label>
                                        <div class="">
                                            <asp:TextBox ID="txtMade" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fuel Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpfuletype" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                 <asp:ListItem Value="0">In Active</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Engine No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtEngineNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Chasis No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtChasis" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle CC</label>
                                        <div class="">
                                            <asp:TextBox ID="txtVehicleCC" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">MFG.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMFG" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Dealer&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpagency" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                OnSelectedIndexChanged="drpagency_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Driver Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtdriver" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Driver Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtdriverContact" runat="server" CssClass="form-control-blue" MaxLength="10" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Insurance No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtInsuranceNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Insurance Expiry&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtInsuranceExpiry" runat="server" CssClass="form-control-blue  datepicker-normal" onchange="InsuranceExpiry(this)"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField runat="server" ID="hdnInsuranceExpiry" />
                                            </div>
                                        </div>
                                    </div>
                                            </ContentTemplate>
                                         </asp:UpdatePanel>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Insurance Expiry Notify&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                                <asp:TextBox ID="txtInsuranceNotifyBefore" runat="server" CssClass="form-control-blue"  MaxLength="2" Text="15" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                             </div>

                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pollution Receipt No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPollutionReceiptNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="u">
                                        <ContentTemplate>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pollution Expiry&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPollutionExpiry" runat="server" CssClass="form-control-blue   datepicker-normal" onchange="PollutionExpiry(this)"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField runat="server" ID="hdnPollutionExpiry" />
                                            </div>
                                        </div>
                                    </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pollution Expiry Notify&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                                <asp:TextBox ID="txtPollutionNotifyBefore" runat="server" CssClass="form-control-blue"  MaxLength="2"  Text="15" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                             </div>

                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtVehicleRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                                     <script>
                                                         Sys.Application.add_load(datetime);
                            </script>


                                </div>


                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fuel Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtfueltype" runat="server" Text='<%# Bind("FuelType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dealer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnerName" runat="server" Text='<%# Bind("OwnerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Driver Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Driver") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Chasis No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label61" runat="server" Text='<%# Bind("ChasisNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label651" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" title="Edit" 
                                                            OnClick="LinkButton3_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click1" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
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






            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Vehicle Type </td>
                                    <td>
                                        <asp:DropDownList ID="drpvehicletype1" CssClass="form-control-blue" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="Button9" runat="server" Style="display: none" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Registration No. </td>
                                    <td>
                                        <asp:TextBox ID="txtRegistrationNo0" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Registration Expiry</td>
                                    <td>
                                        <asp:TextBox ID="txtRegistrationExpiry1" runat="server" CssClass="form-control-blue datepicker-normal" onchange="RegistrationExpiry1(this)" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnRegistrationExpiry1" />
                                    </td>
                                     </tr>
                                 <tr>
                                     <td>Reg. Expiry Notify </td>
                                    <td>
                                        <asp:TextBox ID="txtRegExpiryNotifyBefore1" runat="server" CssClass="form-control-blue"  MaxLength="2" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                        <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vehicle No. </td>
                                    <td>
                                        <asp:TextBox ID="txtNo0" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Model</td>
                                    <td>
                                        <asp:TextBox ID="txtModel0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Body Type </td>
                                    <td>
                                        <asp:TextBox ID="txtBodyType0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Seat Capacity </td>
                                    <td>
                                        <asp:TextBox ID="txtSeatCapacity0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>Made </td>
                                    <td>
                                        <asp:TextBox ID="txtMade0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fuel Type </td>
                                    <td>
                                        <asp:DropDownList ID="drpfuletype1" CssClass="form-control-blue" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Status </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control-blue" runat="server">
                                           <asp:ListItem Value="1">Active</asp:ListItem>
                                           <asp:ListItem Value="0">In Active</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Engine No. </td>
                                    <td>
                                        <asp:TextBox ID="txtEngineNo0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Chasis No. </td>
                                    <td>
                                        <asp:TextBox ID="txtChasis0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vehicle CC </td>
                                    <td>
                                        <asp:TextBox ID="txtVehicleCC0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>MFG </td>
                                    <td>
                                        <asp:TextBox ID="txtMFG0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td>Agency
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpagency1" CssClass="form-control-blue" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Driver
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdriverName1" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Driver Contact No.</td>
                                    <td>
                                        <asp:TextBox ID="txtdriverContact1" runat="server" CssClass="form-control-blue" MaxLength="10" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Insurance No.</td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceNo1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Insurance Expiry</td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceExpiry1" runat="server" CssClass="form-control-blue datepicker-normal" onchange="InsuranceExpiry1(this)" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnInsuranceExpiry1" />
                                    </td>
                                </tr>
                                <tr>
                                     <td>Insurance Expiry Notify  </td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceNotifyBefore1" runat="server" CssClass="form-control-blue" MaxLength="2" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                        <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pollution Receipt No.</td>
                                    <td>
                                        <asp:TextBox ID="txtPollutionReceiptNo1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pollution Expiry</td>
                                    <td>
                                        <asp:TextBox ID="txtPollutionExpiry1" runat="server" CssClass="form-control-blue datepicker-normal" onchange="PollutionExpiry1(this)" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnPollutionExpiry1" />
                                    </td>
                                </tr>
                                <tr>
                                     <td>Pollution Expiry Notify  </td>
                                    <td>
                                        <asp:TextBox ID="txtPollutionNotifyBefore1" runat="server" CssClass="form-control-blue" MaxLength="2" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                        <div style="color:red; font-size:11px;">Notify Before day(s) of Expiry</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark</td>
                                    <td>

                                        <asp:TextBox ID="txtremark1" CssClass="form-control-blue" runat="server" Rows="2" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" style="text-align: center;">
                                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-y"
                                            OnClick="LinkButton5_Click" ValidationGroup="b">Update</asp:LinkButton>
                                        &nbsp;
                                &nbsp;<asp:LinkButton ID="LinkButton6" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                        <div id="msgbox2" runat="server" style="left: 75px;"></div>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </div>
                </asp:Panel>

                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button9" PopupControlID="Panel1"
                    CancelControlID="LinkButton6" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>


            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue"
                                    runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                                        CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                                        PopupControlID="Panel2" TargetControlID="Button7">
                                    </asp:ModalPopupExtender>
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CausesValidation="False" CssClass="button-n"
                                    OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y"
                                    OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>

                    </table>
                    </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <script src="../js/jquery.min.js"></script>
            <script type="text/javascript">
                //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                //    $('.datepicker-normal').datepicker({
                //            dateFormat: 'dd-M-yy'
                //        });
                //});
                function RegistrationExpiry(tis) {
                    $('#<%=hdnRegistrationExpiry.ClientID%>').val($(tis).val());
                };
                function RegistrationExpiry1(tis) {
                    $('#<%=hdnRegistrationExpiry1.ClientID%>').val($(tis).val());
                };
                function PollutionExpiry(tis) {
                    $('#<%=hdnPollutionExpiry.ClientID%>').val($(tis).val());
                };

                function InsuranceExpiry(tis) {
                    $('#<%=hdnInsuranceExpiry.ClientID%>').val($(tis).val());
                };
                function PollutionExpiry(tis) {
                    $('#<%=hdnPollutionExpiry.ClientID%>').val($(tis).val());
                };

                function InsuranceExpiry1(tis) {
                    $('#<%=hdnInsuranceExpiry1.ClientID%>').val($(tis).val());
                };
                function PollutionExpiry1(tis) {
                    $('#<%=hdnPollutionExpiry1.ClientID%>').val($(tis).val());
                };

            </script>
            <script>
                // WRITE THE VALIDATION SCRIPT.
                function isNumber(evt) {
                    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                        return false;

                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

