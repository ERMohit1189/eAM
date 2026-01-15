<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PurchaseOrderPrint.aspx.cs" Inherits="PurchaseOrderPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">

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
        function ChecktenDigitss(inputtxt) {
            var phoneno = /^\d+$/;
            if (inputtxt.value.match(phoneno) && inputtxt != null) {
                inputtxt.style.border = "1px solid #D5D5D5";
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    inputtxt.style.border = "1px solid Red";
                    inputtxt.value = "";
                    inputtxt.focus();
                    return false;
                }
            }
        }
    </script>
    <script>
        function getPONosList() {
            $(function () {
                $("[id$=txtPONo]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/VendorIDName.asmx/GetPoNoAll") %>',
                            data: "{ 'PONo': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hdnPO]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
     <style> 
          
            @media print { 
               .noprint { 
                  visibility: hidden; 
               } 
            } 
        </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getPONosList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">

                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="dovPO">
                                        <label class="control-label">Enter P.O. No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPONo" runat="server" AutoPostBack="true" OnTextChanged="txtPONos_TextChanged" CssClass="form-control-blue validatetxt" MaxLength="20"></asp:TextBox>
                                            <asp:HiddenField ID="hdnPO" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lbtnSearchBy" OnClick="lbtnSearchBy_Click" CssClass="button form-control-blue" runat="server">View </asp:LinkButton>
                                        <div runat="server" id="dvSearch" style="left: 55px;"></div>
                                    </div>
                                    <div class="col-sm-4 btn-a-devices-1-p4-p2   mgbt-xs-15 text-right" runat="server" id="divBtn" visible="false">
                                    <a onclick="PrintDiv();" class="button form-control-blue"><i class="fa fa-print text-primary"></i>&nbsp;Print</a>
                                    <div runat="server" id="msgbox" style="left: 55px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12" id="pnlcontrols" runat="server" visible="false">
                                    <div class="col-sm-12 no-padding" style="border:1px solid #000; padding-top: 20px !important;">
                                    <div class="col-sm-12">
                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                        <h2 class="text-center" style="    font-size: 20px;">Purchase Order</h2>
                                        <div class="col-sm-12 no-padding">
                                            <b style="font-weight: bold; font-size: 15px;">To,</b><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;" class="hide">Vendor Code :
                                                <asp:Label Style="font-weight: bold; font-size: 14px;" runat="server" ID="lblvendorCode"></asp:Label><br /></span>
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;">
                                                <asp:Label Style="font-weight: bold; font-size: 14px;" runat="server" ID="lblOragnization"></asp:Label></span><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;">
                                                <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblAddress"></asp:Label>,</span><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;">
                                                <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblCity"></asp:Label>, </span>
                                            <span style="font-weight: bold; font-size: 14px;">
                                                <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblState"></asp:Label>
                                                -<asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblPin"></asp:Label></span><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;">Contact No. :
                                                <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblMobile"></asp:Label>, </span>
                                            <span style="font-weight: bold; font-size: 14px;">
                                                <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblPhone"></asp:Label></span><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;" class="hide">Registration No. :
                                                <asp:Label Style="font-weight: bold; font-size: 14px;" runat="server" ID="RegNo"></asp:Label></span><br />
                                            <span style="font-weight: bold; font-size: 14px; padding-left: 30px;" class="hide">PAN :
                                                <asp:Label Style="font-weight: bold; font-size: 14px;" runat="server" ID="Pan"></asp:Label></span>
                                            
                                            <div class="col-sm-12 no-padding" style="border-bottom:1px solid #ccc; margin-bottom:5px;">
                                                
                                        <table style="width: 100%; ">
                                             <tr>
                                                <td style="width: 50%; text-align:left;">
                                                    <div>
                                                <span style="font-weight: bold; font-size: 14px;">Subject :
                                                    <asp:Label Style="font-size: 14px; font-weight: normal;" runat="server" ID="lblSubject"></asp:Label> </span>
                                            </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; padding: 10px 0px; text-align:left;">
                                                    <span style="font-weight: bold; font-size: 14px;">Date : <asp:Label ID="lblDate" runat="server"></asp:Label></span>
                                                    
                                                </td>
                                                <td style="width: 50%; padding: 10px 0px; text-align:right;">
                                                    <span style="font-weight: bold; font-size: 14px;">P.O. No.  : <asp:Label ID="lblPONo" runat="server"></asp:Label></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; text-align:left;">
                                                    <div style="width: 100%; min-height: 30px; padding: 0 10px 0 0px;">
                                                        <asp:Label ID="txtPODescription" runat="server" TextMode="MultiLine" Enabled="false"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                            <br />

                                        </div>
                                        
                                        <div class=" table-responsive  table-responsive2">
                                            <h3>Quotation Details</h3>
                                            <asp:GridView ID="gvBankBranchList" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QTN. Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorId" Visible="false" runat="server" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("QtnEnterDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTN. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="QtnNo" runat="server" Text='<%# Bind("QtnNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO No." Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PONo" runat="server" Text='<%# Bind("PONo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Title" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%# Eval("Status").ToString()=="Approve"?"Approved":Eval("Status").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    
                                    
                                    <div class="col-sm-12">
                                        <div class="table-responsive2 table-responsive">
                                            <br />
                                            <h3>P.O. Details</h3>
                                            <asp:GridView ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="itemCatName" runat="server" Text='<%# Bind("Caregory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article">
                                                        <ItemTemplate>
                                                            <asp:Label ID="itemName" runat="server" Text='<%# Bind("itemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HSNCode" runat="server" Text='<%# Bind("HSNCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label2f" runat="server" Style="font-weight: bold" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Qty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSQty" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Rate" runat="server" Text='<%# Bind("Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSAmount" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TaxPercent" runat="server" Text='<%# Bind("TaxPercent") %>'></asp:Label>%=
                                                            <asp:Label ID="Tax" runat="server" Text='<%# Bind("Tax") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSTax" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblsTotal" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 55%; padding: 15px;">
                                                <label class="control-label">Terms&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtTerms" runat="server" CssClass="form-control-blue validatetxt" TextMode="MultiLine" Style="height: 130px; border:0; outline:none;" Enabled="false"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 45%; padding: 15px;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">Amount : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                            <asp:Label ID="lblGTotal" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr> <tr runat="server" id="trNa">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">Tax : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblNa" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trCGST" visible="false">
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">CGST : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                            <asp:Label ID="lblCGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trSGST" visible="false">
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">SGST : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                            <asp:Label ID="lblSGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trIGST" visible="false">
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">IGST : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                            <asp:Label ID="lblIGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trUGST" visible="false">
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">UGST : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                            <asp:Label ID="lblUGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;"></td>
                                                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px; border-top: 1px solid #000;">Total : </td>
                                                        <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px; border-top: 1px solid #000;">
                                                            <asp:Label ID="lblGrandTotal" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>

                                </div>
                                
                            </div>



                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function PrintDiv() {
            $('#vendorCode').addClass('hide');

            var headContent = document.getElementsByTagName('head')[0].innerHTML;
            var divContents = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_pnlcontrols").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1350, class="tbls"');
            printWindow.document.write('<html><head><title>Print PO</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();
            $('#vendorCode').removeClass('hide');
        }
    </script>
</asp:Content>

