<%@ Page Title="" Language="C#" MasterPageFile="~/staff/staff_root-manager.master" AutoEventWireup="true" CodeFile="AllStudentReceiptMonthDate.aspx.cs" Inherits="admin_AllStudentReceiptMonthDate" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script language="javascript" type="text/javascript">
        function disableselect(e) {
            return false;
        }
        function reEnable() {
            return true;
        }

// ReSharper disable once WrongExpressionStatement
        document.onselectstart = new Function("return false");


        if (window.sidebar) {
            document.onmousedown = disableselect; // for mozilla           
            document.onclick = reEnable;
        }

        function clickIE() {
            if (document.all) {
// ReSharper disable once WrongExpressionStatement
                (message);
                return false;
            }
// ReSharper disable once NotAllPathsReturnValue
        }


// ReSharper disable once WrongExpressionStatement
        document.oncontextmenu = new Function("return false");

        var element = document.getElementById('tbl');

        element.onmousedown = function () { return false; }; // mozilla         

    </script>
    <script language="javascript" type="text/javascript">
        $(document).on('keydown', function (e) {
            if (e.ctrlKey && (e.key === "p" || e.charCode === 16 || e.charCode === 112 || e.keyCode === 80)) {
                alert("You have no permission to print this page!");
                e.cancelBubble = true;
                e.preventDefault();
                e.stopImmediatePropagation();
            }
        });
    </script>

    <%--Content starts--%>
    <table class="table">
        <tr>
            <td style="display:none">Select Installment/Date <span class="imp">*</span>
            </td>
            <td style="display:none">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Date Wise</asp:ListItem>
                    <asp:ListItem>Installment Wise</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <asp:DropDownList ID="drpFilter" runat="server" CssClass="textbox" Width="200px">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Installment</asp:ListItem>
                    <asp:ListItem>Yearly</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Mode of Payment <span class="imp">*</span>
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem Selected="True">Cash</asp:ListItem>
                    <asp:ListItem>Check</asp:ListItem>
                    <asp:ListItem>DD</asp:ListItem>
                    <asp:ListItem>Online</asp:ListItem>
                    <asp:ListItem>Card</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>Select Class <span class="imp">*</span>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="Panel2" runat="server">
        <h3>Installment Wise</h3>
        <hr />
        <table class="table">
            <tr>
                <td align="right">Fee Group <span class="imp">*</span></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpGroup" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="DrpGroup_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right">Select Installment <span class="imp">*</span>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownMonth" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="DropDownMonth_SelectedIndexChanged"
                                TabIndex="1" Width="200px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="button" OnClick="LinkButton6_Click">View</asp:LinkButton>
                </td>

            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label32" runat="server" Style="color: #CC0000; font-weight: 700"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <h3>Date Wise</h3>
        <hr />
        <table class="table">
            <tr>
                <td align="right">From Date
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                CssClass="textbox" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                CssClass="textbox" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="FromDD" runat="server" OnSelectedIndexChanged="FromDD_SelectedIndexChanged"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right">To Date
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="textbox"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="textbox"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ToDD" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="button">View</asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>


    <div id="gdv1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table id="abc" runat="server" width="100%">
                    <tr align="center">
                        <td>
                            <asp:Image ID="Image1" runat="server" Height="71px" Width="73px" />

                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblbranchwithcity" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="All Student Receipt Month and Date"></asp:Label>
                            &nbsp; &nbsp;
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                            &nbsp; &nbsp; &nbsp;
                                            <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                            <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text=")"></asp:Label>
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="GridView1_PageIndexChanged"
                                OnPageIndexChanging="GridView1_PageIndexChanging" ShowFooter="True" HorizontalAlign="Center" Width="100%"
                                CssClass="Grid">
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="Label30" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.R. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label24" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                            <asp:Label ID="Label26" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                            <asp:Label ID="Label27" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class">
                                        <ItemTemplate>
                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="Label28" runat="server" Text='<%# Bind("section") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Medium">
                                        <ItemTemplate>
                                            <asp:Label ID="Label22" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt No.">
                                        <ItemTemplate>
                                            &nbsp;
                                                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" Text='<%# Bind("RecieptSrNo") %>' ToolTip='<%# Bind("id") %>'
                                                                        BackColor="#ECE9D8" BorderColor="#C2C2C2" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px"
                                                                        Font-Underline="False" ForeColor="#004CCA"></asp:LinkButton>&nbsp;
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Installment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinstal" runat="server" Text='<%# Bind("FeeMonth") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MOP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMOP" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Label29" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <span>Total Amount </span>
                                                        <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Wrap="True" HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--  <FooterStyle Height="35px" />--%>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <%--<RowStyle Height="30px" />--%>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel4" runat="server" CssClass="popup">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblcancel" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" class="table" cellpadding="4" cellspacing="4">
                <tr>
                    <td align="right">Receipt No. 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Fee 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="Button2" runat="server" Style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Late Fee 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblLate" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Previous Balance 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="Label25" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Conveyance 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="Label33" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Concession 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblConcession" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Hindi Discount 
                    </td>
                    <td>
                        <asp:Panel ID="Panel5" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblDiscountValue" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server" Visible="False">
                                <ContentTemplate>
                                    <asp:Label ID="lblDiscountName" runat="server" Visible="True"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Paid Amount 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Current Balance 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblBalace" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">Remarks 
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="View" />
                        &nbsp;
                                                <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right"></td>
                    <td></td>
                </tr>
            </table>
            <asp:ModalPopupExtender ID="Panel4_ModalPopupExtender" runat="server" CancelControlID="Button1" PopupControlID="Panel4"
                TargetControlID="Button2" BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
        </asp:Panel>
    </div>
    <script language="JavaScript" type="text/javascript">
        document.onkeydown = function () {

            //avoid ctrl+P
            if ((event.ctrlkey) && (event.keycode === 80)) {
                event.cancelbubble = true;
                event.returnvalue = false;
                event.keycode = false;
                return false;
            }

// ReSharper disable once NotAllPathsReturnValue
        }
    </script>
</asp:Content>
