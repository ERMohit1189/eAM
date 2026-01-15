<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bulletin.ascx.cs" Inherits="admin_usercontrol_widgets_Wid1" %>


<div class="col-md-4  mgbt-xs-10 dash-b-w">
   <div class="panel widget panel-bd-top vd_bdt-red  vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-pushpin"></i></span>
                <a href="11/NewsReport.aspx?print=1" target="_blank">Bulletin Board
                </a></h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body no-padding">
            <div class=" pad-lr-15-1024-260">
                <marquee onmouseover="this.stop()" onmouseout="this.start()" direction="up" height="293px" scrollamount="2" scrolldelay="50">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Width="100%" GridLines="None" ShowHeader="False" >
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <table align="left" width="100%">
                                        <tr>
                                            <td >
                                                <h5 class="vd_red mgbt-xs-5"><asp:Label ID="Lbdate" runat="server" CssClass="right_menu"
                                                      Text='<%# Bind("NoticeDate") %>'></asp:Label>
                                               
                                                    <asp:Label ID="lblUploadedby" runat="server" CssClass="right_menu" 
                                                    Text='<%# Bind("LoginName") %>'></asp:Label>
                                                </h5>
                                               
                                                <h4 class="vd_red font-semi-bold mgbt-xs-5 text-justify" >
                                                    <asp:Label ID="LbleHead" runat="server" CssClass="date"
                                                         Text='<%# Bind("NoticeHeading") %>' ForeColor="Black" style="font-size: 14px !important; text-transform:none !important;"></asp:Label></h4>
                                                
                                                <p class=" mgbt-xs-5 text-justify">
                                                    <asp:Label ID="blMess" runat="server" CssClass="right_menu" 
                                                    Text='<%# Bind("NoticeMessage") %>' style="font-size: 14px !important; text-transform:none !important;"></asp:Label>
                                                        
                                                </p>
                                               <%-- <p class="vd_red mgbt-xs-5"></p>--%>
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                               
                                <HeaderStyle BorderStyle="None" />
                               
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BorderStyle="None" />
                        <HeaderStyle BorderStyle="None" />
                        
                    </asp:GridView></marquee>

            </div>
        </div>
    </div>
</div>
