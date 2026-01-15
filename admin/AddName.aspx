<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AddName.aspx.cs" Inherits="admin_AddMobileNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .x-navigation li.active2 > a .fa,
        .x-navigation li.active2 > a .glyphicon {
            color: #ffd559;
        }

        .x-navigation li.active21 > a .fa,
        .x-navigation li.active21 > a .glyphicon {
            color: #ffd559;
        }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) !== -1);

            return ret;
        }


        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)

                return false;
            return true;
        }


        function ValidateAlphaIsNumber(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32)

                return false;
            return true;
        }


    </script>

    <script>
        function callDatatablee() {
            $(document).ready(function () {
                $("#example110").css("width", "100%");
                $('#example110').DataTable({
                    dom: 'Bfrtip',

                    "aLengthMenu": [[100, 500, 1000, 1500, -1], [100, 500, 1000, 1500, "All"]],
                    //scrollX:        true,
                    //scrollCollapse: true,
                    //paging:         false,
                    //fixedColumns:   {
                    //    leftColumns: 1,
                    //    //rightColumns: 1   'print',
                    //},

                    buttons: [
                        //{
                        //    extend: 'copy',
                        //},
                    //{
                    //    extend: 'csv',
                    //    pageSize: 'LEGAL',
                    //    pageSize: 'A3',
                    //    orientation: 'landscape',
                    //},
                      {
                          extend: 'print',
                          pageSize: 'LEGAL',
                          pageSize: 'A3',
                          scale: '60',
                          orientation: 'landscape',
                          messageTop: function () {
                              var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                              return sss.text;
                          },
                          exportOptions: {
                              columns: [0, 1, 2]
                          }
                      },
                {
                    extend: 'excel',
                    pageSize: 'LEGAL',
                    pageSize: 'A3',
                    orientation: 'landscape',
                    messageTop: function () {
                        var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                        return sss.text;
                    },
                    exportOptions: {
                        columns: [0, 1, 2]
                    }
                },
                    {
                        extend: 'pdf',
                        // pageSize: 'LEGAL',
                        pageSize: 'A4',
                        // orientation: 'landscape',
                        messageTop: function () {
                            var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                            return sss;
                        },
                        exportOptions: {
                            columns: [0, 1, 2]
                        }
                    },


                       {
                           extend: 'pageLength'
                       }
                       // 'colvis'
                    ]
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(GetCount(txtStr));
                Sys.Application.add_load(callDatatablee);

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding ">
                                    <fieldset>
                                        <legend>Add Name</legend>
                                        <div class="col-sm-12  no-padding " runat="server" id="table3">
                                            <div class="col-sm-5 col-xs-5 ">

                                                <fieldset>
                                                    <legend>Option 1</legend>

                                                    <div class="col-sm-6 col-xs-6 half-width-50 mgbt-xs-15" id="table1" runat="server">
                                                        <label class="control-label">Enter Name&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgbt-xs-15">
                                                            <asp:TextBox ID="txtMoNo" runat="server" CssClass="form-control-blue validatetxt" Style="text-transform: uppercase;" onkeypress="return ValidateAlpha(event);" MaxLength="200"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMoNo" ErrorMessage="*"
                                                        ValidationGroup="b" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6 col-xs-6 half-width-50 mgbt-xs-15" id="Div2" runat="server">
                                                        <label class="control-label">Remark&nbsp;<span class="vd_red"></span></label>
                                                        <div class="mgbt-xs-15">
                                                            <asp:TextBox ID="txtremark" runat="server" CssClass="form-control-blue" Style="text-transform: uppercase;" onkeypress="return ValidateAlpha(event);" MaxLength="300"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>

                                            </div>
                                            <div class="col-sm-2 col-xs-2 ">
                                                <p style="text-align: center;">OR</p>
                                                </div>
                                            <div class="col-sm-5 col-xs-5 ">
                                                <fieldset>
                                                    <legend>Option 2</legend>
                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Upload Excel File &nbsp;<span class="vd_red">* </span></label>
                                                        <div class="">
                                                            <asp:FileUpload ID="fu1" class="form-control-blue" runat="server"
                                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'xls|xlsx',
                                                 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile',
                                                 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention');" />

                                                            <div class="text-box-msg">
                                                                <asp:HiddenField ID="hfFile" runat="server" />
                                                                <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </fieldset>
                                            </div>
                                            <div class="col-sm-12  col-md-12 no-padding ">
                                                <div class="col-sm-4 col-xs-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button" OnClick="LinkButton1_Click" ValidationGroup="b"><i class="fa fa-paper-plane"></i> &nbsp; Submit</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 110px !important;"></div>
                                                </div>

                                            </div>
                                        </div>
                                        </fieldset>
                                </div>
                                
                                    <div class="col-sm-12  no-padding " runat="server" id="Div3">
                                        <div class="col-sm-2 col-xs-2  mgbt-xs-15 ">
                                            <a href="../uploads/Excel/Visitor/AddName.xlsx" target="_blank" class="button">Download sample Excel</a>
                                        </div>
                                    </div>
                                <div class="col-sm-12  no-padding " runat="server" id="Div1">
                                    <div class="table-responsive">
                                        <table id="example110" class="table text-center table-striped text-center table-hover no-head-border table-bordered pro-table table-header-group" cellspacing="0" width="100%">

                                            <tbody>
                                                <asp:Repeater runat="server" ID="repeatermember">
                                                    <HeaderTemplate>
                                                        <thead>
                                                            <tr>
                                                                <th style="width:4%;">#</th>
                                                                <th style="width:76%; text-align:left;">Name</th>
                                                                <%-- <th>Remark</th>
                                                        <th>Date</th>--%>
                                                                <th style="width:10%;">Edit</th>
                                                                <th style="width:10%;">Delete</th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>

                                                            <td style="text-align:left;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                                                            <%-- <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Date") %>'></asp:Label></td>--%>
                                                            <td>
                                                                <asp:Label ID="lbldeleteeditid" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:Label>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="btnedit" CausesValidation="False" runat="server" title="Edit"  class="btn menu-icon vd_bd-green vd_green" OnClick="btnedit_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnedit" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>


                                                            <td>
                                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                                    CausesValidation="False" title="Delete" 
                                                                    class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                                        <table class="tab-popup">
                                            <tr>
                                                <td>Name </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" Style="text-transform: uppercase;" CssClass="form-control-blue" onkeypress="return  ValidateAlpha(event);" MaxLength="200"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"
                                                            ValidationGroup="a" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Remark</td>
                                                <td>
                                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                    <asp:TextBox ID="TextBox2" runat="server" Style="text-transform: uppercase;" CssClass="form-control-blue" onkeypress="return  ValidateAlpha(event);" MaxLength="300"></asp:TextBox>
                                                </td>

                                            </tr>



                                            <tr>
                                                <td colspan="2">
                                                    <%-- ReSharper disable once CenterTagIsObsolete --%>
                                                    <center>
                                               <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="3" OnClick="btnupdate_Click" />
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel"   />
                                                                 <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </center>
                                                </td>
                                            </tr>

                                        </table>

                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                                        TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">

                                            <tr>
                                                <td align="center">
                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:Button ID="Button7" runat="server" Style="display: none" />
                                                    </h4>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                                    &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnDelete_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

