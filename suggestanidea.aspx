<%@ Page Title="Suggest an Idea | eAM&reg;" Language="C#" MasterPageFile="~/root-manager.master" AutoEventWireup="true" CodeFile="suggestanidea.aspx.cs" Inherits="SuggestEnIdea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="lblClassSection" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Enter Your Idea"></asp:Label>
                                    <div class="col-sm-6  controls mgbt-xs-15">
                                         <asp:TextBox ID="txtIdea" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label2" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Describe Your Idea (Optional)"></asp:Label>
                                    <div class="col-sm-6  controls mgbt-xs-15">
                                        <asp:TextBox ID="txtIdeaDesc" CssClass="form-control-blue" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label3" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Email"></asp:Label>
                                    <div class="col-sm-6  controls mgbt-xs-15">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label4" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Attachment"></asp:Label>
                                    <div class="col-sm-6  controls mgbt-xs-15">
                                        <asp:FileUpload ID="fileAttachment" CssClass="form-control-blue" runat="server" />
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                         
                      
                    </div>
                    <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <div class="col-sm-12  controls mgbt-xs-15 text-center">
                                 <asp:LinkButton runat="server" ID="submit" OnClick="submit_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 70px !important;"></div>
                                        </div>
                                </div>
                             </div>
                        </div>
                </div>
                 
            </div>
           <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2" style="border:none;">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Your Idea">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("idea") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("ideaDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attachment">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="download" NavigateUrl='~/uploads/FeedBack/<%# Bind("attachment") %>'><i class="fa fa-download"></i> Download</asp:HyperLink>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("attachment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
        </div>
    </div>
</asp:Content>

