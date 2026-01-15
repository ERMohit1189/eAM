<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true"
     CodeFile="SmsAlertForIXtoX_1718.aspx.cs" Inherits="common_SmsAlertFroIXtoX_1718" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
 <script>
        function createHTML() {

            var table = document.getElementById("tbl");
            var tr = table.getElementsByTagName("tr");
            var td = tr[1].getElementsByTagName("td");
            var monthname = document.getElementById('<%= drpMonth.ClientID %>');
            var classname = document.getElementById('<%= drpclass.ClientID %>');
            var sectionname = document.getElementById('<%= drpsection.ClientID %>');
            sectionname = document.getElementById('<%= drpsection.ClientID %>');

            for (var i = 2; i < tr.length - 1; i++) {
                var html = "";
                var text = "Dear Parents," + '\n' + monthname.options[monthname.selectedIndex].text + " test marks of " + tr[i].cells[0].innerText.toString() + " Class " + classname.options[classname.selectedIndex].text + " (" + sectionname.options[sectionname.selectedIndex].text+"): ";

                for (var j = 1; j < td.length - 3; j++) {

                    var span = td[j].getElementsByTagName("span");
                    if (span[0] != null) {

                        var subject = span[0].innerHTML.toString();
                        var mmmarks = span[1].innerHTML.toString();

                        var tdnew = tr[i].getElementsByTagName("td");
                        var spannew = tdnew[j].getElementsByTagName("span");

                        var marks = spannew[0].innerHTML.toString();

                        if (html === "") {
                            if (marks !== "") {

                                html = subject.trim() + '- ' + marks + '/' + mmmarks;
                            }
                        }
                        else {
                            if (marks !== "") {
                                html = html + '\n' + subject.trim() + '- ' + marks + '/' + mmmarks;
                            }
                        }
                    }
                }

                var textarea = tr[i].cells[tr[i].cells.length - 3].getElementsByTagName("textarea");
                var input = tr[i].cells[tr[i].cells.length - 3].getElementsByTagName("input");

                if (html !== "")
                {
                    textarea[0].value = text + '\n \n' + html + '\n \n' + "Regards" + '\n' + "Principal" + '\n' + document.getElementById("lblCollegeShortName").innerText + ' ' + document.getElementById('<%= lblCity.ClientID %>').innerText;
                    input[0].value = text + '\n \n' + html + '\n \n' + "Regards" + '\n' + "Principal" + '\n' + document.getElementById("lblCollegeShortName").innerText + ' ' + document.getElementById('<%= lblCity.ClientID %>').innerText;
                }
                else {
                    textarea[0].value = "";
                    input[0].value = "";
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblCity" runat="server" Text="" CssClass="hide"></asp:Label>
            <script>
                
                Sys.Application.add_load(createHTML);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Test&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drptest" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drptest_SelectedIndexChanged">
                                                <asp:ListItem Value="Test1">Test 1</asp:ListItem>
                                                <asp:ListItem Value="Test2">Test 2</asp:ListItem>
                                                <asp:ListItem Value="Test3">Test 3</asp:ListItem>
                                                <asp:ListItem Value="HY">HY</asp:ListItem>
                                                <asp:ListItem Value="Test4">Test 4</asp:ListItem>
                                                <asp:ListItem Value="Test5">Test 5</asp:ListItem>
                                                <asp:ListItem Value="AE">AE</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Test Month&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Jan">Jan</asp:ListItem>
                                                <asp:ListItem Value="Feb">Feb</asp:ListItem>
                                                <asp:ListItem Value="Mar">Mar</asp:ListItem>
                                                <asp:ListItem Value="Apr">Apr</asp:ListItem>
                                                <asp:ListItem Value="May">May</asp:ListItem>
                                                <asp:ListItem Value="Jun">Jun</asp:ListItem>
                                                <asp:ListItem Value="Jul">Jul</asp:ListItem>
                                                <asp:ListItem Value="Aug">Aug</asp:ListItem>
                                                <asp:ListItem Value="Sep">Sep</asp:ListItem>
                                                <asp:ListItem Value="Oct">Oct</asp:ListItem>
                                                <asp:ListItem Value="Nov">Nov</asp:ListItem>
                                                <asp:ListItem Value="Dec">Dec</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="msgbox" runat="server" style="left: 0;"></div>
                                <div style="float: right; font-size: 19px;">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:LinkButton ID="lnkWord" runat="server" CssClass="icon-word-color" OnClick="lnkWord_Click" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkExcel" runat="server" CssClass="icon-excel-color" OnClick="lnkExcel_Click" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkPdf" Visible="false" runat="server" CssClass="icon-pdf-color" OnClick="lnkPdf_Click" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkPrint" runat="server" CssClass="icon-print-color" OnClick="lnkPrint_Click" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--  <asp:PostBackTrigger ControlID="lnkWord" />
                                                <asp:PostBackTrigger ControlID="lnkExcel" />
                                                <asp:PostBackTrigger ControlID="lnkPdf" />--%>
                                                <asp:PostBackTrigger ControlID="lnkPrint" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </div>
                                <div class="col-sm-12  ">
                                    <div runat="server" id="divExport">
                                        <div class=" table-responsive  table-responsive2 ">
                                            <table class="table mp-table no-bm p-table-bordered  table-bordered" id="tbl">
                                                <tbody>
                                                    <tr>
                                                        <td class="p-pad-3 p-tot-tit" colspan="100">
                                                            <div id="header" runat="server" class="col-sm-12" style="width: 80%"></div>

                                                            <div class="col-sm-12 text-center">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" Text="Class Wise Marks"></asp:Label>

                                                            </div>
                                                            <div class="col-sm-12 text-center">

                                                                <span style="font-weight: bold; font-family: Verdana; font-size: 12px">Class:</span><asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                                                                <span style="font-weight: bold; font-family: Verdana; font-size: 12px">Section:</span><asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                                                                <span style="font-weight: bold; font-family: Verdana; font-size: 12px">Eval.:</span><asp:Label ID="lblEval" runat="server" Text=""></asp:Label>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-3 p-tot-tit"></td>
                                                        <asp:Repeater ID="rptSubject" runat="server">
                                                            <ItemTemplate>
                                                                <td class="text-center" style="vertical-align: top">
                                                                    <asp:Label ID="lblSubjectId" runat="server" Visible="false" Text='<%# Eval("SubjectId")  %>'></asp:Label>
                                                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("SubjectName")  %>'></asp:Label>
                                                                    <br />
                                                                    <div class="text-center">
                                                                        <asp:Label ID="lblMM1" runat="server"></asp:Label>
                                                                    </div>
                                                                </td>

                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <td class="p-pad-3 p-tot-tit">SMS</td>
                                                        <td class="p-pad-3 p-tot-tit">Contact No.</td>
                                                        <td>
                                                            <asp:CheckBox ID="chkAll" runat="server" Checked="true" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                        </td>

                                                    </tr>
                                                    <asp:Repeater ID="rptStudents" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsrno" runat="server" Visible="false" Text='<%# Eval("srno")  %>'></asp:Label>
                                                                    <%# Eval("StudentName")  %>
                                                                </td>

                                                                <asp:Repeater ID="rptMarks" runat="server">
                                                                    <ItemTemplate>
                                                                        <td class="text-center">
                                                                            <%# Eval("Marks")  %>
                                                                            <asp:Label ID="lblM1" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                                <td>
                                                                    <asp:TextBox ID="txtMulti" ReadOnly="true" Width="350px" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                                     <asp:HiddenField ID="hdMsg" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("contactno")  %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td colspan="100" class="text-center">
                                                            <div class="col-sm-12  mgbt-xs-15 no-padding">
                                                                <asp:LinkButton ID="lnkSubmit" CssClass="button" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                                                <div id="divmsg" runat="server" style="left: 75px"></div>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
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

