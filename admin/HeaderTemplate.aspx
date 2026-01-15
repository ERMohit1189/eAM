<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="HeaderTemplate.aspx.cs" Inherits="admin_HeaderTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upp" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12   ">
                                        <label class="control-label">Select Header Template</label>
                                        <div class="">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="Report">Report</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="Receipt">Receipt</asp:ListItem>
                                                <asp:ListItem Value="Certificate">Certificate</asp:ListItem>
                                                <asp:ListItem Value="Examination">Examination Report</asp:ListItem>
                                                <asp:ListItem Value="Result">Exam Report Card</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-12 col-sm-12 ">

                                        <fieldset>
                                            <legend>
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                    <asp:ListItem Value="With Header">With Header</asp:ListItem>
                                                    <asp:ListItem Value="Without Header">Without Header</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </legend>


                                            <div class="col-sm-12  no-padding">

                                                <table class="tab-popup">
                                                    <tbody>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right" style="width: 145px;">
                                                                <asp:Label ID="Label11" runat="server" class="txt-bold " Text="Board/ University Logo"></asp:Label></td>
                                                            <td style="width: 145px;">
                                                                <asp:RadioButtonList ID="rbUniversityLogo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbUniversityLogo_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                            <td>
                                                                <asp:Image ID="Image2" CssClass="sub-m-w-70" runat="server" ImageUrl="~/img/cbse-logo.png" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right" style="width: 145px;">
                                                                <asp:Label ID="Label1" runat="server" class="txt-bold " Text="Institute Logo"></asp:Label></td>
                                                            <td style="width: 145px;">
                                                                <asp:RadioButtonList ID="rblogo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rblogo_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                            <td>
                                                                <asp:Image ID="Image1" CssClass="sub-m-w-70" runat="server" /></td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label2" runat="server" class="txt-bold " Text="Institute Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbIns" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbIns_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblInstitute" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label3" runat="server" class=" txt-bold " Text="Address"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbAdd" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success"
                                                                    OnSelectedIndexChanged="rbAdd_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblAddress" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label5" runat="server" class="txt-bold" Text="Branch and City"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbBranchandCity" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true" CssClass="vd_radio radio-success" RepeatLayout="Flow" OnSelectedIndexChanged="rbBranchandCity_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblBranchandCity" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label4" runat="server" class="txt-bold " Text="City"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbCity" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbCity_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblCity" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label6" runat="server" class=" txt-bold " Text="Phone No. & E-mail"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbContactnoandemail" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbContactnoandemail_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblContactnoandemail" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label7" runat="server" class=" txt-bold " Text="Phone No."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbPhoneno" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbPhoneno_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblPhoneNo" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label8" runat="server" class=" txt-bold " Text="E-mail"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbEmail" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbEmail_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblEmail" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label9" runat="server" class=" txt-bold " Text="Website & E-mail"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbWebandemail" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbWebandemail_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblWebsiteandEmail" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label12" runat="server" class=" txt-bold " Text="Website"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbWeb" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbWeb_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblWebsite" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label10" runat="server" class=" txt-bold " Text="Affiliation"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbAffila" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbAffila_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblAffilation" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="box-tb box-bg-ed">
                                                            <td class="text-right">
                                                                <asp:Label ID="Label13" runat="server" class=" txt-bold " Text="Affiliation No."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbAffilationNo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbAffilationNo_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblAffilationNo" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr class="box-tb box-bg-ed">
                                                             <td class="text-right">
                                                                 <asp:Label ID="Label15" runat="server" class=" txt-bold " Text="Institute/School No."></asp:Label>
                                                             </td>
                                                             <td>
                                                                 <asp:RadioButtonList ID="rbSchoolNo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                     RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbSchoolNo_SelectedIndexChanged">
                                                                     <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                     <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                 </asp:RadioButtonList>
                                                             </td>
                                                             <td>
                                                                 <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                 <asp:Label ID="lblSchoolNo" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                             </td>
                                                         </tr>
                                                        <tr>
                                                            <td class="text-right">
                                                                <asp:Label ID="Label14" runat="server" class=" txt-bold " Text=" Slogan"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbSlo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                    RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rbSlo_SelectedIndexChanged">
                                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="false" Selected="True">NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <i class="fa  fa-hand-o-right fa-fw append-icon vd_blue"></i>
                                                                <asp:Label ID="lblSlogan" runat="server" class="  txt-bold " Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                            </div>

                                        </fieldset>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-sm-12 ">
                                        <fieldset>
                                            <legend>Preview</legend>
                                            <div class="header-box-preview " style="width: 100%; margin-left: 0px;">
                                                <table class="full-header-a4">
                                                    <tr>
                                                        <td>
                                                            <div id="div1" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </fieldset>

                                    </div>



                                    <div class="col-lg-6 col-md-12 col-sm-12 ">
                                        <fieldset>
                                            <legend>Document Setting</legend>

                                            <div class="col-sm-12  no-padding">
                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Font Size</label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpFontsize" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpFontsize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10">Small</asp:ListItem>
                                                            <asp:ListItem Value="12" Selected="True">Normal</asp:ListItem>
                                                            <asp:ListItem Value="14">Large</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">View Text</label>
                                                    <div class="">
                                                        <asp:Label ID="lblEx" runat="server" Text="TESTING DEMO"></asp:Label>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Top Margin</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtTop" runat="server" CssClass="form-control-blue" Text="0.50"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Right Margin</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtRight" runat="server" CssClass="form-control-blue" Text="0.50"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Bottom</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtBottom" runat="server" CssClass="form-control-blue" Text="0.50"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Left</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtLeft" runat="server" CssClass="form-control-blue" Text="0.50"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>





                                        </fieldset>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-sm-12 ">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
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

