<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TransportAlert.aspx.cs" Inherits="admin_VehicleNoWiseTransportAllotedStudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular.min.js"></script>
    <script type="text/javascript">
        function GetCount(txtStr) {
            if (txtStr != null) {
                document.getElementById("<%= Label12.ClientID %>").innerHTML = txtStr.length;
            }
        }
        function alertmsg() {
            alert(
                "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(GetCount(txtStr));
            </script>
            <div ng-app="" class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Route&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpRoute" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClientClick="ValidateTextBox('.validatetxts');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton6_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 62px"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-12  no-padding" runat="server" id="table3">
                                        <div class="col-sm-12   mgbt-xs-15">
                                            <label class="control-label">SMS &nbsp;<span class="vd_red">*</span></label>
                                            <div class="mgbt-xs-5">
                                                <asp:TextBox ID="txtMessage" onkeyup="GetCount(this.value);" onblur="GetCount(this.value);" onclick="GetCount(this.value);" runat="server"
                                                    TextMode="MultiLine" Rows="4" Font-Size="12" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                                <div class="text-box-msg ">
                                                    <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                    <span id="spanDisplay">
                                                        <asp:Label ClientIDMode="Static" ID="Label12" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label " Text="(For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                            <asp:LinkButton ID="lnkSend" runat="server" CssClass="button form-control-blue" OnClientClick="return ValidateTextBox('.validatetxt');" OnClick="lnkSend_Click">Send</asp:LinkButton>
                                            <div id="msg1" runat="server" style="left: 70px !important;"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 " id="gdv" runat="server">
                                    <div class=" table-responsive  table-responsive2 " runat="server" id="abc">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                            class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%# Eval("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Eval("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle Route">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleRoute" runat="server" Text='<%# Eval("VehicleRoute") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%# Eval("FamilyContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" Checked="true" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-grey-ll vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
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

