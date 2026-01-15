<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="Student_AttendanceTillDate.aspx.cs" Inherits="Student_AttendanceTillDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="thy" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAttenSection" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Till Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 64px"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-5">
                                    <asp:UpdatePanel ID="UpdatePanel00" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel3" runat="server">
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Visible="false" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" Visible="false" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" Visible="false" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" Visible="false" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div id="gdv1" class="print-row col-sm-12   no-padding" runat="server">
                                                <div id="abc" class="print-row col-sm-12   no-padding" runat="server" visible="false">
                                                    <div class="print-row col-sm-12  no-padding" style="width:85%">
                                                        <div id="header1" runat="server"></div>
                                                    </div>
                                                    <div class="print-row col-sm-12  text-center no-padding">
                                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                        <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                        <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                        <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12  no-padding print-row">
                                                        <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False"
                                                        class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="25px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" Width="70px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblTypeOFAdmision" runat="server" Text='<%# Bind("TypeOFAdmision") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateOfadmission" runat="server" Text='<%# Bind("DateOfAdmiission", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Count From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="cntfrom" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Present">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="presents" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absent">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="absents" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Working Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="total" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="percentage" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                               </div>
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

