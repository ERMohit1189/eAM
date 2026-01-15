<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LocationMaster.aspx.cs" Inherits="LocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .selected {
            background-color: #666;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding" runat="server" id="table1">

                                    <div class="col-sm-5 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Location Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control-blue validatetext"></asp:TextBox>
                                                   
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Distance&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDistance" runat="server" placeholder="eg: 2 KM or 2" CssClass="form-control-blue validatetext"></asp:TextBox>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">One Way Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtOneWayAmt" runat="server" placeholder="0.00" CssClass="form-control-blue validatetext"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Two Way Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtTwoWayAmt" runat="server" placeholder="0.00" CssClass="form-control-blue validatetext"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>

                                    <div class="col-sm-6 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" OnClientClick="ValidateTextBox('.validatetext');return validationReturn(this);" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                         <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"  CssClass="button form-control-blue" Enabled="false">Copy Master from Last session</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
<%--<div class="col-sm-12 mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">Copy Master Button will work only when there is no Location in latest session.

</span>
                            </div>--%>

                                </div>

                                <div class="col-sm-12 no-padding" runat="server" id="table2">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("locationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Distance (in km.)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistance" runat="server" Text='<%# Bind("distanceInKm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="One Way Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOneWayAmount" runat="server" Text='<%# Bind("oneWayAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Two Way Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTwoWayAmt" runat="server" Text='<%# Bind("twoWayAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocationId" runat="server" Text='<%# Bind("locationId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("locationId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="updatePanel" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Location Name <span class="vd_red">*</span> </td>
                            <td>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <asp:TextBox ID="txtLocationNameMP" runat="server" CssClass="form-control-blue validatetext1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Distance in km </td>
                            <td>
                                <asp:TextBox ID="txtDistanceMP" runat="server" CssClass="form-control-blue validatetext1" Height="50px "></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>One Way Amount </td>
                            <td>
                                <asp:TextBox ID="txtOneWayAmtMP" runat="server" CssClass="form-control-blue validatetext1" Height="50px "></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Two Way Amount </td>
                            <td>
                                <asp:TextBox ID="txtTwoWayAmtMP" runat="server" CssClass="form-control-blue validatetext1" Height="50px "></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False" OnClick="lnkUpdate_Click"  OnClientClick="ValidateTextBox('.validatetext1');return validationReturn(this);"
                                    CssClass="button-y">Update</asp:LinkButton>
                                &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblIDMP" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="updatePanel_ModalPopupExtender" runat="server" CancelControlID="lnkCancel" PopupControlID="updatePanel"
                        TargetControlID="Button9" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="panelDelete" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this record ?
                                    <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 50px;">
                                <asp:Button ID="btnCancelDelete" runat="server" CssClass="button-n" Text="No" />
                                &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />


                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="panelDelete_ModalPopupExtender" runat="server" CancelControlID="btnCancelDelete" DynamicServicePath=""
                        Enabled="True" PopupControlID="panelDelete" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>


            <script type="text/javascript" src="../js/jquery.min_1.js"></script>
            <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    $("[id*=GridView1]").sortable({
                        items: 'tr:not(tr:first-child,tr:last-child)',
                        cursor: 'move',
                        axis: 'y',
                        dropOnEmpty: false,
                        start: function (event, ui) {
                            ui.item.addClass("selected");
                        },
                        stop: function (event, ui) {
                            ui.item.removeClass("selected");
                            var gridview = document.getElementById("ContentPlaceHolder1_GridView1");
                            smoke.confirm('Do you want to reorder pickup point!', function (e) {
                                if (e) {
                                    for (var i = 1; i < gridview.rows.length - 1; i++) {
                                        gridview.rows[i].cells[0].childNodes[1].innerHTML = i.toString();
                                        var id = gridview.rows[i].cells[9].childNodes[1].innerHTML;
                                        $.ajax({
                                            type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            url: "VehiclePickupLocationMaster.aspx/updateGridViewReorder",
                                            data: "{'DisplayOrder':'" + i + "','id':'" + id + "'}",
                                            dataType: "json"
                                        });
                                    }
                                    smoke.alert("Updated successfully.", function () {
                                        window.location.href = "VehiclePickupLocationMaster.aspx";
                                    });

                                }
                                else {
                                    window.location.href = "VehiclePickupLocationMaster.aspx";
                                }
                            });
                        }
                    });
                });

                function refreshPage() { location.reload(); }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        var validNumber = new RegExp(/^\d*\.?\d*$/);
        
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46) {
                return false;
            }
            return true;
        }

    </script>

</asp:Content>

