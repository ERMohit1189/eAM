<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Hostel_master.aspx.cs" Inherits="Hostel_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                    <div class="col-sm-12 no-padding"  runat="server">
                                        <div class="col-sm-12 no-padding">
                                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Hostel Name<span class="vd_red">*</span></label>
                                                 <div class=" ">
                                                    <asp:TextBox ID="txtCompanyname" runat="server" CssClass="form-control-blue validatetxt" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyname" ErrorMessage="Can't leave blank !"
                                                    Style="color: #CC0000" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Address<span class="vd_red">*</span></label>
                                                 <div class=" ">
                                                    <asp:TextBox ID="txtaddress1" runat="server" CssClass="form-control-blue validatetxt"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaddress1" ErrorMessage="Can't leave blank !"
                                                    Style="color: #CC0000" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Country<span class="vd_red">*</span></label>
                                                 <div class=" ">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpcountry" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                                OnSelectedIndexChanged="drpcountry_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                        </div>
                                        <div class="col-sm-12 no-padding">
                                             
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">State<span class="vd_red">*</span></label>
                                                 <div class=" ">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpsate" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                                OnSelectedIndexChanged="drpsate_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                              <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">City<span class="vd_red">*</span></label>
                                                 <div class=" ">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpcity" runat="server" CssClass="form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Pin Code</label>
                                                 <div class=" ">
                                                    <asp:TextBox ID="txtpinno" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                        </div>
                                        
                                         <div class="col-sm-12 no-padding">
                                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Phone</label>
                                                 <div class=" ">
                                                     <asp:TextBox ID="txtphone" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Mobile No.</label>
                                                 <div class=" ">
                                                    <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">E-mail</label>
                                                 <div class=" ">
                                                     <asp:TextBox ID="txtemail" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                        </div>
                                        <div class="col-sm-12 no-padding">
                                             
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Website</label>
                                                 <div class=" ">
                                                    <asp:TextBox ID="txtwebsite" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                              <div class="col-sm-8 half-width-50 mgbt-xs-15">
                                                 <label class="control-label">Remark</label>
                                                 <div class=" ">
                                                     <asp:TextBox ID="txtremark" runat="server" Rows="1" CssClass="form-control-blue" TextMode="MultiLine" ></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                             
                                             <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                                 <label class="control-label">Hostel Logo</label>
                                                 <div class=" ">
                                                      <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdate(this, 50000, 'jpg|png|jpeg|gif','logo',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hflogo');" />
                                                     <asp:Image ID="Image1" runat="server" ImageUrl="../uploads/HostelLogo/DefaultHostelLogo.png" CssClass="logo" Height="100px" Width="100px" />
                                            <asp:HiddenField ID="hflogo" runat="server" />
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                            <div class="col-sm-12 btn-a-devices-2-p2  mgbt-xs-15 text-center">
                                              <%--  <asp:UpdatePanel runat="server" ID="upd">
                                                    <ContentTemplate>--%>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                                                     <div id="msgbox" runat="server" style="left:75px"></div>
                                                <%--   </ContentTemplate>
                                                                               <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton1" />
                                    </Triggers>
                                             </asp:UpdatePanel>--%>
                                                
                                            </div>
                                        </div>
                                         <div class="col-sm-12 ">
                                         
                                        </div>
                                        
                                    </div>
                                    <%-- ReSharper disable once Html.TagNotResolved --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <script>

               function checksFileSizeandFileTypeinupdate(fileupload, size, filetype, imageClass, hiddenfield) {
                   var img = document.querySelector('.' + imageClass);
                   if (fileupload.files.length > 0) {
                       var filename = fileupload.files[0].name;
                       var filesize = fileupload.files[0].size;
                       if (filesize <= size) {
                           var extSplit = filename.split('.');
                           var extReverse = extSplit.reverse();
                           var ext = extReverse[0];
                           var splitfileext = filetype.split('|');
                           var flag = false;

                           for (var i = 0; i < splitfileext.length; i++) {
                               if (ext == splitfileext[i]) {
                                   flag = true;
                                   break;
                               }

                           }
                           if (flag == false) {
                               alert('Only ' + filetype + ' files are allowed!');
                               fileupload.value = "";
                           }
                       }
                       else {
                           alert('File size should not more than ' + (size / 1000) + ' Kb');
                           fileupload.value = "";
                       }

                       var reader = new FileReader();
                       reader.onloadend = function () {
                           img.src = reader.result;
                           var base64url = reader.result.split(',')
                           document.getElementById(hiddenfield).value = base64url[base64url.length - 1];
                       }
                       if (fileupload.files[0]) {
                           reader.readAsDataURL(fileupload.files[0]);
                       }
                       else {
                           img.src = "";
                       }
                   }
                   else {
                       img.src = "";
                   }

               }
           </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

