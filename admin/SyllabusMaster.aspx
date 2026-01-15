<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/admin_root-manager.master" CodeFile="SyllabusMaster.aspx.cs" Inherits="admin_SyllabusMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  no-padding ">
                                        <div class="col-sm-2">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       <div class="col-sm-3">
                                        <label class="control-label">Lesson/Topic&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtlesson" runat="server" MaxLength="200"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                        <label class="control-label">Description</label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue" ID="txtdiscription" runat="server" MaxLength="1000" 
            TextMode="MultiLine" 
            Rows="2"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-sm-2">
                                       <label class="control-label">Display Order (Sequence)&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="TextBox1" runat="server" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                          <div class="col-sm-2">
                                        <label class="control-label">Number of Days</label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="TextBox2" runat="server" MaxLength="5"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                       
                                        <div class="col-sm-2">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click"  CssClass="button form-control-blue">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <br />
                                        <div class=" table-responsive  table-responsive2">
                                         <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                           <asp:Label ID="lblLessonID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblClassID" runat="server" Text='<%# Bind("ClassID") %>' Visible="false"></asp:Label>
                                                          <asp:Label ID="lblStreamID" runat="server" Text='<%# Bind("StreamID") %>' Visible="false"></asp:Label>
                                                          <asp:Label ID="lblSubjectID" runat="server" Text='<%# Bind("SubjectID") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblLessonName" runat="server" Text='<%# Bind("LessonName") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblLessondiscription" runat="server" Text='<%# Bind("Discription") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblDisplayofSequence" runat="server" Text='<%# Bind("DisplayofSequence") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblNumberofDays" runat="server" Text='<%# Bind("NumberofDays") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="Label3dd" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LessonName" HeaderText="Lesson/Topic Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                 <asp:BoundField DataField="DisplayofSequence" HeaderText="Display Order" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                 <asp:BoundField DataField="NumberofDays" HeaderText="Days" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                               <%--                  <asp:BoundField DataField="Created_by" HeaderText="Created User" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                 <asp:BoundField DataField="FormattedDate" HeaderText="Created Date" HeaderStyle-CssClass="vd_bg-blue vd_white" />--%>
                                                  <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <div>
            <asp:Label ID="lblNumberofDaysw" runat="server" Text='<%# Bind("Created_by") %>'></asp:Label><br />
            <asp:Label ID="Labelw6" runat="server" Text='<%# Bind("FormattedDate") %>'></asp:Label>
        </div>
                                                       
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                      
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                        </div>
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
                        <tr>
                            <td>
                                <asp:Label ID="lblIDEdit" runat="server" Visible="false"></asp:Label>
                                 <asp:Label ID="Label4" runat="server" Text="Class *"></asp:Label>
                            </td>
                            <td>
                          
                            <asp:DropDownList ID="ddlClassEdit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClassEdit_SelectedIndexChanged" CssClass="form-control-blue validatedrp1">
                            </asp:DropDownList>
                          
                            </td>
                        </tr>
                        <tr>
                            <td>
                                   <asp:Label ID="Label3" runat="server" Text="Stream *"></asp:Label>
                            </td>
                            <td>
                            <asp:DropDownList ID="ddlStreamEdit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStreamEdit_SelectedIndexChanged" CssClass="form-control-blue validatedrp1">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Subject *"></asp:Label>
                            </td>
                            <td>
                                    <asp:DropDownList ID="ddlSubjectEdit" runat="server" CssClass="form-control-blue validatedrp1">
                                   </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Lesson/Topic *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlessonEdit" CssClass="textbox validatetxt1" runat="server" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdiscriptionedit" CssClass="textbox" runat="server" MaxLength="1000"
                                    TextMode="MultiLine" 
            Rows="2"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Display Order *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditDisplayOrder" ReadOnly="true" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Number of Days *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>

                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                &nbsp; &nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblClassids" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />

                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
