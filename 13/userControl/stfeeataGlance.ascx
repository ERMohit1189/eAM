<%@ Control Language="C#" AutoEventWireup="true" CodeFile="stfeeataGlance.ascx.cs" Inherits="sp_userControl_stfeeataGlance" %>
<div class="col-lg-6  mgbt-xs-10 half-w-1280" runat="server" id="divlastfeepaid">
       <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-rupee-sign"></i></span>Fee at a Glance</h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom"
                    class=" menu entypo-icon">
                    <i class="icon-minus3"></i>
                </div>
                <div data-action="refresh" title="Refresh" data-placement="bottom"
                    class=" menu entypo-icon smaller-font">
                    <i class="icon-cycle"></i>
                </div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body">
            <div class=" no-padding">
                <div class="col-sm-12  tab-red" style="display:none">
                    <fieldset>
                        <legend>Next Due</legend>
                        INR&nbsp;<asp:Label ID="lblFeeDue" runat="server" Text=""></asp:Label>
                    </fieldset>
                </div>
                <div class="col-sm-12  tab-green">
                    <fieldset>
                        <legend>Last Paid</legend>

                        INR&nbsp;<asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                        /- &nbsp; 
                                <asp:Label ID="lblMOP" runat="server" Text=""></asp:Label>&nbsp;by
                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        <br />
                        on

                        <asp:Label ID="lblLastPaid" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblLastPaidDay" runat="server" Text=""></asp:Label>
                    </fieldset>
                </div>
               
            </div>
        </div>
    </div>
</div>
