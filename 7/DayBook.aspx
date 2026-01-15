<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DayBook.aspx.cs" Inherits="AdminDayBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Day Book</title>
     <script src="../js/jquery.min.js"></script>
    <script type="text/javascript">

        function fnNumeric() {
            var code = window.event.keyCode;
            if ((code >= 48 && code <= 57) || (code === 45) || (code === 46)) {
                /*checknos = true;*/
                return true;
            }
            else {
                /*checknos= false;*/
                window.event.keyCode = 0;
                return false;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
           

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="upMain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-lg-12 no-padding" id="tblInsert" runat="server">
                                            <div class="col-lg-12 no-padding">

                                                <div class="col-sm-3 mgbt-xs-15 hide">
                                                    <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Cash Opening Amount</label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtOpeningBalance" ReadOnly="true" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Head Type&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged"  CssClass="form-control-blue validatedrp">
                                                             <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                             <asp:ListItem Value="Income">Income</asp:ListItem>
                                                        <asp:ListItem Value="Expense">Expense</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 mgbt-xs-15 ">
                                                    <label class="control-label">Head&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHead" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 ">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadCategory" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                            <div class="col-lg-12 no-padding">
                                                <div class="col-sm-3 mgbt-xs-15 " id="disheadparticulars" runat="server">
                                                    <label class="control-label">Particulars&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDescriptionGen" onkeyup="return evtEnter(event,'txtDescriptionGen',13);" runat="server" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 " id="disheadamount" runat="server">
                                                    <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtAmount" onkeypress="return fnNumeric()" onkeyup="return evtEnter(event,'txtDescriptionGen',13);" runat="server" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 mgbt-xs-15 " id="disheadpaymentmode" runat="server">
                                                    <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" onchange="MODChenge();" CssClass="form-control-blue">
                                                            <%--<asp:ListItem Value=""><--Select--></asp:ListItem>--%>
                                                        <asp:ListItem>Cash</asp:ListItem>
                                                        <asp:ListItem>Cheque</asp:ListItem>
                                                        <asp:ListItem>DD</asp:ListItem>
                                                        <asp:ListItem>Card</asp:ListItem>
                                                        <asp:ListItem>Online Transfer</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disPMdate">
                                                    <asp:Label ID="lblChqDate" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>

                                                        <asp:TextBox ID="txtDDChequeUTRDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disPMcheqeno">

                                                    <asp:Label ID="lblCheque" runat="server" class="control-label txt-bold" Text="Cheque No."></asp:Label>&nbsp;<span class="vd_red">*</span>

                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDDChequeUTRNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disBank">
                                                    <asp:Label ID="lblBankName" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                               <div class="col-sm-12 half-width-50 mgbt-xs-15 text-center">
                                            
                                                    <asp:LinkButton ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnReset" runat="server" CssClass="button form-control-blue" OnClick="btnReset_Click">Reset</asp:LinkButton>
                                                      <div id="msgbox" runat="server"></div>
                                                     <div id="dvMSG" runat="server" style="left: 147px"></div>
                                            </div>
                                            </div>


                                            
                                            <div class="col-lg-12 ">
                                                
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvDayBook" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="true" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Head">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="HeadName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Head Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="HeadCategory" runat="server" Text='<%# Bind("HeadCategory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Particulars">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Bind("Particulars") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMode" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                   <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                  <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Income">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIncome" runat="server" Text='<%# Bind("Income") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalIncome" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Expense">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpens" runat="server" Text='<%# Bind("Expens") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalExpens" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalances" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="true" Text="Total" Visible="false"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="lbtnDelete_Click" Visible="false"
                                                                        title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                         <tr>
                            <td>Head<span class="vd_red">*</span> </td>
                            <td>
                                <asp:DropDownList ID="ddlHead0" runat="server" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Head Category<span class="vd_red">*</span> </td>
                            <td>
                                <asp:DropDownList ID="ddlHeadCategory0" runat="server" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>Particulars<span class="vd_red"></span> </td>
                            <td>
                                <asp:TextBox ID="txtParticulars0" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();javascript:scroll(0,0);" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

                                            <div class="col-lg-12 no-padding">
                                                <div style="overflow: auto; width: 2px; height: 1px">
                                                    <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                                                        <table class="tab-popup text-center">
                                                            <tr>
                                                                <td>
                                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                                        <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:center;" height="50">
                                                                    <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />

                                                                    &nbsp; &nbsp; 
                                                                    <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                                                            Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function MODChenge() {
            $("[id*=disBank]").addClass("hide");
            $("[id*=disPMcheqeno]").addClass("hide");
            $("[id*=disPMdate]").addClass("hide");
            if ($("[id*=ddlPaymentMode]").val() == "Cash" || $("[id*=ddlPaymentMode]").val() == "") {
                $("[id*=txtDDChequeUTRDate]").removeClass("validatetxt");
                $("[id*=txtDDChequeUTRNo]").removeClass("validatetxt");
                $("[id*=txtBank]").removeClass("validatetxt");
                $("[id*=txtBank]").val("");
            }
            else {
                $("[id*=txtDDChequeUTRDate]").addClass("validatetxt");
                $("[id*=txtDDChequeUTRNo]").addClass("validatetxt");
                $("[id*=txtBank]").addClass("validatetxt");
                $("[id*=txtBank]").val("NA");



                $("[id*=disBank]").removeClass("hide");
                $("[id*=disPMcheqeno]").removeClass("hide");
                $("[id*=disPMdate]").removeClass("hide");
                if ($("[id*=ddlPaymentMode]").val() == "Cheque" || $("[id*=ddlPaymentMode]").val() == "DD") {
                    $("[id*=lblChqDate]").html("Instrument Date");
                    $("[id*=lblCheque]").html("Instrument No.");
                    $("[id*=lblBankName]").html("Issuer");
                }
                else if ($("[id*=ddlPaymentMode]").val() == "Online Transfer" || $("[id*=ddlPaymentMode]").val() == "Other") {
                    $("[id*=lblChqDate]").html("Transaction Date");
                    $("[id*=lblCheque]").html("Ref. No.");
                }
                else if ($("[id*=ddlPaymentMode]").val() == "Card") {
                    $("[id*=lblChqDate]").html("Transaction Date");
                    $("[id*=lblCheque]").html("Card No.");
                    $("[id*=lblBankName]").html("Issuer");
                }
                
                if ($("[id*=ddlPaymentMode]").val() == "Other") {
                    $("[id*=lblBankName]").html("Reference Name");
                }
            }

        }
    </script>
</asp:Content>



