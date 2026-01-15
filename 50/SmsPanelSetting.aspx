<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="SmsPanelSetting.aspx.cs"
    Inherits="SuperAdmin_SmsPanelSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4 col-xs-4  mgbt-xs-15">
                                        <label class="control-label">Select Panel&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" RepeatLayout="Flow " CssClass="vd_radio radio-success"
                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">HTTPS Gateway</asp:ListItem>
                                                <asp:ListItem Value="2">GSM Device</asp:ListItem>
                                                <asp:ListItem Value="3">SMPP Gateway</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   mgbt-xs-15">
                                        <label class="control-label">Branch</label>
                                          <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                                <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                            </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="panel12" runat="server" visible="true">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Panel URL&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPanelurl" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPanelurl" ErrorMessage="Can't leave blank!"
                                                    SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Username &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtUserId" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtUserId" ErrorMessage="Can't leave blank!"
                                                    SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Password&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtPassword" ErrorMessage="Can't leave blank!"
                                                    SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Sender ID&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtSenderId" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtSenderId" ErrorMessage="Can't leave blank!"
                                                    SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Priority&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtPriority" runat="server" CssClass="form-control-blue">ndnd</asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">SMS Type&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtSmstype" runat="server" CssClass="form-control-blue">normal</asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">SMS Facility</label>
                                        <div class="">
                                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="a" Text="Activate" ValidationGroup="a" RepeatLayout="Flow " CssClass="vd_radio radio-success" />
                                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="a" Text="Deactivate" ValidationGroup="a" RepeatLayout="Flow " CssClass="vd_radio radio-success" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="panel3" runat="server" visible="false">
                                    <div class="col-sm-6  ">
                                        <fieldset>
                                            <legend>GSM Device Information</legend>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Connected on Port&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Operating Mode&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Check for messages every&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Check connection every&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Use synchronized delay&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label class="control-label">Milisecond(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <div class="">
                                                    <asp:CheckBox ID="CheckBox1" Text="Maintain a log for this connection" runat="server" CssClass="vd_checkbox checkbox-success" />
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button form-control-blue">Test GSM Device Connection</asp:LinkButton>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="col-sm-6  ">
                                        <fieldset>
                                            <legend>Detected GSM Device</legend>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Port&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">IMEI Number&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Make&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Model&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Software Version&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Memory Status&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Port&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="panel4" runat="server" visible="false">
                                    <div class="col-sm-4  ">
                                        <fieldset>
                                            <legend>SMSC Location and Setting </legend>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Host IP&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Port&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Bind Mode&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">SMPP Version&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">System Type&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12  no-padding mgbt-xs-15">
                                                <div class="col-sm-6  ">
                                                    <label class="control-label">Username&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6  ">
                                                    <label class="control-label">Password&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Bind Timeout&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">

                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="col-sm-4  ">
                                        <fieldset>
                                            <legend>Source and Destination Addressing </legend>
                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Source TON&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Source NPI&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Source Address&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Address Range&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Destination TON&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label">Destination NPI&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>

                                    <div class="col-sm-4  ">
                                        <fieldset>
                                            <legend>Other</legend>


                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Send Messages Every&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Send Enqure Link Every&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList16" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Reconnection Attempts&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList13" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label"></label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Reconnection Interval&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList14" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label">Second(s)</label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <label class="control-label  no-padding">Messages / Second&nbsp;<span class="vd_red">*</span></label>
                                                <div class="col-xs-8 no-padding">
                                                    <asp:DropDownList ID="DropDownList17" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 ">
                                                    <label class="control-label"></label>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <div class="">
                                                    <asp:CheckBox ID="CheckBox2" Text="Maintain a log for this connection" runat="server" CssClass="vd_checkbox checkbox-success" />
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>


                                        </fieldset>
                                    </div>

                                </div>

                                <div class="col-sm-12  half-width-50  mgbt-xs-15">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 74px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
