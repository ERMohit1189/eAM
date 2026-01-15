<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ConveyanceAllotment.aspx.cs"
    Inherits="ConveyanceAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
        <script src="../js/jquery-1.4.3.min.js"></script>
        <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <style>
        .disabledCell {
            pointer-events: none; /* Disabling mouse events */
            opacity: 0.6; /* Making the cell semi-transparent to indicate it's disabled */
            /* You can add more styling as needed */
        }
    </style>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}", 
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
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                    Sys.Application.add_load(datetime);

                    Sys.Application.add_load(prettyphoto);
                    Sys.Application.add_load(disablebtn);
                }
                catch (ex) {

                }
            </script>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" runat="server" id="table1">


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divEnter2" runat="server" visible="true">
                                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Name/ S.R. No." CssClass="form-control-blue validatetxt" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"
                                            onkeyup="onchangetxt();KeyPress(event);" onpaste="onchangeatcopyandpaste()" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <script>
                                        function onchangetxt() {

                                            if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {

                                                document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                            }

                                        }

                                        function KeyPress(e) {
                                            e = e || window.event;

                                            if ((e.which === 65 || e.keyCode === 65) && e.ctrlKey) {
                                                document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                            }
                                        }

                                        function onchangeatcopyandpaste() {

                                            document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                        }
                                    </script>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">

                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click"
                                            CssClass="button form-control-blue" OnClientClick="return ValidateTextBox('.validatetxt');"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                        <div id="Div1" runat="server" style="left: 60px;"></div>
                                    </div>
                                    <div class="col-sm-8">
                                        <div id="msgbox" runat="server"></div>
                                    </div>

                                </div>

                                <div id="divStudent" class="col-sm-12 mgbt-xs-15" runat="server" visible="false">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="studentInfoGrid" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                                    <asp:Label ID="lblClassid" CssClass="hide" runat="server" Text='<%# Bind("classid") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("CardId") %>'></asp:Label>
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
                                                    </asp:GridView>

                                                </td>
                                                <td class="tab-top tab-profile text-center onprint">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <%--<a href="#" target="_blank" class="more-btn">more...</a>--%>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                        <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                    </div>
                                    
                                </div>

                                <div class="col-sm-12 " runat="server" id="detailsDiv" visible="false">
                                    <br />

                                    <div class="col-sm-12  no-padding">
                                        <div class="col-sm-12 no-padding" runat="server" id="divPickup">

                                            <div class=" table-responsive  table-responsive2 text-center">

                                                <asp:GridView ID="grdAllotment" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:label runat="server" ID="lll" Text='<%# Container.DataItemIndex+1 %>'></asp:label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="col-sm-2 text-left" HeaderText="Installment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InstallmentId" Visible="false" runat="server" Text='<%# Bind("MonthId") %>' Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="Insttalment" runat="server" Text='<%# Bind("MonthName") %>' Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="text-left" />
                                                             
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <div style="width: 50%; text-align: center; float: left;">
                                                                    <asp:DropDownList ID="ddlVehicleH" runat="server" CssClass="form-control-blue select-pad-small" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicleH_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                                <div style="width: 50%; text-align: center; float: left;">
                                                                    <asp:DropDownList ID="ddlLocationH" runat="server" CssClass="form-control-blue select-pad-small" AutoPostBack="true" OnSelectedIndexChanged="ddlLocationH_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                               
                                                                <asp:DropDownList ID="ddlVehicle" runat="server" Width="49%" CssClass="form-control-blue select-pad-small" AutoPostBack ="True" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="49%" CssClass="form-control-blue select-pad-small" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkPickupH" runat="server" Text="&nbsp;Pickup"  AutoPostBack="True" OnCheckedChanged="chkPickupH_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkPickupL" runat="server" AutoPostBack="true" OnCheckedChanged="chkPickupL_CheckedChanged" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="text-left" />
                                                            <ItemStyle CssClass="text-left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkDropH" runat="server" Text="&nbsp;Drop" AutoPostBack="True" OnCheckedChanged="chkDropH_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDropL" runat="server" AutoPostBack="true" OnCheckedChanged="chkDropL_CheckedChanged" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="jjj" runat="server" Text="" Font-Bold="true" Style="float: right;">Total</asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="text-left" />
                                                            <ItemStyle CssClass="text-left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="hhf" runat="server" Text="Amount" Style="float: right;"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblTotalAmount" runat="server" Text="" CssClass="form-control-blue text-right"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGTotalAmount" runat="server" Text="" Font-Bold="true" Style="float: right;"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="100px" />
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="text-left" />
                                                            <ItemStyle CssClass="text-left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding text-left">
                                            <asp:LinkButton ID="Linksubmit" runat="server" CssClass="button form-control-blue" OnClick="Linksubmit_Click">Submit</asp:LinkButton>
                                            <div id="Div2" runat="server"></div>
                                        </div>
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
