<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FeeCardInstructions.aspx.cs" Inherits="_11.FeeCardInstructions" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="https://cdn.ckeditor.com/4.7.3/standard/ckeditor.js"></script>
    <script>
        function CKEditor() {
            CKEDITOR.replace('CKEditorControl1');
        }

        CKEditor();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                //Sys.Application.add_load(CKEditor);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label class="control-label">Choose Template &nbsp;<span class="vd_red"></span></label><br />
                                            <asp:RadioButton runat="server" ID="rdotemletOne" OnCheckedChanged="rdotemletOne_CheckedChanged" AutoPostBack="true" Text="Template One" />
                                            <asp:RadioButton runat="server" ID="rdotemletTwo" OnCheckedChanged="rdotemletTwo_CheckedChanged" AutoPostBack="true" Text="Template Two" />
                                            <asp:HiddenField runat="server" ID="hdnTempletId" />
                                        </div>
                                        <div class="col-sm-12">
                                            <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" required="required" Height="300"></CKEditor:CKEditorControl>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <br />
                                            <asp:LinkButton ID="lnkSubmit" runat="server"
                                                OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 76px"></div>
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:Image runat="server" ID="imgTemletOne" Visible="false" ImageUrl="../uploads/FeecardTemplates/Template1.png" />
                                            <asp:Image runat="server" ID="imgTemletTwo" Visible="false" ImageUrl="../uploads/FeecardTemplates/Template2.png" />
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

