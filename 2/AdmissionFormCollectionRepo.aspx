<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdmissionFormCollectionRepo.aspx.cs"
    Inherits="_2.AdminAdmissionFormCollectionRepo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12 no-padding ">
                                     <div class="col-sm-4 " id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                                     <div class="col-sm-2 " id="divSession" runat="server" visible="false">
                                <label class="control-label">Session</label>
                                <div class="">
                                    <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                </div>
                            </div>
                                                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYearTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonthTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDateTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  mgbt-xs-15">
                                <label class="control-label">Mode of Payment</label>
                                <div class="">
                                    <asp:DropDownList ID="DdlpaymentMode" runat="server" CssClass="vd_radio radio-success">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>DD</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>Online Transfer</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                        <asp:ListItem Value="Online">Online</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                                     <div class="col-sm-2  mgbt-xs-15">
                                <label class="control-label">Status</label>
                                <div class="">
                                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Paid</asp:ListItem>
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Select User&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Admission Type </label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAdmissionType" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Selected="True" Text="<--Select-->" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                                    <asp:ListItem Text="Old" Value="Old"></asp:ListItem>
                                                    <asp:ListItem Text="New (Provisional)" Value="Provisional"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 63px;"></div>
                                    </div>
                                </div>

                                

                                <div class="col-sm-12 mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel110" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class=" table-responsive  table-responsive2" id="divExport" runat="server">
                                                <div id="gdv1" runat="server">
                                                <table id="abc" runat="server" width="100%">
                                                    <tr style="text-align: center;">
                                                         <td>
                                                        <div id="header1" runat="server"></div><br />
                                                   
                                                       
                                                            <asp:Label ID="lbltitel" runat="server"></asp:Label><br />

                                                    <asp:Label ID="lbloptions" runat="server" Font-Bold="True"></asp:Label><br />
                                                       
                                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group "
                                                                ShowFooter="True">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label31" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("RecieptNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Prospectus/ Booklet No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lblbooklet" runat="server" Text='<%# Bind("Prospectus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("AdmissionFromDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type of Admission ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="AdmissionType" runat="server" Text='<%# Bind("AdmissionType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student's Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Father's Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label40" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Contact No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("FatherContactNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                       
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Stream">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label34" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="mop" runat="server" Text='<%# Bind("mop") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmountTotal" runat="server"></asp:Label>.00
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
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
