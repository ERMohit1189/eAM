<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ReturnProduct.aspx.cs" Inherits="_10.ReturnProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        var validNumber = new RegExp(/^\d*\.?\d*$/);


        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)

                return false;
            return true;
        }

    </script>
    <script>
        function fillTextbox() {
            var txtCompanyName = document.getElementById('<%= txtStaff.ClientID %>').value;
            document.getElementById('<%= txtStaff.ClientID %>').value = txtCompanyName.toString().trim();
        }
    </script>
    <script type="text/javascript">
        function getEmployeeList() {
            $(function () {
                $("[id$=txtStaff]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployeeForCode") %>',
                            data: "{ 'empId': '" + request.term + "'}",
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
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfEmployeeId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15" runat="server" id="divProType">
                                        <label class="control-label">Enter Emp. ID/ Username/ Name  &nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtStaff" CssClass="form-control-blue" runat="server" onblur="javascript:return fillTextbox();" AutoPostBack="true" OnTextChanged="txtHeaderEmpId_TextChanged" placeholder="Enter Emp. ID/ Username/ Name "></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" OnClientClick="ValidateTextBox('.validatetxtss');return validationReturn();" OnClick="lnkShow_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>
                                    <div class="col-sm-12 half-width-50 mgbt-xs-15 no-padding" runat="server" id="divTools" visible="false">
                                        <div class="col-sm-12 ">
                                                    <div class=" table-responsive  table-responsive2">
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border 
                                                            table-bordered text-center">
                                                            <Columns>
                                                                

                                                                <asp:TemplateField HeaderText="Username">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Emp. Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Product &nbsp;<span class="vd_red"></span></label>
                                    <div class=" ">
                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control-blue" runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                
                                        </div>
                            </div>
                            <div class="col-sm-12  mgbt-xs-10 hide" runat="server" id="divExport" visible="false">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <div style="float: right; font-size: 19px;" id="Div1" runat="server">

                                            <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                            <script>
                                                
                                            </script>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ImageButton1" />
                                        <asp:PostBackTrigger ControlID="ImageButton2" />
                                        <asp:PostBackTrigger ControlID="ImageButton3" />
                                        <asp:PostBackTrigger ControlID="ImageButton4" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-sm-12 " runat="server" id="pnlcontrols" visible="false">
                                <div class="col-lg-12 " runat="server">

                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                        <tr class="hide">
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading"  class="hide" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister"  class="hide" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <div class="col-sm-12" runat="server" id="divlistshow">
                                                                        <div class="table-responsive2 table-responsive">
                                                                            <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                                                <tbody>
                                                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                                                        <HeaderTemplate>
                                                                                            <tr>
                                                                                                <th class="vd_bg-blue-np vd_white-np">#</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Username</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Emp. Name</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Product</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Qty.</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Return Qty.</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Date of Issue</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Username</th>
                                                                                                <th class="vd_bg-blue-np vd_white-np">Return</th>
                                                                                            </tr>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmpCode") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblItemIds" runat="server" Visible="false" Text='<%# Bind("itemid") %>'></asp:Label>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblReturnQty" runat="server" Text='<%# Bind("ReturnQtys") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("IssueDates") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RecordedDates") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                                                </td>


                                                                                                <td class="menu-action" style="width: 40px;">

                                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:LinkButton ID="LinkButton1" CausesValidation="False" runat="server" title="Edit"  CssClass="btn menu-icon vd_bd-green vd_green" OnClick="LinkButton1_OnClick"><i class="icon-reply"></i></asp:LinkButton>
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>

                                                                                                </td>

                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tbody>
                                                                            </table>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                            <div class="col-sm-12 ">
                                <table class="tab-popup">
                                    <tr>
                                        <td>Return Date *</td>
                                        <td>
                                            <div class="input-group input-group-margin">
                                                <script>
                                                    Sys.Application.add_load(datetime);
                                                </script>
                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                <asp:TextBox ID="txtRerurnEditDate" runat="server" Enabled="false" class="form-control-blue validatetxtt datepicker-normal"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Qty. *</td>
                                        <td>
                                            <div class="input-group input-group-margin">
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                <asp:Label ID="txtReturnableQty" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblEmpCode" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblItemIdsss" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtEditQty" CssClass="form-control-blue validatetxtt" MaxLength="100" runat="server" AutoPostBack="true" OnTextChanged="txtEditQty_TextChanged"></asp:TextBox>

                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Remark *</td>
                                        <td>
                                            <div class="input-group input-group-margin">
                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                <asp:TextBox ID="txtEditRemark" runat="server" MaxLength="100" class="form-control-blue validatetxtt"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="text-align: center;">
                                                <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Submit" TabIndex="3" OnClick="btnupdate_OnClick" OnClientClick="ValidateTextBox('.validatetxtt');ValidateDropdown('.validatedrpp');return validationReturn();" />
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                <div id="Div2" runat="server" style="left: 76px"></div>
                                            </div>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                        TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                    </ajaxToolkit:ModalPopupExtender>
                </div>




            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

