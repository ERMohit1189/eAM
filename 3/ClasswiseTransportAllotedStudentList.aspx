<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ClasswiseTransportAllotedStudentList.aspx.cs" Inherits="admin_VehicleNoWiseTransportAllotedStudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .vd_bg-grey-ll {
            background-color: hsl(228, 29%, 97%) !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCardType" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>

                                                    <div class="text-box-msg">
                                                   
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsection" runat="server"  CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlBranch" runat="server"  CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Location&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 62px"></div>
                                    </div>
                                </div>


                                <div class="col-sm-12  mgbt-xs-5" id="printbox" runat="server" visible="false">

                                    <div style="float: right; font-size: 19px;">
                                        <asp:Panel ID="Panel2" runat="server">

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>

                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                    <script>
                                                        
                                                    </script>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ImageButton1" />
                                                    <asp:PostBackTrigger ControlID="ImageButton2" />
                                                    <asp:PostBackTrigger ControlID="ImageButton3" />
                                                    <asp:PostBackTrigger ControlID="ImageButton4" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </asp:Panel>

                                    </div>
                                </div>


                                <div class="col-sm-12 " id="gdv" runat="server">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <table runat="server" id="abc" class="table no-p-b-table table-nb no-bm">
                                            <tr style="text-align:center;">
                                                <td id="header" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="p-pad-1 text-center p-h-titel-box">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="List of Transport availed students"></asp:Label><br />
                                                    <asp:Label ID="lblGenBy" Font-Bold="true" runat="server"></asp:Label><br />
                                                    <asp:Label ID="hding" Font-Bold="true" runat="server"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-pad-1 ">

                                                    <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-bm no-head-border table-bordered" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

