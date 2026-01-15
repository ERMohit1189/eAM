<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="main-icon-selection-list.aspx.cs" Inherits="main_icon_selection_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-12 no-padding">
                            <div class="col-lg-6 no-padding">
                                <div class="form-group ">
                                    <div class="col-sm-8 controls mgbt-xs-20 hide">
                                        <asp:DropDownList ID="drpUserType" runat="server" CssClass="form-control-blue" Enabled="false">
                                            <asp:ListItem>Admin</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 no-padding">
                            <div class="col-lg-6 ">
                                <div class="form-group ">
                                    <div class="controls">

                                        <div class="vd_checkbox  checkbox-done example-icon">
                                            <asp:Repeater runat="server" ID="Repeater1">
                                                <ItemTemplate>
                                                    <div class="vd_checkbox  checkbox-done example-icon ">
                                                        <span class="menu-icon append-icon"><i class="<%# Eval("ParentClassName") %>"></i></span>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("MenuID") %>' Visible="false"></asp:Label>
                                                        <asp:CheckBox ID="CheckBox1" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" runat="server" Text='<%# Eval("text") %>' />
                                                    </div>
                                                    <asp:Repeater runat="server" ID="Repeater2">
                                                        <ItemTemplate>
                                                            <div class="vd_checkbox  checkbox-done example-icon ">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="menu-icon append-icon"><i class="<%# Eval("ParentClassName") %>"></i></span>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MenuID") %>' Visible="false"></asp:Label>
                                                                &nbsp;<asp:CheckBox ID="CheckBox2" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true" runat="server" Text='<%# Eval("text") %>' data-toggle="modal" data-target="#myModal" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--Popup Modal -->
                        <div runat="server" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header vd_bg-blue vd_white">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></button>
                                        <h4 class="modal-title" id="myModalLabel">Select Icon</h4>
                                    </div>
                                    <div class="modal-body controls">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value=" fa  fa-dashboard"><i class="append-icon fa fa-fw  fa-dashboard"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-sitemap"><i class="append-icon fa fa-fw  fa-sitemap"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-cogs"><i class="append-icon fa fa-fw fa-cogs"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-table"><i class="append-icon fa fa-fw  fa-table"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-picture-o"><i class="append-icon fa fa-fw  fa-picture-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-suitcase"><i class="append-icon fa fa-fw  fa-suitcase"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-ban"><i class="append-icon fa fa-fw  fa-ban"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-bullseye"><i class="append-icon fa fa-fw  fa-bullseye"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-bullhorn"><i class="append-icon fa fa-fw  fa-bullhorn"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa fa-user-plus"><i class="append-icon fa fa-fw  fa-user-plus"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-user"><i class="append-icon fa fa-fw  fa-user"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-users"><i class="append-icon fa fa-fw  fa-users"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-female"><i class="append-icon fa fa-fw  fa-female"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-child"><i class="append-icon fa fa-fw  fa-child"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-male"><i class="append-icon fa fa-fw  fa-male"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-hand-o-left"><i class="append-icon fa fa-fw  fa-hand-o-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-hand-o-right"><i class="append-icon fa fa-fw  fa-hand-o-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-hand-o-down"><i class="append-icon fa fa-fw fa-hand-o-down"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-hand-o-up"><i class="append-icon fa fa-fw fa-hand-o-up"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-thumbs-down"><i class="append-icon fa fa-fw  fa-thumbs-down"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-thumbs-up"><i class="append-icon fa fa-fw  fa-thumbs-up"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-building"><i class="append-icon fa fa-fw fa-building"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-bank"><i class="append-icon fa fa-fw fa-bank"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-inbox"><i class="append-icon fa fa-fw fa-inbox"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-key"><i class="append-icon fa fa-fw  fa-key"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-unlock"><i class="append-icon fa fa-fw fa-unlock"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-unlock-alt"><i class="append-icon fa fa-fw fa-unlock-alt"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-trophy"><i class="append-icon fa fa-fw  fa-trophy"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-print"><i class="append-icon fa fa-fw  fa-print"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-briefcase"><i class="append-icon fa fa-fw  fa-briefcase"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-hdd-o"><i class="append-icon fa fa-fw fa-hdd-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-book"><i class="append-icon fa fa-fw  fa-book"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-bookmark"><i class="append-icon fa fa-fw  fa-bookmark"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-calendar"><i class="append-icon fa fa-fw  fa-calendar"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-adjust"><i class="append-icon fa fa-fw  fa-adjust"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-archive"><i class="append-icon fa fa-fw  fa-archive"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-database"><i class="append-icon fa fa-fw  fa-database"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-truck"><i class="append-icon fa fa-fw  fa-truck"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-bus"><i class="append-icon fa fa-fw  fa-bus"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-upload"><i class="append-icon fa fa-fw  fa-upload"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-download"><i class="append-icon fa fa-fw  fa-download"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-envelope"><i class="append-icon fa fa-fw  fa-envelope"></i> </asp:ListItem>

                                            <asp:ListItem Value=" fa  fa-eye"><i class="append-icon fa fa-fw  fa-eye"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-certificate"><i class="append-icon fa fa-fw fa-certificate"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-file-excel-o"><i class="append-icon fa fa-fw  fa-file-excel-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-file-word-o"><i class="append-icon fa fa-fw  fa-file-word-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-file-powerpoint-o"><i class="append-icon fa fa-fw  fa-file-powerpoint-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-file-pdf-o"><i class="append-icon fa fa-fw  fa-file-pdf-o"></i> </asp:ListItem>

                                            <asp:ListItem Value=" fa  fa-file-text"><i class="append-icon fa fa-fw  fa-file-text"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-file-text-o"><i class="append-icon fa fa-fw  fa-file-text-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-files-o"><i class="append-icon fa fa-fw  fa-files-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-clipboard"><i class="append-icon fa fa-fw  fa-clipboard"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-comments-o"><i class="append-icon fa fa-fw  fa-comments-o"></i> </asp:ListItem>

                                            <asp:ListItem Value="fa fa-barcode"><i class="append-icon fa fa-barcode"></i> </asp:ListItem>

                                            <asp:ListItem Value=" fa  fa-check-square"><i class="append-icon fa fa-fw  fa-check-square"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-check-square-o"><i class="append-icon fa fa-fw fa-check-square-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-dot-circle-o"><i class="append-icon fa fa-fw  fa-dot-circle-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-plus-square"><i class="append-icon fa fa-fw  fa-plus-square"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-minus-square"><i class="append-icon fa fa-fw  fa-minus-square"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-money"><i class="append-icon fa fa-fw  fa-money"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-rupee"><i class="append-icon fa fa-fw  fa-rupee"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-usd"><i class="append-icon fa fa-fw  fa-usd"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-link "><i class="append-icon fa fa-fw  fa-link"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-paperclip"><i class="append-icon fa fa-fw  fa-paperclip"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-arrow-circle-left"><i class="append-icon fa fa-fw  fa-arrow-circle-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-arrow-circle-right"><i class="append-icon fa fa-fw  fa-arrow-circle-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  a-arrow-left"><i class="append-icon fa fa-fw  fa-arrow-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-arrow-right"><i class="append-icon fa fa-fw  fa-arrow-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-caret-left"><i class="append-icon fa fa-fw  fa-caret-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-caret-right"><i class="append-icon fa fa-fw  fa-caret-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-chevron-circle-left"><i class="append-icon fa fa-fw  fa-chevron-circle-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-chevron-circle-right"><i class="append-icon fa fa-fw  fa-chevron-circle-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-chevron-left"><i class="append-icon fa fa-fw  fa-chevron-left"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-chevron-right"><i class="append-icon fa fa-fw  fa-chevron-right"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-check-square-o"><i class="append-icon fa  fa-check-square-o" aria-hidden="true"></i></asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-check-square"><i class="append-icon fa fa-fw fa-check-square"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-pencil-square-o"><i class="append-icon fa fa-fw fa-pencil-square-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-address-book"><i class="append-icon fa fa-fw fa-address-book"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-address-book-o"><i class="append-icon fa fa-fw fa-address-book-o"></i> </asp:ListItem>

                                            <asp:ListItem Value=" fa  fa-address-card"><i class="append-icon fa fa-fw fa-address-card"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-address-card-o"><i class="append-icon fa fa-fw fa-address-card-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-drivers-license"><i class="append-icon fa fa-fw fa-drivers-license"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-drivers-license-o"><i class="append-icon fa fa-fw fa-drivers-license-o"></i> </asp:ListItem>
                                            <asp:ListItem Value=" fa  fa-id-badge"><i class="append-icon fa fa-fw fa-id-badge"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa  fa-user-circle"><i class="append-icon fa fa-fw fa-user-circle"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa  fa-birthday-cake"><i class="append-icon fa fa-fw fa-birthday-cake"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa  fa-bug"><i class="append-icon fa fa-fw fa-bug"></i> </asp:ListItem>

                                            <asp:ListItem Value="fa fa-barcode"><i class="append-icon fa fa-fw fa-barcode"></i> </asp:ListItem>

                                            <asp:ListItem Value="fa fa-history"><i class="append-icon fa fa-fw fa-history"></i> </asp:ListItem>

                                            <asp:ListItem Value="fa fa-ticket"><i class="append-icon fa fa-ticket"></i> </asp:ListItem>

                                            <asp:ListItem Value="fa fa-wrench"><i class="append-icon fa fa-wrench"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-pencil-square"><i class="append-icon fa-fw fa fa-pencil-square"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-pencil"><i class="append-icon fa-fw fa fa-pencil"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-area-chart"><i class="append-icon fa-fw fa fa-area-chart"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-bar-chart"><i class="append-icon fa-fw fa fa-bar-chart"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-line-chart"><i class="append-icon fa-fw fa fa-line-chart"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-pie-chart"><i class="append-icon fa-fw fa fa-pie-chart"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-trophy"><i class="append-icon fa-fw fa fa-trophy"></i> </asp:ListItem>
                                            <asp:ListItem Value="fa fa-bus"><i class="append-icon fa-fw fa fa-bus"></i> </asp:ListItem>
                                            

                                           <asp:ListItem Value="fa fa-ban"><i class="append-icon fa-fw fa fa-ban"></i> </asp:ListItem>
                                            <asp:ListItem Value=""><i class="append-icon fa-fw "></i> </asp:ListItem>


                                            <asp:ListItem Value="glyphicon glyphicon-briefcase"><i class="append-icon glyphicon glyphicon-briefcase"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-bullhorn"><i class="append-icon glyphicon glyphicon-bullhorn"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-cog"><i class="append-icon glyphicon glyphicon-cog"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-compressed"><i class="append-icon glyphicon glyphicon-compressed"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-envelope"><i class="append-icon glyphicon glyphicon-envelope"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-export"><i class="append-icon glyphicon glyphicon-export"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-eye-open"><i class="append-icon glyphicon glyphicon-eye-open"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-file"><i class="append-icon glyphicon glyphicon-file"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-folder-open"><i class="append-icon glyphicon glyphicon-folder-open"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-hdd"><i class="append-icon glyphicon glyphicon-hdd"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-import"><i class="append-icon glyphicon glyphicon-import"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-inbox"><i class="append-icon glyphicon glyphicon-inbox"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-lock"><i class="append-icon glyphicon glyphicon-lock"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-map-marker"><i class="append-icon glyphicon glyphicon-map-marker"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-paperclip"><i class="append-icon glyphicon glyphicon-paperclip"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-print"><i class="append-icon glyphicon glyphicon-print"></i></asp:ListItem>
                                            <asp:ListItem Value="glyphicon glyphicon-pushpin"><i class="append-icon glyphicon glyphicon-pushpin"></i></asp:ListItem>


                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="modal-footer background-login">
                                        <asp:Button ID="Button1" runat="server" class="button pull-left" OnClick="Button1_Click" Text="Save Changes" />
                                        <asp:Button ID="Button2" runat="server" class="button pull-right" OnClick="Button2_Click" data-dismiss="modal" Text="Close" />
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                        <!-- /.Popup modal -->

                    </div>
                    <!-- panel-body-list -->
                </div>
            </div>
        </div>
</asp:Content>

