<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ItoVRemark.aspx.cs" Inherits="common_ItoVRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="txt-middle">

                                    <div id="mainDiv" class="col-sm-12  no-padding " runat="server">

                                        <div class="col-sm-4">
                                            <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpEval" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged" CssClass="form-control-blue ">
                                                           
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 no-padding">
                                        <br />
                                        <asp:Repeater ID="rpt" runat="server">
                                            <HeaderTemplate>
                                                <%-- ReSharper disable once RequiresFallbackColor --%>
                                                <div class="col-md-12">
                                                    <div class="col-md-1 text-center" style="border: 1px solid #d7d7d7; background-color: #edeef2; width: 3%;">
                                                        <h1 style="font-size: 14px; margin: 4px 0 5px; font-weight: 500;">#</h1>
                                                    </div>
                                                    <div class="col-md-2 text-left" style="border: 1px solid #d7d7d7; background-color: #edeef2;">
                                                        <h1 style="font-size: 14px; margin: 4px 0 5px; font-weight: 500;">S.R. No.</h1>
                                                    </div>
                                                    <div class="col-md-4 text-left" style="border: 1px solid #d7d7d7; background-color: #edeef2;">
                                                        <h1 style="font-size: 14px; margin: 4px 0 5px; font-weight: 500;">Student's Name</h1>
                                                    </div>
                                                    <div class="col-md-5 text-center" style="border: 1px solid #d7d7d7; background-color: #edeef2; width: 47%">
                                                        <h1 style="font-size: 14px; margin: 4px 0 5px; font-weight: 500;">Class Teacher's Remark</h1>
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="col-md-12">
                                                    <div class="col-md-1 text-center" style="border: 1px solid #d7d7d7; padding: 12px 5px; width: 3%;">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-2" style="border: 1px solid #d7d7d7; padding: 12px 5px;">
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("Srno") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-4" style="border: 1px solid #d7d7d7; padding: 12px 5px;">
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-5" style="border: 1px solid #d7d7d7; padding: 6px 5px; width: 47%">
                                                        <asp:TextBox ID="txtCaption" placeholder="Type Here" Visible="false" runat="server" Text='<%# Eval("Caption") %>'></asp:TextBox>
                                                        <asp:DropDownList runat="server" ID="ddlRemark"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                        <div class="col-sm-12   text-center">
                                            <br />
                                            <asp:LinkButton ID="lblSubmit" runat="server" Visible="false" CssClass="button" OnClick="lblSubmit_Click">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 70px"></div>
                                        </div>

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

