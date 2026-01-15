<%@ Page Title="Admission Details | eAM ®" Language="C#" MasterPageFile="~/ap/admin_root-manager.master" AutoEventWireup="true" CodeFile="Admission_Details.aspx.cs" Inherits="ap.ApAdmissionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 ">

                            <div class="table-responsive" runat="server" id="divshow">
                                <asp:GridView ID="grdStRecord" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Convert.ToDateTime(Eval("AdmissionFromDate")).ToString("yyyy MMM dd") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receipt No.">
                                            <ItemTemplate>
                                                <asp:Label ID="RecieptNo" runat="server" Text='<%# Eval("RecieptNo") %>'></asp:Label>
                                                <asp:Label ID="TxnID" runat="server" Visible="False" Text='<%# Eval("TxnID") %>'></asp:Label>
                                                <asp:Label ID="SessionName" runat="server" Visible="False" Text='<%# Eval("SessionName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Student's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Father's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Mobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Print Receipt">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_OnClick"
                                                            title="Print" Visible='<%# Eval("Status").ToString() == "Paid" %>'  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print Form">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPrintAF" runat="server" OnClick="lnkPrintAF_OnClick"
                                                    title="Print Admission Form" Visible='<%# Eval("Status").ToString() == "Paid" %>'  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Test Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntrenceDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Result">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnteranceStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Registration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdmission" runat="server" Visible="false" Text="Pending"></asp:Label>
                                                <asp:Label ID="lblFail" runat="server" Visible="false" class="btn menu-icon vd_bd-green vd_green" Style="background-color: red !important; color: #fff;"><i class="fa fa-close" style="color:#fff;"></i></asp:Label>
                                                <asp:Label ID="lblReg" runat="server" Visible="false" class="btn menu-icon vd_bd-green vd_green" Style="background-color: #1fae66 !important; color: #fff;"><i class="fa fa-check" style="color:#fff;"></i></asp:Label>
                                                <asp:LinkButton ID="linkRegister" runat="server" OnClick="linkRegister_Click" title="Register Now"  class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-user-plus"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

