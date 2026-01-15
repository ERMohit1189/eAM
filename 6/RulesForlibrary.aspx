<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RulesForlibrary.aspx.cs" Inherits="admin_RulesForlibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdorulesFor" AutoPostBack="true" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdorulesFor_SelectedIndexChanged">
                                            <asp:ListItem Value="Student" Selected="True">Student</asp:ListItem>
                                            <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-12 ">
                                        <span class="txt-rep-title-13">RULE 1 </span>
                                        <span class="txt-rep-title-12 tab-in-05">RETURN OF ISSUED BOOK AFTER
                                          <asp:TextBox ID="txtR1" runat="server" onkeyup="CheckIntegerValueonKeyUp(event,this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt"></asp:TextBox>
                                            DAYS FROM ISSUE DATE.</span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 2 </span>
                                        <span class="txt-rep-title-12 tab-in-05">EACH STUDENT WOULD BE ISSUED
                                <asp:TextBox ID="txtR2" runat="server" onkeyup="CheckIntegerValueonKeyUp(event,this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt"></asp:TextBox>BOOKS AT A TIME.
                                        </span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 3 </span>
                                        <span class="txt-rep-title-12 tab-in-05">REISSUE OF SAME BOOKS BY A USER MAY BE ALLOWED MAXIMUM
                                <asp:TextBox ID="txtR3" runat="server" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt"></asp:TextBox>
                                            MORE AFTER FIRST ISSUE IN A MONTH.
                                        </span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 4 </span>
                                        <span class="txt-rep-title-12 tab-in-05">FAILURE TO RETURN BOOKS ON DUE-DATE WOULD COMPULSORILY LEAD TO PENALTY OF RS.
                                <asp:TextBox ID="txtR4" runat="server" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt"></asp:TextBox>/- PER DAY FOR ALL STUDENTS.
                                        </span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 5 </span>
                                        <span class="txt-rep-title-12 tab-in-05">NEWLY PURCHASED BOOKS WILL NOT BE ISSUED TO ANY USER FOR AT LEAST
                                        <asp:TextBox ID="txtR5" runat="server" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt"></asp:TextBox>
                                            MONTH. BUT IT WILL BE DISPLAYED SEPARATELY AS NEW ARRIVALS.
                                        </span>
                                    </div>



                                    <div class="col-sm-12  mgtp-5">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
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

