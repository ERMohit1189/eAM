<%@ Page Language="C#" AutoEventWireup="true"MasterPageFile="~/Master/admin_root-manager.master" CodeFile="ListofAllVehicles.aspx.cs" Inherits="_3_ListofAllVehiclesNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="Div1" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Vehicle Type&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpclass" class="form-control-blue"></asp:DropDownList>

                                        </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Fuel Type&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue" ></asp:DropDownList>

                                        </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Status</label>
                                            <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue">
                                                <asp:ListItem Value="-1">All</asp:ListItem>
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                        </div>

                                    <div class="col-sm-3  half-width-50  btn-a-devices-3-p6  mgbt-xs-15" style="padding-top:24px;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkView_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>

                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding ">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  mgbt-xs-10" runat="server" id="icondivs" visible="false">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div style="float: right; font-size: 19px;" id="Panel2" runat="server">
                                                            <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" 
                                                                title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" 
                                                                title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton3" runat="server" Visible="false" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" 
                                                                title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" 
                                                                title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                            <script>

</script>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="ImageButton1" />
                                                        <asp:PostBackTrigger ControlID="ImageButton2" />
                                                        <asp:PostBackTrigger ControlID="ImageButton3" />
                                                        <asp:PostBackTrigger ControlID="ImageButton4" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-12 " runat="server" id="pnlcontrols" visible="false">
                                        <div class="col-lg-12 " runat="server">

                                            <div class=" table-responsive  table-responsive2">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div id="gdv1" runat="server">
                                                            <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                                <tr>
                                                                    <td>
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                              <asp:Label ID="vehicleTyps" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                 <asp:Label ID="fuelTyps" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                  <asp:Label ID="statussS" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                            <br />
                                                    <div class=" table-responsive  table-responsive2">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                                            CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
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
                                            <asp:TemplateField HeaderText="Driver Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Driver") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Driver Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label54" runat="server" Text='<%# Bind("driverContact") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Seat Capacity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label544" runat="server" Text='<%# Bind("SeatCapacity") %>'></asp:Label>
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
                                                      <asp:TemplateField HeaderText="Engine No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtfueltypeEngineNo" runat="server" Text='<%# Bind("EngineNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Chasis No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label61" runat="server" Text='<%# Bind("ChasisNo") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Manufacturing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnerNamem" runat="server" Text='<%# Bind("MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Insurance No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnerNameI" runat="server" Text='<%# Bind("InsuranceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pollution Receipt No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnerNamep" runat="server" Text='<%# Bind("PollutionReceiptNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Registration Expiry">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnerNamee" runat="server" Text='<%# Bind("RegistrationExpiry") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Insurance Expiry">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOfwnercName" runat="server" Text='<%# Bind("InsuranceExpiry") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Pollution Expiry">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOwnecjdjrName" runat="server" Text='<%# Bind("PollutionExpiry") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
