<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GenralMessage.aspx.cs" Inherits="admin_GenralMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function GetCount(txtStr) {
            document.getElementById("<%= Label8.ClientID %>").innerHTML = txtStr.length;
        }
        function alertmsg() {
            alert(
                "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(GetCount(txtStr));
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-12 no-padding " style="display: none">
                                        <div class=" col-sm-6  no-padding half-width-50 mgbt-xs-15">

                                            <asp:Label ID="Label14" runat="server" class="col-sm-2 col-xs-3 control-label" Text="Type"></asp:Label>
                                            <div class="col-sm-8 col-xs-9 controls ">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow"
                                                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True">Manual</asp:ListItem>
                                                    <asp:ListItem>Listwise</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  no-padding " runat="server" id="table3" visible="false">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table2" runat="server" visible="false">
                                            <label class="control-label">Select Group&nbsp;<span class="vd_red">*</span></label>
                                            <div class="controls txt-middle">
                                                <asp:DropDownList ID="drpGrpName" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6  half-width-50 mgbt-xs-15" id="table1" runat="server" visible="false">
                                            <label class="control-label">Enter No. (Type 10 digit mobile number in each line without 91)&nbsp;<span class="vd_red">*</span></label>
                                            <div class="mgbt-xs-15">
                                                <asp:TextBox ID="txtNo" runat="server" CssClass="form-control-blue" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNo" ErrorMessage="*"
                                                        ValidationGroup="a" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Excel&nbsp;<span class="vd_red">*</span></label>
                                            <asp:FileUpload ID="fu1" runat="server" />
                                            <asp:Label ID="Label1" runat="server" Text="Note: First column should have only mobile numbers without 91." ForeColor="Red"></asp:Label>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button">Show</asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkShow" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>

                                        <div class="col-sm-12   mgbt-xs-15">
                                            <label class="control-label">SMS&nbsp;<span class="vd_red">*</span></label>
                                            <div class="mgbt-xs-15">
                                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Font-Size="12" Rows="4" CssClass="form-control-blue" onkeyup="GetCount(this.value);"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                        ControlToValidate="txtMessage" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                    <span style="text-align: left">
                                                        <asp:Label ID="Label8" runat="server" Text="0"></asp:Label></span>
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label " Text=" (For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-12   ">

                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClick="LinkButton1_Click" ValidationGroup="a"><i class="fa fa-paper-plane"></i> &nbsp; Send</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 85px !important;"></div>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button" OnClick="LinkButton2_Click" ValidationGroup="a"><i class="fa fa-paper-plane"></i> &nbsp; Send</asp:LinkButton>
                                            <div id="msgbox1" runat="server" style="left: 85px !important;"></div>

                                        </div>

                                    </div>
                                    <div class="col-sm-12 no-padding ">
                                    <div id="Div1" runat="server" style="left: 85px !important;" class="text-danger"></div>
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

