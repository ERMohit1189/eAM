<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ListofStudentRoutWise.aspx.cs" Inherits="ListofStudentRoutWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">

                                    

                                     <div class="col-sm-2 half-width-50 mgbt-xs-15">
     <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
     <div class="">
         <asp:DropDownList ID="drpInstallments" runat="server" CssClass="form-control-blue" AutoPostBack="true"
             OnSelectedIndexChanged="drpInstallments_SelectedIndexChanged">
         </asp:DropDownList>
     </div>
 </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle Type&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpVehicleType" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle No.&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpVehicleNo" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpVehicleNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Location&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-sm-10  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                       
        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">View</asp:LinkButton>
                                            
                                        <div id="msgbox" runat="server" style="left: 62px"></div>
                                    </div>
                                </div>


                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                    title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                    title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                    title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                    title="Print"><i class="fa fa-print "></i></asp:LinkButton>

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


                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2 ">
                                           <asp:UpdatePanel ID="UpdatePanel4" runat="server">
       <ContentTemplate>
                                        <div id="gdv" runat="server">
                                            <div id="header1" class="col-sm-12 text-center" runat="server"></div>
                                            <br />
                                            <div class="col-sm-12 text-center">
                                                <asp:Label ID="Label1" runat="server" Text="List of Students (Routewise)"></asp:Label><br />
                                                <asp:Label ID="Label4" runat="server" Text="Installment-"></asp:Label>&nbsp;<asp:Label ID="lblMonth" runat="server" Text=""></asp:Label><br />
                                                <asp:Label ID="Label3" runat="server" Text="Vehicle No.-"></asp:Label>&nbsp;<asp:Label ID="lblVehicle" runat="server" Text=""></asp:Label>
                                            </div>
                                            <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-bm no-head-border table-bordered"
                                                AutoGenerateColumns="false" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFname" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("StLocalAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactno" runat="server" Text='<%# Eval("FamilyContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRoute" runat="server" Text='<%# Eval("RouteName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vehicle No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Eval("VehicleNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pickup">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPickup" runat="server" Text='<%# Eval("PickupPoint") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Drop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrop" runat="server" Text='<%# Eval("DropPoint") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltTLF" runat="server" Style="font-weight: bold;">Total</asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" CssClass="vd_bg-grey-ll vd_white" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Inst. Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInst" runat="server" Text='<%# Eval("InstallmentAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblInstF" runat="server" Style="font-weight: bold;"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" CssClass="vd_bg-grey-ll vd_white" />
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Due">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDue" runat="server" Text='<%# Eval("Due") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDueF" runat="server" Style="font-weight: bold;"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" CssClass="vd_bg-grey-ll vd_white" />
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deposit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeposit" runat="server" Text='<%# Eval("Deposit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDepositF" runat="server" Style="font-weight: bold;"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" CssClass="vd_bg-grey-ll vd_white" />
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblBalanceF" runat="server" Style="font-weight: bold;"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" CssClass="vd_bg-grey-ll vd_white" />
                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

