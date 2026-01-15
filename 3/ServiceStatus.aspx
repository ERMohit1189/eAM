<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ServiceStatus.aspx.cs" Inherits="admin_ServiceStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12  no-padding">
                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle Type</label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleType" runat="server" AutoPostBack="True" CssClass="form-control-blue"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle No.</label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleNo" runat="server" AutoPostBack="True" CssClass="form-control-blue"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12  no-padding">
                             <h3><span class="imp">Service Status Details</span></h3>
                            <div width="100%">
                                <asp:Panel ID="Panel1" runat="server">
                                   
                                    <asp:GridView ID="GridView1" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due Service">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complete Service">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cancle">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

