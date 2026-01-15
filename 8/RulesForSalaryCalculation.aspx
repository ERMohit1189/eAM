<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RulesForSalaryCalculation.aspx.cs" Inherits="admin_RulesForSalaryCalculation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="th" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 well">
                                    <div class="col-sm-4 ">
                                        <span class="txt-rep-title-13">Designation</span><br />
                                        <span class="txt-rep-title-12 tab-in-05">
                                            <asp:DropDownList ID="drpEmpDesigation" runat="server" CssClass="form-control-blue drpValidate" AutoPostBack="true" OnSelectedIndexChanged="drpEmpdes_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding well" runat="server" visible="false" id="divData">
                                    <div class="col-sm-6 ">
                                            <span class="txt-rep-title-13">RULE 1 </span>
                                            <span class="txt-rep-title-12 tab-in-05">Allowed CL in a month
                                          <asp:TextBox ID="txtAllowedCL" runat="server" onkeyup="CheckDecimalNumber(this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt" style="width: 73px !important; text-align:right !important;"></asp:TextBox>
                                            </span>
                                        </div>
                                     <div class="col-sm-6">
                                            <span class="txt-rep-title-13">RULE 2 </span>
                                            <span class="txt-rep-title-12 tab-in-05"> 
                                            <asp:TextBox ID="txtNooflateToCL" runat="server" onblur="CheckDecimalNumber(this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt" style="width: 73px !important; text-align:right !important;"></asp:TextBox>
                                               Late is equal to 1 CL
                                            </span>
                                        </div>
                                        <div class="col-sm-6">
                                            <span class="txt-rep-title-13">RULE 3 </span>
                                            <span class="txt-rep-title-12 tab-in-05">
                                            <asp:TextBox ID="txtNoofSLToCL" runat="server" onblur="CheckDecimalNumber(this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt" style="width: 73px !important; text-align:right !important;"></asp:TextBox>
                                            SL is equal to 1 CL
                                            </span>
                                        </div>
                                    

                                         <div class="col-sm-6">
                                            <span class="txt-rep-title-13">RULE 4 </span>
                                            <span class="txt-rep-title-12 tab-in-05"> 
                                          <asp:TextBox ID="txtNoofHDToCL" runat="server" onblur="CheckDecimalNumber(this);" CssClass="underlined text-center input-border-btm width-5 vd_bd-red validatetxt" style="width: 73px !important; text-align:right !important;"></asp:TextBox>
                                             HD is equal to 1 CL
                                            </span>
                                        </div>
                                    <div class="col-sm-12 text-center">
                                        <br />
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateDropdown('.drpValidate');ValidateTextBox('.validatetxt'); return validationReturn();"
                                            CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="divmsg" runat="server" style="left: 76px;"></div>
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

