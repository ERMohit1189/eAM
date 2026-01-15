<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="IXtoXSelfAwareness.aspx.cs" Inherits="common_IXtoXSelfAwareness" %>

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

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
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

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
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

                                       <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpEval" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" CssClass="form-control-blue ">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Type of Self Awareness&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpSelfAwareness" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpSelfAwareness_SelectedIndexChanged" CssClass="form-control-blue ">
                                                           
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Repeater ID="rpt" runat="server">
                                            <HeaderTemplate>
                                                <%-- ReSharper disable once RequiresFallbackColor --%>
                                                 <div class="col-sm-12 " >
                                                <div class="col-md-12 well" style="padding: 3px 0 3px 0; margin-bottom: 5px; 
                                                     background-color:rgba(35, 112, 158, 0.85); color:white;">
                                                    <div class="col-md-2 text-left">
                                                        <h1 style="font-size: 16px;margin: 10px 0 10px;">S.R.NO.</h1>
                                                    </div>
                                                    <div class="col-md-4 text-left">
                                                        <h1 style="font-size: 16px;margin: 10px 0 10px;">STUDENT NAME</h1>
                                                    </div>
                                                    <div class="col-md-6 text-center">
                                                        <h1 style="font-size: 16px;margin: 10px 0 10px;">CAPTION</h1>
                                                    </div>
                                                </div>
                                                     </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <div class="col-sm-12 " >
                                                <div class="col-md-12 well" style="padding: 3px 0 3px 0; margin-bottom: 5px;">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("Srno") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtCaption" placeholder="Type Here" runat="server" Text='<%# Eval("Caption") %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                     </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="col-sm-12   text-center">
                                            <asp:LinkButton ID="lblSubmit" runat="server" CssClass="button" OnClick="lblSubmit_Click">Submit</asp:LinkButton>
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

