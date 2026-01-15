<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Remark_Student.aspx.cs" Inherits="SuperAdmin_Customized_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function GetCount(txtStr) {
            document.getElementById("<%= Label12.ClientID %>").innerHTML = txtStr.length;
            <%-- var maxlenth = document.getElementById("<%= Label12.ClientID %>").setAttribute('maxlength', 1000);
            alert(maxlenth);--%>
        }
       <%-- function GetCount1(txtStr1) {
            document.getElementById("<%= Label9.ClientID %>").innerHTML = txtStr1.length;
        //onkeyup="GetCount1(this.value);displaytooltip(this);" onblur="GetCount1(this.value);" onclick="GetCount1(this.value);"
        }--%>
        function alertmsg() {
            alert(
                "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
        }
        function x1() {
            var t1 = document.getElementById('<%=txtremark.ClientID%>').value;
            document.getElementById('<%=txtMessage.ClientID%>').value = t1;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(GetCount(txtStr));
                Sys.Application.add_load(GetCount1(txtStr1));
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-12  no-padding " id="table1" runat="server" visible="False">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DrpEnter" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                                    <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Enter No.&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="TxtEnter" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                            <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return ValidateTextBox('.validatetxt1');" OnClick="LinkButton2_Click" CssClass="button form-control-blue">Show</asp:LinkButton>--%>
                                            <div id="msgbox" runat="server" style="left: 70px !important;"></div>
                                            <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>

                                        </div>
                                    </div>



                                    <div class="col-sm-12  no-padding " id="table2" runat="server">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpClassCourse" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpClassCourse_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="red" runat="server" ControlToValidate="drpClassCourse" ErrorMessage="Select class!" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Section</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Stream</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control-blue validatedrp">
                                                    <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                                    <asp:ListItem Text="Positive" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Negative" Value="Red"></asp:ListItem>
                                                </asp:DropDownList>

                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server" ControlToValidate="ddlcategory" ErrorMessage="Select category!" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <%-- ReSharper disable once UnknownCssClass --%>
                                                <asp:TextBox ID="txtDate" runat="server" Enabled="False" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-12  no-padding" runat="server" id="table3">
                                        <div class="col-sm-12 ">
                                            <label class="control-label">Review &nbsp;<span class="vd_red">* <%--(Characters MaxLenth 1000)--%></span></label>
                                            <div class="mgbt-xs-5">
                                                <asp:TextBox ID="txtremark" onkeypress="displaytooltip(this);" onkeyup="displaytooltip(this);" runat="server" MaxLength="1000" TextMode="MultiLine" Rows="4" Font-Size="12" CssClass="form-control-blue  validatetxt"></asp:TextBox>



                                                <div class="text-box-msg ">
                                                    <%-- <asp:Label ID="Label8" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                    <span id="spanDisplay1">
                                                        <asp:Label ClientIDMode="Static" ID="Label9" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>--%>
                                                    <asp:Label ID="Label10" runat="server" CssClass="control-label "></asp:Label>
                                                </div>
                                            </div>
                                           
                                        </div>
                                         <div class="col-sm-12   mgbt-xs-15">
                                            <div class="col-sm-5 col-xs-5 text-left" style="padding: 0; color: red">
                                                <span class="txt-rep-title-11">Note: Maximum character length is 1000!</span>
                                                
                                            </div>

                                            <div class="col-sm-7 col-xs-7  controls text-right">
                                                <script>
                                                    function displaytooltip(element) {
                                                        var divtooptip = document.getElementById('divtooptip');
                                                        var awesometext = divtooptip.getElementsByTagName('i');
                                                        if (element.value.length > 0) {
                                                            if (element.value.length > 900 && element.value.length <= 1000) {
                                                                divtooptip.className = "ttip-box-tr-2 ttip-box-dis-b vd_bg-yellow";
                                                                divtooptip.innerHTML = "<i class='fa fa-exclamation-triangle' aria-hidden='true'></i> &nbsp; You have typed " + element.value.length + " character(s)!";
                                                            }
                                                            else if (element.value.length >= 1000) {
                                                                divtooptip.className = "ttip-box-tr-2 ttip-box-dis-b vd_bg-red";
                                                                divtooptip.innerHTML = "<i class='fa fa-times' aria-hidden='true'></i> &nbsp; Sorry, You have crossed maximum character(s) limit";
                                                                element.value = element.value.substring(0, 900);
                                                            }
                                                            else {
                                                                divtooptip.className = "ttip-box-tr-2 ttip-box-dis-b vd_bg-green";
                                                                divtooptip.innerHTML = "<i class='fa fa-check' aria-hidden='true'></i>You have typed " + element.value.length + " character(s).";
                                                            }
                                                        }
                                                        else {
                                                            divtooptip.className = "ttip-box-tr-2 ttip-box-dis-n vd_bg-green";
                                                        }
                                                    }
                                                </script>
                                                <div class="ttip-box-tr-2 ttip-box-dis-n vd_bg-green" id="divtooptip"></div>
                                            </div>

                                            </div>

                                        <div class="col-sm-12   mgbt-xs-15" runat="server" visible="False">
                                            <label class="control-label">Message &nbsp;<span class="vd_red"><%--(Characters MaxLenth 500)--%></span></label>
                                            <div class="mgbt-xs-5">
                                                <asp:TextBox ID="txtMessage" onkeyup="GetCount(this.value);" onblur="GetCount(this.value);" onclick="GetCount(this.value);" runat="server" MaxLength="500" TextMode="MultiLine" Rows="4" Font-Size="12" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg ">
                                                    <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                    <span id="spanDisplay">
                                                        <asp:Label ClientIDMode="Static" ID="Label12" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label " Text="(For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClientClick="return ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                            <div id="msg1" runat="server" style="left: 70px !important;"></div>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button mrgn-tb-25 form-control-blue" OnClick="LinkButton3_Click" Visible="false">Stop</asp:LinkButton>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                        (<asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Section" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Medium" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Admission">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" AutoPostBack="true" Checked="True"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk" runat="server" Checked="True"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
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

