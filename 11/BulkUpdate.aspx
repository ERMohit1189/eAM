<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BulkUpdate.aspx.cs" Inherits="BulkUpdate" %>

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
                                         <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                         <div class="">                                             
                                            <asp:DropDownList ID="drpStatus" runat="server" AutoPostBack="true"                                                             
                                                OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                    <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                                    <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                                    <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                                    <asp:ListItem Value="B">Blocked</asp:ListItem>
                                                    <asp:ListItem Value="TCI">TC Issued</asp:ListItem>
                                            </asp:DropDownList>
                                             <div class="text-box-msg">
                                             </div>
                                         </div>
                                     </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkView" OnClick="LinkView_Click" OnClientClick="ValidateDropdown('.validatedrp'); return validationReturn();" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-eye"></i> &nbsp; View </asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12  half-width-50 text-left">
                                <div runat="server" id="divExport" visible="false">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table" id="tbl">
                                            <tbody>
                                                <tr>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 5%;vertical-align: top !important;">
                                                         <asp:CheckBox ID="chkAll" runat="server" Checked="true"
                                                             AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                                    </th>
                                                    <th class="p-pad-3 text-left" style="width: 5%;vertical-align: top !important;">#</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">S.R. No.<br /> Student's Name</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">Class</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">UDISE PEN</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">APAAR ID</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">Student's Mobile</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">Student's Email</th>
                                                    <th class="p-pad-3 text-left" style="width: 10%;vertical-align: top !important;">
                                                         Blood Group
                                                         <asp:DropDownList ID="drpBloodGroupALL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpBloodGroupALL_SelectedIndexChanged" ></asp:DropDownList>  
                                                    </th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 10%;vertical-align: top !important;">
                                                        House
                                                        <asp:DropDownList ID="drpHouseALL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpHouseALL_SelectedIndexChanged"></asp:DropDownList>
                                                    </th>  
                                                </tr>
                                                <asp:Repeater ID="rptStudents" runat="server" OnItemDataBound="rptStudents_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center">
                                                                <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                            </td>
                                                            <td class="text-left"><%# Container.ItemIndex+1  %></td>
                                                            <td>
                                                                <asp:Label ID="LblSrno" runat="server" Text='<%# Eval("srno")  %>'></asp:Label><br />
                                                                <%# Eval("name")  %>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelClass" runat="server" Text='<%# Eval("CombineClassName")  %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUDISEPEN" runat="server" Text='<%# Eval("udisePen")  %>'></asp:TextBox>
                                                            </td>
                                                            
                                                            <td>
                                                                <asp:TextBox ID="txtAPAARID" runat="server" Text='<%# Eval("apaarID")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtStudentMobile" runat="server" Text='<%# Eval("studentMobile")  %>' MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                            </td>
                                                             <td>
                                                                <asp:TextBox ID="txtStudentEmail" runat="server" Text='<%# Eval("studentEmail")  %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="hdBloodGroup" runat="server" Value='<%# Eval("booldGroup")  %>' />
                                                                 <asp:DropDownList ID="drpBloodGroup" runat="server" ></asp:DropDownList>                                                                
                                                            </td>
                                                            <td>
                                                                 <asp:HiddenField ID="hdHouse" runat="server"  Value='<%# Eval("houseName")  %>' />
                                                                 <asp:DropDownList ID="drpHouse" runat="server"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div></div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15 text-left">
                                    <label class="control-label"></label>
                                    <div class="">
                                        <asp:UpdatePanel runat="server" ID="dssd">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkUpdate" OnClick="LinkUpdate_Click" Visible="false" CssClass="btn vd_btn vd_bg-blue" runat="server"> Update </asp:LinkButton>
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

