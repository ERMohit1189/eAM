<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TillDueReportwithInstallmentwise.aspx.cs" Inherits="admin_TillDueReportwithInstallmentwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">FeeGroup&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFeegroup" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged"
                                                CssClass="form-control-blue drpUpdate">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpInsttalment" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div id="Div1" class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" visible="false">
                                        <label class="control-label">Select</label>
                                        <div class="txt-middle">
                                            <asp:CheckBox ID="chkTransport" runat="server" Text="Transport" Checked="True" CssClass="vd_checkbox checkbox-success" />
                                            <asp:CheckBox ID="chkDiscount" runat="server" Text="Discount" Checked="True" CssClass="vd_checkbox checkbox-success" />
                                            <asp:CheckBox ID="chkLatefee" runat="server" Text="Without Late Fee" Checked="True" CssClass="vd_checkbox checkbox-success" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnshow" runat="server" Text="View" CssClass="button form-control-blue" OnClick="btnshow_Click" />
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color"
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

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <table width="100%" id="abc" runat="server">
                                                <tr>
                                                    <td>
                                                        <div id="header1" runat="server" style="width:80%"></div>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClassId" runat="server" Text='<%# Bind("Classid") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Strength" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStrength" runat="server" Text='<%# Bind("Strength") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalStrength" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <%--------------------------------------------------------------------------------%>
                                                                <asp:TemplateField HeaderText="Due Amount/(a)" FooterStyle-HorizontalAlign="Center" FooterText="Total Due Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Arreir Amount/(b)" FooterStyle-HorizontalAlign="Center" FooterText="Total Arreir Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArreirAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalArreirAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transport Amount/(c)" FooterStyle-HorizontalAlign="Center" FooterText="Total Transport Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransportAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalTransportAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Late Fee Amount/(d)" FooterStyle-HorizontalAlign="Center" FooterText="Total Late Fee Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLateFeeAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalLateFeeAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Due Amount/(e=a+b+c+d)" FooterStyle-HorizontalAlign="Center" FooterText="Total Arreir Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalDueAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblGrossDueAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <%--------------------------------------------------------------------------------%>

                                                                <asp:TemplateField HeaderText="Discount Amount/(f)" FooterStyle-HorizontalAlign="Center" FooterText="Total Discount Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalDiscountAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Concession Amount/(g)" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConcessionAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalConcessionAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Due Amount/(h=e-(f+g))" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblActualDueAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalActualDueAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Deposit Amount/(i)" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepositeAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalDepositeAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Actual Deposit Amount" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblActualDepositAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalActualDepositAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Balance Amount/(j=h-i)" FooterStyle-HorizontalAlign="Center" FooterText="Total Balance Amount" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBalanceAmount" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalBalanceAmount" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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

