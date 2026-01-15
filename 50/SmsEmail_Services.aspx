<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="SmsEmail_Services.aspx.cs" Inherits="SuperAdmin_Sms_Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
          <div id="loader" runat="server"></div>  <%-- ==== in aspx file   --%>                            

<asp:UpdatePanel ID ="tyu" runat="server">
        <ContentTemplate>



    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-12 no-padding">
                            <div class="col-sm-4 no-padding">
                               <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                             </div>
                            <div class="col-lg-12 no-padding">
                                <div class=" table-responsive  table-responsive2">
                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("SmsTitle") %>'></asp:Label>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notification" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblad" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow " CssClass="vd_radio radio-success">
                                                        <asp:ListItem Text="Activate"></asp:ListItem>
                                                        <asp:ListItem Text="Deactivate"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-lg-12 no-padding text-center">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Update</asp:LinkButton>
                           <div id="msgbox" runat="server" style="left: 76px;"></div>
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

