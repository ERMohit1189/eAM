<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GatePassReport.aspx.cs" Inherits="_1.AdminGatePassReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
     <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
    <div class="vd_content-section clearfix">
         <script>
             Sys.Application.add_load(datetime);
         </script>
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                <label class="control-label">From </label>
                                <div class="">
                                    <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control-blue datepicker-normal currDate"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                <label class="control-label">To </label>
                                <div class="">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal currDate"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                <asp:LinkButton ID="LinkView" runat="server" OnClick="LinkView_Click" CssClass="button">View</asp:LinkButton>
                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                            </div>
                        </div>
                        <div class="col-sm-12  no-padding" >
                            <div class="table-responsive2 table-responsive" runat="server" id="rptDiv">

                        <table class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                            <thead>
                                <tr>
                                    <th class="vd_bg-blue-np vd_white-np">ID</th>
                                    <th class="vd_bg-blue-np vd_white-np">S.R. No.</th>
                                    <th class="vd_bg-blue-np vd_white-np">Student's Name</th>
                                    <th class="vd_bg-blue-np vd_white-np">Father's Name</th>
                                    <th class="vd_bg-blue-np vd_white-np">Class</th>
                                    <th class="vd_bg-blue-np vd_white-np">Date</th>
                                    <th class="vd_bg-blue-np vd_white-np">Reason</th>
                                    <th class="vd_bg-blue-np vd_white-np">Contact No.</th>
                                    <th class="vd_bg-blue-np vd_white-np">Photo</th>
                                    <th class="vd_bg-blue-np vd_white-np">Print</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("MaxId")%>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("srno") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("combineClassName") %>'></asp:Label>
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="Label21" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <div class="table-pic-box">
                                                    <%-- <asp:Image ID="Image2" runat="server" alt="" />--%>
                                                    <asp:Image ID="Image1" runat="server" AlternateText='<%# Eval("srno") %>' ImageUrl='<%# ResolveClientUrl(Eval("StudentPhotopath").ToString()!=""?Eval("StudentPhotopath").ToString():@"~\img\user-pic\user-profile-demo.png") %>' />
                                                </div>
                                            </td>
                                            <td class="menu-action">
                                                <%--  <a title="print"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></a>--%>

                                                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_OnClick" ToolTip="Print" runat="server" title="print" data-placement="top" class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i><asp:Label runat="server" ID="lblmaxid" Text='<%# Bind("MaxId")%>' Visible="false"></asp:Label></asp:LinkButton>
                                               
                                                 <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" Visible="false" ToolTip='<%# Bind("MaxId")%>' runat="server" title="delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
                        </table>

                    </div>
                        </div>
                            
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                    <table width="100%">
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <h4>Do you really want to Cancel this GatePass?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button8" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="btnYes" runat="server" CssClass="button form-control-blue" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnNo" runat="server" CssClass="button form-control-blue" CausesValidation="False" Text="No" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Text="Button" Style="Display: none" />
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    TargetControlID="Button2" PopupControlID="Panel1" CancelControlID="btnNo">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

            </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>

