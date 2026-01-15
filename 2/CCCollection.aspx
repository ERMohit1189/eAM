<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CCCollection.aspx.cs" Inherits="_2.admin_CCCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
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
                                response($.map(data.d,
                                    function(item) {
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
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">

                                    <%--<div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblAdmissionType" class="control-label" runat="server" Text="Select No."></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                            </asp:DropDownList>--%>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                         <div class="">
                                        <asp:DropDownList ID="DrpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R. No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                       <%-- <asp:Label ID="lblMedium" runat="server" Text="Enter No." class="control-label"></asp:Label>--%>
                                        <div class="">
                                            <asp:TextBox ID="txtSrNo" runat="server" placeholder="Enter Name/ S.R. No." 
                                                OnTextChanged="TxtEnter_TextChanged" CssClass="form-control-blue" AutoPostBack="True"
                                                
                                                onblur="javascript:__doPostBack('ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderMainBox$LinkButton6','')"
                                                ></asp:TextBox>
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSrNo" ErrorMessage="Please enter S.R. No./Enrollment No."
                                                    SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-sm-4  half-width-50 mgbt-xs-15" id="divEnter2" runat="server" visible="true">
                                        <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" CssClass="form-control-blue txtbox"
                                            OnTextChanged="txtSearch_TextChanged" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>--%>

                                    <div class="col-sm-4  half-width-50   mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"  class="button form-control-blue"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>

                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-5">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;" runat="server" ID="grdshow" Visible="False">
                                        <tr>
                                        <td class="tab-top"> <asp:GridView ID="GrdStudent" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label35" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Width="200px" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("combineClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                             
                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server"  Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Admission">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Transport" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("TransportRequired") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView></td>
                                                <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" style="Width: 48px; Height: 60px;" />
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
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" runat="server" id="divTools" visible="false">

                                     <div class="col-sm-4  half-width-50">
                                         <asp:Label ID="Label9" runat="server" class="control-label" Text="Date"></asp:Label>
                                         <div class="">
                                             <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                 <ContentTemplate>
                                                     <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged" Enabled="false"
                                                         CssClass="form-control-blue col-xs-4">
                                                     </asp:DropDownList>
                                                     <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged" Enabled="false"
                                                         CssClass="form-control-blue col-xs-4">
                                                     </asp:DropDownList>
                                                     <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" Enabled="false" CssClass="form-control-blue col-xs-4">
                                                     </asp:DropDownList>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
                                             <div class="text-box-msg">
                                             </div>
                                         </div>
                                     </div>
 
                                     <div class="col-sm-4  half-width-50 ">
                                         <asp:Label ID="Label8" runat="server" class="control-label" Text="Mode"></asp:Label>
                                         <div class="">
                                             <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True" TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                 <asp:ListItem>Cash</asp:ListItem>
                                                             <asp:ListItem>Cheque</asp:ListItem>
                                                             <asp:ListItem>DD</asp:ListItem>
                                                             <asp:ListItem>Card</asp:ListItem>
                                                             <asp:ListItem>Online Transfer</asp:ListItem>
                                                             <%--<asp:ListItem>Other</asp:ListItem>--%>
                                             </asp:DropDownList>
                                             <div class="text-box-msg">
                                             </div>
                                         </div>
                                     </div>
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-lg-15" id="table1" runat="server" visible="false">
                                        <asp:Label ID="Label11" class="control-label" runat="server">
                                            <asp:Label ID="Label42" class="control-label" runat="server" style="margin-bottom: 0px;"></asp:Label>
                                            <span class="vd_red">*</span>
                                        </asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50" id="table2" runat="server" visible="false">
                                        <asp:Label ID="Label12" runat="server" class="control-label">
                                            <asp:Label ID="lblchequedate" class="control-label" runat="server" style="margin-bottom: 0px;"></asp:Label>
                                            <span class="vd_red">*</span>
                                        </asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtchequeDate" runat="server" CssClass="form-control-blue  datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table4" runat="server" visible="false">
                                        <asp:Label ID="Label6" runat="server" class="control-label"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="ddlChequeStatus" CssClass="form-control-blue">
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table12" runat="server" visible="false">
                                        <asp:Label ID="Label43" runat="server" class="control-label" Text="Bank Name"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   

                                    <div class="col-sm-4  half-width-50 hide">
                                        <label class="control-label">Student's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control-blue validatetxt" ReadOnly="True"></asp:TextBox>
                                            
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSex" runat="server" CssClass="form-control-blue" Enabled="False">
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt" ReadOnly="True"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control-blue validatetxt" ReadOnly="True"></asp:TextBox>
                                            
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtClass" runat="server" CssClass="form-control-blue validatetxt"> </asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Student Status</label>
                                        <div class="">
                                           <asp:DropDownList ID="ddlStudentStatus" runat="server" CssClass="form-control-blue"
                                               AutoPostBack="true" OnSelectedIndexChanged="ddlStudentStatus_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="PASSED">PASSED</asp:ListItem>
                                                <asp:ListItem Value="FAILED">FAILED</asp:ListItem>
                                                <asp:ListItem Value="DETAINED">DETAINED</asp:ListItem>
                                                <asp:ListItem Value="ABSENT">ABSENT</asp:ListItem>
                                                <asp:ListItem Value="ESSENTIALREPEAT">ESSENTIAL REPEAT</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Year&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtyear" runat="server" Text="" CssClass="validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control-blue validatetxt" AutoPostBack="True"
                                                OnTextChanged="txtAmt_TextChanged" Enabled="false"></asp:TextBox>
                                            
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <asp:Label ID="Label3" runat="server" class="control-label" Text="Fine"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtCBFee" runat="server" CssClass="form-control-blue"
                                                            Enabled="false" Text="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Exemption</label>
                                        <div class="">
                                            <asp:TextBox ID="txtConcession" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnTextChanged="txtConcession_TextChanged" Enabled="False"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Received Amount</label>
                                        <div class="">
                                            <asp:TextBox ID="txtReceviedAmount" runat="server" CssClass="form-control-blue" Enabled="False"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9" runat="server" visible="false">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" Rows="1" CssClass="form-control-blue" TextMode="MultiLine" Text="NA"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="Submit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn(this);" OnClick="Submit_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    </div>

                                </div>


                                <div class="col-lg-12  mgbt-xs-5">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Bind("sno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Receipt No.">
                                                    <ItemTemplate>
                                                         <asp:Label ID="Label10" runat="server" Text='<%# Bind("RecieptNo") %>'></asp:Label>
                                                        <%--<label></label>
                                                                <asp:LinkButton ID="LinkButton2s" runat="server" title="Print Receipt" OnClick="LinkRecept_Click" 
                                                                    data-placement="top" Text='<%# Bind("RecieptNo") %>'></asp:LinkButton>--%>
                                                        <asp:Label ID="Label18" Visible="false" runat="server" Text='<%# Bind("RecieptNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("CCissuedate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label39" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MOP" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" Visible='<%# Eval("Status").ToString()=="Cancelled"?false:true %>'
                                                            title="Edit"  class="btn menu-icon vd_bd-yellow vd_yellow"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cancel" Visible="false">
                                                    <ItemTemplate>


                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Cancel"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-times"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Receipt">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                                            title="Print Receipt"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CC">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                                                            title="Print Character Certificate" Visible='<%# Eval("Status").ToString()=="Cancelled"?false:true %>'
                                                            class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                

                                            </Columns>

                                        </asp:GridView>
                                      <%--  <style>
                                           .rowYellow1 {
                                                background: #ff8c00 !important;
                                                color: #000 !important;
                                                padding: 2px 5px 2px 5px !important;
                                            }
                                        </style>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr style="display: none">
                            <td >S.R. No. <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSrNoPanel" runat="server" CssClass="form-control-blue" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td >T.C. Date <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDYearP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                            CssClass="textbox">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DDMonthP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                            CssClass="textbox">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DDDateP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                            CssClass="textbox">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td >Student's Name <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentNamePanel" runat="server" CssClass="form-control-blue validatetxt2"></asp:TextBox>
                            </td>
                        </tr>--%>
                       <%-- <tr>
                            <td >Gender <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSexPanel" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >Father's Name <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFatherNamePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr style="display: none">
                            <td>Contact No. <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Class <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassPanel" CssClass="form-control-blue validatetxt2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status <span class="vd_red">*</span>
                            </td>
                            <td>
                                 <asp:DropDownList ID="drpStatusPanel" runat="server" CssClass="form-control-blue validatetxt2">
                                      <asp:ListItem Value=""><--Select--></asp:ListItem>
                                      <asp:ListItem Value="PASSED">PASSED</asp:ListItem>
                                      <asp:ListItem Value="FAILED">FAILED</asp:ListItem>
                                      <asp:ListItem Value="DETAINED">DETAINED</asp:ListItem>
                                      <asp:ListItem Value="ABSENT">ABSENT</asp:ListItem>
                                      <asp:ListItem Value="ESSENTIALREPEAT">ESSENTIAL REPEAT</asp:ListItem>
                                  </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Year <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtyearPanel" runat="server" CssClass="form-control-blue validatetxt2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Amount <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmtPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Remark <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue" Rows="1" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Conduct and Work <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConductPanel" runat="server" CssClass="form-control-blue" Rows="1" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td >
                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" OnClick="LinkButton4_Click" OnClientClick="ValidateTextBox('.validatetxt2');return validationReturn();" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp; &nbsp;
                               <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" OnClick="LinkButton5_Click" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
                        TargetControlID="Button9" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup">
                        <tr>
                            <td style="text-align: center;">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" CssClass="button-y" CausesValidation="False" OnClick="Button8_Click" Text="No" />
                               
                                &nbsp; &nbsp;
                     <asp:Button ID="btnDelete" runat="server" CssClass="button-n" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                        Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
                <Triggers>
            <asp:PostBackTrigger ControlID="txtSrno" />
        </Triggers>
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
</asp:Content>

