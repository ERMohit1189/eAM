<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="QuickUpdate.aspx.cs" Inherits="QuickUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                
            </script>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkView" OnClick="LinkView_Click" OnClientClick="ValidateDropdown('.validatedrp'); return validationReturn();" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-paper-plane"></i> &nbsp; View </asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div runat="server" id="divExport" visible="false">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table" id="tbl">
                                            <tbody>
                                                <tr>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 3%">#</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 10%">S.R. No./ Student's Name</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 7%">Class</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 7%">DOB&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%">Father's Name&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%">Mother's Name&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 8%">Guardian's Contact No.&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 8%" runat="server" id="thIdentity">Aadhaar No.</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 14%">Present Address&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%">Country&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%">State&nbsp;<span class="vd_red">*</span></th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%">City&nbsp;<span class="vd_red">*</span></th>
                                                </tr>
                                                <asp:Repeater ID="rptStudents" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center"><%# Container.ItemIndex+1  %></td>
                                                            <td>
                                                                <asp:Label ID="LblSrno" runat="server" Text='<%# Eval("srno")  %>'></asp:Label><br />
                                                                <%# Eval("name")  %>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelClass" runat="server" Text='<%# Eval("CombineClassName")  %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDob" runat="server" class="form-control-blue datepicker-normal" placeholder="dd-MMM-yyyy" Text='<%# Eval("dob", "{0:dd-MMM-yyyy}")  %>'></asp:TextBox>
                                                            </td>
                                                            
                                                            <td>
                                                                <asp:TextBox ID="txtFatherName" runat="server" Text='<%# Eval("FatherName")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMotherName" runat="server" Text='<%# Eval("MotherName")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFatherContactNo" runat="server" Text='<%# Eval("FamilyContactNo")  %>' MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                            </td>
                                                             <td>
                                                                <asp:TextBox ID="txtAadharNo" runat="server" Text='<%# Eval("AadharNo")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtStLocalAddress" runat="server" TextMode="MultiLine" Text='<%# Eval("StLocalAddress")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>

                                                                <asp:Label ID="CountryId" runat="server" CssClass="hide" Text='<%# Eval("StLocalCountryId")  %>'></asp:Label>
                                                                <asp:DropDownList ID="drpCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="stateId" runat="server" CssClass="hide" Text='<%# Eval("StLocalStateId")  %>'></asp:Label>
                                                                <asp:DropDownList ID="drpState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="cityId" runat="server" CssClass="hide" Text='<%# Eval("StLocalCityId")  %>'></asp:Label>
                                                                <asp:DropDownList ID="drpCity" runat="server"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15 text-center">
                                    <label class="control-label"></label>
                                    <div class="">
                                        <asp:UpdatePanel runat="server" ID="dssd">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkUpdate" OnClick="LinkUpdate_Click" Visible="false" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-paper-plane"></i> &nbsp; Update </asp:LinkButton>
                                                <div class="text-box-msg">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <div id="msgbox" runat="server" style="left: 0;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

