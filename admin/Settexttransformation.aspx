<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Settexttransformation.aspx.cs" Inherits="Administrator_Settexttransformation" %>

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
                        <div class="col-sm-12 no-padding">
                            <div class="col-lg-7 no-padding">
                                <div class="form-group ">
                                    
                                    <div class="col-sm-12 controls mgbt-xs-20">
                                        <asp:Label ID="Label4" runat="server" class="txt-bold " Text="Select Text Transformation"></asp:Label><br />
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow " CssClass="vd_radio radio-success">
                                            <asp:ListItem Value="U">Upper Case</asp:ListItem>
                                            <asp:ListItem Value="L">Lower Case</asp:ListItem>
                                            <asp:ListItem Value="C">Capitalize</asp:ListItem>
                                            <asp:ListItem Value="T">Typed</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                           
                        </div>
                        <div class="col-sm-12">
                            <ul class="vd_li" style="padding: 0px !important;">
                                        <li><i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>Upper Case <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                            <asp:Label ID="Label1" runat="server" Text="THIS IS SAMPLE TEXT"></asp:Label>
                                        </li>
                                        <li><i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>Lower Case <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                            <asp:Label ID="Label2" runat="server" Text="this is sample text"></asp:Label>
                                        </li>
                                        <li><i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>Capitalize <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                            <asp:Label ID="Label3" runat="server" Text="This Is Sample Text"></asp:Label>
                                        </li>

                                    </ul>
                        </div>
                         <div class="col-lg-12 mgbt-xs-20">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

