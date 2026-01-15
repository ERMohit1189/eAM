<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ArrearEntry.aspx.cs" Inherits="ArrearEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  mgbt-xs-10">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" CssClass="form-control-blue validatedrp" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection"  CssClass="form-control-blue validatedrp" runat="server" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" CssClass="form-control-blue validatedrp" runat="server" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAdmissionType" runat="server" OnSelectedIndexChanged="drpAdmissionType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue">
                                                <asp:ListItem Value="New" Selected="True">New</asp:ListItem>
                                                <asp:ListItem Value="Old">Old</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S.R. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student's Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Father's Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFathername" runat="server" Text='<%# Bind("Fathername") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                  <asp:TextBox ID="txtTutionfeeArrear" runat="server" style="width:47%; height: 25px;" placeholder="Amount" onkeyup="CheckDigitNumber(this);"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lbltuea">Tuition Fee Arrear</asp:Label><br />
                                                    <asp:DropDownList runat="server" ID="ddlTutionHead" CssClass="form-contrlo-blue"></asp:DropDownList>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTransportArrear" runat="server"  style="width:47%; height: 25px;" placeholder="Amount" onkeyup="CheckDigitNumber(this);"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lbltra">Transport Fee Arrear</asp:Label><br />
                                                    <asp:DropDownList runat="server" ID="ddlTransportHead" CssClass="form-contrlo-blue"></asp:DropDownList>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                   <asp:TextBox ID="txtHostelArrear" runat="server"  style="width:47%; height: 25px;" placeholder="Amount" onBlur="CheckDigitNumber(this); Calculate(this);"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lblhos">Hostel Fee Arrear</asp:Label><br />
                                                    <asp:DropDownList runat="server" ID="ddlHostelHead" CssClass="form-contrlo-blue"></asp:DropDownList>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" Visible="false" OnClientClick="ValidateTextBox('.validatetext');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 75px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>

        function CheckDigitNumber(inputtxt) {
            var $this = $(inputtxt);
            $this.val($this.val().replace(/[^\d.]/g, ''));
        }
      
    </script>
</asp:Content>

