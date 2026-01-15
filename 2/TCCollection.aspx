<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TCCollection.aspx.cs"
    Inherits="TCCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%-- ==== in aspx file   --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSrNo]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudentForTc") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    response($.map(data.d,
                                        function (item) {
                                            return {
                                                label: item.split('@')[0],
                                                val: item.split('@')[1]
                                            }
                                        }));
                                }
                                else {
                                    $("[id$=hfStudentId]").val("000000");
                                }
                            },
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
                            failure: function (response) {
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                Sys.Application.add_load(scrollbar);
                
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(datetime);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">

                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="padding-bottom:30px !important;">


                                    <div class="col-sm-4  ">
                                        <div class="">
                                            <asp:TextBox ID="txtSrNo" placeholder="Enter Name/ S.R. No." runat="server" CssClass="form-control-blue validatetxt1" OnTextChanged="TxtEnter_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                            <div class="text-box-msg">
                                        <asp:Label ID="lblResult" runat="server" Style="color: #FF0000"></asp:Label><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSrNo" ErrorMessage="Please enter S.R. No./Enrollment No."
                                                    SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-1  ">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClientClick="return ValidateTextBox('.validatetxt1');" OnClick="LinkButton6_Click" class="button"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-5  ">
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                         <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;" runat="server" id="grdshow" visible="False">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="GrdStudent" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2f3" runat="server" Text='<%# Bind("combineclassname") %>'></asp:Label>
                                                                    <asp:Label ID="lblClass" runat="server" CssClass="hide" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" CssClass="hide" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    <asp:Label ID="lblSection" runat="server" CssClass="hide" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDOA" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle />
                                                        <RowStyle />
                                                    </asp:GridView>
                                                </td>
                                                <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 53px;" />
                                                            </asp:HyperLink>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                        <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                            </div>
                                        <div class="col-sm-12  no-padding " id="Div1" runat="server" visible="False">

                                            <div class="col-sm-4  " style="margin: 0 !important; padding: 0 !important;">
                                                <label class="control-label">Date of Withdrawal&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpYY" runat="server" CssClass="form-control-blue col-sm-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpMM" runat="server" CssClass="form-control-blue col-sm-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpDD" runat="server" CssClass="form-control-blue col-sm-4">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-4  ">
                                                <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem>Passed</asp:ListItem>
                                                        <asp:ListItem>Failed</asp:ListItem>
                                                        <asp:ListItem>Left</asp:ListItem>
                                                        <asp:ListItem>Absent</asp:ListItem>
                                                        <asp:ListItem>Compartment</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  " style="padding-right: 0; padding-left: 0;">
                                                <label class="control-label">Reason&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtReason" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control-blue" Text="N/A"></asp:TextBox>

                                                    <div class="text-box-msg">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtReason" ErrorMessage="Please enter reason."
                                                            SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="b" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>





                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  no-padding">


                                        <div id="divControls" runat="server" visible="false">
                                            <div class="row" style="padding-left:15px; padding-right:15px;">
                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label16" runat="server" class="control-label">Date of Application&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                                    CssClass="form-control-blue col-xs-4">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                                    CssClass="form-control-blue col-xs-4">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DDDate" runat="server"
                                                                    CssClass="form-control-blue col-xs-4 ">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label41" runat="server" class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True" TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
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
                                                <div  class="col-sm-3 " id="table1" runat="server" visible="false">
                                                    <asp:Label ID="Label4" class="control-label" runat="server"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtChequeDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div  class="col-sm-3 " id="table2" runat="server" visible="false">
                                                    <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div  class="col-sm-3 " id="table12" runat="server" visible="false">
                                                    <asp:Label ID="Label43" runat="server" class="control-label" Text="Bank Name"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div  class="col-sm-3 " id="table4" runat="server" visible="false">
                                                    <asp:Label ID="Label5" runat="server" class="control-label" Text="Status"></asp:Label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlChequeStatus" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="Pending"></asp:ListItem>
                                                            <asp:ListItem Value="Paid"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>


                                                <div  class="col-sm-3 " style="display: none">
                                                    <asp:Label ID="Label12" runat="server" class="control-label" Text="Class"></asp:Label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue" Enabled="False">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label13" runat="server" class="control-label">Amount&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control-blue validatetxt" Enabled="false" AutoPostBack="True" Text="0" OnTextChanged="txtAmt_TextChanged"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt" ErrorMessage="*"
                                                                SetFocusOnError="True" Style="color: #FF0000" ValidationExpression="[-+]?[0-9]{0,3}\.?[0-9]{1,2}" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label3" runat="server" class="control-label" Text="Fine"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtCBFee" runat="server" CssClass="form-control-blue"
                                                            AutoPostBack="True" OnTextChanged="txtCBFee_TextChanged" Enabled="false" Text="0"></asp:TextBox>

                                                        <div class="text-box-msg">

                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCBFee" ErrorMessage="*"
                                                                SetFocusOnError="True" Style="color: #FF0000" ValidationExpression="[-+]?[0-9]{0,3}\.?[0-9]{1,2}" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label14" runat="server" class="control-label" Text="Exemption"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtConcession" runat="server" CssClass="form-control-blue" AutoPostBack="True" Text="0" OnTextChanged="txtConcession_TextChanged" Enabled="False"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label15" runat="server" class="control-label">Received Amount&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtReceviedAmount" runat="server" CssClass="form-control-blue  validatetxt" Enabled="False" Text="0"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div  class="col-sm-3 ">
                                                    <asp:Label ID="Label6" runat="server" class="control-label">TC Template&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlTemplate" runat="server" CssClass="form-control-blue validatedrp">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="Template 1">CBSE-English</asp:ListItem>
                                                            <asp:ListItem Value="Template 2">CBSE-Hindi</asp:ListItem>
                                                            <asp:ListItem Value="Template 3">ICSE/ISC</asp:ListItem>
                                                            <asp:ListItem Value="Template 4">UPBoard-Hindi</asp:ListItem>
                                                             <asp:ListItem Value="Template 5">UPBoard-English</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div  class="col-sm-3 " runat="server" id="divCopy">
                                                    <asp:Label ID="Label2" runat="server" class="control-label">Copy&nbsp;<span class="vd_red">*</span></asp:Label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlCopy" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlCopy_SelectedIndexChanged">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="Original">Original</asp:ListItem>
                                                            <asp:ListItem Value="Duplicate">Duplicate</asp:ListItem>
                                                            <asp:ListItem Value="Triplicate">Triplicate</asp:ListItem>
                                                            <asp:ListItem Value="Quadruplicate">Quadruplicate</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                                <ajaxToolkit:Accordion ID="Accordion1" runat="server" AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                                                    RequireOpenedPane="false" SelectedIndex="-1" CssClass="panel mgbt-xs-5 widget">
                                                    <Panes>
                                                        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                                                            <Header>
                                                                <script>
                                                                    function changetext(header) {
                                                                        var i = header.getElementsByTagName("i");
                                                                        if (i[0].getAttribute("class") === "fa fa-arrow-circle-down") {
                                                                            i[0].setAttribute("class", "fa fa-arrow-circle-up");
                                                                        }
                                                                        else {
                                                                            i[0].setAttribute("class", "fa fa-arrow-circle-down");
                                                                        }
                                                                    }
                                                                </script>
                                                                <span class="btn btn-default small-btn" style="margin-bottom: 30px !important; margin-top: 30px !important; margin-left: 14px !important;" id="header" runat="server" onclick="changetext(this);" title="Click here for more details.">
                                                                    <i class="fa fa-arrow-circle-down"></i>&nbsp;Expand</span>
                                                            </Header>
                                                            <Content>
                                                                <div class="col-sm-4  hide">
                                                                    <asp:Label ID="Label7" runat="server" class="control-label" Text="Board Registration No."></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtInrollmentNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  ">
                                                                    <asp:Label ID="Label22" runat="server" class="control-label" Text="Book No."></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtBookNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label8" runat="server" class="control-label" Text="Date of Struck Off"></asp:Label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDYearStruck" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearStruck_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDMonthStruck" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthStruck_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDDateStruck" runat="server"
                                                                                    CssClass="form-control-blue col-xs-4 ">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-lg-15">
                                                                    <asp:Label ID="Label40" runat="server" class="control-label" Text="Date of Issue"></asp:Label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDYear1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear1_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDMonth1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth1_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDDate1" runat="server"
                                                                                    CssClass="form-control-blue col-xs-4 ">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <asp:Label ID="Label45" runat="server" class="control-label" Text="Date of first admission in the school"></asp:Label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDYearFAD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDMonthFAD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DDDateFAD" runat="server"
                                                                                    CssClass="form-control-blue col-xs-4 ">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <asp:Label ID="Label46" runat="server" class="control-label" Text="First admission in the school in class"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtFAC" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label49" runat="server" class="control-label" Text="Whether qualified for promotion to the higher class"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtIsQualified" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <asp:Label ID="Label50" runat="server" class="control-label" Text="If so, to which class"></asp:Label>
                                                                   
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtIsQualifiedtowhichclass" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-sm-4  mgbt-lg-15">
                                                                    <asp:Label ID="Label21" runat="server" class="control-label" Text="Whether the pupil has paid all dues to the school"></asp:Label>
                                                                 
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtDues" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <asp:Label ID="Label24" runat="server" class="control-label" Text="Whether any fee concession was availed"></asp:Label>
                                                              
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtConcessiontype" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 ">
                                                                    <asp:Label ID="Label25" runat="server" class="control-label" Text="Total No. of working days"></asp:Label>
                                                              
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtTWD" runat="server" CssClass="form-control-blue">0</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label26" runat="server" class="control-label" Text="Total No. of working days present"></asp:Label>
                                                                   
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtTWDP" runat="server" CssClass="form-control-blue">0</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <asp:Label ID="Label29" runat="server" class="control-label" Text="Whether the pupil is NCC Cadet/Boy Scout/Girl Guide"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtNCC" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <asp:Label ID="Label35" runat="server" class="control-label" Text="Games played or extra curricular activities"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtECA" runat="server" CssClass="form-control-blue" MaxLength="100"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 " >
                                                                    <asp:Label ID="Label19" runat="server" class="control-label" Text="General conduct"></asp:Label>
                                                                
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtConduct" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <asp:Label ID="Label17" runat="server" class="control-label" Text="Reason for leaving the school"></asp:Label> 
                                                                   
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue" Rows="1" TextMode="MultiLine">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                
                                                                <div class="col-sm-4 ">
                                                                    <asp:Label ID="Label20" runat="server" class="control-label" Text="Subjects Studied"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtSubjects" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine">N/A</asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 ">
                                                                    <asp:Label ID="Label44" runat="server" class="control-label" Text="Any Other Remarks"></asp:Label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtAOR" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </Content>
                                                        </ajaxToolkit:AccordionPane>
                                                    </Panes>
                                                </ajaxToolkit:Accordion>
                                            </div>
                                            <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 20px; text-align: left;">
                                                <div class="col-sm-12">
                                                    <asp:LinkButton ID="Submit" runat="server" OnClick="Submit_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" CssClass="button">Submit</asp:LinkButton>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 " style="padding-left:15px; padding-right:15px; padding-top:20px;">
                                            <div class="table-responsive2 table-responsive">
                                                <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4s" runat="server" CssClass="hide" Text='<%# Bind("srno") %>'></asp:Label>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date of Application">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AdmissionFromDate" runat="server" Text='<%# Bind("AdmissionFromDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date of Issue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="TCIssueDate" runat="server" Text='<%# Bind("TCIssueDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reciept No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label18" runat="server" Text='<%# Bind("RecieptNo") %>' ></asp:Label> <%--CssClass="hide"--%>
                                                                <%--<asp:LinkButton ID="LinkButton2s" runat="server" title="Print Receipt" OnClick="LinkRecept_Click" data-placement="top" Text='<%# Bind("RecieptNo") %>'></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ReceivedAmount" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MOP" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Copy">
                                                            <ItemTemplate>
                                                                <asp:Label ID="TCType" runat="server" Text='<%# Bind("TCType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" title="Edit T.C."  OnClick="LinkButton1_Click"
                                                                    class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cancel" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label37" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                    title="Cancel T.C."  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Receipt" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label38" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                                                    title="Print Receipt"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# (Eval("Cancel").ToString()==""?"Received":"Canceled") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T.C.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFormate" runat="server" Text='<%# Bind("Formate") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRecieptNo" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:HyperLink ID="LinkButton7" runat="server" Target="_blank"
                                                                    title="Print T.C. "  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="grid_heading_default" />
                                                    <RowStyle CssClass="grid_details_default" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div class="" style="max-height: 600px; max-width: 1200px; overflow: auto;">
                                            <div class="col-sm-12">
                                                <div id="msg2" runat="server" style="left: 60px;"></div>
                                            </div>
                                            <div class="col-sm-3 hide">
                                                <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtSrNoPanel" runat="server" CssClass="form-control-blue" ReadOnly="True"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 hide">
                                                <label class="control-label">Board Registration No.&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtInrollmentNoPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">Book No.&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtBookNoPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 hide">
                                                <label class="control-label">Date of Application&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearP_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthP_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDateP" runat="server"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                        <asp:Button ID="Button9" runat="server" Style="display: none" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">Date of Struck Off&nbsp;<span class="vd_red"></span></label>
                                                <div class="">

                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearPStruck" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearPStruck_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthPStruck" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthPStruck_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDatePStruck" runat="server"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                        <asp:Button ID="Button3" runat="server" Style="display: none" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">Date of Issue&nbsp;<span class="vd_red"></span></label>
                                                <div class="">

                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearP1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearP1_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthP1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthP1_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDateP1" runat="server"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Student's Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtStudentNamePanel" runat="server" CssClass="form-control-blue"
                                                        ReadOnly="True"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpSexPanel" runat="server" CssClass="form-control-blue"
                                                        Enabled="False" AutoPostBack="True">
                                                        <asp:ListItem>Male</asp:ListItem>
                                                        <asp:ListItem>Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtFatherNamePanel" runat="server" CssClass="form-control-blue"
                                                        ReadOnly="True"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Mother's Name &nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtMotherNamePanel" runat="server" CssClass="form-control-blue" ReadOnly="True"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue"
                                                        ReadOnly="True"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 mgbt-lg-15">
                                                <label class="control-label">Class in which the pupil last Studied&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="drpClassPanel" runat="server" CssClass="form-control-blue">
                                                    </asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">School/Board Examination last taken&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="drpClassWithResult" runat="server" CssClass="form-control-blue">
                                                    </asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="col-sm-3 ">
                                                 <label class="control-label">Result Status</label>
                                                 <div class="">
                                                     <asp:TextBox ID="txtResultStatus" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                     <div class="text-box-msg">
                                                     </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Amount &nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtAmtPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                        OnTextChanged="txtAmtPanel_TextChanged"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-sm-3" style="display: none">
                                                <label class="control-label">Exemption&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtConcession1" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                        OnTextChanged="txtConcession1_TextChanged" Enabled="False"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 hide">
                                                <label class="control-label">Received Amount&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtReceviedAmount1" runat="server" CssClass="form-control-blue" Enabled="False"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:Label ID="Label47" class="control-label" runat="server" 
                                                    Text="Date of first admission in the school"></asp:Label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearFADPanel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearFADPanel_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthFADPanel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthFADPanel_SelectedIndexChanged"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDateFADPanel" runat="server"
                                                                CssClass="col-xs-4 form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 mgbt-lg-15">
                                                <asp:Label ID="Label48" runat="server" CssClass="control-label" Text="First admission in the school in class"></asp:Label>
                                                
                                                <div class="">
                                                    <asp:TextBox ID="txtFACPanel" runat="server" CssClass="form-control-blue">N/A</asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 mgbt-lg-15">
                                                <asp:Label ID="Label51" runat="server" CssClass="control-label" Text="Whether qualified for promotion to"></asp:Label>
                                                <div class="">
                                                    <asp:TextBox ID="txtIsQualifiedPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label class="control-label">If so, to which class</label>
                                              
                                                <div class="">
                                                    <asp:TextBox ID="txtIsQualifiedtowhichclassPanel" runat="server" CssClass="form-control-blue" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label class="control-label"> Whether the pupil has paid all dues &nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtDuesPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 mgbt-lg-15">
                                                <label class="control-label">Whether any fee concession was availed&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtConcessiontypePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label class="control-label">Total No. of working days&nbsp;<span class="vd_red"></span></label>
                                            
                                                <div class="">
                                                    <asp:TextBox ID="txtTWDPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" >
                                                <label class="control-label">Total No. of working days present&nbsp;<span class="vd_red"></span></label>
                                             
                                                <div class="">
                                                    <asp:TextBox ID="txtTWDPPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label class="control-label">Whether the pupil is NCC Cadet&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtNCCPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 mgbt-lg-15">
                                                <label class="control-label">Games played or extra curricular&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtECAPanel" runat="server" CssClass="form-control-blue" MaxLength="100"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label class="control-label">General Conduct&nbsp;<span class="vd_red"></span></label>
                                             
                                                <div class="">
                                                    <asp:TextBox ID="txtConductPanel" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3 ">
                                                <label class="control-label">Reason for leaving the school &nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtRemarkPanel" runat="server" Rows="2" CssClass="form-control-blue" TextMode="MultiLine"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 " runat="server" visible="false">
                                                <label class="control-label">UDISE (PEN)</label>
                                                <div class="">
                                                    <asp:TextBox ID="txt_pen" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            
                                           
                                            <div class="col-sm-3 ">
                                                <label class="control-label">Subjects Studied&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtSubjectsPanel" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 mgbt-lg-15">
                                                <label class="control-label">Any Other Remarks&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtAORPanel" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="col-sm-3">
                                                <label class="control-label">Template</label>
                                                <div class="">
                                             <asp:DropDownList ID="DropDownList1" runat="server" class="form-control-blue">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">CBSE-English</asp:ListItem>
                                                <asp:ListItem Value="Template 2"> CBSE-Hindi</asp:ListItem>
                                                <asp:ListItem Value="Template 3">ICSE/ISC</asp:ListItem>
                                                <asp:ListItem Value="Template 4">UPBoard-Hindi</asp:ListItem>
                                                <asp:ListItem Value="Template 5">UPBoard-English</asp:ListItem>
                                            </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="col-sm-3 ">
                                                 <label class="control-label">&nbsp;</label>
                                                 <div class="">
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                    <asp:Button ID="Button4s" runat="server" CausesValidation="False" 
                                                    CssClass="button-y" OnClick="LinkButtons_Click" Text="Update" />&nbsp;&nbsp;
                                                    <asp:Button ID="Button3s" runat="server" CausesValidation="False" 
                                                    CssClass="button-n" OnClick="LinkButton4_Click" Text="Cancel" />
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                        </div>
                                <asp:Button ID="Button2" runat="server" Style="display: none" />
                                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button3s"
                                    Enabled="True" PopupControlID="Panel2" TargetControlID="Button3s" BackgroundCssClass="popup_bg">
                                </ajaxToolkit:ModalPopupExtender>
                                </asp:Panel>


                                  
                            </div>

                            <div style="overflow: auto; width: 1px; height: 1px">
                                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                    <table class="tab-popup text-center">
                                        <tr>
                                            <td style="text-align: center;">
                                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="Button8" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button8_Click" Text="No" />
                                                &nbsp;  &nbsp;
                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnDelete_Click1" Text="Yes" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                                        Enabled="True" PopupControlID="Panel1" TargetControlID="Button8" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>

                                </asp:Panel>

                                

                               
                            </div>
                        </div>
                    </div>
                </div>
                    </div>
                </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton4" />
            <asp:PostBackTrigger ControlID="LinkButtons" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <style>
        a.pp_next {
            display: none !important;
        }

        a.pp_previous {
            display: none !important;
        }

        div.light_square .pp_gallery a.pp_arrow_previous, div.light_square .pp_gallery a.pp_arrow_next {
            display: none !important;
        }

        .pp_gallery div {
            display: none !important;
        }
    </style>

    <script>

        $(document).ready(function () {
            $("[id*=LinkButton1]").click(function () {
                window.scrollTo(x - coord, y - coord);
            });
            $("[id*=LinkButton5]").click(function () {
                $("[id*=divPopup]").addClass("hide");
            });
        });
    </script>
</asp:Content>
